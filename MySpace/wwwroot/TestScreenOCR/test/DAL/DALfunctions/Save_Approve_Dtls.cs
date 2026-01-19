        public static void Save_Approve_Dtls(SqlTransaction newTransaction, int ID, string Account_num, string sign1, string sign2, string sign3, string sign4, int EMP_CODE, int Apprve_sts)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("DAL: Save_Approve_Dtls called.");

                SqlParameter parID = new SqlParameter("@ID", ID);
                SqlParameter parAccount_num = new SqlParameter("@Account_num", string.IsNullOrEmpty(Account_num) ? (object)DBNull.Value : Account_num);
                SqlParameter parSign1 = new SqlParameter("@sign1", string.IsNullOrEmpty(sign1) ? (object)DBNull.Value : sign1);
                SqlParameter parSign2 = new SqlParameter("@sign2", string.IsNullOrEmpty(sign2) ? (object)DBNull.Value : sign2);
                SqlParameter parSign3 = new SqlParameter("@sign3", string.IsNullOrEmpty(sign3) ? (object)DBNull.Value : sign3);
                SqlParameter parSign4 = new SqlParameter("@sign4", string.IsNullOrEmpty(sign4) ? (object)DBNull.Value : sign4);
                SqlParameter parEMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter parApprve_sts = new SqlParameter("@Apprve_sts", Apprve_sts);

                SqlParameter[] parameters = { parID, parAccount_num, parSign1, parSign2, parSign3, parSign4, parEMP_CODE, parApprve_sts };

                // Log for debugging
                System.Diagnostics.Debug.WriteLine("DAL: Parameters prepared.");

                Sqlhelper.ExecuteNonQuery(newTransaction, CommandType.StoredProcedure, Storedprocedure.SAVE_APPROVED_DTLS, 0, parameters);

                System.Diagnostics.Debug.WriteLine("DAL: Save_Approve_Dtls succeeded.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DAL Error: {ex.Message}");
                throw new Exception("DAL Error: " + ex.Message);
            }
        }