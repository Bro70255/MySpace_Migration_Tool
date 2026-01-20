        public static DataTable Crf_dtls_for_userfeedback(SqlTransaction newTransaction, string crf_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", crf_ID);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_CRF_DETAILS_FOR_USERFEEDBACK_DTLS, dtDetails, 0, par_CRF_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }