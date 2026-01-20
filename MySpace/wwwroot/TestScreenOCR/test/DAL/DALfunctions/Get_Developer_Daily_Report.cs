        public static DataTable Get_Developer_Daily_Report(SqlTransaction newTransaction, int value, DateTime From_date, DateTime To_date, int Developer, string Module)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter parvalue = new SqlParameter("@value", value);
                SqlParameter parFrom_date = new SqlParameter("@From_date", From_date);
                SqlParameter parTo_date = new SqlParameter("@To_date", To_date);
                SqlParameter parDeveloper = new SqlParameter("@Developer", Developer);
                SqlParameter parModule = new SqlParameter("@Module", Module);
                SqlParameter[] parameters = { parvalue, parFrom_date, parTo_date, parDeveloper, parModule };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_DEVELOPER_DAILY_REPORT, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }