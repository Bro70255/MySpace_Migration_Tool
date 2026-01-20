        public JsonResult Bind_Crf_Id_For_User_Acceptance()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int User = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Bind_Crf_Id_For_User_Acceptance(User);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }