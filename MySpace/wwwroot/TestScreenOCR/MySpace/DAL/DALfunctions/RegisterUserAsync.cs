        public async Task<bool> RegisterUserAsync(string firstName, string lastName, string email, string username, string password)
        {
            try
            {
                bool exists = await _context.Users.AnyAsync(x =>
                    x.Email == email || x.Username == username);

                if (exists)
                    return false;

                string hash = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new User
                {
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    Email = email.Trim().ToLower(),
                    Username = username.Trim(),
                    PasswordHash = hash
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }