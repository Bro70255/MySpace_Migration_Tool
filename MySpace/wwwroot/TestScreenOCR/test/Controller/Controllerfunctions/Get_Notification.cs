        public JsonResult Get_Notification()
        {
            DataTable dtDetails = new DataTable();

            try
            {
                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                var FIRM = Convert.ToInt32(Session["FIRM"]);
                var UserType = Convert.ToInt32(Session["UserType"]);
                var Team_id = Convert.ToInt32(Session["Team_id"]);
                var Unit = Convert.ToInt32(Session["Unit"]);

                dtDetails = CRF_Tracker_bll.Get_Notification(EMP_CODE, FIRM, UserType, Team_id, Unit);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }