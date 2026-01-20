        public static DataTable Save_Userfeedback_dtls(SqlTransaction newTransaction, int user_feedback, string crf_ID, string Link, string Path, int developer, string Remark)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_user_feedback = new SqlParameter("@user_feedback", user_feedback);
                SqlParameter par_crf_ID = new SqlParameter("@crf_ID", crf_ID);
                SqlParameter par_Link = new SqlParameter("@Link", Link);
                SqlParameter par_Path = new SqlParameter("@Path", Path);
                SqlParameter par_developer = new SqlParameter("@developer", developer);
                SqlParameter par_Remark = new SqlParameter("@Remark", Remark);

                SqlParameter[] parameters = {
                                               par_user_feedback,
                                               par_crf_ID,
                                               par_Link,
                                               par_Path,
                                               par_developer,
                                               par_Remark

            };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.SAVE_DEV_USRFEEDBACK_DTLS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }