        public static DataTable Bind_CRF_Id_for_IT_head(SqlTransaction newTransaction, int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.BIND_CRF_ID_WITH_SUBJECT_FOR_IT_HEAD, dtDetails, 0, par_firm);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }