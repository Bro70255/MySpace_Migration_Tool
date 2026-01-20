        public static void Save_Assit_or_techlead_verification(int DWU_ID, int EMP_CODE, int A_TL)
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

                            CRF_Tracker_dal.Save_Assit_or_techlead_verification(NewTransation, DWU_ID, EMP_CODE, A_TL);
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