        public ActionResult Attachments(string crf_id)
        {
            ViewBag.IsView = 0;

            if (!string.IsNullOrEmpty(crf_id))
            {
                byte[] data = System.Convert.FromBase64String(crf_id);
                string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);

                if (base64Decoded.Contains("VIEW"))
                {
                    ViewBag.IsView = 1;
                    Session["crf_id"] = base64Decoded.Substring(4);
                }
                else
                {
                    Session["crf_id"] = "";
                }
            }
            else
            {
                Session["crf_id"] = null;
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