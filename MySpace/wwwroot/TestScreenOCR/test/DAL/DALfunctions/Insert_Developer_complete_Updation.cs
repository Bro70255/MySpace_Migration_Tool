        public static DataTable Insert_Developer_complete_Updation(SqlTransaction newTransaction, string crf_ID, int status, string Remark, string module_name, string Tfs_name, string Uat_link, string Uat_path, int Developer)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_crf_ID = new SqlParameter("@crf_ID", crf_ID);
                SqlParameter par_status = new SqlParameter("@status", status);
                SqlParameter par_Remark = new SqlParameter("@Remark", Remark);
                SqlParameter par_module_name = new SqlParameter("@module_name", module_name);
                SqlParameter par_Tfs_name = new SqlParameter("@Tfs_name", Tfs_name);
                SqlParameter par_Uat_link = new SqlParameter("@Uat_link", Uat_link);
                SqlParameter par_Uat_path = new SqlParameter("@Uat_path", Uat_path);
                SqlParameter par_Developer = new SqlParameter("@Developer", Developer);

                SqlParameter[] parameters = {
                                               par_crf_ID,
                                               par_status,
                                               par_Remark,
                                               par_module_name,
                                               par_Tfs_name,
                                               par_Uat_link,
                                               par_Uat_path,
                                               par_Developer

            };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.INSERT_DEVELOPER_COMPLETE_UPDATION, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }