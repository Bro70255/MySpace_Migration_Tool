using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
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

        public IActionResult Projects()
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

        //    [HttpPost]
        //    public async Task<IActionResult> UploadScreenFolder(int projectId,string projectName,List<IFormFile> files)
        //    {
        //        if (files == null || files.Count == 0)
        //            return Json(new { success = false, message = "No files uploaded" });

        //        if (projectId <= 0 || string.IsNullOrWhiteSpace(projectName))
        //            return Json(new { success = false, message = "Invalid project" });

        //        int userId = Convert.ToInt32(HttpContext.Request.Cookies["USER_ID"]);
        //        var data = await _dal.Get_Project_Details(userId);
        //        // 🔐 Sanitize project name for folder usage
        //        projectName = string.Concat(
        //            projectName.Split(Path.GetInvalidFileNameChars())
        //        ).Trim();

        //        // ================= BASE PATH =================
        //        string basePath = Path.Combine(
        //            Directory.GetCurrentDirectory(),
        //            "wwwroot",
        //            "TestScreenOCR",
        //            projectName
        //        );

        //        // ================= MODULE FOLDERS =================
        //        string viewsPath = Path.Combine(basePath, "Views");
        //        string jsPath = Path.Combine(basePath, "js");
        //        string cssPath = Path.Combine(basePath, "css");
        //        string controllerPath = Path.Combine(basePath, "Controller");
        //        string databasePath = Path.Combine(basePath, "Database");

        //        Directory.CreateDirectory(viewsPath);
        //        Directory.CreateDirectory(jsPath);
        //        Directory.CreateDirectory(cssPath);
        //        Directory.CreateDirectory(controllerPath);
        //        Directory.CreateDirectory(databasePath);

        //        // ================= FILE PROCESS =================
        //        foreach (var file in files)
        //        {
        //            if (file.Length == 0)
        //                continue;

        //            string safeFileName = Path.GetFileName(file.FileName);
        //            string extension = Path.GetExtension(safeFileName).ToLower();
        //            string originalName = Path.GetFileNameWithoutExtension(safeFileName);

        //            string savePath;
        //            string fileType;

        //            switch (extension)
        //            {
        //                case ".cshtml":
        //                    savePath = Path.Combine(viewsPath, originalName + ".txt");
        //                    fileType = "cshtml";
        //                    break;

        //                case ".js":
        //                    savePath = Path.Combine(jsPath, originalName + ".txt");
        //                    fileType = "js";
        //                    break;

        //                case ".css":
        //                    savePath = Path.Combine(cssPath, originalName + ".txt");
        //                    fileType = "css";
        //                    break;

        //                case ".cs":
        //                    savePath = Path.Combine(controllerPath, originalName + ".txt");
        //                    fileType = "cs";
        //                    break;

        //                case ".sql":
        //                    savePath = Path.Combine(databasePath, originalName + ".txt");
        //                    fileType = "sql";
        //                    break;

        //                default:
        //                    continue;
        //            }

        //            // ================= READ CONTENT =================
        //            string textContent;
        //            using (var reader = new StreamReader(file.OpenReadStream()))
        //            {
        //                textContent = await reader.ReadToEndAsync();
        //            }

        //            // ================= SAVE FILE =================
        //            await System.IO.File.WriteAllTextAsync(savePath, textContent);

        //            // ================= DB SAVE (PARENT) =================
        //            int parentFileId = await _dal.Save_File_Details(
        //                projectId,
        //                0,
        //                safeFileName,
        //                savePath,
        //                fileType,
        //                textContent
        //            );

        //            // ================= CHILD EXTRACTION =================
        //            switch (fileType)
        //            {
        //                case "js":
        //                    await SplitJSFunctions(projectId, projectName, savePath, parentFileId);
        //                    break;

        //                case "cs":
        //                    await SplitCSharpMethods(projectId, projectName, savePath, parentFileId);
        //                    break;

        //                case "sql":
        //                    await SplitSqlTablesAndProcedures(projectId, projectName, savePath, parentFileId);
        //                    break;

        //                case "cshtml":
        //                    await ExtractAllCshtmlFunctions(projectId, savePath, parentFileId);
        //                    break;
        //            }
        //        }

        //        return Json(new
        //        {
        //            success = true,
        //            message = "Files uploaded, saved under project folder, and extracted successfully"
        //        });
        //    }


        // =========================================================
        // JS FUNCTION SPLITTER
        // =========================================================
        //private async Task SplitJSFunctions(int projectId, string projectName, string sourcePath, int parentFileId)
        //{
        //    var outputDir = Path.Combine(
        //        Directory.GetCurrentDirectory(),
        //         "wwwroot", "TestScreenOCR", projectName, "js", "jsfunctions"
        //    );

        //    Directory.CreateDirectory(outputDir);

        //    string content = await System.IO.File.ReadAllTextAsync(sourcePath);

        //    var functionRegex = new Regex(
        //        @"function\s+([a-zA-Z0-9_]+)\s*\(",
        //        RegexOptions.Multiline);

        //    foreach (Match match in functionRegex.Matches(content))
        //    {
        //        string functionName = match.Groups[1].Value;
        //        int start = match.Index;

        //        int braceStart = content.IndexOf('{', start);
        //        if (braceStart == -1) continue;

        //        int count = 0, end = braceStart;

        //        for (int i = braceStart; i < content.Length; i++)
        //        {
        //            if (content[i] == '{') count++;
        //            else if (content[i] == '}') count--;

        //            if (count == 0) { end = i; break; }
        //        }

        //        if (count != 0) continue;

        //        string body = content.Substring(start, end - start + 1);

        //        string filePath = Path.Combine(outputDir, functionName + ".txt");
        //        await System.IO.File.WriteAllTextAsync(filePath, body);


        //        // 1. Save Js file
        //        int FileId = await _dal.Save_File_Details(
        //            projectId,
        //            parentFileId,
        //            functionName,
        //            filePath,
        //            "js-function",
        //            body
        //        );

        //        // 🔥 Extract API Calls
        //        await ExtractControllerCalls(projectId, body, FileId);
        //    }
        //}

        //// =========================================================
        // C# METHOD SPLITTER
        // =========================================================
        //private async Task SplitCSharpMethods(int projectId, string projectName, string sourcePath, int parentFileId)
        //{
        //    var outputDir = Path.Combine(
        //        Directory.GetCurrentDirectory(),
        //        "wwwroot", "TestScreenOCR", projectName, "Controller", "Controllerfunctions"
        //    );

        //    Directory.CreateDirectory(outputDir);

        //    var content = System.IO.File.ReadAllText(sourcePath);
        //    var regex = new Regex(
        //        @"(public|private|protected|internal)\s+[\w\<\>\[\]]+\s+([a-zA-Z0-9_]+)\s*\(",
        //        RegexOptions.Multiline
        //    );

        //    foreach (Match match in regex.Matches(content))
        //    {
        //        string name = match.Groups[2].Value;
        //        int start = match.Index;

        //        int braceStart = content.IndexOf('{', start);
        //        if (braceStart == -1) continue;

        //        int count = 0, end = braceStart;

        //        for (int i = braceStart; i < content.Length; i++)
        //        {
        //            if (content[i] == '{') count++;
        //            else if (content[i] == '}') count--;

        //            if (count == 0) { end = i; break; }
        //        }

        //        if (count != 0) continue;

        //        string body = content.Substring(start, end - start + 1);
        //        string filePath = Path.Combine(outputDir, name + ".txt");

        //        System.IO.File.WriteAllText(filePath, body);

        //        // 1. Save Controller file
        //        int FileId = await _dal.Save_File_Details(
        //            projectId,
        //            parentFileId,
        //            name,
        //            filePath,
        //            "Controller-function",
        //            body
        //        );
        //    }
        //}

        //// =========================================================
        //// SQL TABLE & PROCEDURE SPLITTER
        //// =========================================================
        //private async Task SplitSqlTablesAndProcedures(int projectId, string projectName, string sourcePath, int parentFileId)
        //{
        //    string baseDir = Path.Combine(
        //        Directory.GetCurrentDirectory(),
        //        "wwwroot", "TestScreenOCR", projectName, "Database"
        //    );

        //    string tableDir = Path.Combine(baseDir, "Tables");
        //    string procDir = Path.Combine(baseDir, "Procedures");

        //    Directory.CreateDirectory(tableDir);
        //    Directory.CreateDirectory(procDir);

        //    string sql = System.IO.File.ReadAllText(sourcePath);

        //    var batches = Regex.Split(sql, @"^\s*GO\s*$",
        //        RegexOptions.Multiline | RegexOptions.IgnoreCase);

        //    foreach (var batch in batches)
        //    {
        //        string block = batch.Trim();
        //        if (string.IsNullOrWhiteSpace(block)) continue;

        //        if (Regex.IsMatch(block, @"^CREATE\s+TABLE", RegexOptions.IgnoreCase))
        //        {
        //            var m = Regex.Match(block,
        //                @"CREATE\s+TABLE\s+(\[[^\]]+\]\.\[[^\]]+\])",
        //                RegexOptions.IgnoreCase);

        //            if (m.Success)
        //            {
        //                string name = m.Groups[1].Value.Replace("[", "").Replace("]", "").Replace(".", "_");
        //                string path = Path.Combine(tableDir, name + ".txt");

        //                System.IO.File.WriteAllText(path, block);


        //                // 1. Save Controller file
        //                int FileId = await _dal.Save_File_Details(
        //                    projectId,
        //                    parentFileId,
        //                    name,
        //                    path,
        //                    "sql-table",
        //                    block
        //                );
        //            }
        //        }
        //        else if (Regex.IsMatch(block, @"^(CREATE|ALTER)\s+PROC", RegexOptions.IgnoreCase))
        //        {
        //            var m = Regex.Match(block,
        //                @"(CREATE|ALTER)\s+PROC(?:EDURE)?\s+(\[[^\]]+\]\.\[[^\]]+\])",
        //                RegexOptions.IgnoreCase);

        //            if (m.Success)
        //            {
        //                string name = m.Groups[2].Value.Replace("[", "").Replace("]", "").Replace(".", "_");
        //                string path = Path.Combine(procDir, name + ".txt");

        //                System.IO.File.WriteAllText(path, block);


        //                // 1. Save Controller file
        //                int FileId = await _dal.Save_File_Details(
        //                    projectId,
        //                    parentFileId,
        //                    name,
        //                    path,
        //                    "sql-procedure",
        //                    block
        //                );
        //            }
        //        }
        //    }
        //}

        //private async Task ExtractAllCshtmlFunctions(int projectId, string filePath, int parentFileId)
        //{
        //    string content = await System.IO.File.ReadAllTextAsync(filePath);

        //    HashSet<string> functions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    // JS keywords + jQuery boilerplate (IGNORED)
        //    HashSet<string> jsKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        //{
        //    "if","else","for","while","switch","return",
        //    "function","var","let","const","new",
        //    "document","window","console","log",
        //    "settimeout","setinterval","parseint","parsefloat",
        //    "alert","this","true","false","null","undefined",
        //    "ready","$"
        //};

        //    /* =============================
        //       1. JS function declarations
        //       function myFunc() {}
        //    ============================== */
        //    foreach (Match match in Regex.Matches(content,
        //        @"function\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*\("))
        //    {
        //        functions.Add(match.Groups[1].Value);
        //    }

        //    /* =============================
        //       2. Arrow functions
        //       const myFunc = () => {}
        //    ============================== */
        //    foreach (Match match in Regex.Matches(content,
        //        @"(var|let|const)\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*\("))
        //    {
        //        functions.Add(match.Groups[2].Value);
        //    }

        //    /* =============================
        //       3. Anonymous functions
        //       var myFunc = function() {}
        //    ============================== */
        //    foreach (Match match in Regex.Matches(content,
        //        @"(var|let|const)\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*function\s*\("))
        //    {
        //        functions.Add(match.Groups[2].Value);
        //    }

        //    /* =============================
        //       4. HTML event handlers
        //       onclick="myFunc()"
        //    ============================== */
        //    foreach (Match match in Regex.Matches(content,
        //        @"on\w+\s*=\s*[""']\s*([a-zA-Z_][a-zA-Z0-9_]*)\s*\(",
        //        RegexOptions.IgnoreCase))
        //    {
        //        functions.Add(match.Groups[1].Value);
        //    }

        //    /* =============================
        //       5. Function calls
        //       myFunc();
        //       (filter keywords & jQuery)
        //    ============================== */
        //    foreach (Match match in Regex.Matches(content,
        //        @"\b([a-zA-Z_][a-zA-Z0-9_]*)\s*\("))
        //    {
        //        string funcName = match.Groups[1].Value;

        //        if (jsKeywords.Contains(funcName))
        //            continue;

        //        if (funcName.StartsWith("$"))
        //            continue;

        //        functions.Add(funcName);
        //    }

        //    /* =============================
        //       Save extracted functions
        //    ============================== */
        //    foreach (string func in functions)
        //    {
        //        await _dal.Save_Child_File_Details(
        //            projectId,
        //            parentFileId,
        //            func,
        //            "cshtml-function"
        //        );
        //    }
        //}

        //    private async Task ExtractControllerCalls(int projectId,string jsBody, int parentFunctionId)
        //    {
        //        var apiRegex = new Regex(
        //            @"(?:(?:\$\.ajax\s*\(\s*\{[\s\S]*?type\s*:\s*['""]?(GET|POST)['""]?[\s\S]*?url\s*:\s*['""]([^'""]+)['""])|" +
        //            @"(?:\$\.(get|post)\s*\(\s*['""]([^'""]+)['""])|" +
        //            @"(?:fetch\s*\(\s*['""]([^'""]+)['""]\s*,\s*\{[\s\S]*?method\s*:\s*['""]?(GET|POST)['""]?)|" +
        //            @"(?:axios\.(get|post)\s*\(\s*['""]([^'""]+)['""]))",
        //            RegexOptions.IgnoreCase);

        //        foreach (Match match in apiRegex.Matches(jsBody))
        //        {
        //            string httpMethod = "GET";
        //            string url = null;

        //            if (!string.IsNullOrEmpty(match.Groups[1].Value))
        //            {
        //                httpMethod = match.Groups[1].Value;
        //                url = match.Groups[2].Value;
        //            }
        //            else if (!string.IsNullOrEmpty(match.Groups[3].Value))
        //            {
        //                httpMethod = match.Groups[3].Value.ToUpper();
        //                url = match.Groups[4].Value;
        //            }
        //            else if (!string.IsNullOrEmpty(match.Groups[5].Value))
        //            {
        //                url = match.Groups[5].Value;
        //                httpMethod = match.Groups[6].Value;
        //            }
        //            else if (!string.IsNullOrEmpty(match.Groups[7].Value))
        //            {
        //                httpMethod = match.Groups[7].Value.ToUpper();
        //                url = match.Groups[8].Value;
        //            }

        //            if (string.IsNullOrEmpty(url)) continue;

        //            // Parse /Controller/Action
        //            var parts = url.Trim('/').Split('/');
        //            if (parts.Length < 2) continue;

        //            string controller = parts[0];
        //            string action = parts[1];

        //            await _dal.Save_Child_File_Details(
        //                projectId,
        //                parentFunctionId,
        //                $"{controller}/{action}",
        //                $"{httpMethod}-controller"
        //            );
        //        }
        //    }


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

            int userId = Convert.ToInt32(HttpContext.Request.Cookies["USER_ID"]);


            await _dal.Save_Create_Project(model, userId);

            return Json(new { success = true });
        }

        public async Task<JsonResult> Get_Project_Details()
        {
            int userId = Convert.ToInt32(HttpContext.Request.Cookies["USER_ID"]);

            var data = await _dal.Get_Project_Details(userId);
            return Json(data);
        }


        [HttpPost]
        public async Task<IActionResult> UploadScreenFolder(int projectId, string projectName, string fileType, List<IFormFile> files)
        {
            if (files == null || files.Count == 0) return Json(new { success = false, message = "No files uploaded" });
            if (projectId <= 0 || string.IsNullOrWhiteSpace(projectName)) return Json(new { success = false, message = "Invalid project" });
            if (string.IsNullOrWhiteSpace(fileType)) return Json(new { success = false, message = "File type not selected" });

            if (!int.TryParse(HttpContext.Request.Cookies["USER_ID"], out int userId)) return Json(new { success = false, message = "User not found" });

            List<ProjectMaster> projects = await _dal.Get_Project_Details(userId);
            var project = projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (project == null) return Json(new { success = false, message = "Project not found" });

            string projectFlowJson = project.ProjectFlow ?? "[]";
            var (flowOrder, flowSet) = ParseAndNormalizeProjectFlow(projectFlowJson);

            string selectedModule = NormalizeSelectedFileType(fileType);
            if (string.IsNullOrWhiteSpace(selectedModule)) return Json(new { success = false, message = "Invalid file type selected" });
            if (!flowSet.Contains(selectedModule)) return Json(new { success = false, message = $"'{selectedModule}' not allowed in ProjectFlow" });

            projectName = string.Concat(projectName.Split(Path.GetInvalidFileNameChars())).Trim();

            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TestScreenOCR", projectName);
            Directory.CreateDirectory(basePath);

            // ================= MODULE PATH MAP (DYNAMIC) =================
            Dictionary<string, string> modulePathMap = new(StringComparer.OrdinalIgnoreCase);

            foreach (var module in flowOrder)
            {
                string folderName = module switch
                {
                    "View" => "Views",
                    "JavaScript" => "js",
                    "CSS" => "css",
                    _ => module
                };

                string modulePath = Path.Combine(basePath, folderName);
                Directory.CreateDirectory(modulePath);
                modulePathMap[module] = modulePath;
            }

            if (!modulePathMap.TryGetValue(selectedModule, out string moduleRoot)) return Json(new { success = false, message = "Module folder not created" });

            int processed = 0;
            int skipped = 0;

            // ================= FILE PROCESS =================
            foreach (var file in files)
            {
                if (file == null || file.Length == 0) { skipped++; continue; }

                string uploadedRelativePath = file.FileName ?? "";
                string safeFileName = Path.GetFileName(uploadedRelativePath);
                string extension = Path.GetExtension(safeFileName).ToLowerInvariant();
                string originalName = Path.GetFileNameWithoutExtension(safeFileName);

                // Validate extension belongs to selected module (prevents BLL/DAL mixing into Controller)
                if (!IsValidExtensionForModule(extension, selectedModule)) { skipped++; continue; }

                // Keep subfolders if present (after module folder if found, else keep full folder structure)
                var subDirs = GetSubDirsForSelectedModule(uploadedRelativePath, selectedModule);
                string finalDir = moduleRoot;
                foreach (var seg in subDirs) finalDir = Path.Combine(finalDir, SanitizePathSegment(seg));
                Directory.CreateDirectory(finalDir);

                string savePath = Path.Combine(finalDir, originalName + ".txt");

                string textContent;
                using (var reader = new StreamReader(file.OpenReadStream())) { textContent = await reader.ReadToEndAsync(); }
                await System.IO.File.WriteAllTextAsync(savePath, textContent);

                string dbFileType = GetDbFileTypeForModule(selectedModule);

                int parentFileId = await _dal.Save_File_Details(projectId, 0, safeFileName, savePath, dbFileType, textContent);

                // ================= CHILD EXTRACTION (SEPARATE FOR CONTROLLER/BLL/DAL) =================
                if (selectedModule == "JavaScript") await SplitJSFunctions(selectedModule, projectId, modulePathMap, savePath, parentFileId);
                else if (selectedModule == "Controller") await SplitCSharpMethods(selectedModule, projectId, modulePathMap, savePath, parentFileId, "Controller");
                else if (selectedModule == "BLL") await SplitCSharpMethods(selectedModule, projectId, modulePathMap, savePath, parentFileId, "BLL");
                else if (selectedModule == "DAL") await SplitCSharpMethods(selectedModule, projectId, modulePathMap, savePath, parentFileId, "DAL");
                else if (selectedModule == "Database") await SplitSqlTablesAndProcedures(selectedModule, projectId, modulePathMap, savePath, parentFileId);
                else if (selectedModule == "View") await ExtractViewFunctionsAndControllerCalls(selectedModule, projectId, savePath, parentFileId);

                processed++;
            }

            return Json(new { success = true, message = $"Upload done. Processed: {processed}, Skipped: {skipped}, Module: {selectedModule}" });
        }

        // =========================================================
        // FLOW PARSER
        // =========================================================
        private static (List<string>, HashSet<string>) ParseAndNormalizeProjectFlow(string json)
        {
            List<string> raw;
            try { raw = JsonSerializer.Deserialize<List<string>>(json) ?? new(); }
            catch { raw = new(); }

            if (raw.Count == 0) raw = new() { "View", "JavaScript", "Controller", "BLL", "DAL", "Database" };

            var order = raw.Select(NormalizeModuleName).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            return (order, new HashSet<string>(order, StringComparer.OrdinalIgnoreCase));
        }

        private static string NormalizeModuleName(string name)
        {
            name = name?.Trim() ?? "";
            return name switch
            {
                "Views" => "View",
                "JS" => "JavaScript",
                "Scripts" => "JavaScript",
                "Controllers" => "Controller",
                "DB" => "Database",
                "Styles" => "CSS",
                _ => name
            };
        }

        private static string NormalizeSelectedFileType(string fileType)
        {
            fileType = (fileType ?? "").Trim();
            if (string.IsNullOrWhiteSpace(fileType)) return "";

            string ft = fileType.ToLowerInvariant();
            return ft switch
            {
                "cshtml" => "View",
                "view" => "View",
                "js" => "JavaScript",
                "javascript" => "JavaScript",
                "css" => "CSS",
                "sql" => "Database",
                "database" => "Database",
                "db" => "Database",
                "controller" => "Controller",
                "controllers" => "Controller",
                "bll" => "BLL",
                "dal" => "DAL",
                _ => NormalizeModuleName(fileType)
            };
        }

        private static string GetDbFileTypeForModule(string module)
        {
            module = NormalizeModuleName(module);
            return module switch
            {
                "View" => "cshtml",
                "JavaScript" => "js",
                "CSS" => "css",
                "Database" => "sql",
                "Controller" => "controller",
                "BLL" => "bll",
                "DAL" => "dal",
                _ => "unknown"
            };
        }

        private static bool IsValidExtensionForModule(string ext, string module)
        {
            ext = (ext ?? "").ToLowerInvariant();
            module = NormalizeModuleName(module);

            return module switch
            {
                "View" => ext == ".cshtml",
                "JavaScript" => ext == ".js",
                "CSS" => ext == ".css",
                "Database" => ext == ".sql",
                "Controller" or "BLL" or "DAL" => ext == ".cs",
                _ => false
            };
        }

        // =========================================================
        // HELPERS
        // =========================================================
        private static List<string> GetSubDirsForSelectedModule(string path, string module)
        {
            var parts = (path ?? "").Replace("\\", "/").Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
            if (parts.Count <= 1) return new();

            parts.RemoveAt(parts.Count - 1); // remove filename

            var aliases = GetModuleFolderAliases(module);
            int idx = parts.FindIndex(p => aliases.Any(a => p.Equals(a, StringComparison.OrdinalIgnoreCase)));

            if (idx >= 0) return parts.Skip(idx + 1).ToList();

            // If module folder not found in uploaded path, keep full folder structure (prevents flattening)
            return parts;
        }

        private static IEnumerable<string> GetModuleFolderAliases(string module)
        {
            module = NormalizeModuleName(module);
            return module switch
            {
                "Controller" => new[] { "Controller", "Controllers" },
                "View" => new[] { "View", "Views" },
                "JavaScript" => new[] { "JavaScript", "JS", "Scripts", "Script", "js" },
                "CSS" => new[] { "CSS", "Styles", "Style", "css" },
                "Database" => new[] { "Database", "DB", "Sql", "SQL" },
                "BLL" => new[] { "BLL" },
                "DAL" => new[] { "DAL" },
                _ => new[] { module }
            };
        }

        private static string SanitizePathSegment(string s) { return string.Concat((s ?? "").Split(Path.GetInvalidFileNameChars())).Trim(); }

        // =========================================================
        // SPLITTERS
        // =========================================================
        private async Task SplitCSharpMethods(string selectedModule, int projectId, Dictionary<string, string> map, string sourcePath, int parentId, string module)
        {
            string outDir = Path.Combine(map[module], $"{module}functions");
            Directory.CreateDirectory(outDir);

            string content = await System.IO.File.ReadAllTextAsync(sourcePath);

            var regex = new Regex(
                @"(?m)^\s*(?!//)\s*" +
                @"(public|private|protected|internal)\s+" +
                @"(?:static\s+|async\s+|virtual\s+|override\s+|sealed\s+|new\s+)*" +
                @"[\w\.<>\[\]\?,]+\s+" +
                @"(?<name>\w+)\s*\(",
                RegexOptions.Compiled
            );

            foreach (Match m in regex.Matches(content))
            {
                string name = m.Groups["name"].Value;
                int methodStart = m.Index;

                int braceStart = content.IndexOf('{', m.Index);
                if (braceStart == -1) continue;

                int depth = 0;
                int i = braceStart;

                for (; i < content.Length; i++)
                {
                    if (content[i] == '{') depth++;
                    else if (content[i] == '}')
                    {
                        depth--;
                        if (depth == 0) { i++; break; }
                    }
                }

                if (depth != 0) continue;

                string methodText = content.Substring(methodStart, i - methodStart);
                string filePath = Path.Combine(outDir, $"{name}.cs");

                await System.IO.File.WriteAllTextAsync(filePath, methodText);

                int id = await _dal.Save_File_Details(
                    projectId,
                    parentId,
                    name,
                    filePath,
                    $"{module}-function",
                    methodText
                );

                // ✅ PASS REAL FILE PATH
                await ExtractViewFunctionsAndControllerCalls(selectedModule, projectId, filePath, id);
            }
        }

        private async Task SplitJSFunctions(
            string selectedModule,
            int projectId,
            Dictionary<string, string> map,
            string sourcePath,
            int parentId)
        {
            string outDir = Path.Combine(map["JavaScript"], "jsfunctions");
            Directory.CreateDirectory(outDir);

            string content = await System.IO.File.ReadAllTextAsync(sourcePath);

            var regex = new Regex(
                @"\bfunction\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(",
                RegexOptions.Multiline
            );

            foreach (Match m in regex.Matches(content))
            {
                string name = m.Groups[1].Value;

                // ✅ extract full JS function body
                string fullFunction = ExtractFullJsFunction(content, m.Index);
                if (string.IsNullOrWhiteSpace(fullFunction))
                    continue;

                string filePath = Path.Combine(outDir, $"{name}.js");

                // ✅ IMPORTANT: fully qualify File
                await System.IO.File.WriteAllTextAsync(filePath, fullFunction);

                int id = await _dal.Save_File_Details(
                    projectId,
                    parentId,
                    name,
                    filePath,
                    "js-function",
                    fullFunction
                );

                await ExtractViewFunctionsAndControllerCalls(
                    selectedModule,
                    projectId,
                    filePath,
                    id
                );
            }
        }

        private static string ExtractFullJsFunction(string content, int startIndex)
        {
            int braceStart = content.IndexOf('{', startIndex);
            if (braceStart == -1) return null;

            int depth = 0;

            for (int i = braceStart; i < content.Length; i++)
            {
                if (content[i] == '{') depth++;
                else if (content[i] == '}') depth--;

                if (depth == 0)
                    return content.Substring(startIndex, i - startIndex + 1);
            }

            return null;
        }



        private async Task SplitSqlTablesAndProcedures(
     string selectedModule,
     int projectId,
     Dictionary<string, string> map,
     string sourcePath,
     int parentId)
        {
            if (!map.ContainsKey("Database"))
                return;

            string baseDir = map["Database"];
            Directory.CreateDirectory(baseDir);

            // ⚠ IMPORTANT: fully qualify File (ControllerBase conflict fix)
            string sql = await System.IO.File.ReadAllTextAsync(sourcePath);

            // 1️⃣ Match CREATE / ALTER headers
            var headerRegex = new Regex(
                @"(CREATE|ALTER)\s+(TABLE|PROC|PROCEDURE|FUNCTION)\s+([^\s\(]+)",
                RegexOptions.IgnoreCase | RegexOptions.Multiline
            );

            MatchCollection headers = headerRegex.Matches(sql);

            for (int i = 0; i < headers.Count; i++)
            {
                Match header = headers[i];

                string objectType = header.Groups[2].Value.ToUpper();
                string objectName = header.Groups[3].Value
                    .Replace("[", "")
                    .Replace("]", "");

                if (objectName.Contains("."))
                    objectName = objectName.Split('.').Last();

                int startIndex = header.Index;
                int endIndex = sql.Length;

                // ===============================
                // TABLE → up to next CREATE/ALTER
                // ===============================
                if (objectType == "TABLE")
                {
                    endIndex = (i + 1 < headers.Count)
                        ? headers[i + 1].Index
                        : sql.Length;
                }
                // ======================================
                // PROC / FUNCTION → until END or GO
                // ======================================
                else
                {
                    var endRegex = new Regex(
                        @"\bEND\b\s*(GO\b)?",
                        RegexOptions.IgnoreCase | RegexOptions.Multiline
                    );

                    Match endMatch = endRegex.Match(sql, startIndex);

                    endIndex = endMatch.Success
                        ? endMatch.Index + endMatch.Length
                        : (i + 1 < headers.Count ? headers[i + 1].Index : sql.Length);
                }

                // 🔥 FULL SQL BLOCK
                string sqlBlock = sql.Substring(startIndex, endIndex - startIndex).Trim();

                string saveType =
                    objectType == "TABLE" ? "sql-table" :
                    objectType == "FUNCTION" ? "sql-function" :
                    "sql-procedure";

                string filePath = Path.Combine(baseDir, objectName + ".sql");

                // ⚠ IMPORTANT: fully qualify File
                await System.IO.File.WriteAllTextAsync(filePath, sqlBlock);

                await _dal.Save_File_Details(
                    projectId,
                    parentId,
                    objectName,
                    filePath,
                    saveType,
                    sqlBlock
                );
            }
        }


        private async Task ExtractViewFunctionsAndControllerCalls(
     string selectedModule,
     int projectId,
     string filePath,
     int parentId)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !System.IO.File.Exists(filePath))
                return;

            string content = await System.IO.File.ReadAllTextAsync(filePath);

            bool isView = selectedModule == "View";
            bool isJavaScript = selectedModule == "JavaScript";

            /* =========================================================
               VIEW FUNCTIONS (cshtml)
            ========================================================= */
            if (isView)
            {
                // ============================
                // REGEX DEFINITIONS
                // ============================

                // function MyFunc() {}
                var functionDefRegex = new Regex(
                    @"\bfunction\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(",
                    RegexOptions.Multiline
                );

                // const MyFunc = function() {} | const MyFunc = () => {}
                var assignedFunctionRegex = new Regex(
                    @"\b(?:const|let|var)\s+([A-Za-z_][A-Za-z0-9_]*)\s*=\s*(?:function\s*\(|\(\s*\)\s*=>)",
                    RegexOptions.Multiline
                );

                // onclick="MyFunc()" | onchange="MyFunc()"
                var inlineEventRegex = new Regex(
                    @"on[a-zA-Z]+\s*=\s*""\s*([A-Za-z_][A-Za-z0-9_]*)\s*\(",
                    RegexOptions.IgnoreCase
                );

                // BUSINESS function calls only (filters out noise by regex)
                var functionCallRegex = new Regex(
                    @"(?<!function\s)\b([A-Za-z_]*[A-Z_][A-Za-z0-9_]*)\s*\(",
                    RegexOptions.Multiline
                );

                // ============================
                // MINIMAL FILTERS
                // ============================
                HashSet<string> jsKeywords = new HashSet<string>
    {
        "if","for","while","switch","catch","return","function",
        "ready","on","trigger",
        "$","jQuery",
        "addEventListener",
        "RegExp"
    };

                // ============================
                // STORAGE
                // ============================
                HashSet<string> saved = new HashSet<string>();
                HashSet<string> definedFunctions = new HashSet<string>();

                void Save(string name, string type, bool force = false)
                {
                    if (string.IsNullOrWhiteSpace(name))
                        return;

                    // Inline events are always trusted
                    if (!force && jsKeywords.Contains(name))
                        return;

                    string key = $"{name}:{type}";
                    if (saved.Add(key))
                    {
                        _dal.Save_Child_File_Details(
                            projectId,
                            parentId,
                            name,
                            type
                        ).Wait();
                    }
                }

                // ============================
                // EXTRACTION ORDER (CRITICAL)
                // ============================

                // 1️⃣ INLINE EVENTS → ALWAYS KEEP
                foreach (Match m in inlineEventRegex.Matches(content))
                {
                    Save(m.Groups[1].Value, "inline-event", force: true);
                }

                // 2️⃣ FUNCTION DECLARATIONS
                foreach (Match m in functionDefRegex.Matches(content))
                {
                    string name = m.Groups[1].Value;
                    definedFunctions.Add(name);
                    Save(name, "view-function");
                }

                // 3️⃣ ASSIGNED / ARROW FUNCTIONS
                foreach (Match m in assignedFunctionRegex.Matches(content))
                {
                    string name = m.Groups[1].Value;
                    definedFunctions.Add(name);
                    Save(name, "view-function");
                }

                // 4️⃣ FUNCTION CALLS (BUSINESS ONLY)
                foreach (Match m in functionCallRegex.Matches(content))
                {
                    string name = m.Groups[1].Value;

                    if (definedFunctions.Contains(name))
                        continue;

                    if (jsKeywords.Contains(name))
                        continue;

                    Save(name, "view-function-call");
                }
            }






            /* =========================================================
               JAVASCRIPT (inline <script> OR .js file)
            ========================================================= */
            if (isJavaScript || isView)
            {
                // Only scan JS content inside <script> when View
                string jsContent = content;

                if (isView)
                {
                    var scriptRegex =
                        new Regex(@"<script[^>]*>([\s\S]*?)<\/script>",
                                  RegexOptions.IgnoreCase);

                    jsContent = string.Join(
                        Environment.NewLine,
                        scriptRegex.Matches(content)
                                   .Select(m => m.Groups[1].Value)
                    );
                }

                if (!string.IsNullOrWhiteSpace(jsContent))
                {
                    // Controller/action calls
                    var controllerRegex =
                        new Regex(@"['""]\/([A-Za-z0-9_]+)\/([A-Za-z0-9_]+)['""]");

                    foreach (Match match in controllerRegex.Matches(jsContent))
                    {
                        string controllerCall =
                            $"{match.Groups[1].Value}/{match.Groups[2].Value}";

                        await _dal.Save_Child_File_Details(
                            projectId,
                            parentId,
                            controllerCall,
                            "controller-call"
                        );
                    }
                }
            }

            /* =========================================================
               CONTROLLER / BLL
            ========================================================= */
            if (selectedModule == "Controller" || selectedModule == "BLL")
            {
                // 1️⃣ Regex to capture method calls
                var methodCallRegex =
                    new Regex(@"\b(?:\w+\.)?([A-Za-z_][A-Za-z0-9_]*)\s*\(",
                              RegexOptions.Compiled);

                // 2️⃣ Excluded methods (noise)
                var excludedMethods = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        // C# keywords
        "if", "catch", "using", "typeof", "exception", "for", "foreach",
        "while", "switch", "return", "try", "throw", "new", "lock",

        // Framework / common methods
        "ToString", "ToInt32", "WriteLine",
        "SerializeObject", "DeserializeObject", "Json",

        // LINQ
        "Select", "ToList", "AsEnumerable", "Where", "FirstOrDefault",

        // Crypto / system
        "GetBytes", "ComputeHash", "ToBase64String",

        // Types / helpers
        "DataTable", "Convert"
    };

                // 3️⃣ Business method naming filter
                bool IsBusinessMethod(string methodName)
                {
                    return methodName.StartsWith("Get_", StringComparison.OrdinalIgnoreCase)
                        || methodName.StartsWith("Add_", StringComparison.OrdinalIgnoreCase)
                        || methodName.StartsWith("Save_", StringComparison.OrdinalIgnoreCase)
                        || methodName.StartsWith("Insert_", StringComparison.OrdinalIgnoreCase)
                        || methodName.StartsWith("Update_", StringComparison.OrdinalIgnoreCase)
                        || methodName.StartsWith("Delete_", StringComparison.OrdinalIgnoreCase)
                        || methodName.StartsWith("Approve_", StringComparison.OrdinalIgnoreCase)
                        || methodName.StartsWith("Duplicate_", StringComparison.OrdinalIgnoreCase);
                }

                // 4️⃣ Avoid duplicate inserts
                var uniqueMethods = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (Match match in methodCallRegex.Matches(content))
                {
                    string methodName = match.Groups[1].Value;

                    // ❌ Skip noise
                    if (excludedMethods.Contains(methodName))
                        continue;

                    // ❌ Skip non-business methods
                    if (!IsBusinessMethod(methodName))
                        continue;

                    // ❌ Skip duplicates
                    if (!uniqueMethods.Add(methodName))
                        continue;

                    // 5️⃣ Save relationship
                    await _dal.Save_Child_File_Details(
                        projectId,
                        parentId,
                        methodName,
                        selectedModule == "Controller"
                            ? "controller-link"
                            : "bll-link"
                    );
                }
            }

            /* =========================================================
               DAL
            ========================================================= */
            /* =========================================================
               DAL (Stored Procedure Extraction) — STRING + CONST SAFE
            ========================================================= */
            if (selectedModule == "DAL")
            {
                var spRegex = new Regex(
                    // 1️⃣ String-based SPs
                    @"CommandText\s*=\s*""([^""]+)""|" +
                    @"new\s+SqlCommand\s*\(\s*""([^""]+)""|" +
                    @"Execute(?:Reader|Scalar|NonQuery|Dataset|DataTable)?\s*\(\s*""([^""]+)""|" +

                    // 2️⃣ Constant / Enum-based SPs
                    @"CommandType\.StoredProcedure\s*,\s*([A-Za-z0-9_\.]+)",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled
                );

                var savedSps = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (Match match in spRegex.Matches(content))
                {
                    string spName =
                        match.Groups[1].Success ? match.Groups[1].Value :
                        match.Groups[2].Success ? match.Groups[2].Value :
                        match.Groups[3].Success ? match.Groups[3].Value :
                        match.Groups[4].Value;

                    if (string.IsNullOrWhiteSpace(spName))
                        continue;

                    // ✅ Normalize constant-based SP names
                    // StoreProcedure.INSERT_SIGNUP_DETAILS → INSERT_SIGNUP_DETAILS
                    if (spName.Contains("."))
                        spName = spName.Split('.').Last();

                    // ❌ Ignore inline SQL
                    if (spName.Contains(" ") ||
                        spName.StartsWith("select", StringComparison.OrdinalIgnoreCase) ||
                        spName.StartsWith("update", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (!savedSps.Add(spName))
                        continue;

                    await _dal.Save_Child_File_Details(
                        projectId,
                        parentId,
                        spName,
                        "stored-procedure"
                    );
                }
            }

        }

        public async Task<IActionResult> Get_File_Path_For_View_Code(string filename)
        {
            int userId = Convert.ToInt32(HttpContext.Request.Cookies["USER_ID"]);

            var file = await _dal.Get_File_Path_For_View_Code(userId, filename);

            if (file == null)
                return Json(null);

            return Json(new
            {
                fileId = file.FileId,
                fileName = file.FileName,
                filePath = file.FilePath,
                fileType = file.FileType,
                textContent = System.IO.File.Exists(file.FilePath)
                    ? await System.IO.File.ReadAllTextAsync(file.FilePath)
                    : null
            });
        }

    }

}
