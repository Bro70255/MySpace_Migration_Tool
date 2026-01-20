        public JsonResult Get_Bind_Unit(int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                dtDetails = CRF_Tracker_bll.Get_Bind_Unit(firm);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }