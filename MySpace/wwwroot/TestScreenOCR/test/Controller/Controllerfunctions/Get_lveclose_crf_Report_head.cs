
        public JsonResult Get_lveclose_crf_Report_head(int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                dtDetails = CRF_Tracker_bll.Get_lveclose_crf_Report_head(firm);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }