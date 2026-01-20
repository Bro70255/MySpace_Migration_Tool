        public static DataTable Get_Bind_Technicalanalysis_Techlead(SqlTransaction newTransaction, int Techlead)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Techlead = new SqlParameter("@Techlead", Techlead);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_TECHNICALANALYSIS_TECHLEAD, dtDetails, 0, par_Techlead);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }