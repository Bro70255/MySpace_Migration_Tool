        public static DataTable Get_bankaccount_crrection_dtls(SqlTransaction newTransaction, int ID)
        {
            DataTable dtDetails = new DataTable();
            SqlParameter par_vendorId = new SqlParameter("@ID", ID);

            try
            {
                Sqlhelper.FillDatatable(newTransaction, CommandType.StoredProcedure, Storedprocedure.GET_BANK_CORRECTION_DTLS, dtDetails, par_vendorId);
            }
            catch (Exception)
            {
                throw;
            }

            return dtDetails;
        }