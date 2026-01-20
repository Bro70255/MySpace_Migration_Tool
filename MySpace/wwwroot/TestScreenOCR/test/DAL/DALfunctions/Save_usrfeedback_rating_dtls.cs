        public static DataTable Save_usrfeedback_rating_dtls(SqlTransaction newTransaction, int User_Liveclose, int USER, string crf_ID, int ratingValue1, int ratingValue2, int ratingValue3, int ratingValue4, int ratingValue5, string Remark)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter parUser_Liveclose = new SqlParameter("@User_Liveclose", User_Liveclose);
                SqlParameter parUSER = new SqlParameter("@USER", USER);
                SqlParameter parcrf_ID = new SqlParameter("@crf_ID", crf_ID);
                SqlParameter parratingValue1 = new SqlParameter("@ratingValue1", ratingValue1);
                SqlParameter parratingValue2 = new SqlParameter("@ratingValue2", ratingValue2);
                SqlParameter parratingValue3 = new SqlParameter("@ratingValue3", ratingValue3);
                SqlParameter parratingValue4 = new SqlParameter("@ratingValue4", ratingValue4);
                SqlParameter parratingValue5 = new SqlParameter("@ratingValue5", ratingValue5);
                SqlParameter parRemark = new SqlParameter("@Remark", Remark);

                SqlParameter[] parameters = {
                                              parUser_Liveclose,
                                              parUSER,
                                              parcrf_ID,
                                              parratingValue1,
                                              parratingValue2,
                                              parratingValue3,
                                              parratingValue4,
                                              parratingValue5,
                                              parRemark

            };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.SAVE_USRFEEDBACK_RATING_DTLS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }