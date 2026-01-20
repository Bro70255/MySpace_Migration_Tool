        public static DataTable Get_CRF_Attachments(SqlTransaction newTransaction, string crf_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                SqlParameter par_crf_id = new SqlParameter("@crf_id", crf_ID);

                SqlParameter[] parameters = {
                 par_crf_id
                };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_CRF_ATTACHMENTS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }