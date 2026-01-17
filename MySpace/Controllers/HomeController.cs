using Microsoft.AspNetCore.Mvc;
using MySpace.Models;
using MySpace_Common;
using MySpace_Common.ControllerModels;
using MySpace_Common.EntityModels;
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MySpace_Login()
        {
            return View();
        }

        public IActionResult CreateAccount()
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
        public IActionResult Create_project()
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
        public async Task<IActionResult> RegisterUser([FromBody] RegisterVM model)
        {
            // -------- Model Validation --------
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid input data" });

            // -------- Password Match Validation --------
            if (model.Password != model.ConfirmPassword)
                return Json(new { success = false, message = "Passwords do not match" });

            // -------- Call DAL to Register User --------
            bool result = await _dal.RegisterUserAsync(
                model.FirstName,
                model.LastName,
                model.Email,
                model.Username,
                model.Password
            );

            // -------- User Already Exists --------
            if (!result)
                return Json(new { success = false, message = "User already exists" });

            // -------- Registration Success --------
            return Json(new
            {
                success = true,
                message = "Account created successfully"
            });
        }


        [HttpPost]
        public async Task<JsonResult> Sign_In(string username, string password)
        {
            var user = await _dal.Sign_InAsync(username, password);

            if (user == null)
            {
                return Json(new { success = false, message = "Invalid username or password" });
            }

            // Cookies / Session
            Response.Cookies.Append("USER_ID", user.UserId.ToString());
            Response.Cookies.Append("USERNAME", user.Username);

            return Json(new
            {
                success = true,
                username = user.Username
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
        public async Task<IActionResult> UploadScreenFolder(int projectId,string projectName,List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Json(new { success = false, message = "No files uploaded" });

            if (projectId <= 0 || string.IsNullOrWhiteSpace(projectName))
                return Json(new { success = false, message = "Invalid project" });

            // 🔐 Sanitize project name for folder usage
            projectName = string.Concat(
                projectName.Split(Path.GetInvalidFileNameChars())
            ).Trim();

            // ================= BASE PATH =================
            string basePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "TestScreenOCR",
                projectName
            );

            // ================= MODULE FOLDERS =================
            string viewsPath = Path.Combine(basePath, "Views");
            string jsPath = Path.Combine(basePath, "js");
            string cssPath = Path.Combine(basePath, "css");
            string controllerPath = Path.Combine(basePath, "Controller");
            string databasePath = Path.Combine(basePath, "Database");

            Directory.CreateDirectory(viewsPath);
            Directory.CreateDirectory(jsPath);
            Directory.CreateDirectory(cssPath);
            Directory.CreateDirectory(controllerPath);
            Directory.CreateDirectory(databasePath);

            // ================= FILE PROCESS =================
            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                string safeFileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(safeFileName).ToLower();
                string originalName = Path.GetFileNameWithoutExtension(safeFileName);

                string savePath;
                string fileType;

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

                    case ".sql":
                        savePath = Path.Combine(databasePath, originalName + ".txt");
                        fileType = "sql";
                        break;

                    default:
                        continue;
                }

                // ================= READ CONTENT =================
                string textContent;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    textContent = await reader.ReadToEndAsync();
                }

                // ================= SAVE FILE =================
                await System.IO.File.WriteAllTextAsync(savePath, textContent);

                // ================= DB SAVE (PARENT) =================
                int parentFileId = await _dal.Save_File_Details(
                    projectId,
                    0,
                    safeFileName,
                    savePath,
                    fileType,
                    textContent
                );

                // ================= CHILD EXTRACTION =================
                switch (fileType)
                {
                    case "js":
                        await SplitJSFunctions(projectId, projectName, savePath, parentFileId);
                        break;

                    case "cs":
                        await SplitCSharpMethods(projectId, projectName, savePath, parentFileId);
                        break;

                    case "sql":
                        await SplitSqlTablesAndProcedures(projectId, projectName, savePath, parentFileId);
                        break;

                    case "cshtml":
                        await ExtractAllCshtmlFunctions(projectId, savePath, parentFileId);
                        break;
                }
            }

            return Json(new
            {
                success = true,
                message = "Files uploaded, saved under project folder, and extracted successfully"
            });
        }


        // =========================================================
        // JS FUNCTION SPLITTER
        // =========================================================
        private async Task SplitJSFunctions(int projectId,string projectName,string sourcePath, int parentFileId)
        {
            var outputDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                 "wwwroot", "TestScreenOCR", projectName, "js", "jsfunctions"
            );

            Directory.CreateDirectory(outputDir);

            string content = await System.IO.File.ReadAllTextAsync(sourcePath);

            var functionRegex = new Regex(
                @"function\s+([a-zA-Z0-9_]+)\s*\(",
                RegexOptions.Multiline);

            foreach (Match match in functionRegex.Matches(content))
            {
                string functionName = match.Groups[1].Value;
                int start = match.Index;

                int braceStart = content.IndexOf('{', start);
                if (braceStart == -1) continue;

                int count = 0, end = braceStart;

                for (int i = braceStart; i < content.Length; i++)
                {
                    if (content[i] == '{') count++;
                    else if (content[i] == '}') count--;

                    if (count == 0) { end = i; break; }
                }

                if (count != 0) continue;

                string body = content.Substring(start, end - start + 1);

                string filePath = Path.Combine(outputDir, functionName + ".txt");
                await System.IO.File.WriteAllTextAsync(filePath, body);


                // 1. Save Js file
                int FileId = await _dal.Save_File_Details(
                    projectId,
                    parentFileId,
                    functionName,
                    filePath,
                    "js-function",
                    body
                );

                // 🔥 Extract API Calls
                await ExtractControllerCalls(projectId,body, FileId);
            }
        }

        // =========================================================
        // C# METHOD SPLITTER
        // =========================================================
        private async Task SplitCSharpMethods(int projectId,string projectName,string sourcePath, int parentFileId)
        {
            var outputDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", projectName, "Controller", "Controllerfunctions"
            );

            Directory.CreateDirectory(outputDir);

            var content = System.IO.File.ReadAllText(sourcePath);
            var regex = new Regex(
                @"(public|private|protected|internal)\s+[\w\<\>\[\]]+\s+([a-zA-Z0-9_]+)\s*\(",
                RegexOptions.Multiline
            );

            foreach (Match match in regex.Matches(content))
            {
                string name = match.Groups[2].Value;
                int start = match.Index;

                int braceStart = content.IndexOf('{', start);
                if (braceStart == -1) continue;

                int count = 0, end = braceStart;

                for (int i = braceStart; i < content.Length; i++)
                {
                    if (content[i] == '{') count++;
                    else if (content[i] == '}') count--;

                    if (count == 0) { end = i; break; }
                }

                if (count != 0) continue;

                string body = content.Substring(start, end - start + 1);
                string filePath = Path.Combine(outputDir, name + ".txt");

                System.IO.File.WriteAllText(filePath, body);

                // 1. Save Controller file
                int FileId = await _dal.Save_File_Details(
                    projectId,
                    parentFileId,
                    name,
                    filePath,
                    "Controller-function",
                    body
                );
            }
        }

        // =========================================================
        // SQL TABLE & PROCEDURE SPLITTER
        // =========================================================
        private async Task SplitSqlTablesAndProcedures(int projectId,string projectName, string sourcePath, int parentFileId)
        {
            string baseDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", projectName, "Database"
            );

            string tableDir = Path.Combine(baseDir, "Tables");
            string procDir = Path.Combine(baseDir, "Procedures");

            Directory.CreateDirectory(tableDir);
            Directory.CreateDirectory(procDir);

            string sql = System.IO.File.ReadAllText(sourcePath);

            var batches = Regex.Split(sql, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (var batch in batches)
            {
                string block = batch.Trim();
                if (string.IsNullOrWhiteSpace(block)) continue;

                if (Regex.IsMatch(block, @"^CREATE\s+TABLE", RegexOptions.IgnoreCase))
                {
                    var m = Regex.Match(block,
                        @"CREATE\s+TABLE\s+(\[[^\]]+\]\.\[[^\]]+\])",
                        RegexOptions.IgnoreCase);

                    if (m.Success)
                    {
                        string name = m.Groups[1].Value.Replace("[", "").Replace("]", "").Replace(".", "_");
                        string path = Path.Combine(tableDir, name + ".txt");

                        System.IO.File.WriteAllText(path, block);


                        // 1. Save Controller file
                        int FileId = await _dal.Save_File_Details(
                            projectId,
                            parentFileId,
                            name,
                            path,
                            "sql-table",
                            block
                        );
                    }
                }
                else if (Regex.IsMatch(block, @"^(CREATE|ALTER)\s+PROC", RegexOptions.IgnoreCase))
                {
                    var m = Regex.Match(block,
                        @"(CREATE|ALTER)\s+PROC(?:EDURE)?\s+(\[[^\]]+\]\.\[[^\]]+\])",
                        RegexOptions.IgnoreCase);

                    if (m.Success)
                    {
                        string name = m.Groups[2].Value.Replace("[", "").Replace("]", "").Replace(".", "_");
                        string path = Path.Combine(procDir, name + ".txt");

                        System.IO.File.WriteAllText(path, block);


                        // 1. Save Controller file
                        int FileId = await _dal.Save_File_Details(
                            projectId,
                            parentFileId,
                            name,
                            path,
                            "sql-procedure",
                            block
                        );
                    }
                }
            }
        }

        private async Task ExtractAllCshtmlFunctions(int projectId,string filePath, int parentFileId)
        {
            string content = await System.IO.File.ReadAllTextAsync(filePath);

            HashSet<string> functions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // JS keywords + jQuery boilerplate (IGNORED)
            HashSet<string> jsKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "if","else","for","while","switch","return",
        "function","var","let","const","new",
        "document","window","console","log",
        "settimeout","setinterval","parseint","parsefloat",
        "alert","this","true","false","null","undefined",
        "ready","$"
    };

            /* =============================
               1. JS function declarations
               function myFunc() {}
            ============================== */
            foreach (Match match in Regex.Matches(content,
                @"function\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*\("))
            {
                functions.Add(match.Groups[1].Value);
            }

            /* =============================
               2. Arrow functions
               const myFunc = () => {}
            ============================== */
            foreach (Match match in Regex.Matches(content,
                @"(var|let|const)\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*\("))
            {
                functions.Add(match.Groups[2].Value);
            }

            /* =============================
               3. Anonymous functions
               var myFunc = function() {}
            ============================== */
            foreach (Match match in Regex.Matches(content,
                @"(var|let|const)\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*function\s*\("))
            {
                functions.Add(match.Groups[2].Value);
            }

            /* =============================
               4. HTML event handlers
               onclick="myFunc()"
            ============================== */
            foreach (Match match in Regex.Matches(content,
                @"on\w+\s*=\s*[""']\s*([a-zA-Z_][a-zA-Z0-9_]*)\s*\(",
                RegexOptions.IgnoreCase))
            {
                functions.Add(match.Groups[1].Value);
            }

            /* =============================
               5. Function calls
               myFunc();
               (filter keywords & jQuery)
            ============================== */
            foreach (Match match in Regex.Matches(content,
                @"\b([a-zA-Z_][a-zA-Z0-9_]*)\s*\("))
            {
                string funcName = match.Groups[1].Value;

                if (jsKeywords.Contains(funcName))
                    continue;

                if (funcName.StartsWith("$"))
                    continue;

                functions.Add(funcName);
            }

            /* =============================
               Save extracted functions
            ============================== */
            foreach (string func in functions)
            {
                await _dal.Save_Child_File_Details(
                    projectId,
                    parentFileId,
                    func,
                    "cshtml-function"
                );
            }
        }

        private async Task ExtractControllerCalls(int projectId,string jsBody, int parentFunctionId)
        {
            var apiRegex = new Regex(
                @"(?:(?:\$\.ajax\s*\(\s*\{[\s\S]*?type\s*:\s*['""]?(GET|POST)['""]?[\s\S]*?url\s*:\s*['""]([^'""]+)['""])|" +
                @"(?:\$\.(get|post)\s*\(\s*['""]([^'""]+)['""])|" +
                @"(?:fetch\s*\(\s*['""]([^'""]+)['""]\s*,\s*\{[\s\S]*?method\s*:\s*['""]?(GET|POST)['""]?)|" +
                @"(?:axios\.(get|post)\s*\(\s*['""]([^'""]+)['""]))",
                RegexOptions.IgnoreCase);

            foreach (Match match in apiRegex.Matches(jsBody))
            {
                string httpMethod = "GET";
                string url = null;

                if (!string.IsNullOrEmpty(match.Groups[1].Value))
                {
                    httpMethod = match.Groups[1].Value;
                    url = match.Groups[2].Value;
                }
                else if (!string.IsNullOrEmpty(match.Groups[3].Value))
                {
                    httpMethod = match.Groups[3].Value.ToUpper();
                    url = match.Groups[4].Value;
                }
                else if (!string.IsNullOrEmpty(match.Groups[5].Value))
                {
                    url = match.Groups[5].Value;
                    httpMethod = match.Groups[6].Value;
                }
                else if (!string.IsNullOrEmpty(match.Groups[7].Value))
                {
                    httpMethod = match.Groups[7].Value.ToUpper();
                    url = match.Groups[8].Value;
                }

                if (string.IsNullOrEmpty(url)) continue;

                // Parse /Controller/Action
                var parts = url.Trim('/').Split('/');
                if (parts.Length < 2) continue;

                string controller = parts[0];
                string action = parts[1];

                await _dal.Save_Child_File_Details(
                    projectId,
                    parentFunctionId,
                    $"{controller}/{action}",
                    $"{httpMethod}-controller"
                );
            }
        }


        // API – used by JS
        [HttpGet]
        public async Task<IActionResult> GetBlueprint()
        {
            var data = await _dal.GetBlueprintData();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create_Project([FromBody] ProjectCreateDto model)
        {
            if (model == null)
                return BadRequest("Invalid data");

            string userId = HttpContext.Request.Cookies["USER_ID"];

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _dal.Save_Create_Project(model, userId);

            return Json(new { success = true });
        }

        public async Task<JsonResult> Get_Project_Details()
        {
            string userId = HttpContext.Request.Cookies["USER_ID"];

            if (string.IsNullOrEmpty(userId))
                return Json(new List<ProjectMaster>());

            var data = await _dal.Get_Project_Details(userId);
            return Json(data);
        }


    }

}
