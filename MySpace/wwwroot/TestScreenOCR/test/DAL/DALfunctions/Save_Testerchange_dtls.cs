        public static void Save_Testerchange_dtls(SqlTransaction newTransation, int testerid, string crf, int new_tester)
        {
            try
            {
                SqlParameter partesterid = new SqlParameter("@testerid", testerid);
                SqlParameter parcrf = new SqlParameter("@crf", crf);
                SqlParameter parnew_tester = new SqlParameter("@new_tester", new_tester);

                SqlParameter[] parameters = {
                                  partesterid,
                                  parcrf,
                                  parnew_tester


                };

                SqlHelper.ExecuteNonQuery(newTransation
                    , CommandType.StoredProcedure
                    , StoreProcedure.SAVE_TESTERCHANGE_DTLS
                    , 0
                    , parameters
                    );

            }
            catch (Exception ex) { throw ex; }
        }