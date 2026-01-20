        public JsonResult Save_Hold_CRF_Details(string CRFid, DateTime Hold_from, DateTime Hold_end, string Reason, string Remark)

        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Employee_Code = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Save_Hold_CRF_Details(CRFid, Hold_from, Hold_end, Reason, Remark, Employee_Code);

            }
            catch (Exception ex)
            {

                throw (ex);
            }
            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }