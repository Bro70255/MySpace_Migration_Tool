using Microsoft.AspNetCore.Mvc;
using MySpace.Models;
using MySpace_Common;
using MySpace_DAL;
using System.Diagnostics;
using System.Text;
using System.Text.Json;


namespace MySpace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MySapce_Login()
        {
            return View();
        }
        public IActionResult MySpace_Dashboard()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Registration_Report()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult Review()
        {
            return View();
        }
        public IActionResult Blueprint()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private readonly Data_Layer _dal;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HomeController(
            Data_Layer dal,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _dal = dal;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }


        [HttpPost]
        public async Task<JsonResult> Sign_In(int employeeCode, string password)
        {
            var result = await _dal.Sign_InAsync(employeeCode, password);

            if (result == null)
            {
                return Json(new { success = false, message = "Invalid Employee Code or Password" });
            }

            // Set cookies
            Response.Cookies.Append("EMP_CODE", result.Emp_Code.ToString());
            Response.Cookies.Append("EMP_NAME", result.Emp_Name);
            Response.Cookies.Append("BRANCH_ID", result.Branch_ID.ToString());

            return Json(new
            {
                success = true,
                empName = result.Emp_Name,
                empCode = result.Emp_Code,
                branchId = result.Branch_ID
            });
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Registration model)
        {
            if (ModelState.IsValid)
            {
                var result = await _dal.Save_Registration_Form(model);

                if (result)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "User saved successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to save user"
                    });
                }
            }

            return BadRequest(new
            {
                success = false,
                message = "Validation failed",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }

        public async Task<JsonResult> Get_Registration_Report_Details(string search)
        {
            var result = await _dal.Get_Registration_Report_Details(search);

            return Json(result); // return list to AJAX
        }

        [HttpPost]
        public async Task<JsonResult> Call_AI([FromBody] Blue_Print_01 request)
        {
            try
            {
                var apiKey = _configuration["Gemini:ApiKey"];

                var url =
                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

                var prompt = $@"
Screen Name: {request.ScreenName}

Screen Code:
{request.ScreenCode}

Explain what this screen does in simple words.
";

                var requestBody = new
                {
                    contents = new[]
                    {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        status = "Error",
                        message = responseText
                    });
                }

                return Json(new
                {
                    status = "Success",
                    response = responseText
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = "Exception",
                    message = ex.Message
                });
            }
        }


        [HttpGet]
        public IActionResult ReadOCRFile()
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "TestScreenOCR",
                "Blue_Print_Project.cshtml" // ✅ corrected filename
            );

            if (!System.IO.File.Exists(path))
                return NotFound("File not found");

            var content = System.IO.File.ReadAllText(path);
            return Json(new { success = true, data = content });
        }


        [HttpGet]
        public IActionResult List_out_the_Files_in_Folder_ReadOCRFile()
        {
            var rootPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "TestScreenOCR"
            );

            if (!Directory.Exists(rootPath))
                return NotFound("Folder not found");

            var tree = BuildDirectoryTree(rootPath);

            return Json(new
            {
                success = true,
                data = tree
            });
        }

        private FileNode BuildDirectoryTree(string path)
        {
            var node = new FileNode
            {
                Name = Path.GetFileName(path),
                IsDirectory = true
            };

            // Folders
            foreach (var dir in Directory.GetDirectories(path))
            {
                node.Children.Add(BuildDirectoryTree(dir));
            }

            // Files
            foreach (var file in Directory.GetFiles(path))
            {
                node.Children.Add(new FileNode
                {
                    Name = Path.GetFileName(file),
                    IsDirectory = false
                });
            }

            return node;
        }


        [HttpPost]
        public IActionResult UploadScreenFolder([FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Json(new { success = false, message = "No files received" });

            var rootPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "TestScreenOCR"
            );

            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                // Only allow text-based files
                var extension = Path.GetExtension(file.FileName).ToLower();

                var textExtensions = new[]
                {
            ".txt", ".csv", ".log",
            ".cshtml", ".html",
            ".js", ".css",
            ".json", ".xml"
        };

                if (!textExtensions.Contains(extension))
                    continue;

                // Create TXT path (DO NOT save original file)
                var txtRelativePath = Path.ChangeExtension(file.FileName, ".txt");
                var txtSavePath = Path.Combine(rootPath, txtRelativePath);

                var directory = Path.GetDirectoryName(txtSavePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // ✅ Convert directly to TXT
                using var reader = new StreamReader(file.OpenReadStream());
                var content = reader.ReadToEnd();

                System.IO.File.WriteAllText(txtSavePath, content);
            }

            return Json(new
            {
                success = true,
                message = $"✅ {files.Count} file(s) converted to TXT"
            });
        }

    }

}
