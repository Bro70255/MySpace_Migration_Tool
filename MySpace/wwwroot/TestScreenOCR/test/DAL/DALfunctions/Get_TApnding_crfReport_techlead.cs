        public static DataTable Get_TApnding_crfReport_techlead(SqlTransaction newTransaction, int firm, int Team_id)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlParameter par_Team = new SqlParameter("@Team_id", Team_id);
                SqlParameter[] parameters = { par_firm, par_Team };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_TAPENDING_REPORT_TECHLEAD, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }