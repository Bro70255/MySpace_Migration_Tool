        public JsonResult Get_Emp_Name()
        {
            DataTable dtDetails = new DataTable();

            try
            {
                var EMP_code = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Get_Emp_Name(EMP_code);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }