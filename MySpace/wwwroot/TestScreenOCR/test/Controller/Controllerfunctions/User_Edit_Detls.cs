        public JsonResult User_Edit_Detls(Returnusr_dtls return_details)
        {
            try
            {
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                CRF_Tracker_bll.User_Edit_Detls(return_details, EMP_CODE);

                return Json(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }