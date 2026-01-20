        public JsonResult Save_Head_or_Coordinator_verification(int DWU_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int HC = 1;
                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                CRF_Tracker_bll.Save_Head_or_Coordinator_verification(DWU_ID, EMP_CODE, HC);
            }
            catch (Exception ex)
            {

            }
            string JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }