        public static DataTable Save_File_Upload(SqlTransaction newtransaction, List<string> uploadedFiles, string id)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                foreach (string fileName in uploadedFiles)
                {

                    SqlParameter par_uploadedFiles = new SqlParameter("@File_Name", fileName); // Use the current fileName in the loop
                    SqlParameter par_id = new SqlParameter("@id", id);
                    SqlParameter[] parameters = {
           par_uploadedFiles,
           par_id
            };

                    SqlHelper.FillDatatable(newtransaction, CommandType.StoredProcedure, StoreProcedure.INSERT_FILE_UPLOAD_DTLS, dtDetails, 0, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }