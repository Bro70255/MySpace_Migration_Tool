        public async Task<List<BlueprintEdgeDto>> GetBlueprintData()
        {
            var list = new List<BlueprintEdgeDto>();

            await using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "dbo.sp_GetBlueprintData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;

            await using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new BlueprintEdgeDto
                    {
                        FromNode = reader.IsDBNull(0) ? null : reader.GetString(0),
                        ToNode = reader.IsDBNull(1) ? null : reader.GetString(1)
                    });
                }
            }

            return list;
        }