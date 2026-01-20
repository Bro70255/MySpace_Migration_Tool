        public JsonResult Save_Daily_Work_Updation(List<List<string>> Details)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                var FIRM = Convert.ToInt32(Session["FIRM"]);
                var UserType = Convert.ToInt32(Session["UserType"]);
                CRF_Tracker_bll.Save_Daily_Work_Updation(EMP_CODE, FIRM, UserType, Details);
            }
            catch (Exception ex)
            {

            }
            string JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }