        public static DataTable Get_Testing_ta_pnding_crfReport_testlead(SqlTransaction newTransaction, int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlParameter[] parameters = { par_firm };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_TESTING_TA_PNDINGREPORT_TESTLEAD, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }