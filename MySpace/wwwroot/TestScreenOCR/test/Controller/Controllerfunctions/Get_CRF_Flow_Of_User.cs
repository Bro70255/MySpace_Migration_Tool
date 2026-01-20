        public JsonResult Get_CRF_Flow_Of_User()
        {
            DataTable dtDetails = new DataTable();
            try
            {

                String CRF_ID = Session["CRF_id"].ToString();

                dtDetails = CRF_Tracker_bll.Get_CRF_Flow_Of_User(CRF_ID);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }