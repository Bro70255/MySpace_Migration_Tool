        public static void Save_Bug_Verify(SqlTransaction newTransation, string Tester_Bug_Report_ID, int bugfix)
        {
            try
            {
                SqlParameter parTester_Bug_Report_ID = new SqlParameter("@Tester_Bug_Report_ID", Tester_Bug_Report_ID);
                SqlParameter parbugfix = new SqlParameter("@bugfix", bugfix);

                SqlParameter[] parameters = {
                                  parTester_Bug_Report_ID,
                                  parbugfix


                };

                SqlHelper.ExecuteNonQuery(newTransation
                    , CommandType.StoredProcedure
                    , StoreProcedure.SAVE_BUG_VERIFY
                    , 0
                    , parameters
                    );

            }
            catch (Exception ex) { throw ex; }
        }