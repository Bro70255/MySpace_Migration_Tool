        public static DataTable Get_Report_dtls(string bank, int unit)
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
                            dtDetail = DAL.Get_Report_dtls(transaction, bank, unit);
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