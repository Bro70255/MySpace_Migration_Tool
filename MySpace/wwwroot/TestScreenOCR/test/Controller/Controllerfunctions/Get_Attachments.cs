
        public JsonResult Get_Attachments()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                String CRF_ID = Session["crf_id"].ToString();
                dtDetails = CRF_Tracker_bll.Get_Attachments(CRF_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }