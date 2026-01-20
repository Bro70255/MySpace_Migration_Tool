        public static DataTable Update_Head_reject(SqlTransaction newtransaction, int HEAD_REJECT, int EMP_CODE, string crf_ID, string Remark)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_HEAD_REJECT = new SqlParameter("@head", HEAD_REJECT);
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", crf_ID);
                SqlParameter par_Remark = new SqlParameter("@Remark", Remark);

                SqlParameter[] parameters = {

                                            par_HEAD_REJECT,
                                            par_EMP_CODE,
                                            par_CRF_ID,
                                            par_Remark
                };

                SqlHelper.FillDatatable(newtransaction, CommandType.StoredProcedure, StoreProcedure.UPLOAD_HEAD_APPROVAL, dtDetails, 0, parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }