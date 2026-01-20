        public JsonResult Get_head_approval_pending_Techlead()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int firm = Convert.ToInt32(Session["Firm"]);
                int Team_id = Convert.ToInt32(Session["Team_id"]);
                dtDetails = CRF_Tracker_bll.Get_head_approval_pending_Techlead(firm, Team_id);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }