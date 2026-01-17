using BCrypt.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySpace_Common;
using MySpace_Common.ControllerModels;
using MySpace_Common.EntityModels;
using Newtonsoft.Json;
using System.Data;

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
        public async Task<bool> RegisterUserAsync(string firstName, string lastName, string email, string username, string password)
        {
            try
            {
                bool exists = await _context.Users.AnyAsync(x =>
                    x.Email == email || x.Username == username);

                if (exists)
                    return false;

                string hash = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new User
                {
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
            catch
            {
                return false;
            }
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
        public async Task<int> Save_File_Details(int parentFileId, string fileName, string filePath, string fileType, string textContent)
        {
            var entity = new FileDetails
            {
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
        public async Task Save_Child_File_Details(int parentFileId, string name, string type)
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
        public async Task<List<BlueprintScreenDto>> GetBlueprintData()
        {
            var result = new List<dynamic>();

            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_GetBlueprintData";
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new
                {
                    ScreenId = reader.GetInt32(0),
                    ScreenName = reader.GetString(1),
                    JsFunctionId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                    JsFunctionName = reader.IsDBNull(3) ? null : reader.GetString(3),
                    ControllerAction = reader.IsDBNull(4) ? null : reader.GetString(4),
                    HttpType = reader.IsDBNull(5) ? null : reader.GetString(5)
                });
            }

            return result
                .GroupBy(x => new { x.ScreenId, x.ScreenName })
                .Select(screen => new BlueprintScreenDto
                {
                    ScreenId = screen.Key.ScreenId,
                    ScreenName = screen.Key.ScreenName,
                    JsFunctions = screen
                        .Where(x => x.JsFunctionId != null)
                        .GroupBy(x => new { x.JsFunctionId, x.JsFunctionName })
                        .Select(js => new BlueprintJsDto
                        {
                            JsFunctionId = js.Key.JsFunctionId,
                            JsFunctionName = js.Key.JsFunctionName,
                            Controllers = js
                                .Where(x => x.ControllerAction != null)
                                .Select(c => new BlueprintControllerDto
                                {
                                    ControllerAction = c.ControllerAction,
                                    HttpType = c.HttpType
                                }).ToList()
                        }).ToList()
                }).ToList();
        }
        public async Task<int> Save_Create_Project(ProjectCreateDto model, string userId)
        {
            var entity = new ProjectMaster
            {
                ProjectName = model.ProjectName,
                ProjectType = model.ProjectType,
                ProjectFlow = JsonConvert.SerializeObject(model.ProjectFlow),
                CreatedBy = userId,
                CreatedOn = DateTime.Now
            };

            await _context.ProjectMaster.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ProjectId;
        }
    }
}
