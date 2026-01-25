

        public async Task<int> Save_Create_Project(ProjectCreateDto model, int userId)
        {
            var entity = new ProjectMaster
            {
                ProjectName = model.ProjectName,
                ProjectType = model.ProjectType,
                ProjectFlow = JsonConvert.SerializeObject(model.ProjectFlow),
                CreatedBy = userId,
                CreatedOn = DateTime.Now
            };

            await _context.ProjectMasters.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ProjectId;
        }