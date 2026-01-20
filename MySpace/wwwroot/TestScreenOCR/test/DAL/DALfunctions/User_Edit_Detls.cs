        public static void User_Edit_Detls(SqlTransaction newTransaction, Returnusr_dtls return_details, int EMP_CODE)
        {
            try
            {

                SqlParameter parcrf_ID = new SqlParameter("@crf_ID", return_details.selectedCrfId);
                SqlParameter parremark = new SqlParameter("@remark", return_details.remark);
                SqlParameter parAttach_file = new SqlParameter("@Attach_file", return_details.Attach_file);
                SqlParameter parEMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter[] parameters = {
            parcrf_ID,
            parremark,
            parAttach_file,
            parEMP_CODE
        };
                SqlHelper.ExecuteNonQuery(newTransaction
       , CommandType.StoredProcedure
       , StoreProcedure.SAVE_USER_RETURN_DTLS
       , parameters
       );
                // Continue with the rest of your code...
            }
            catch (Exception ex)
            {
                throw ex;
                // Handle any exceptions here...
            }
        }