        public JsonResult Save_User_Acceptance(string selectedCrfId, string Remark)
        {
            try
            {
                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                int user_acceptance = 16;
                CRF_Tracker_bll.Save_User_Acceptance(selectedCrfId, Remark, EMP_CODE, user_acceptance);
            }
            catch (Exception ex) { throw ex; }

            return Json(1);
        }