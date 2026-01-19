        public static void Insert_bank_account_correction_dtls(int correction_id, string acc_number, string Signature1, string Signature2, string Signature3, string Signature4, int Emp_Code,int Crrction_edit)
        {
            try
            {
                using (SqlConnection NewConnection = new SqlConnection(Connection.ConnectionString))
                {
                    NewConnection.Open();
                    using (SqlTransaction NewTransaction = NewConnection.BeginTransaction())
                    {
                        try
                        {
                            DAL.Insert_bank_account_correction_dtls(NewTransaction, correction_id, acc_number, Signature1, Signature2, Signature3, Signature4, Emp_Code, Crrction_edit);
                            NewTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            if (NewTransaction != null)
                                NewTransaction.Rollback();
                            throw; // Rethrow the exception for higher-level handling
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw; // Rethrow the exception for higher-level handling
            }
        }