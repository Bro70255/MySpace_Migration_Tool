
        private static string NormalizeSelectedFileType(string fileType)
        {
            fileType = (fileType ?? "").Trim();
            if (string.IsNullOrWhiteSpace(fileType)) return "";

            string ft = fileType.ToLowerInvariant();
            return ft switch
            {
                "cshtml" => "View",
                "view" => "View",
                "js" => "JavaScript",
                "javascript" => "JavaScript",
                "css" => "CSS",
                "sql" => "Database",
                "database" => "Database",
                "db" => "Database",
                "controller" => "Controller",
                "controllers" => "Controller",
                "bll" => "BLL",
                "dal" => "DAL",
                _ => NormalizeModuleName(fileType)
            };
        }