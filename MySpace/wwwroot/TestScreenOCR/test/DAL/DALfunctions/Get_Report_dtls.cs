        public static DataTable Get_Report_dtls(SqlTransaction transaction, string bank, int unit)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                // Define the parameters for the stored procedure
                SqlParameter par_bankName = new SqlParameter("@bankName", bank);
                SqlParameter par_unitName = new SqlParameter("@unitName", unit);
                SqlParameter[] parameters = {
            par_bankName,
            par_unitName
        };

                // Pass the transaction and parameters array to the FillDatatable method
                Sqlhelper.FillDatatable(transaction, CommandType.StoredProcedure, Storedprocedure.GET_REPORT_DTLS, dtDetails, parameters);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
            return dtDetails;
        }