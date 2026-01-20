        public JsonResult Bind_Hold_CRF_Id_With_Subject()
        {
            DataTable dtDetails = new DataTable();
            try
            {

                int EMP_code = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Bind_Hold_CRF_Id_With_Subject(EMP_code);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }