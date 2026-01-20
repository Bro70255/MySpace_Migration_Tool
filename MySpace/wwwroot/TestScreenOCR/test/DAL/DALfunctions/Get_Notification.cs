        public static DataTable Get_Notification(SqlTransaction newTransaction, int EMP_CODE, int FIRM, int UserType, int Team_id, int Unit)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_EMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_FIRM = new SqlParameter("@FIRM", FIRM);
                SqlParameter par_UserType = new SqlParameter("@UserType", UserType);
                SqlParameter par_Team_id = new SqlParameter("@Team_id", Team_id);
                SqlParameter par_Unit = new SqlParameter("@Unit", Unit);
                SqlParameter[] parameters = {

                    par_EMP_CODE,
                    par_FIRM,
                    par_UserType,
                    par_Team_id,
                    par_Unit
                };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_NOTIFICATION_DTLS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }