        public static DataTable Get_Bind_Unit(SqlTransaction newTransaction, int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_UNIT, dtDetails, 0, par_firm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }