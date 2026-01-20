        public static void Save_Developer_Change_dtls(SqlTransaction newTransaction, int Developer, string Crf_id, int New_Developer)
        {
            try
            {
                SqlParameter par_Developer = new SqlParameter("@Developer", Developer);
                SqlParameter par_Crf_id = new SqlParameter("@Crf_id", Crf_id);
                SqlParameter par_New_Developer = new SqlParameter("@New_Developer", New_Developer);

                SqlParameter[] parameters = {
            par_Developer,
            par_Crf_id,
            par_New_Developer
        };
                SqlHelper.ExecuteNonQuery(newTransaction
       , CommandType.StoredProcedure
       , StoreProcedure.SAVE_DEVELOPER_CHANGE_DTLS
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