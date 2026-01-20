        public static DataTable Get_Bind_CRF_for_Tester(SqlTransaction newTransaction, int Tester)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Tester = new SqlParameter("@Tester", Tester);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_CRF_FOR_TESTER, dtDetails, 0, par_Tester);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }