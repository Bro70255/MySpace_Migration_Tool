        public static DataTable Get_Daily_Work_Update_Approvel_Teach_Lead(SqlTransaction newTransaction)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_DAILY_WORK_UPDATE_APPROVEL_TEACH_LEAD, dtDetails, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }