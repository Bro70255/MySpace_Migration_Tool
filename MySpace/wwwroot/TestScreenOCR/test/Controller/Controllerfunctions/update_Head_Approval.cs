        public JsonResult update_Head_Approval(string crf_id, string remark)
        {
            try
            {
                int Head = 5;

                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                string CRF_ID = crf_id.Substring(0, 7);

                CRF_Tracker_bll.update_Head_Approval(Head, EMP_CODE, CRF_ID, remark);

                return Json(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }