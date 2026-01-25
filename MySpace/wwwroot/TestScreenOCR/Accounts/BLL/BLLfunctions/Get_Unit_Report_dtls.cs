
        public static DataTable Get_Unit_Report_dtls(int unit)
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
                            dtDetail = DAL.Get_Unit_Report_dtls(transaction,unit);
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