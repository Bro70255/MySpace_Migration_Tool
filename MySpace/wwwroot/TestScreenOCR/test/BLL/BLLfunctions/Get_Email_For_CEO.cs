        public static DataTable Get_Email_For_CEO(string CRF_ID, int FIRM)
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
                            DataTable dt = CRF_Tracker_dal.Get_Email_For_CEO(NewTransaction, CRF_ID, FIRM);
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