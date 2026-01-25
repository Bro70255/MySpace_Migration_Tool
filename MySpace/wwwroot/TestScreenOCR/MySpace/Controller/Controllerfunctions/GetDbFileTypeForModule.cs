
        private static string GetDbFileTypeForModule(string module)
        {
            module = NormalizeModuleName(module);
            return module switch
            {
                "View" => "cshtml",
                "JavaScript" => "js",
                "CSS" => "css",
                "Database" => "sql",
                "Controller" => "controller",
                "BLL" => "bll",
                "DAL" => "dal",
                _ => "unknown"
            };
        }