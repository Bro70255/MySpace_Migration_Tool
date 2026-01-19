        public static DataTable Login(SqlTransaction newTransaction, int employeeCode, string loginPassword)
        {
            try
            {
                SqlParameter par_employeeCode = new SqlParameter("@employeeCode", employeeCode);
                SqlParameter par_loginPassword = new SqlParameter("@password", loginPassword);

                SqlParameter[] parameters =
                {
                  par_employeeCode, par_loginPassword
                };

                //SqlHelper.ExecuteNonQuery(newTransaction, CommandType.StoredProcedure, StoreProcedure.CHECKING_EMPCODE_AND_PASSWORD, parameters);
                DataTable dtDetails = new DataTable();
                Sqlhelper.FillDatatable(newTransaction, CommandType.StoredProcedure, Storedprocedure.LOGINDETAILS, dtDetails, 0, parameters);
                return dtDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }