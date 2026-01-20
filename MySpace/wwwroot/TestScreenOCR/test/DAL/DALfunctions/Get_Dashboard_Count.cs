        public static DataTable Get_Dashboard_Count(SqlTransaction newTransaction, int EMP_CODE, int UserID, int FIRM, int UNIT, int Team_id, int UserType)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_UserID = new SqlParameter("@UserID", UserID);
                SqlParameter par_FIRM = new SqlParameter("@FIRM", FIRM);
                SqlParameter par_UNIT = new SqlParameter("@UNIT", UNIT);
                SqlParameter par_Team_id = new SqlParameter("@Team_id", Team_id);
                SqlParameter par_UserType = new SqlParameter("@UserType", UserType);


                SqlParameter[] parameters = {

                                            par_EMP_CODE,
                                            par_UserID,
                                            par_FIRM,
                                            par_UNIT,
                                            par_Team_id,
                                            par_UserType


                };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_DASHBOARD_COUNT, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }