        public static void Add_Newsignature(SqlTransaction newTransaction,int created_by, int unitName, string bankName, decimal accountnum, string signature, string signatureName)
        {
            try
            {
                SqlParameter par_created_by = new SqlParameter("@created_by", created_by);
                SqlParameter par_unitName = new SqlParameter("@unitName", unitName);
                SqlParameter par_bankName = new SqlParameter("@bankName", bankName);
                SqlParameter par_accountnum = new SqlParameter("@accountnum", accountnum);
                SqlParameter par_signature = new SqlParameter("@signature", signature);
                SqlParameter par_signatureName = new SqlParameter("@signatureName", signatureName);

                SqlParameter[] parameters = { par_created_by,par_unitName, par_bankName, par_accountnum, par_signature, par_signatureName };

                Sqlhelper.ExecuteNonQuery(newTransaction, CommandType.StoredProcedure, Storedprocedure.ADD_NEW_SIGNATURE, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }