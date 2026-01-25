        public async Task<int> Save_File_Details(int projectId,int parentFileId, string fileName, string filePath, string fileType, string textContent)
        {
            var entity = new FileDetails
            {
                ProjectId = projectId,
                ParentFileId = parentFileId,
                FileName = fileName,
                FilePath = filePath,
                FileType = fileType,
                TextContent = textContent,
                UploadedOn = DateTime.Now
            };

            await _context.FileDetails.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.FileId;
        }