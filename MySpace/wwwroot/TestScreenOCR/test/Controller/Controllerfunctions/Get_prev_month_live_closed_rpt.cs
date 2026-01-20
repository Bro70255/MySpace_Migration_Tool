        public JsonResult Get_prev_month_live_closed_rpt(int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Employee_Code = Convert.ToInt32(Session["EMP_CODE"]);
                int UserType = Convert.ToInt32(Session["UserType"]);
                int Team_id = Convert.ToInt32(Session["Team_id"]);
                int Unit = Convert.ToInt32(Session["UNIT"]);
                dtDetails = CRF_Tracker_bll.Get_prev_month_live_closed_rpt(firm, Employee_Code, UserType, Team_id, Unit);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }