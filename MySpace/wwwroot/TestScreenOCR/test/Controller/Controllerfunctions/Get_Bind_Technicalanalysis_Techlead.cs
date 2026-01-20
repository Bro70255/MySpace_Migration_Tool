        public JsonResult Get_Bind_Technicalanalysis_Techlead()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Techlead = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Get_Bind_Technicalanalysis_Techlead(Techlead);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }