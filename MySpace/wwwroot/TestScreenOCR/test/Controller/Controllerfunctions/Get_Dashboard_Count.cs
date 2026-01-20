        public JsonResult Get_Dashboard_Count()
        {
            DataTable dtDetails = new DataTable();

            try
            {

                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                var UserID = Convert.ToInt32(Session["UserID"]);
                // var NAME = Convert.ToInt32(Session["NAME"]);
                var FIRM = Convert.ToInt32(Session["FIRM"]);
                var UNIT = Convert.ToInt32(Session["UNIT"]);
                var Team_id = Convert.ToInt32(Session["Team_id"]);
                var UserType = Convert.ToInt32(Session["UserType"]);

                dtDetails = CRF_Tracker_bll.Get_Dashboard_Count(EMP_CODE, UserID, FIRM, UNIT, Team_id, UserType);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }