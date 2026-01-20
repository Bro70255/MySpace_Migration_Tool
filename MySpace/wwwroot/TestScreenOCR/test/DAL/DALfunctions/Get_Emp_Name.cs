        public static DataTable Get_Emp_Name(SqlTransaction newTransaction, int EMP_code)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_code);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_EMP_NAME, dtDetails, 0, par_EMP_CODE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }