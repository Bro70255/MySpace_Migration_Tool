        public JsonResult Get_Rejected_crfReport_techlead()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Team_id = Convert.ToInt32(Session["Team_id"]);
                int firm = Convert.ToInt32(Session["Firm"]);

                dtDetails = CRF_Tracker_bll.Get_Rejected_crfReport_techlead(firm, Team_id);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }