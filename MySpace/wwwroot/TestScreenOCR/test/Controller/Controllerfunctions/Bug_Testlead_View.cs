        public ActionResult Bug_Testlead_View(string Crf_id)
        {
            ViewBag.IsView = 0;

            if (!string.IsNullOrEmpty(Crf_id))
            {
                byte[] data = System.Convert.FromBase64String(Crf_id);
                string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);

                if (base64Decoded.Contains("VIEW"))
                {
                    ViewBag.IsView = 1;
                    Session["Crf_id"] = base64Decoded.Substring(4);
                }
                else
                {
                    Session["Crf_id"] = "";
                }
            }
            else
            {
                Session["Crf_id"] = null;
            }

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