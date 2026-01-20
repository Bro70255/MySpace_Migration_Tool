        public static DataTable Get_Attachment_Files(SqlTransaction newTransaction, string CRF_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", CRF_ID);

                SqlParameter[] parameters = {
                 par_CRF_ID
                };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_ATTACHMENT_FILES, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }