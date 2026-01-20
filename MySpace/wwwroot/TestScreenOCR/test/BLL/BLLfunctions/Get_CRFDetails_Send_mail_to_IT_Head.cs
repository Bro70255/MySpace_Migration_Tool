



        public static DataTable Get_CRFDetails_Send_mail_to_IT_Head(string CRF_ID)
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
                            dtDetail = CRF_Tracker_dal.Get_CRFDetails_Send_mail_to_IT_Head(NewTransation, CRF_ID);
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