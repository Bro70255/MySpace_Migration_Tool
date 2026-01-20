        public JsonResult Get_Total_crf_Report(int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                dtDetails = CRF_Tracker_bll.Get_Total_crf_Report(firm);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }