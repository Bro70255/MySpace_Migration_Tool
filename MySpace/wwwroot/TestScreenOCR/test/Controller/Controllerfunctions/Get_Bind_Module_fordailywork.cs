        public JsonResult Get_Bind_Module_fordailywork()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Firm = Convert.ToInt32(Session["Firm"]);
                dtDetails = CRF_Tracker_bll.Get_Bind_Module_fordailywork(Firm);
                ;
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }