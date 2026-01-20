        public ActionResult Prev_month_live_closed()
        {
            if (Session["EMP_CODE"] == null)
            {
                // Session variable "EMP_CODE" is null, so redirect to the login page
                return RedirectToAction("SessionTimeout", "Home");
            }
            else
            {
                return View();
            }
        }