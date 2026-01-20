        public JsonResult Get_Bind_Crf_Id_For_update_publish()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                int Techlead = Convert.ToInt32(Session["EMP_CODE"]);
                dtDetails = CRF_Tracker_bll.Get_Bind_Crf_Id_For_update_publish(Techlead);

            }
            catch (Exception ex) { throw ex; }

            string JsResult;
            JsResult = JsonConvert.SerializeObject(dtDetails);
            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }