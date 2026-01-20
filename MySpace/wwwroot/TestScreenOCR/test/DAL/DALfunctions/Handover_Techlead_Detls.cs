        public static void Handover_Techlead_Detls(SqlTransaction newTransaction, Dictionary<string, string> Handover_dtls, int EMP_CODE, int Techlead_Status)
        {
            try
            {
                string Crf_id = Handover_dtls["Crf_id"];
                string Remarks = Handover_dtls["Remarks"];
                string Techlead2 = Handover_dtls["Techlead2"];
                {
                    SqlParameter parCrf_id = new SqlParameter("@Crf_id", Crf_id);
                    SqlParameter parRemarks = new SqlParameter("@Remarks", Remarks);
                    SqlParameter parTechlead2 = new SqlParameter("@Techlead2", Techlead2);
                    SqlParameter parEMP_CODE = new SqlParameter("@Techlead", EMP_CODE);
                    SqlParameter parTechlead_Status = new SqlParameter("@Techlead_Status", Techlead_Status);
                    SqlParameter[] detailParameters = { parCrf_id, parRemarks, parTechlead2, parEMP_CODE, parTechlead_Status };

                    SqlHelper.ExecuteNonQuery(newTransaction
                        , CommandType.StoredProcedure
                        , StoreProcedure.INSERT_TECHLEAD_HANDOVER_DTLS
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