        public static DataTable Get_dtls_For_Email_HOD(SqlTransaction newTransaction, string id)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_CRF_ID = new SqlParameter("@CRF_ID", id);
               
                SqlParameter[] parameters =
                {

                    par_CRF_ID
                   
                };

                SqlHelper.FillDatatable(
                    newTransaction,
                    CommandType.StoredProcedure,
                    StoreProcedure.GET_DTLS_FOR_EMAIL_HOD, // This should resolve to "SP_Get_Email_For_CEO"
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