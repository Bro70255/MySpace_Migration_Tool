        public static void Save_Approve_Dtls(int ID, string Account_num, string sign1, string sign2, string sign3, string sign4, int EMP_CODE, int Apprve_sts)
        {
            try
            {
                using (SqlConnection NewConnection = new SqlConnection(Connection.ConnectionString))
                {
                    NewConnection.Open();
                    using (SqlTransaction NewTransaction = NewConnection.BeginTransaction())
                    {
                        try
                        {
                            System.Diagnostics.Debug.WriteLine("BLL: Starting transaction.");

                            // Call DAL to save the details
                            DAL.Save_Approve_Dtls(NewTransaction, ID, Account_num, sign1, sign2, sign3, sign4, EMP_CODE, Apprve_sts);

                            NewTransaction.Commit();
                            System.Diagnostics.Debug.WriteLine("BLL: Transaction committed.");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"BLL Error: {ex.Message}");

                            // Rollback on failure
                            if (NewTransaction != null)
                            {
                                NewTransaction.Rollback();
                                System.Diagnostics.Debug.WriteLine("BLL: Transaction rolled back.");
                            }

                            throw new Exception("BLL Transaction failed: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BLL Outer Error: {ex.Message}");
                throw new Exception("BLL Outer exception: " + ex.Message);
            }
        }