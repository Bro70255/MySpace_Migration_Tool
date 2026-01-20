        public static string Crf_Detls(Crfdtls Crf_Details, int EMP_CODE)
        {
            try
            {
                string crfId;
                using (SqlConnection NewConnection = new SqlConnection(Connection.ConnectionString))
                {
                    NewConnection.Open();
                    using (SqlTransaction NewTransaction = NewConnection.BeginTransaction())
                    {
                        try
                        {
                            crfId = CRF_Tracker_dal.Crf_Detls(NewTransaction, Crf_Details, EMP_CODE);
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
                return crfId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }