        public static DataTable Get_attached_file(String crfId)
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
                            dtDetail = CRF_Tracker_dal.Get_attached_file(NewTransation, crfId);
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
            catch (Exception ex) { throw ex; }
        }