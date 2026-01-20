        public ActionResult CRF_Status_Flow(string CRF_id)
        {
            Session["CRF_id"] = CRF_id;
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