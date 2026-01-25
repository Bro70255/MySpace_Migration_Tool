
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