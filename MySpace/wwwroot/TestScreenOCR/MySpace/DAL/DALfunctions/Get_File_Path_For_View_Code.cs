
        public async Task<FileDetails?> Get_File_Path_For_View_Code(int userId, string filename)
        {
            string inputName = Path.GetFileNameWithoutExtension(filename);

            return _context.FileDetails
                .Where(x =>
                    _context.ProjectMasters.Any(p =>
                        p.ProjectId == x.ProjectId &&
                        p.CreatedBy == userId
                    )
                )
                .AsEnumerable() // 🔥 switch to in-memory
                .Where(x =>
                    Path.GetFileNameWithoutExtension(x.FileName)
                        .Equals(inputName, StringComparison.OrdinalIgnoreCase)
                )
                .OrderByDescending(x => x.UploadedOn)
                .FirstOrDefault();
        }