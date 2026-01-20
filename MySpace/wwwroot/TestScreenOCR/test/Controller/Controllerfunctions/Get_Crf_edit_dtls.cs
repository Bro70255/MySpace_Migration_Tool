        public JsonResult Get_Crf_edit_dtls()
        {
            DataTable dtDetails = new DataTable();

            try
            {
                var emp_code = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Get_Crf_edit_dtls(emp_code);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }