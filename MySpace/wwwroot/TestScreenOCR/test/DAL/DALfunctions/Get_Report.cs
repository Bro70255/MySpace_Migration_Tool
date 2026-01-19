
        public static DataTable Get_Report(SqlTransaction newTransaction)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                Sqlhelper.FillDatatable(newTransaction, CommandType.StoredProcedure, Storedprocedure.GET_REPORT, dtDetails, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }