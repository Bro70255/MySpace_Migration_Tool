        public static DataTable Get_Crf_edit_dtls(SqlTransaction newTransaction, int emp_code)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter parEMP_CODE = new SqlParameter("@EMP_CODE", emp_code);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_CRF_DETAILS_FOR_USER, dtDetails, 0, parEMP_CODE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }