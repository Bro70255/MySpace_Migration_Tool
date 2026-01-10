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
            string databasePath = Path.Combine(basePath, "Database");

            Directory.CreateDirectory(viewsPath);
            Directory.CreateDirectory(jsPath);
            Directory.CreateDirectory(cssPath);
            Directory.CreateDirectory(controllerPath);
            Directory.CreateDirectory(databasePath);

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

                    case ".sql":
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

                // 1. Save original file
                int parentFileId = await _dal.Save_File_Details(
                    safeFileName,
                    savePath,
                    fileType,
                    textContent
                );

                // 2. Extract & save children
                if (fileType == "js")
                    await SplitJSFunctions(savePath, parentFileId);
                else if (fileType == "cs")
                    await SplitCSharpMethods(savePath, parentFileId);
                else if (fileType == "sql")
                    await SplitSqlTablesAndProcedures(savePath, parentFileId);
                else if (fileType == "cshtml")
                    await ExtractAllCshtmlFunctions(savePath, parentFileId);

            }

            return Json(new
            {
                success = true,
                message = "Files uploaded, converted and extracted successfully"
            });
        }

        // =========================================================
        // JS FUNCTION SPLITTER
        // =========================================================
        private async Task SplitJSFunctions(string sourcePath, int parentFileId)
        {
            var outputDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "js", "jsfunctions"
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

                // Save JS Function
                int jsFunctionId = await _dal.Save_Extracted_File(
                    parentFileId,
                    functionName,
                    filePath,
                    "js-function"
                );

                // 🔥 Extract API Calls
                await ExtractControllerCalls(body, jsFunctionId);
            }
        }


        // =========================================================
        // C# METHOD SPLITTER
        // =========================================================
        private async Task SplitCSharpMethods(string sourcePath, int parentFileId)
        {
            var outputDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "Controller", "Controllerfunctions"
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

                await _dal.Save_Extracted_File(
                    parentFileId,
                    name,
                    filePath,
                    "cs-method"
                );
            }
        }

        // =========================================================
        // SQL TABLE & PROCEDURE SPLITTER
        // =========================================================
        private async Task SplitSqlTablesAndProcedures(string sourcePath, int parentFileId)
        {
            string baseDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "TestScreenOCR", "Database"
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

                        await _dal.Save_Extracted_File(
                            parentFileId,
                            name,
                            path,
                            "sql-table"
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

                        await _dal.Save_Extracted_File(
                            parentFileId,
                            name,
                            path,
                            "sql-procedure"
                        );
                    }
                }
            }
        }

        private async Task ExtractAllCshtmlFunctions(string filePath, int parentFileId)
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
                    parentFileId,
                    func,
                    "cshtml-function"
                );
            }
        }

        private async Task ExtractControllerCalls(string jsBody, int parentFunctionId)
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

    }

}
