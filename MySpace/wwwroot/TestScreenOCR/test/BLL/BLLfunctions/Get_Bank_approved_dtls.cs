        public static DataTable Get_Bank_approved_dtls(int usertype)
        {
            try
            {
                DataTable dtDetail;
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            dtDetail = DAL.Get_Bank_approved_dtls(transaction, usertype);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                return dtDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }