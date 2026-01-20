        public JsonResult Bind_CRF_for_Dailywork()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                var FIRM = Convert.ToInt32(Session["FIRM"]);
                var UserType = Convert.ToInt32(Session["UserType"]);
                dtDetails = CRF_Tracker_bll.Bind_CRF_for_Dailywork(EMP_CODE, FIRM, UserType);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }