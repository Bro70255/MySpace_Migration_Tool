        public static DataTable Get_Bind_CRF_for_dev_usrfeedback(SqlTransaction newTransaction, int developer)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_developer = new SqlParameter("@developer", developer);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_CRF_FOR_DEV_USRFEEDBACK, dtDetails, 0, par_developer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }