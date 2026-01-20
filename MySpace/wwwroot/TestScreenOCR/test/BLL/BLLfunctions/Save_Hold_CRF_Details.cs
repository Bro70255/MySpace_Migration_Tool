        public static DataTable Save_Hold_CRF_Details(string CRFid, DateTime Hold_from, DateTime Hold_end, string Reason, string Remark, int Employee_Code)
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

                            dtDetail = CRF_Tracker_dal.Save_Hold_CRF_Details(NewTransation, CRFid, Hold_from, Hold_end, Reason, Remark, Employee_Code);
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