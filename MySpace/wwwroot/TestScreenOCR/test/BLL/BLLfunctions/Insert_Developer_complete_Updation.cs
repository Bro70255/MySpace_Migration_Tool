        public static void Insert_Developer_complete_Updation(string crf_ID, int status, string Remark, string module_name, string Tfs_name, string Uat_link, string Uat_path, int Developer)
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

                            CRF_Tracker_dal.Insert_Developer_complete_Updation(NewTransation, crf_ID, status, Remark, module_name, Tfs_name, Uat_link, Uat_path, Developer);
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