
        public JsonResult Get_pendingcrf_Report(int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                dtDetails = CRF_Tracker_bll.Get_pendingcrf_Report(firm);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }