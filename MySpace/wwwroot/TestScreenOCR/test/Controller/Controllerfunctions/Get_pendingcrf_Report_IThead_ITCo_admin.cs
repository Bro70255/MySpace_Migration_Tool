        public JsonResult Get_pendingcrf_Report_IThead_ITCo_admin(int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Employee_Code = Convert.ToInt32(Session["EMP_CODE"]);
                int UserType = Convert.ToInt32(Session["UserType"]);
                int Team_id = Convert.ToInt32(Session["Team_id"]);
                int Unit = Convert.ToInt32(Session["UNIT"]);
                dtDetails = CRF_Tracker_bll.Get_pendingcrf_Report_IThead_ITCo_admin(firm, Employee_Code, UserType, Team_id, Unit);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }