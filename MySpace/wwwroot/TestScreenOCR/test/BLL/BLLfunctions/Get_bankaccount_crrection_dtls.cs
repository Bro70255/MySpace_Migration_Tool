        public static DataTable Get_bankaccount_crrection_dtls(int ID)
        {
            DataTable dtDetail;

            using (SqlConnection NewConnection = new SqlConnection(Connection.ConnectionString))
            {
                NewConnection.Open();
                using (SqlTransaction NewTransaction = NewConnection.BeginTransaction())
                {
                    try
                    {
                        dtDetail = DAL.Get_bankaccount_crrection_dtls(NewTransaction, ID); // Pass the ID
                        NewTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        NewTransaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (NewConnection.State == ConnectionState.Open)
                        {
                            NewConnection.Close();
                        }
                    }
                }
            }

            return dtDetail;
        }