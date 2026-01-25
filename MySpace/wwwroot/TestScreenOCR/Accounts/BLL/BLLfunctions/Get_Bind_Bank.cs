        public static DataTable Get_Bind_Bank(int unit)
        {
            try
            {
                DataTable dtDetail;
                using (SqlConnection NewConnection = new SqlConnection(Connection.ConnectionString))
                {
                    NewConnection.Open();
                    using (SqlTransaction NewTransation = NewConnection.BeginTransaction())
                    {
                        try
                        {
                            dtDetail = DAL.Get_Bind_Bank(NewTransation, unit);
                            NewTransation.Commit();
                        }
                        catch (Exception ex)
                        {
                            if (NewTransation != null)
                                NewTransation.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (NewConnection != null && NewConnection.State == ConnectionState.Open)
                            {
                                NewConnection.Close();
                                NewConnection.Dispose();
                            }
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