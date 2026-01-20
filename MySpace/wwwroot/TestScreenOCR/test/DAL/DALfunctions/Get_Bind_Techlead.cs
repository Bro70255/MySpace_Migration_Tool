        public static DataTable Get_Bind_Techlead(SqlTransaction newTransaction, int Firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Firm = new SqlParameter("@Firm", Firm);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_TECHLEAD, dtDetails, 0, par_Firm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }