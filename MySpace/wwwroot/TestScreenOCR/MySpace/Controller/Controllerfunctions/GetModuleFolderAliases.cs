
        private static IEnumerable<string> GetModuleFolderAliases(string module)
        {
            module = NormalizeModuleName(module);
            return module switch
            {
                "Controller" => new[] { "Controller", "Controllers" },
                "View" => new[] { "View", "Views" },
                "JavaScript" => new[] { "JavaScript", "JS", "Scripts", "Script", "js" },
                "CSS" => new[] { "CSS", "Styles", "Style", "css" },
                "Database" => new[] { "Database", "DB", "Sql", "SQL" },
                "BLL" => new[] { "BLL" },
                "DAL" => new[] { "DAL" },
                _ => new[] { module }
            };
        }