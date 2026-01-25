        public async Task<IActionResult> Register([FromBody] Registration model)
        {
            if (ModelState.IsValid)
            {
                var result = await _dal.Save_Registration_Form(model);

                if (result)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "User saved successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to save user"
                    });
                }
            }

            return BadRequest(new
            {
                success = false,
                message = "Validation failed",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }