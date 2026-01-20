        public static DataTable Get_developerwise_Report(SqlTransaction newTransaction, DateTime Startdate, DateTime Enddate, int Developer, int firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Startdate = new SqlParameter("@Startdate", Startdate);
                SqlParameter par_Enddate = new SqlParameter("@Enddate", Enddate);
                SqlParameter par_Developer = new SqlParameter("@Developer", Developer);
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlParameter[] parameters = { par_Startdate, par_Enddate, par_Developer, par_firm };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_DEVELOPERWISE_REPORT, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }