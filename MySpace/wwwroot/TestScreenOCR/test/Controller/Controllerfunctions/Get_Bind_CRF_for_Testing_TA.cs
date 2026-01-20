        public JsonResult Get_Bind_CRF_for_Testing_TA()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Testlead = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Get_Bind_CRF_for_Testing_TA(Testlead);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }