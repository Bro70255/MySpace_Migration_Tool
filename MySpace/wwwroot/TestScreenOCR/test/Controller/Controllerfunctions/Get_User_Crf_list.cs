        public JsonResult Get_User_Crf_list()
        {
            DataTable dtDetails = new DataTable();

            try
            {

                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Get_User_Crf_list(EMP_CODE);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }