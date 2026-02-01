using BCrypt.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySpace_Common;
using MySpace_Common.ControllerModels;
using MySpace_Common.EntityModels;
using Newtonsoft.Json;
using System.Data;
using System.Security.Cryptography;

namespace MySpace_DAL
{
    public class Data_Layer
    {
        private readonly MyDbContext _context;

        public Data_Layer(MyDbContext context)
        {
            _context = context;
        }

        // =========================
        // USER REGISTRATION
        // =========================
        public async Task<bool> RegisterUserAsync(
    string firstName,
    string lastName,
    string email,
    string username,
    string password)
        {
            try
            {
                bool exists = await _context.Users.AnyAsync(x =>
                    x.Email == email || x.Username == username);

                if (exists)
                    return false;

                string hash = BCrypt.Net.BCrypt.HashPassword(password);

                int userId = await GenerateUniqueUserIdAsync(); // 👈 8-digit ID

                var user = new User
                {
                    UserId = userId,
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    Email = email.Trim().ToLower(),
                    Username = username.Trim(),
                    PasswordHash = hash
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Optional: log ex
                return false;
            }
        }

        private async Task<int> GenerateUniqueUserIdAsync()
        {
            int userId;
            bool exists;

            do
            {
                userId = RandomNumberGenerator.GetInt32(10000000, 99999999); // 8 digits
                exists = await _context.Users.AnyAsync(u => u.UserId == userId);
            }
            while (exists);

            return userId;
        }

        // =========================
        // USER LOGIN
        // =========================
        public async Task<User?> Sign_InAsync(string username, string password)
        {
            var param = new SqlParameter("@Username", username);

            var user = _context.Users
                .FromSqlRaw("EXEC SP_GET_USER_FOR_LOGIN @Username", param)
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();

            if (user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValid)
                return null;

            return user;
        }


        // =========================
        // REGISTRATION FORM
        // =========================
        public async Task<bool> Save_Registration_Form(Registration model)
        {
            try
            {
                await _context.Registrations.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // =========================
        // FILE DETAILS
        // =========================
        public async Task<int> Save_File_Details(int projectId, int parentFileId, string fileName, string filePath, string fileType, string textContent)
        {
            var entity = new FileDetails
            {
                ProjectId = projectId,
                ParentFileId = parentFileId,
                FileName = fileName,
                FilePath = filePath,
                FileType = fileType,
                TextContent = textContent,
                UploadedOn = DateTime.Now
            };

            await _context.FileDetails.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.FileId;
        }

        public async Task<int> Save_Extracted_File(int parentFileId, string extractedName, string extractedPath, string extractedType)
        {
            var entity = new ExtractedFileDetails
            {
                ParentFileId = parentFileId,
                ExtractedName = extractedName,
                ExtractedPath = extractedPath,
                ExtractedType = extractedType,
                CreatedOn = DateTime.Now
            };

            await _context.ExtractedFileDetails.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ExtractedId;
        }

        // =========================
        // REGISTRATION REPORT
        // =========================
        public async Task<List<Registration>> Get_Registration_Report_Details(string search)
        {
            var query = _context.Registrations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();

                query = query.Where(x =>
                    x.FullName.ToLower().Contains(search) ||
                    x.Email.ToLower().Contains(search) ||
                    x.Phone.ToLower().Contains(search) ||
                    x.Address.ToLower().Contains(search) ||
                    x.Place.ToLower().Contains(search) ||
                    x.PinCode.ToLower().Contains(search));
            }

            return await query.ToListAsync();
        }

        // =========================
        // CHILD FILE DETAILS
        // =========================
        public async Task Save_Child_File_Details(int projectId, int parentFileId, string name, string type)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            name = name.Trim();

            bool exists = await _context.FileChildDetails
                .AsNoTracking()
                .AnyAsync(x =>
                    x.ParentFileId == parentFileId &&
                    x.Name == name &&
                    x.Type == type);

            if (exists)
                return;

            var entity = new FileChildDetail
            {
                ProjectId = projectId,
                ParentFileId = parentFileId,
                Name = name,
                Type = type,
                CreatedOn = DateTime.Now
            };

            _context.FileChildDetails.Add(entity);
            await _context.SaveChangesAsync();
        }

        // =========================
        // BLUEPRINT DATA
        // =========================
        public async Task<List<BlueprintEdgeDto>> GetBlueprintData()
        {
            var list = new List<BlueprintEdgeDto>();

            await using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.sp_GetBlueprintData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;

            await using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new BlueprintEdgeDto
                    {
                        FromNode = reader.IsDBNull(0) ? null : reader.GetString(0),
                        ToNode = reader.IsDBNull(1) ? null : reader.GetString(1)
                    });
                }
            }

            return list;
        }

        public async Task<int> Save_Create_Project(ProjectCreateDto model, int userId)
        {
            var entity = new ProjectMaster
            {
                ProjectName = model.ProjectName,
                ProjectType = model.ProjectType,
                ProjectFlow = JsonConvert.SerializeObject(model.ProjectFlow),
                CreatedBy = userId,
                CreatedOn = DateTime.Now
            };

            await _context.ProjectMasters.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ProjectId;
        }
        public async Task<List<ProjectMaster>> Get_Project_Details(int userId)
        {
            return await _context.ProjectMasters
                                 .Where(x => x.CreatedBy == userId)
                                 .OrderByDescending(x => x.CreatedOn)
                                 .ToListAsync();
        }

        public async Task<FileDetails?> Get_File_Path_For_View_Code(int userId, string filename)
        {
            string inputName = Path.GetFileNameWithoutExtension(filename);

            return _context.FileDetails
                .Where(x =>
                    _context.ProjectMasters.Any(p =>
                        p.ProjectId == x.ProjectId &&
                        p.CreatedBy == userId
                    )
                )
                .AsEnumerable() // 🔥 switch to in-memory
                .Where(x =>
                    Path.GetFileNameWithoutExtension(x.FileName)
                        .Equals(inputName, StringComparison.OrdinalIgnoreCase)
                )
                .OrderByDescending(x => x.UploadedOn)
                .FirstOrDefault();
        }

        public async Task SaveMemoryAsync(
       int userId,
       string memoryType,
       string question,
       string answer,
       string page)
        {
            var conn = _context.Database.GetDbConnection();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "Save_ZooZoo_Memory";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserId", userId));
            cmd.Parameters.Add(new SqlParameter("@MemoryType", memoryType));
            cmd.Parameters.Add(new SqlParameter("@Question", question));
            cmd.Parameters.Add(new SqlParameter("@Answer", answer));
            cmd.Parameters.Add(new SqlParameter("@Page", page));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<string?> GetLearnedAnswerAsync(int userId, string question)
        {
            var conn = _context.Database.GetDbConnection();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "Get_ZooZoo_Memory";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserId", userId));
            cmd.Parameters.Add(new SqlParameter("@Question", question));

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return reader["Answer"]?.ToString();

            return null;
        }

    }
}
