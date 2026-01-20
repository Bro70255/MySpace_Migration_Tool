        public JsonResult Get_Crf_dtls_for_Bug_view(string crf_id)
        {
            DataTable dtDetails = new DataTable();

            try
            {
                string crf_ID = Convert.ToString(Session["crf_id"]);
                // Use crf_id in your method to fetch details
                dtDetails = CRF_Tracker_bll.Get_Crf_dtls_for_Bug_view(crf_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }