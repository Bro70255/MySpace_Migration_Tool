        public static DataTable Get_User_Crf_list(SqlTransaction newTransaction, int EMP_CODE)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);

                SqlParameter[] parameters = {

                                            par_EMP_CODE

                };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_USER_CRF_LIST, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }