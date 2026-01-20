        public JsonResult Update_hod_recommendation(string crf_id, string Remark)
        {
            try
            {
                int HOD = 1;
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                string crf_ID = crf_id.Substring(0, 7);

                CRF_Tracker_bll.Update_hod_recommendation(HOD, EMP_CODE, crf_ID, Remark);

                return Json(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }