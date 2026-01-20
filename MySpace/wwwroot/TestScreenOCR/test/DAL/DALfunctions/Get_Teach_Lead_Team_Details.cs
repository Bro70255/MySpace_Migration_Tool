        public static DataTable Get_Teach_Lead_Team_Details(SqlTransaction newTransaction, int EMP_CODE, int Team_id)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_Team_id = new SqlParameter("@Team_id", Team_id);

                SqlParameter[] parameters = {

                                            par_EMP_CODE,
                                            par_Team_id
                };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_TEACH_LEAD_TEAM_DETAILS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }