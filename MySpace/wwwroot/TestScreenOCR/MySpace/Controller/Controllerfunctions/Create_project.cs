        public async Task<IActionResult> Create_Project([FromBody] ProjectCreateDto model)
        {
            if (model == null)
                return BadRequest("Invalid data");

            int userId = Convert.ToInt32(HttpContext.Request.Cookies["USER_ID"]);


            await _dal.Save_Create_Project(model, userId);

            return Json(new { success = true });
        }