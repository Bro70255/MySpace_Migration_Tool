

        public static DataTable Testcase_File_Upload(SqlTransaction newtransaction, string crf_id, string Remark, List<string> uploadedFiles, int EMP_CODE, int UserType, int FIRM)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                foreach (string fileName in uploadedFiles)
                {
                    SqlParameter par_crf_id = new SqlParameter("@crf_id", crf_id);
                    SqlParameter par_Remark = new SqlParameter("@Remark", Remark);
                    SqlParameter par_uploadedFiles = new SqlParameter("@File_Name", fileName);
                    SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                    SqlParameter par_UserType = new SqlParameter("@UserType", UserType);
                    SqlParameter par_FIRM = new SqlParameter("@FIRM", FIRM);
                    // Use the current fileName in the loop

                    SqlParameter[] parameters = {
                par_crf_id,
                par_Remark,
                par_uploadedFiles,
                par_EMP_CODE,
                par_UserType,
                par_FIRM

            };

                    SqlHelper.FillDatatable(newtransaction, CommandType.StoredProcedure, StoreProcedure.INSERT_TESTCASE_FILE_UPLOAD_DTLS, dtDetails, 0, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }