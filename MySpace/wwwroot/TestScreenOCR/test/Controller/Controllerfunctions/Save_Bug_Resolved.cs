        public JsonResult Save_Bug_Resolved(string Tester_Bug_Report_ID, string remark)
        {
            try
            {
                int bugfix = 1;
                CRF_Tracker_bll.Save_Bug_Resolved(Tester_Bug_Report_ID, bugfix, remark);
            }
            catch (Exception ex) { throw ex; }

            return Json(1);
        }