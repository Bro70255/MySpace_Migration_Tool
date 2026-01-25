        public async Task<User?> Sign_InAsync(string username, string password)
        {
            var param = new SqlParameter("@Username", username);

            var user = _context.Users
                .FromSqlRaw("EXEC SP_GET_USER_FOR_LOGIN @Username", param)
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();

            if (user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValid)
                return null;

            return user;
        }