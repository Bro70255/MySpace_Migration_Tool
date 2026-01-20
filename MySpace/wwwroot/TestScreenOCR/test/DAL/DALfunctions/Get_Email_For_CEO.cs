
        public static DataTable Get_Email_For_CEO(SqlTransaction newTransaction, string CRF_ID, int FIRM)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", CRF_ID);
                //SqlParameter par_CEO = new SqlParameter("@CEO", CEO);

                SqlParameter par_FIRM = new SqlParameter("@Firm ", FIRM);

                SqlParameter[] parameters =
                {

                    //par_EMP_CODE,
                    par_CRF_ID,
                    //par_CEO,
                    par_FIRM
                };

                SqlHelper.FillDatatable(
                    newTransaction,
                    CommandType.StoredProcedure,
                    StoreProcedure.GET_EMAIL_FOR_CEO, // This should resolve to "SP_Get_Email_For_CEO"
                    dtDetails,
                    0,
                    parameters
                );
            }
            catch (Exception ex)
            {
                throw;
            }

            return dtDetails;
        }