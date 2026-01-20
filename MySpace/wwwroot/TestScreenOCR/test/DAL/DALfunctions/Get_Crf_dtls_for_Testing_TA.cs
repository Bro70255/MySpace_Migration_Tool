        public static DataTable Get_Crf_dtls_for_Testing_TA(SqlTransaction newTransaction, string crf_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", crf_ID);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_CRF_DTLS_FOR_TESTING_TA, dtDetails, 0, par_CRF_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }