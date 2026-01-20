        public JsonResult Set_Firm(string selectedLabel)
        {
            try
            {
                // Create and add a cookie named "name" with the selected label
                HttpCookie cookie = new HttpCookie("name", selectedLabel)
                {
                    Expires = DateTime.Now.AddDays(30) // Set cookie expiration
                };
                Response.Cookies.Add(cookie);

                // Return a success message as JSON
                return Json(new { success = true, message = "Cookie set successfully!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the exception (if needed)
                return Json(new { success = false, message = "An error occurred.", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }