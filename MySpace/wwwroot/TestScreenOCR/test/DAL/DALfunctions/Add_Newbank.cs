        public static void Add_Newbank(SqlTransaction newTransaction, int unit, string bank)
        {
            try
            {
                SqlParameter par_unit = new SqlParameter("@unit", unit);
                SqlParameter par_bank = new SqlParameter("@bank", bank);



                SqlParameter[] parameters = {
                    par_unit,
                    par_bank

                };

                Sqlhelper.ExecuteNonQuery(newTransaction, CommandType.StoredProcedure, Storedprocedure.ADD_NEW_BANK, parameters);

                // Continue with the rest of your code...
            }
            catch (Exception ex)
            {
                throw ex;
                // Handle any exceptions here...
            }
        }