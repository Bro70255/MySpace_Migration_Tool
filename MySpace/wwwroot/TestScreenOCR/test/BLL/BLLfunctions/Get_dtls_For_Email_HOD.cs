        public static DataTable Get_dtls_For_Email_HOD(string id)
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
                            DataTable dt = CRF_Tracker_dal.Get_dtls_For_Email_HOD(NewTransaction, id);
                            NewTransaction.Commit();
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            if (NewTransaction != null)
                                NewTransaction.Rollback();
                            throw;
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }