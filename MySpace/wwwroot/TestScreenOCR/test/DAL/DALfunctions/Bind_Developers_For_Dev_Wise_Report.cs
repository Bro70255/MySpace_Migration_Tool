        public static DataTable Bind_Developers_For_Dev_Wise_Report(SqlTransaction newTransaction, int Firm_Id)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Firm_Id = new SqlParameter("@Firm_Id", Firm_Id);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.BIND_DEVELOPERS_FOR_DEV_WISE_REPORT, dtDetails, 0, par_Firm_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }