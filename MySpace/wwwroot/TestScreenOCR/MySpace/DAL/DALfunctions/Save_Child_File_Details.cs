        public async Task Save_Child_File_Details(int projectId,int parentFileId, string name, string type)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            name = name.Trim();

            bool exists = await _context.FileChildDetails
                .AsNoTracking()
                .AnyAsync(x =>
                    x.ParentFileId == parentFileId &&
                    x.Name == name &&
                    x.Type == type);

            if (exists)
                return;

            var entity = new FileChildDetail
            {
                ProjectId = projectId,
                ParentFileId = parentFileId,
                Name = name,
                Type = type,
                CreatedOn = DateTime.Now
            };

            _context.FileChildDetails.Add(entity);
            await _context.SaveChangesAsync();
        }