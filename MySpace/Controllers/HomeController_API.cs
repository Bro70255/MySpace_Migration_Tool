using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MySpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        // ✅ Single Base Root (No Ambiguity)
        private readonly string _baseRoot =
            Path.Combine(@"C:\inetpub\wwwroot", "UserProjects");

        //private readonly string _baseRoot =
        //Path.Combine(@"G:\UserProjects");

        [HttpPost("upload")]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> UploadBackup(
            [FromForm] IFormFile file,
            [FromForm] string path)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            if (string.IsNullOrWhiteSpace(path))
                return BadRequest("Path is required");

            string normalizedBase = Path.GetFullPath(_baseRoot);
            string incomingFullPath = Path.GetFullPath(path);

            string relativePath;

            // If full path sent including base root
            if (incomingFullPath.StartsWith(normalizedBase,
                StringComparison.OrdinalIgnoreCase))
            {
                relativePath = incomingFullPath
                    .Substring(normalizedBase.Length)
                    .TrimStart(Path.DirectorySeparatorChar);
            }
            else
            {
                // If only relative path sent
                relativePath = path;
            }

            // Prevent directory traversal
            if (relativePath.Contains(".."))
                return BadRequest("Invalid path");

            // Combine safely
            string finalFolderPath = Path.GetFullPath(
                Path.Combine(normalizedBase, relativePath));

            // Final security check
            if (!finalFolderPath.StartsWith(normalizedBase,
                StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid path");
            }

            // Create folder if not exists
            Directory.CreateDirectory(finalFolderPath);

            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(finalFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new
            {
                success = true,
                message = "Backup uploaded successfully",
                savedTo = filePath
            });
        }
    }
}
