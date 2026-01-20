        public static DataTable Get_Bind_Testerstatus(SqlTransaction newTransaction)
        {
            DataTable dtDetails = new DataTable();
            try
            {


                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_TESTERSTATUS, dtDetails, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }