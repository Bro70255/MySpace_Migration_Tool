        public static DataTable Get_Bind_Developers_for_Dailywork_report(SqlTransaction newTransaction, int Firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Firm_Id = new SqlParameter("@Firm_Id", Firm);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_DEVELOPERS_FOR_DAILYWORK_REPORT, dtDetails, 0, par_Firm_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }