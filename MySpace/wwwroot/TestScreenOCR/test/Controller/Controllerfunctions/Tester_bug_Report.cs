        public JsonResult Tester_bug_Report(Tester_Bug_Reported_Dtls_ Tester_Bug_Reported_Dtls_)
        {
            try
            {
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                string crf_ID = Convert.ToString(Session["Crf_id"]);
                CRF_Tracker_bll.Tester_bug_Report(Tester_Bug_Reported_Dtls_, EMP_CODE, crf_ID);

                return Json(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }