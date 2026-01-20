        public static DataTable Get_Bind_CRF_for_Testing_TA(SqlTransaction newTransaction, int Testlead)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Testlead = new SqlParameter("@Testlead", Testlead);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_CRF_FOR_TESTING_TA, dtDetails, 0, par_Testlead);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }