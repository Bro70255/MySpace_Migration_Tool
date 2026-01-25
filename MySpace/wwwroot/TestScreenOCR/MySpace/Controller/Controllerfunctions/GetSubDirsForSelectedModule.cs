        private static List<string> GetSubDirsForSelectedModule(string path, string module)
        {
            var parts = (path ?? "").Replace("\\", "/").Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
            if (parts.Count <= 1) return new();

            parts.RemoveAt(parts.Count - 1); // remove filename

            var aliases = GetModuleFolderAliases(module);
            int idx = parts.FindIndex(p => aliases.Any(a => p.Equals(a, StringComparison.OrdinalIgnoreCase)));

            if (idx >= 0) return parts.Skip(idx + 1).ToList();

            // If module folder not found in uploaded path, keep full folder structure (prevents flattening)
            return parts;
        }