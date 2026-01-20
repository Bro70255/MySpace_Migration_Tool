        public static void update_Head_Approval(int Head, int EMP_CODE, string CRF_ID, string remark)
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
                            CRF_Tracker_dal.update_Head_Approval(NewTransaction, Head, EMP_CODE, CRF_ID, remark);
                            NewTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            if (NewTransaction != null)
                                NewTransaction.Rollback();

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
            }
            catch (Exception ex) { throw ex; }
        }