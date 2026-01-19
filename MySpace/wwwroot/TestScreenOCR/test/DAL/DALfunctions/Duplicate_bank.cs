        public static DataTable Duplicate_bank(SqlTransaction newTransaction, string bank)
        {
            try
            {
                SqlParameter par_unit = new SqlParameter("@bank", bank);


                SqlParameter[] parameters =
                {
                  par_unit
                };

                //SqlHelper.ExecuteNonQuery(newTransaction, CommandType.StoredProcedure, StoreProcedure.CHECKING_EMPCODE_AND_PASSWORD, parameters);
                DataTable dtDetails = new DataTable();
                Sqlhelper.FillDatatable(newTransaction, CommandType.StoredProcedure, Storedprocedure.CHECKING_DUPLICATE_BANK, dtDetails, 0, parameters);
                return dtDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }