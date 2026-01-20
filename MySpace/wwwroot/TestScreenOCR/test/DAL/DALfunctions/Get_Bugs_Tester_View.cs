        public static DataTable Get_Bugs_Tester_View(SqlTransaction newTransaction, string crf_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_crf_ID = new SqlParameter("@crf_ID", crf_ID);
                SqlParameter[] parameters = {
                                par_crf_ID
                };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BUG_VIEW_TESTER_DTLS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }