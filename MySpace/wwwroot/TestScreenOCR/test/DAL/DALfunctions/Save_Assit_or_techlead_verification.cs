        public static void Save_Assit_or_techlead_verification(SqlTransaction newTransation, int DWU_ID, int EMP_CODE, int A_TL)
        {
            try
            {
                SqlParameter parDWU_ID = new SqlParameter("@DWU_ID", DWU_ID);
                SqlParameter parempId = new SqlParameter("@empId", EMP_CODE);
                SqlParameter parA_TL = new SqlParameter("@A_TL", A_TL);
                SqlParameter[] parameters = {

                parDWU_ID,
                parempId,
                parA_TL
            };

                SqlHelper.ExecuteNonQuery(newTransation
                    , CommandType.StoredProcedure
                    , StoreProcedure.SAVE_ASSIT_OR_TECHLEAD_VERIFICATION
                    , 0
                    , parameters
                );

            }
            catch (Exception ex) { throw ex; }
        }