        public static void Return_To_User_Detls(SqlTransaction newTransaction, Dictionary<string, string> Returnuser_dtls, int EMP_CODE, int Techlead_Status)
        {
            try
            {
                string Crf_id = Returnuser_dtls["Crf_id"];
                string Remarks = Returnuser_dtls["Remarks"];

                {
                    SqlParameter parCrf_id = new SqlParameter("@Crf_id", Crf_id);
                    SqlParameter parRemarks = new SqlParameter("@Remarks", Remarks);
                    SqlParameter parEMP_CODE = new SqlParameter("@Techlead", EMP_CODE);
                    SqlParameter parTechlead_Status = new SqlParameter("@Techlead_Status", Techlead_Status);
                    SqlParameter[] detailParameters = { parCrf_id, parRemarks, parEMP_CODE, parTechlead_Status };

                    SqlHelper.ExecuteNonQuery(newTransaction
                        , CommandType.StoredProcedure
                        , StoreProcedure.INSERT_RETURN_DTLS
                        , detailParameters
                    );
                }
                // Continue with the rest of your code...
            }
            catch (Exception ex)
            {
                throw ex;
                // Handle any exceptions here...
            }
        }