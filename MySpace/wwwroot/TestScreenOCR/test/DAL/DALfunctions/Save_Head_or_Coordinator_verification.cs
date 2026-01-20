        public static void Save_Head_or_Coordinator_verification(SqlTransaction newTransation, int DWU_ID, int EMP_CODE, int HC)
        {
            try
            {
                SqlParameter parDWU_ID = new SqlParameter("@DWU_ID", DWU_ID);
                SqlParameter parempId = new SqlParameter("@empId", EMP_CODE);
                SqlParameter parHC = new SqlParameter("@HC", HC);
                SqlParameter[] parameters = {

                parDWU_ID,
                parempId,
                parHC
            };

                SqlHelper.ExecuteNonQuery(newTransation
                    , CommandType.StoredProcedure
                    , StoreProcedure.SAVE_HEAD_OR_COORDINATOR_VERIFICATION
                    , 0
                    , parameters
                );

            }
            catch (Exception ex) { throw ex; }
        }