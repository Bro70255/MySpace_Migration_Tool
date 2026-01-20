        public static void Technicalanalysis_Testlead_Detls(Dictionary<string, string> Technicalanalysis_Techleaddtls, List<List<string>> Detail, int EMP_CODE, int Testlead_Status)
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

                            CRF_Tracker_dal.Technicalanalysis_Testlead_Detls(NewTransation, Technicalanalysis_Techleaddtls, Detail, EMP_CODE, Testlead_Status);
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