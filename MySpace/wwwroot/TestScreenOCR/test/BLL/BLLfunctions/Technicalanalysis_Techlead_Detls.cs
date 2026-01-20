        public static void Technicalanalysis_Techlead_Detls(Dictionary<string, string> Technicalanalysis_dtls, List<List<string>> Details, int EMP_CODE, int Techlead_Status)
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

                            CRF_Tracker_dal.Technicalanalysis_Techlead_Detls(NewTransation, Technicalanalysis_dtls, Details, EMP_CODE, Techlead_Status);
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