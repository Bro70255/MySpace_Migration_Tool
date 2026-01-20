        public static void Save_Live_Publish(SqlTransaction newTransation, string selectedCrfId, DateTime publish_date, string Remark, int EMP_CODE, int Live_published)
        {
            try
            {
                SqlParameter parselectedCrfId = new SqlParameter("@selectedCrfId", selectedCrfId);
                SqlParameter parpublish_date = new SqlParameter("@publish_date", publish_date);
                SqlParameter parRemark = new SqlParameter("@Remark", Remark);
                SqlParameter parEMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter parLive_published = new SqlParameter("@Live_published", Live_published);
              

                SqlParameter[] parameters = {
                                  parselectedCrfId,
                                  parpublish_date,
                                  parRemark,
                                  parEMP_CODE,
                                  parLive_published

                };

                SqlHelper.ExecuteNonQuery(newTransation
                    , CommandType.StoredProcedure
                    , StoreProcedure.SAVE_LIVE_PUBLISH
                    , 0
                    , parameters
                    );

            }
            catch (Exception ex) { throw ex; }
        }