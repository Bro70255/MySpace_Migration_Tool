        public static DataTable Bind_Crf_Id_For_User_Acceptance(SqlTransaction newTransaction, int User)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_User = new SqlParameter("@user_emp_code", User);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.BIND_CRF_ID_FOR_USER_ACCEPTANCE, dtDetails, 0, par_User);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }