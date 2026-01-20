        public static void Save_User_Acceptance(SqlTransaction newTransation, string selectedCrfId, string Remark, int EMP_CODE, int user_acceptance)
        {
            try
            {
                SqlParameter parselectedCrfId = new SqlParameter("@selectedCrfId", selectedCrfId);
                SqlParameter paruser_acceptance = new SqlParameter("@user_acceptance", user_acceptance);
                SqlParameter parRemark = new SqlParameter("@Remark", Remark);
                SqlParameter parEMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);

                SqlParameter[] parameters = {
                                  parselectedCrfId,
                                  paruser_acceptance,
                                  parRemark,
                                  parEMP_CODE


                };

                SqlHelper.ExecuteNonQuery(newTransation
                    , CommandType.StoredProcedure
                    , StoreProcedure.SAVE_USER_ACCEPTANCE
                    , 0
                    , parameters
                    );

            }
            catch (Exception ex) { throw ex; }
        }