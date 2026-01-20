        public JsonResult Get_Manpower_Fortesting_Ta(string crf_id)
        {
            DataTable dtDetails = new DataTable();

            try
            {
                dtDetails = CRF_Tracker_bll.Get_Manpower_Fortesting_Ta(crf_id);
            }
            catch (Exception ex) { throw ex; }
            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }