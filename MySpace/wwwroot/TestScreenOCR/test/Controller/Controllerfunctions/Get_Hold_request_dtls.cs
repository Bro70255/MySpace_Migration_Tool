        public JsonResult Get_Hold_request_dtls(string crf_id)
        {
            DataTable dtDetails = new DataTable();

            try
            {
                string crf_ID = crf_id.Substring(0, 7);
                // Use crf_id in your method to fetch details
                dtDetails = CRF_Tracker_bll.Get_Hold_request_dtls(crf_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }