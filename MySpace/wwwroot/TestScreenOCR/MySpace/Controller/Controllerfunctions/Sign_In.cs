        public async Task<JsonResult> Sign_In(string username, string password)
        {
            var user = await _dal.Sign_InAsync(username, password);

            if (user == null)
            {
                return Json(new { success = false, message = "Invalid username or password" });
            }

            // Cookies / Session
            Response.Cookies.Append("USER_ID", user.UserId.ToString());
            Response.Cookies.Append("USERNAME", user.Username);

            return Json(new
            {
                success = true,
                username = user.Username
            });
        }