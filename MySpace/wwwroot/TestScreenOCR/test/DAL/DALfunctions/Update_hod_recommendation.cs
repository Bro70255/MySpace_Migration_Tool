        public static DataTable Update_hod_recommendation(SqlTransaction newTransaction, int HOD, int EMP_CODE, string crf_ID, string Remark)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_HOD = new SqlParameter("@HOD", HOD);
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", crf_ID);
                SqlParameter par_Remark = new SqlParameter("@Remark", Remark);

                SqlParameter[] parameters = {
                                               par_HOD,
                                               par_EMP_CODE,
                                               par_CRF_ID,
                                               par_Remark

            };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.UPLOAD_HOD_RECOMMENDATION, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }