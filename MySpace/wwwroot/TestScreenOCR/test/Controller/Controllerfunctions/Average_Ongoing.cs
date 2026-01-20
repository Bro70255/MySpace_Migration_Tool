        public JsonResult Average_Ongoing(int firm)
        {
            try
            {
                int Employee_Code = Convert.ToInt32(Session["EMP_CODE"]);
                int UserType = Convert.ToInt32(Session["UserType"]);

                DataTable result = CRF_Tracker_bll.Average_Ongoing(firm, Employee_Code, UserType);

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