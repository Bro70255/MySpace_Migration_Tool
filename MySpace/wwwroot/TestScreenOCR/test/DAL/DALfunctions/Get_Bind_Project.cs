        public static DataTable Get_Bind_Project(SqlTransaction newTransaction, int impactmodule)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_impactmodule = new SqlParameter("@impactmodule", impactmodule);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_PROJECT, dtDetails, 0, par_impactmodule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }