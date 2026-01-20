
        public static DataTable Get_Bind_Department(SqlTransaction newTransaction, int unit)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_unit = new SqlParameter("@unit", unit);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_DEPARTMENT, dtDetails, 0, par_unit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }