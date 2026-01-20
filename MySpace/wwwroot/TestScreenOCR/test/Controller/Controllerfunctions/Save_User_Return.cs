        public JsonResult Save_User_Return(string selectedCrfId, string Remark)
        {
            try
            {
                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                int user_return = 23;
                CRF_Tracker_bll.Save_User_Return(selectedCrfId, Remark, EMP_CODE, user_return);
            }
            catch (Exception ex) { throw ex; }

            return Json(1);
        }