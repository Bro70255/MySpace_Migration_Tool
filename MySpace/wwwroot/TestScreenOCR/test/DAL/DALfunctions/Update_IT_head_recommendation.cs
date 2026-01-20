        public static DataTable Update_IT_head_recommendation(SqlTransaction newTransaction, int IT_head, int EMP_CODE, string CRF_ID, string Remark)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_IT_head = new SqlParameter("@IT_head", IT_head);
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", CRF_ID);
                SqlParameter par_Remark = new SqlParameter("@Remark", Remark);



                SqlParameter[] parameters =
                {
                    par_IT_head,
                    par_EMP_CODE,
                    par_CRF_ID,
                    par_Remark

                };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.UPLOAD_IT_HEAD_RECOMMENDATION, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }