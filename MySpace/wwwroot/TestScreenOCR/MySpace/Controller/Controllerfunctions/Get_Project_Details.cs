
        public async Task<JsonResult> Get_Project_Details()
        {
            int userId = Convert.ToInt32(HttpContext.Request.Cookies["USER_ID"]);

            var data = await _dal.Get_Project_Details(userId);
            return Json(data);
        }