        public JsonResult Get_Average_live_close(int firm)
        {
            try
            {
                int employeeCode = Convert.ToInt32(Session["EMP_CODE"]);
                int userType = Convert.ToInt32(Session["UserType"]);

                DataTable result = CRF_Tracker_bll.Get_Average_live_close(firm, employeeCode, userType);

                if (result.Rows.Count > 0)
                {
                    double average = Convert.ToDouble(result.Rows[0]["AverageDayDifference"]);
                    return Json(new { success = true, average = average }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No Data Found" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }