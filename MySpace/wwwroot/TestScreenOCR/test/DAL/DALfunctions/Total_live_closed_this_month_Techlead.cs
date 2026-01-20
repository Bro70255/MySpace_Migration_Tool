        public static DataTable Total_live_closed_this_month_Techlead(SqlTransaction newTransaction, int firm, int Team_id)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlParameter par_Team = new SqlParameter("@Team_id", Team_id);
                SqlParameter[] parameters = { par_firm, par_Team };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.TOTAL_LIVE_CLOSED_THIS_MONTH_TECHLEAD, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }