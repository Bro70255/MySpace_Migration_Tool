        public JsonResult Get_Daily_Work_Update_Approvel_Head()
        {
            DataTable dtDetails = new DataTable();
            try
            {

                dtDetails = CRF_Tracker_bll.Get_Daily_Work_Update_Approvel_Head();
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }