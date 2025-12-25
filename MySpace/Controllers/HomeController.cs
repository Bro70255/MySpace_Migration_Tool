using Microsoft.AspNetCore.Mvc;
using MySpace.Models;
using MySpace_Common;
using MySpace_DAL;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace MySpace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Split_Table_And_Procedures_Functions_And_Save();
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
        public async Task<IActionResult> UploadScreenFolder(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Json(new { success = false, message = "No files uploaded" });

            string basePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "TestScreenOCR"
            );

            string viewsPath = Path.Combine(basePath, "Views");
            string jsPath = Path.Combine(basePath, "js");
            string cssPath = Path.Combine(basePath, "css");
            string controllerPath = Path.Combine(basePath, "Controller");
            string databasePath = Path.Combine(basePath, "Database"); // ✅ NEW

            Directory.CreateDirectory(viewsPath);
            Directory.CreateDirectory(jsPath);
            Directory.CreateDirectory(cssPath);
            Directory.CreateDirectory(controllerPath);
            Directory.CreateDirectory(databasePath); // ✅ NEW

            foreach (var file in files)
            {
                string safeFileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(safeFileName).ToLower();
                string originalName = Path.GetFileNameWithoutExtension(safeFileName);

                string savePath = null;
                string fileType = null;

                switch (extension)
                {
                    case ".cshtml":
                        savePath = Path.Combine(viewsPath, originalName + ".txt");
                        fileType = "cshtml";
                        break;

                    case ".js":
                        savePath = Path.Combine(jsPath, originalName + ".txt");
                        fileType = "js";
                        break;

                    case ".css":
                        savePath = Path.Combine(cssPath, originalName + ".txt");
                        fileType = "css";
                        break;

                    case ".cs":
                        savePath = Path.Combine(controllerPath, originalName + ".txt");
                        fileType = "cs";
                        break;

                    case ".sql": // ✅ NEW SQL SUPPORT
                        savePath = Path.Combine(databasePath, originalName + ".txt");
                        fileType = "sql";
                        break;

                    default:
                        continue;
                }

                using var reader = new StreamReader(file.OpenReadStream());
                string content = await reader.ReadToEndAsync();

                string textContent =
                    $"// ORIGINAL FILE: {safeFileName}{Environment.NewLine}" +
                    $"// CONVERTED ON: {DateTime.Now}{Environment.NewLine}{Environment.NewLine}" +
                    content;

                await System.IO.File.WriteAllTextAsync(savePath, textContent);

                // ✅ Save DB entry
                await _dal.Save_File_Details(
                    safeFileName,
                    savePath,
                    fileType
                );
            }

            return Json(new
            {
                success = true,
                message = "Files converted and saved as .txt successfully"
            });
        }

        [HttpGet]
        public IActionResult Split_JS_Functions_And_Save()
        {
            var sourcePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "js", "account.txt"
            );

            var outputDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "js", "jsfunctions"
            );

            if (!System.IO.File.Exists(sourcePath))
                return NotFound("Source JS file not found.");

            Directory.CreateDirectory(outputDir);

            var content = System.IO.File.ReadAllText(sourcePath);

            // Step 1: find function declarations ONLY
            var functionHeaderRegex = new Regex(
                @"function\s+([a-zA-Z0-9_]+)\s*\(",
                RegexOptions.Multiline
            );

            var matches = functionHeaderRegex.Matches(content);

            int extractedCount = 0;

            foreach (Match match in matches)
            {
                string functionName = match.Groups[1].Value;
                int startIndex = match.Index;

                // Find first opening brace '{'
                int braceStart = content.IndexOf('{', startIndex);
                if (braceStart == -1) continue;

                int braceCount = 0;
                int endIndex = braceStart;

                // Step 2: Manual brace matching
                for (int i = braceStart; i < content.Length; i++)
                {
                    if (content[i] == '{') braceCount++;
                    else if (content[i] == '}') braceCount--;

                    if (braceCount == 0)
                    {
                        endIndex = i;
                        break;
                    }
                }

                if (braceCount != 0) continue; // safety

                string fullFunction = content.Substring(
                    startIndex,
                    endIndex - startIndex + 1
                );

                string filePath = Path.Combine(outputDir, $"{functionName}.txt");
                System.IO.File.WriteAllText(filePath, fullFunction);

                extractedCount++;
            }

            return Ok(new
            {
                success = true,
                functionsExtracted = extractedCount
            });
        }

        [HttpGet]
        public IActionResult Split_CSharp_Functions_And_Save()
        {
            var sourcePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "Controller", "HomeController.txt"
            );

            var outputDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "Controller", "Controllerfunctions"
            );

            if (!System.IO.File.Exists(sourcePath))
                return NotFound("Source C# file not found.");

            Directory.CreateDirectory(outputDir);

            var content = System.IO.File.ReadAllText(sourcePath);

            // ✅ C# method signature matcher
            var methodRegex = new Regex(
                @"(public|private|protected|internal)\s+" +     // access modifier
                @"[\w\<\>\[\]]+\s+" +                            // return type
                @"([a-zA-Z0-9_]+)\s*" +                           // method name
                @"\([^\)]*\)\s*" +                               // parameters
                @"\{",
                RegexOptions.Multiline
            );

            var matches = methodRegex.Matches(content);
            int extractedCount = 0;

            foreach (Match match in matches)
            {
                string methodName = match.Groups[2].Value;
                int startIndex = match.Index;

                int braceStart = content.IndexOf('{', startIndex);
                if (braceStart == -1) continue;

                int braceCount = 0;
                int endIndex = braceStart;

                // ✅ Proper brace matching
                for (int i = braceStart; i < content.Length; i++)
                {
                    if (content[i] == '{') braceCount++;
                    else if (content[i] == '}') braceCount--;

                    if (braceCount == 0)
                    {
                        endIndex = i;
                        break;
                    }
                }

                if (braceCount != 0) continue;

                string fullMethod = content.Substring(
                    startIndex,
                    endIndex - startIndex + 1
                );

                string filePath = Path.Combine(outputDir, $"{methodName}.txt");
                System.IO.File.WriteAllText(filePath, fullMethod);

                extractedCount++;
            }

            return Ok(new
            {
                success = true,
                methodsExtracted = extractedCount
            });
        }

        [HttpGet]
        public IActionResult Split_Table_And_Procedures_Functions_And_Save()
        {
            var sourcePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "Database", "SQLQuery12.txt"
            );

            if (!System.IO.File.Exists(sourcePath))
                return NotFound("Source SQL file not found.");

            string baseDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "Database"
            );

            string tableDir = Path.Combine(baseDir, "Tables");
            string procDir = Path.Combine(baseDir, "Procedures");

            Directory.CreateDirectory(tableDir);
            Directory.CreateDirectory(procDir);

            string sql = System.IO.File.ReadAllText(sourcePath);

            // ✅ Split by GO (SQL Server batches)
            var batches = Regex.Split(
                sql,
                @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase
            );

            int tableCount = 0;
            int procCount = 0;

            foreach (var batch in batches)
            {
                string block = batch.Trim();
                if (string.IsNullOrWhiteSpace(block)) continue;

                // =========================
                // TABLE EXTRACTION
                // =========================
                if (Regex.IsMatch(block, @"^CREATE\s+TABLE", RegexOptions.IgnoreCase))
                {
                    var nameMatch = Regex.Match(
                        block,
                        @"CREATE\s+TABLE\s+(\[[^\]]+\]\.\[[^\]]+\])",
                        RegexOptions.IgnoreCase
                    );

                    if (nameMatch.Success)
                    {
                        string tableName = nameMatch.Groups[1].Value
                            .Replace("[", "")
                            .Replace("]", "")
                            .Replace(".", "_");

                        System.IO.File.WriteAllText(
                            Path.Combine(tableDir, tableName + ".txt"),
                            block
                        );

                        tableCount++;
                    }
                }

                // =========================
                // PROCEDURE EXTRACTION
                // =========================
                else if (Regex.IsMatch(block, @"^(CREATE|ALTER)\s+PROC", RegexOptions.IgnoreCase))
                {
                    var nameMatch = Regex.Match(
                        block,
                        @"(CREATE|ALTER)\s+PROC(?:EDURE)?\s+(\[[^\]]+\]\.\[[^\]]+\])",
                        RegexOptions.IgnoreCase
                    );

                    if (nameMatch.Success)
                    {
                        string procName = nameMatch.Groups[2].Value
                            .Replace("[", "")
                            .Replace("]", "")
                            .Replace(".", "_");

                        System.IO.File.WriteAllText(
                            Path.Combine(procDir, procName + ".txt"),
                            block
                        );

                        procCount++;
                    }
                }
            }

            return Ok(new
            {
                success = true,
                tablesExtracted = tableCount,
                proceduresExtracted = procCount
            });
        }
    }

}
