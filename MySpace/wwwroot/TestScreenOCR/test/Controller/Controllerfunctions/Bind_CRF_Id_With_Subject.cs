        public JsonResult Bind_CRF_Id_With_Subject()
        {
            DataTable dtDetails = new DataTable();
            try
            {

                int firm = Convert.ToInt32(Session["FIRM"]);
                int unit = Convert.ToInt32(Session["UNIT"]);
                int Hod = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Bind_CRF_Id_With_Subject(firm, unit, Hod);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }