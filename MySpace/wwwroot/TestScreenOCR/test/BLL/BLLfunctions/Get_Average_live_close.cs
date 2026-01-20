        public static DataTable Get_Average_live_close(int firm, int Employee_Code, int UserType)
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
                            // Call DAL and retrieve the result into a DataTable
                            DataTable result = CRF_Tracker_dal.Get_Average_live_close(NewTransation, firm, Employee_Code, UserType);
                            NewTransation.Commit();

                            return result;
                        }
                        catch (Exception ex)
                        {
                            NewTransation.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }