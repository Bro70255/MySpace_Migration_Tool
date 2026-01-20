        public JsonResult Get_Bind_Description_of_work()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dtDetails = CRF_Tracker_bll.Get_Bind_Description_of_work();
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }