        public static DataTable Get_Details(SqlTransaction newTransaction)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                Sqlhelper.FillDatatable(newTransaction, CommandType.StoredProcedure, Storedprocedure.GET_DETAILS, dtDetails, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }