        public JsonResult Get_For_Horizontal_Flow_Data()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                String CRF_ID = Session["CRF_id"].ToString();
                dtDetails = CRF_Tracker_bll.Get_For_Horizontal_Flow_Data(CRF_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }