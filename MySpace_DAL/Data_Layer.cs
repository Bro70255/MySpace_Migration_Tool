using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySpace_Common;
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

        public async Task<Employee?> Sign_InAsync(int employeeCode, string password)
        {
            var empCodeParam = new SqlParameter("@EmployeeCode", employeeCode);
            var passParam = new SqlParameter("@Password", password);

            var employees = await _context.Employees
                .FromSqlRaw("EXEC SP_SIGN_IN @EmployeeCode, @Password", empCodeParam, passParam)
                .ToListAsync();

            return employees.FirstOrDefault();
        }

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

        public async Task<int> Save_File_Details(string fileName, string filePath, string fileType,string textContent)
        {
            var entity = new FileDetails
            {
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

        public async Task<int> Save_Extracted_File(
    int parentFileId,
    string extractedName,
    string extractedPath,
    string extractedType
)
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

            return entity.ExtractedId;   // ✅ FIXED
        }


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
                    x.PinCode.ToLower().Contains(search)
                );
            }

            return await query.ToListAsync();
        }


        public async Task Save_Child_File_Details(
     int parentFileId,
     string name,
     string type)
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


        public async Task<List<BlueprintScreenDto>> GetBlueprintData()
        {
            var result = new List<dynamic>();

            using (var conn = _context.Database.GetDbConnection())
            {
                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_GetBlueprintData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
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
                    }
                }
            }

            // 🔄 Transform flat result → hierarchy
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
                            JsFunctionId = js.Key.JsFunctionId,   // ✅ FIXED
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

    }
}
