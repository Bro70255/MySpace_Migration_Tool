        public JsonResult Get_Attachment_Files()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                String CRF_ID = Session["CRF_id"].ToString();
                dtDetails = CRF_Tracker_bll.Get_Attachment_Files(CRF_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }