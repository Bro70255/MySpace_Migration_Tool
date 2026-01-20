        public JsonResult Crf_Detls(Crfdtls Crf_Details)
        {
            try
            {
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                string crfId = CRF_Tracker_bll.Crf_Detls(Crf_Details, EMP_CODE);
                Session["ID"] = crfId;
                return Json(0);
            }
            catch (Exception ex)
            {
                // Handle exception here if needed
                return Json(new { error = "Error processing CRF details", message = ex.Message });
            }
        }