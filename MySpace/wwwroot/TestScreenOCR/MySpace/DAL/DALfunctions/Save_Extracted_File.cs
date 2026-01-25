
        public async Task<int> Save_Extracted_File(int parentFileId, string extractedName, string extractedPath, string extractedType)
        {
            var entity = new ExtractedFileDetails
            {
                ParentFileId = parentFileId,
                ExtractedName = extractedName,
                ExtractedPath = extractedPath,
                ExtractedType = extractedType,
                CreatedOn = DateTime.Now
            };

            await _context.ExtractedFileDetails.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ExtractedId;
        }