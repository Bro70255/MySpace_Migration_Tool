
        public JsonResult Sign_Up(SignUp_model Details)
        {
            try
            {
                CRF_Tracker_bll.Sign_Up(Details);

                return Json(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }