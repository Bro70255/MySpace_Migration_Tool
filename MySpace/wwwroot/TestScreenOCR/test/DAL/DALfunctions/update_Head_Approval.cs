        public static DataTable update_Head_Approval(SqlTransaction NewTransaction, int Head, int EMP_CODE, string CRF_ID, string remark)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Head = new SqlParameter("@head", Head);
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", CRF_ID);
                SqlParameter par_Remark = new SqlParameter("@Remark", remark);

                SqlParameter[] parameters =
                {
                    par_Head,
                    par_EMP_CODE,
                    par_CRF_ID,
                    par_Remark

                };
                SqlHelper.FillDatatable(NewTransaction, CommandType.StoredProcedure, StoreProcedure.UPLOAD_HEAD_APPROVAL, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }