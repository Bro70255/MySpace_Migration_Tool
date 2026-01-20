
        public static DataTable Get_TestCase_Updation_Report(SqlTransaction newTransaction, DateTime startdate, DateTime enddate, int EMP_CODE, int UserType, int FIRM)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Startdate = new SqlParameter("@Startdate", startdate);
                SqlParameter par_Enddate = new SqlParameter("@Enddate", enddate);
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_UserType = new SqlParameter("@UserType", UserType);
                SqlParameter par_FIRM = new SqlParameter("@FIRM", FIRM);

                SqlParameter[] parameters = { par_Startdate, par_Enddate, par_EMP_CODE, par_UserType, par_FIRM };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_TEST_CASE_REPORT, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }