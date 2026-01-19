        public static DataTable Duplicate_unit(SqlTransaction newTransaction, string unit)
        {
            try
            {
                SqlParameter par_unit = new SqlParameter("@unit", unit);


                SqlParameter[] parameters =
                {
                  par_unit
                };

                //SqlHelper.ExecuteNonQuery(newTransaction, CommandType.StoredProcedure, StoreProcedure.CHECKING_EMPCODE_AND_PASSWORD, parameters);
                DataTable dtDetails = new DataTable();
                Sqlhelper.FillDatatable(newTransaction, CommandType.StoredProcedure, Storedprocedure.CHECKING_DUPLICATE_UNIT, dtDetails, 0, parameters);
                return dtDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }