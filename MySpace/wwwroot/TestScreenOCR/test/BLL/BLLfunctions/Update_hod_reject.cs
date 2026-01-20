        public static void Update_hod_reject(int HOD_REJECT, int EMP_CODE, string crf_ID, string Remark)
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
                            CRF_Tracker_dal.Update_hod_reject(NewTransation, HOD_REJECT, EMP_CODE, crf_ID, Remark);
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