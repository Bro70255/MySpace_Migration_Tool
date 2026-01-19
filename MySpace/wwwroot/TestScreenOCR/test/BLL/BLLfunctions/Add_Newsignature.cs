        public static void Add_Newsignature(int created_by,int unitName,string bankName, decimal accountnum, string signature, string signatureName)
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
                            DAL.Add_Newsignature(NewTransaction,created_by ,unitName, bankName, accountnum, signature, signatureName);
                            NewTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            NewTransaction?.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (NewConnection.State == ConnectionState.Open)
                            {
                                NewConnection.Close();
                                NewConnection.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }