

        public static DataTable Get_attached_file(SqlTransaction newTransation, String crfId)
        {
            try
            {

                SqlParameter Par_crfId = new SqlParameter("@crfId", crfId);

                SqlParameter[] parameters = {
                                               Par_crfId

            };

                DataTable dtDetails = new DataTable();
                SqlHelper.FillDatatable(newTransation, CommandType.StoredProcedure, StoreProcedure.GET_ATTACHED_FILES_FOR_ASSIGNED_WORKS_FOR_DEVELOPER_REPORT, dtDetails, 0, parameters);
                return dtDetails;
            }
            catch (Exception ex) { throw ex; }
        }