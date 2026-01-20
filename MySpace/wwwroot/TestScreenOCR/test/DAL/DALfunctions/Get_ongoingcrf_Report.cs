        public static DataTable Get_ongoingcrf_Report(SqlTransaction newTransaction, int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);

                SqlParameter[] parameters = { par_firm };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_ONGOINGCRF_REPORT_HEAD, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }