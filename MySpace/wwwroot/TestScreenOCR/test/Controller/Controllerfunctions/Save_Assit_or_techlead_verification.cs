        public JsonResult Save_Assit_or_techlead_verification(int DWU_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int A_TL = 1;
                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                //int empId = HttpContext.Request.Cookies["UserID"] != null && int.TryParse(HttpContext.Request.Cookies["UserID"].Value, out int userId) ? userId : 0;
                CRF_Tracker_bll.Save_Assit_or_techlead_verification(DWU_ID, EMP_CODE, A_TL);
            }
            catch (Exception ex)
            {

            }
            string JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }