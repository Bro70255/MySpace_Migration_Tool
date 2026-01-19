        public static void Add_Newunit(SqlTransaction newTransaction, string unit)
        {
            try
            {
                SqlParameter par_category = new SqlParameter("@unit", unit);



                SqlParameter[] parameters = {
                    par_category

                };

                Sqlhelper.ExecuteNonQuery(newTransaction, CommandType.StoredProcedure, Storedprocedure.ADD_NEW_UNIT, parameters);

                // Continue with the rest of your code...
            }
            catch (Exception ex)
            {
                throw ex;
                // Handle any exceptions here...
            }
        }