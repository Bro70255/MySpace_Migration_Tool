
        public static DataTable Get_Unit_Report_dtls(SqlTransaction transaction,int unit)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                
                SqlParameter par_unitName = new SqlParameter("@unitName", unit);
                SqlParameter[] parameters = {
            par_unitName
        };

                // Pass the transaction and parameters array to the FillDatatable method
                Sqlhelper.FillDatatable(transaction, CommandType.StoredProcedure, Storedprocedure.GET_UNIT_REPORT_DTLS, dtDetails, parameters);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
            return dtDetails;
        }