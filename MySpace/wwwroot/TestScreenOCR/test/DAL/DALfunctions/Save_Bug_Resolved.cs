        public static void Save_Bug_Resolved(SqlTransaction newTransation, string Tester_Bug_Report_ID, int bugfix, string remark)
        {
            try
            {
                SqlParameter parTester_Bug_Report_ID = new SqlParameter("@Tester_Bug_Report_ID", Tester_Bug_Report_ID);
                SqlParameter parbugfix = new SqlParameter("@bugfix", bugfix);
                SqlParameter parRemark = new SqlParameter("@Remark", remark);
                SqlParameter[] parameters = {
                                  parTester_Bug_Report_ID,
                                  parbugfix,
                                  parRemark

                };

                SqlHelper.ExecuteNonQuery(newTransation
                    , CommandType.StoredProcedure
                    , StoreProcedure.BUG_RESOLVED_DTLS
                    , 0
                    , parameters
                    );

            }
            catch (Exception ex) { throw ex; }
        }