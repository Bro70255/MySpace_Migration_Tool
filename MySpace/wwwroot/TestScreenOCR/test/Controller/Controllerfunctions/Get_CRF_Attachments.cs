        public JsonResult Get_CRF_Attachments(string crf_id)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                string crf_ID = crf_id.Substring(0, 7);
                dtDetails = CRF_Tracker_bll.Get_CRF_Attachments(crf_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }