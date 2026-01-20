        public JsonResult Get_Current_Work_Assigned_Of_Developers_Report(int Developer, int last_dev_endate)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Firm = Convert.ToInt32(Session["Firm"]);
                dtDetails = CRF_Tracker_bll.Get_Current_Work_Assigned_Of_Developers_Report(Firm, Developer, last_dev_endate);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }