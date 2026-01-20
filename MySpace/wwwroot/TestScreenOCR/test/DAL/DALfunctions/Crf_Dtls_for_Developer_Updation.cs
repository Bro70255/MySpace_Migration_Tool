        public static DataTable Crf_Dtls_for_Developer_Updation(SqlTransaction newTransaction, string crf_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", crf_ID);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_CRF_DTLS_FOR_DEVELOPER_UPDATION, dtDetails, 0, par_CRF_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }