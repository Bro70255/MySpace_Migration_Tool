        public static DataTable Update_IT_head_reject(SqlTransaction newtransaction, int IT_head_REJECT, int EMP_CODE, string crf_ID, string Remark)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_IT_head_REJECT = new SqlParameter("@IT_head", IT_head_REJECT);
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", crf_ID);
                SqlParameter par_Remark = new SqlParameter("@Remark", Remark);

                SqlParameter[] parameters = {

                                            par_IT_head_REJECT,
                                            par_EMP_CODE,
                                            par_CRF_ID,
                                            par_Remark
                };

                SqlHelper.FillDatatable(newtransaction, CommandType.StoredProcedure, StoreProcedure.UPLOAD_IT_HEAD_RECOMMENDATION, dtDetails, 0, parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }