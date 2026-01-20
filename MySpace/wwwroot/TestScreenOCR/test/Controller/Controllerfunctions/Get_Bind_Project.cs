        public JsonResult Get_Bind_Project(int impactmodule)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dtDetails = CRF_Tracker_bll.Get_Bind_Project(impactmodule);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }