
        private static string SanitizePathSegment(string s) { return string.Concat((s ?? "").Split(Path.GetInvalidFileNameChars())).Trim(); }