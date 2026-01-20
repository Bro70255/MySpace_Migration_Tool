        public static DataTable Get_CRF_Flow_Of_User(SqlTransaction newTransaction, string CRF_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", CRF_ID);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_DETAILS_OF_CRF_STATUS_FLOW, dtDetails, 0, par_CRF_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }