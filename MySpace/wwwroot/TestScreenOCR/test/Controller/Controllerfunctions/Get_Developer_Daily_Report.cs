        public JsonResult Get_Developer_Daily_Report(int value, DateTime From_date, DateTime To_date, int Developer, string Module)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                //if (Module != "")
                //{
                //    value = 2;
                //}
                dtDetails = CRF_Tracker_bll.Get_Developer_Daily_Report(value, From_date, To_date, Developer, Module);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }