        public static DataTable Get_CRF_for_Tester_Bug_Tracking(SqlTransaction newTransaction, int EMP_CODE)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);

                SqlParameter[] parameters = {

                                            par_EMP_CODE

                };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_CRF_FOR_TESTER_BUG_TRACKING, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }