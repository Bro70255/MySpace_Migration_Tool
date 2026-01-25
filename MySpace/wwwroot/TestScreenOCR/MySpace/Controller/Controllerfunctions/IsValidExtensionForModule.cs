
        private static bool IsValidExtensionForModule(string ext, string module)
        {
            ext = (ext ?? "").ToLowerInvariant();
            module = NormalizeModuleName(module);

            return module switch
            {
                "View" => ext == ".cshtml",
                "JavaScript" => ext == ".js",
                "CSS" => ext == ".css",
                "Database" => ext == ".sql",
                "Controller" or "BLL" or "DAL" => ext == ".cs",
                _ => false
            };
        }