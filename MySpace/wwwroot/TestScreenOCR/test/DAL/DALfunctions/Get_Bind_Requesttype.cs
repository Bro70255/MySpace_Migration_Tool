        public static DataTable Get_Bind_Requesttype(SqlTransaction newTransaction)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_REQUESTTYPE, dtDetails, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }