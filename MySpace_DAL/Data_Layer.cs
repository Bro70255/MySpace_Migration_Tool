using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySpace_Common;


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

        public async Task<int> Save_File_Details(string fileName, string filePath, string fileType)
        {
            var entity = new FileDetails
            {
                FileName = fileName,
                FilePath = filePath,
                FileType = fileType,
                UploadedOn = DateTime.Now
            };

            await _context.FileDetails.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.FileId;
        }

        public async Task Save_Extracted_File(
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

    }
}
