        public JsonResult Get_Timeline_Comparison_Report(string financial_year)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int firm = Convert.ToInt32(Session["Firm"]);
                dtDetails = CRF_Tracker_bll.Get_Timeline_Comparison_Report(firm, financial_year);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }