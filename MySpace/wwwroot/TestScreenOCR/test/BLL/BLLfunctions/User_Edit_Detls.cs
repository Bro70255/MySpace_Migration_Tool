        public static void User_Edit_Detls(Returnusr_dtls return_details, int EMP_CODE)
        {
            try
            {

                using (SqlConnection NewConnection = new SqlConnection(Connection.ConnectionString))
                {
                    NewConnection.Open();
                    using (SqlTransaction NewTransation = NewConnection.BeginTransaction())
                    {
                        try
                        {

                            CRF_Tracker_dal.User_Edit_Detls(NewTransation, return_details, EMP_CODE);
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

            }
            catch (Exception ex) { throw ex; }
        }