        public JsonResult Insert_Tester_dtls(string crf_id, int status, string Remark)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Tester = Convert.ToInt32(Session["EMP_CODE"]);
                string crf_ID = crf_id.Substring(0, 7);
                dtDetails = CRF_Tracker_bll.Insert_Tester_dtls(crf_ID, status, Remark, Tester);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }