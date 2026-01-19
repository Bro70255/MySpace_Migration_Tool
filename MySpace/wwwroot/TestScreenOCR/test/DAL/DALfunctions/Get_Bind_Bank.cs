        public static DataTable Get_Bind_Bank(SqlTransaction newTransation, int unit)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_bankName = new SqlParameter("@unitId", unit);
                Sqlhelper.FillDatatable(newTransation, CommandType.StoredProcedure, Storedprocedure.GET_BIND_BANK, dtDetails, 0, par_bankName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }