        public JsonResult Technicalanalysis_Techlead_Detls(Dictionary<string, string> Technicalanalysis_dtls, List<List<string>> Details)
        {
            try
            {
                int Techlead_Status = 7;
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                CRF_Tracker_bll.Technicalanalysis_Techlead_Detls(Technicalanalysis_dtls, Details, EMP_CODE, Techlead_Status);

                return Json(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }