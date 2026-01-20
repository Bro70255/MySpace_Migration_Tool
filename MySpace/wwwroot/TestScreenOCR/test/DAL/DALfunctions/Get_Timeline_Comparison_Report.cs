        public static DataTable Get_Timeline_Comparison_Report(SqlTransaction newTransaction, int firm, string financial_year)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlParameter par_financial_year = new SqlParameter("@financial_year", financial_year);

                SqlParameter[] parameters = { par_firm, par_financial_year };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_TIMELINE_COMPARISON_REPORT, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }