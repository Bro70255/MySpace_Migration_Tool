        public static DataTable Insert_Tester_dtls(SqlTransaction newTransaction, string crf_ID, int status, string Remark, int Tester)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_crf_ID = new SqlParameter("@crf_ID", crf_ID);
                SqlParameter par_status = new SqlParameter("@status", status);
                SqlParameter par_Remark = new SqlParameter("@Remark", Remark);
                SqlParameter par_Tester = new SqlParameter("@Tester", Tester);

                SqlParameter[] parameters = {
                                               par_crf_ID,
                                               par_status,
                                               par_Remark,
                                               par_Tester

            };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.INSERT_TESTER_DETAILS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }