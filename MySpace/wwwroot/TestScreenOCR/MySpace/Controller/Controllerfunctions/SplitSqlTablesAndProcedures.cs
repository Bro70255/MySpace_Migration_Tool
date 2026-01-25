


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