        public static void Update_IT_head_recommendation(int IT_head, int EMP_CODE, string CRF_ID, string Remark)
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
                            CRF_Tracker_dal.Update_IT_head_recommendation(NewTransaction, IT_head, EMP_CODE, CRF_ID, Remark);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }