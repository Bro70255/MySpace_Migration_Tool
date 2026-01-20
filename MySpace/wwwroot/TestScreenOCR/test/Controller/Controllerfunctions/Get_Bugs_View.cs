        public JsonResult Get_Bugs_View()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                string crf_ID = Convert.ToString(Session["crf_id"]);
                dtDetails = CRF_Tracker_bll.Get_Bugs_View(crf_ID);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }