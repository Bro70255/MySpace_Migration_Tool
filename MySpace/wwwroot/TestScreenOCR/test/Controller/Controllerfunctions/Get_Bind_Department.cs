
        public JsonResult Get_Bind_Department()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int unit = Convert.ToInt32(Session["UNIT"]);
                dtDetails = CRF_Tracker_bll.Get_Bind_Department(unit);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }