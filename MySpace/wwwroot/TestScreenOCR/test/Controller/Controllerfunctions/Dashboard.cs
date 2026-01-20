        public ActionResult Dashboard()
        {
            if (Session["EMP_CODE"] == null)
            {
                return RedirectToAction("SessionTimeout", "Home");
            }

            HttpCookie cookie = Request.Cookies["name"];
            if (cookie != null)
            {
                ViewBag.SelectedLabel = cookie.Value; // Set the selected label from cookie
            }
            else
            {
                ViewBag.SelectedLabel = "mafound"; // Or set a default value if needed
            }

            return View();
        }