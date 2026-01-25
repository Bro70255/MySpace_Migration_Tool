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