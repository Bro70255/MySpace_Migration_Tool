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