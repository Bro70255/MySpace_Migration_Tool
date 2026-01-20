        public JsonResult Update_IT_head_reject(string crf_id, string Remark)
        {
            try
            {
                int IT_head_REJECT = 4;
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                string crf_ID = crf_id.Substring(0, 7);

                CRF_Tracker_bll.Update_IT_head_reject(IT_head_REJECT, EMP_CODE, crf_ID, Remark);

                return Json(0);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }