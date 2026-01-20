        public static DataTable Bind_Firm_For_Developer_Wise_Report(SqlTransaction newTransaction, int EMP_CODE)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_EMP_CODE = new SqlParameter("@Employee_Code", EMP_CODE);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_FIRM_FOR_DEVELOPER_WISE_REPORT, dtDetails, 0, par_EMP_CODE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }