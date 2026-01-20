        public static DataTable Get_Bind_Change_Type(SqlTransaction newTransaction)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_CHANGE_TYPE, dtDetails, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }