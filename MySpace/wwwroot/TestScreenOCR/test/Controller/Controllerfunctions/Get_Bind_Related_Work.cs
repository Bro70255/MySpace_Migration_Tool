        public JsonResult Get_Bind_Related_Work(int change_type)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dtDetails = CRF_Tracker_bll.Get_Bind_Related_Work(change_type);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }