        public static DataTable Bind_Hold_CRF_Id_With_Subject(SqlTransaction newTransaction, int EMP_code)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                SqlParameter par_EMP_code = new SqlParameter("@EMP_code", EMP_code);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.BIND_HOLD_CRF_ID_WITH_SUBJECT, dtDetails, 0, par_EMP_code);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }