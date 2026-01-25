
        private static string NormalizeModuleName(string name)
        {
            name = name?.Trim() ?? "";
            return name switch
            {
                "Views" => "View",
                "JS" => "JavaScript",
                "Scripts" => "JavaScript",
                "Controllers" => "Controller",
                "DB" => "Database",
                "Styles" => "CSS",
                _ => name
            };
        }