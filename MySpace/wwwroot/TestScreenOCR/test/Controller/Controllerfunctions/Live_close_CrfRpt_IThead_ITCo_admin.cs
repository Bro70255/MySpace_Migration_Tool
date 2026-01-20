        public ActionResult Live_close_CrfRpt_IThead_ITCo_admin()
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