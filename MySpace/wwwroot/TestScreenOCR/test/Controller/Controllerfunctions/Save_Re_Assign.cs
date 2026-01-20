        public JsonResult Save_Re_Assign(string Tester_Bug_Report_ID)
        {
            try
            {
                int bugfix = 0;
                CRF_Tracker_bll.Save_Re_Assign(Tester_Bug_Report_ID, bugfix);
            }
            catch (Exception ex) { throw ex; }

            return Json(1);
        }