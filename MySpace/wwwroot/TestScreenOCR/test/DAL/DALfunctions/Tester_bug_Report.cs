        public static void Tester_bug_Report(SqlTransaction newTransaction, Tester_Bug_Reported_Dtls_ Tester_Bug_Reported_Dtls_, int EMP_CODE, string crf_ID)
        {
            try
            {
                SqlParameter parTracker = new SqlParameter("@Tracker", Tester_Bug_Reported_Dtls_.Tracker);
                SqlParameter parcrf_ID = new SqlParameter("@crf_ID", crf_ID);
                SqlParameter parsubject = new SqlParameter("@Subject", Tester_Bug_Reported_Dtls_.subject);
                SqlParameter parDescription = new SqlParameter("@Description", Tester_Bug_Reported_Dtls_.Description);
                SqlParameter parseverity = new SqlParameter("@severity", Tester_Bug_Reported_Dtls_.severity);
                SqlParameter parpriority = new SqlParameter("@priority", Tester_Bug_Reported_Dtls_.priority);
                SqlParameter parEnvironment = new SqlParameter("@Environment", Tester_Bug_Reported_Dtls_.Environment);
                SqlParameter parDeveloper = new SqlParameter("@Developer", Tester_Bug_Reported_Dtls_.Developer);
                SqlParameter parAttach_file = new SqlParameter("@Attach_file", Tester_Bug_Reported_Dtls_.Attach_file);
                SqlParameter parEMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter[] parameters = {
            parTracker,
            parcrf_ID,
            parsubject,
            parDescription,
            parseverity,
            parpriority,
            parEnvironment,
            parDeveloper,
            parAttach_file,
            parEMP_CODE
        };
                SqlHelper.ExecuteNonQuery(newTransaction
       , CommandType.StoredProcedure
       , StoreProcedure.SAVE_TESTER_BUG_REPORT_DTLS
       , parameters
       );
                // Continue with the rest of your code...
            }
            catch (Exception ex)
            {
                throw ex;
                // Handle any exceptions here...
            }
        }