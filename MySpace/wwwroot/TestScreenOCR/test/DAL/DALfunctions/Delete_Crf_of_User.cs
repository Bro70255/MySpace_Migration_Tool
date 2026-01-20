        public static void Delete_Crf_of_User(SqlTransaction newTransation, string crfId, string remarks, int user_crf_delete)
        {
            try
            {
                SqlParameter parcrfId = new SqlParameter("@crfId", crfId);
                SqlParameter paruser_crf_delete = new SqlParameter("@user_crf_delete", user_crf_delete);
                SqlParameter parRemark = new SqlParameter("@Remark", remarks);

                SqlParameter[] parameters = {
                                  parcrfId,
                                  paruser_crf_delete,
                                  parRemark


                };

                SqlHelper.ExecuteNonQuery(newTransation
                    , CommandType.StoredProcedure
                    , StoreProcedure.DELETE_CRF_OF_USER
                    , 0
                    , parameters
                    );

            }
            catch (Exception ex) { throw ex; }
        }