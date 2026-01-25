
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