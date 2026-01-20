


        public static DataTable Get_CRFDetails_Send_mail_to_IT_Head(SqlTransaction newTransaction, string CRF_ID)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_crfId = new SqlParameter("@crfId", CRF_ID);
              
                SqlParameter[] parameters = { par_crfId };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_CRFDETAILS_SEND_MAIL_TO_IT_HEAD, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }