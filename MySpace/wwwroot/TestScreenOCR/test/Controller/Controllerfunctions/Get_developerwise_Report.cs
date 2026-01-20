        public JsonResult Get_developerwise_Report(DateTime Startdate, DateTime Enddate, int Developer, int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                dtDetails = CRF_Tracker_bll.Get_developerwise_Report(Startdate, Enddate, Developer, firm);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }