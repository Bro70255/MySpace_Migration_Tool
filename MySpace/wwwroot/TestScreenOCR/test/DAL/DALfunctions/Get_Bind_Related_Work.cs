        public static DataTable Get_Bind_Related_Work(SqlTransaction newTransaction, int change_type)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_change_type = new SqlParameter("@change_type", change_type);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_RELATED_WORK, dtDetails, 0, par_change_type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }