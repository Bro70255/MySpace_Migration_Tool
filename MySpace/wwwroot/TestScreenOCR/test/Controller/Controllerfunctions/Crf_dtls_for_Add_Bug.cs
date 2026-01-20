        public JsonResult Crf_dtls_for_Add_Bug(string crf_id)
        {
            DataTable dtDetails = new DataTable();

            try
            {
                string crf_ID = Convert.ToString(Session["Crf_id"]);

                // Use crf_id in your method to fetch details
                dtDetails = CRF_Tracker_bll.Crf_dtls_for_hod_Recommendation(crf_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }