        public async Task<IActionResult> RegisterUser([FromBody] RegisterVM model)
        {
            // -------- Model Validation --------
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid input data" });

            // -------- Password Match Validation --------
            if (model.Password != model.ConfirmPassword)
                return Json(new { success = false, message = "Passwords do not match" });

            // -------- Call DAL to Register User --------
            bool result = await _dal.RegisterUserAsync(
                model.FirstName,
                model.LastName,
                model.Email,
                model.Username,
                model.Password
            );

            // -------- User Already Exists --------
            if (!result)
                return Json(new { success = false, message = "User already exists" });

            // -------- Registration Success --------
            return Json(new
            {
                success = true,
                message = "Account created successfully"
            });
        }