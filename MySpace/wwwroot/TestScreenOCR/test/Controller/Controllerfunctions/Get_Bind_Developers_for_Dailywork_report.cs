        public JsonResult Get_Bind_Developers_for_Dailywork_report()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Firm = Convert.ToInt32(Session["Firm"]);
                dtDetails = CRF_Tracker_bll.Get_Bind_Developers_for_Dailywork_report(Firm);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }