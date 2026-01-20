        public JsonResult Get_CRF_for_Testlead_Bug_Report()
        {
            DataTable dtDetails = new DataTable();
            try
            {

                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);

                dtDetails = CRF_Tracker_bll.Get_CRF_for_Testlead_Bug_Report(EMP_CODE);
            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }