        public JsonResult Get_TestCase_Updation_Report(DateTime startdate, DateTime enddate)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                var UserType = Convert.ToInt32(Session["UserType"]);
                var FIRM = Convert.ToInt32(Session["FIRM"]);

                dtDetails = CRF_Tracker_bll.Get_TestCase_Updation_Report(startdate, enddate, EMP_CODE, UserType, FIRM);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }