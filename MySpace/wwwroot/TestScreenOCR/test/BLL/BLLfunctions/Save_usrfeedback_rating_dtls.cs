        public static void Save_usrfeedback_rating_dtls(int User_Liveclose, int USER, string crf_ID, int ratingValue1, int ratingValue2, int ratingValue3, int ratingValue4, int ratingValue5, string Remark)
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

                            CRF_Tracker_dal.Save_usrfeedback_rating_dtls(NewTransation, User_Liveclose, USER, crf_ID, ratingValue1, ratingValue2, ratingValue3, ratingValue4, ratingValue5, Remark);
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