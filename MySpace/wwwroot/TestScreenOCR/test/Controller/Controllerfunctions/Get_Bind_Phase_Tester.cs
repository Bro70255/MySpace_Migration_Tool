        public JsonResult Get_Bind_Phase_Tester()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dtDetails = CRF_Tracker_bll.Get_Bind_Phase_Tester();
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }