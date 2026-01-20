        public JsonResult Bind_Firm_For_Developer_Wise_Report()
        {
            DataTable dtDetails = new DataTable();
            try
            {

                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);

                dtDetails = CRF_Tracker_bll.Bind_Firm_For_Developer_Wise_Report(EMP_CODE);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }