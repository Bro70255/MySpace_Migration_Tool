        public static DataTable Bind_Crf_Id_for_Head_Approval(int firm)
        {
            try
            {
                DataTable dtDetails;
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    using (SqlTransaction NewTransaction = connection.BeginTransaction())
                    {
                        try
                        {
                            dtDetails = CRF_Tracker_dal.Bind_Crf_Id_for_Head_Approval(NewTransaction, firm);
                            NewTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            if (NewTransaction != null)
                                NewTransaction.Rollback();
                            throw ex; ;
                        }
                        finally
                        {
                            if (connection != null && connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                                connection.Dispose();
                            }
                        }
                    }
                }
                return dtDetails;
            }
            catch (Exception ex) { throw ex; }
        }