        public JsonResult Get_Testing_ta_pnding_crfReport_testlead()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int firm = Convert.ToInt32(Session["Firm"]);
                dtDetails = CRF_Tracker_bll.Get_Testing_ta_pnding_crfReport_testlead(firm);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }