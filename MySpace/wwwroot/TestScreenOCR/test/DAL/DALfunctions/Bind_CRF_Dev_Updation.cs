        public static DataTable Bind_CRF_Dev_Updation(SqlTransaction newTransaction, int EMP_CODE)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter parEMP_CODE = new SqlParameter("@Developer", EMP_CODE);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.BIND_CRFID_DEVLOPER_UPDATION, dtDetails, 0, parEMP_CODE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }