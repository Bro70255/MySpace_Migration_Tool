        public static DataTable Get_Bind_CRF_for_userfeedback(SqlTransaction newTransaction, int user)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_user = new SqlParameter("@user", user);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_CRF_FOR_USRFEEDBACK, dtDetails, 0, par_user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }