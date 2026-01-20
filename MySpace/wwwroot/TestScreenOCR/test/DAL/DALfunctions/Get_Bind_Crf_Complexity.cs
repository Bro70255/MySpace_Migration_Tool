        public static DataTable Get_Bind_Crf_Complexity(SqlTransaction newTransaction)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_CRF_COMPLEXITY, dtDetails, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }