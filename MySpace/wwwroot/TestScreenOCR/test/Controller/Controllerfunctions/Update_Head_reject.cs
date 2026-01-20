        public JsonResult Update_Head_reject(string crf_id, string Remark)
        {
            try
            {
                int HEAD_REJECT = 6;
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                string crf_ID = crf_id.Substring(0, 7);
                CRF_Tracker_bll.Update_Head_reject(HEAD_REJECT, EMP_CODE, crf_ID, Remark);
                return Json(0);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }