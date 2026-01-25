
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