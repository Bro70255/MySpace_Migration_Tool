        public static DataTable Get_HOD_Recommendation_Pending_Crf_To_Remind(SqlTransaction newTransaction)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_HOD_RECOMMENDATION_PENDING_CRF_TO_REMIND, dtDetails, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }