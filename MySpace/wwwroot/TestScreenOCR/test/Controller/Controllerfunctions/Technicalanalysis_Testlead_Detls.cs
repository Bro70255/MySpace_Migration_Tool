        public JsonResult Technicalanalysis_Testlead_Detls(Dictionary<string, string> Technicalanalysis_Techleaddtls, List<List<string>> Detail)
        {
            try
            {
                int Testlead_Status = 10;
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                CRF_Tracker_bll.Technicalanalysis_Testlead_Detls(Technicalanalysis_Techleaddtls, Detail, EMP_CODE, Testlead_Status);
                return Json(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }