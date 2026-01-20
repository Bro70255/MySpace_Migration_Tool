        public static DataTable Bind_CRF_Id_for_IT_head(int firm)
        {
            try
            {
                DataTable dtDetails;
                using (SqlConnection Newconnection = new SqlConnection(Connection.ConnectionString))
                {
                    Newconnection.Open();
                    using (SqlTransaction NewTransaction = Newconnection.BeginTransaction())
                    {

                        try
                        {
                            dtDetails = CRF_Tracker_dal.Bind_CRF_Id_for_IT_head(NewTransaction, firm);
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
                            if (Newconnection != null && Newconnection.State == ConnectionState.Open)
                            {
                                Newconnection.Close();
                                Newconnection.Dispose();
                            }
                        }

                    }

                }
                return dtDetails;
            }
            catch (Exception ex) { throw ex; }
        }