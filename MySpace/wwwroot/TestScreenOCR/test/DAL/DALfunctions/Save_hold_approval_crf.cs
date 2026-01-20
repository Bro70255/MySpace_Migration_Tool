        public static DataTable Save_hold_approval_crf(SqlTransaction newTransaction, int EMP_CODE, string crf_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", crf_ID);


                SqlParameter[] parameters = {
                                               par_EMP_CODE,
                                               par_CRF_ID

            };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.SAVE_HOLD_APPROVAL_CRF, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }