        public static DataTable Get_Live_Closed_crf_Reports(SqlTransaction newTransaction, int firm, int Employee_Code, int UserType, int Team_id, int Unit)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlParameter par_Employee_Code = new SqlParameter("@Employee_Code", Employee_Code);
                SqlParameter par_UserType = new SqlParameter("@UserType", UserType);
                SqlParameter par_Team_id = new SqlParameter("@Team_id", Team_id);
                SqlParameter par_Unit = new SqlParameter("@Unit", Unit);


                SqlParameter[] parameters = { par_firm, par_Employee_Code, par_UserType, par_Team_id, par_Unit };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_LIVE_CLOSED_CRF_REPORTS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }