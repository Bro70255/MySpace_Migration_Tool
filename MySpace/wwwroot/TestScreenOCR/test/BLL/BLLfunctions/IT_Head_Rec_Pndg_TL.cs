        public static DataTable IT_Head_Rec_Pndg_TL(int firm, int Team_id)
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
                            dtDetail = CRF_Tracker_dal.IT_Head_Rec_Pndg_TL(NewTransation, firm, Team_id);
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