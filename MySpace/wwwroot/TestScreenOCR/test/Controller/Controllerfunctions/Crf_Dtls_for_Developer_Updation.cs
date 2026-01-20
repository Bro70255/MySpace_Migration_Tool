        public JsonResult Crf_Dtls_for_Developer_Updation(string crf_id)
        {
            DataTable dtDetails = new DataTable();

            try
            {
                string crf_ID = crf_id.Substring(0, 7);
                // Use crf_id in your method to fetch details
                dtDetails = CRF_Tracker_bll.Crf_Dtls_for_Developer_Updation(crf_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }