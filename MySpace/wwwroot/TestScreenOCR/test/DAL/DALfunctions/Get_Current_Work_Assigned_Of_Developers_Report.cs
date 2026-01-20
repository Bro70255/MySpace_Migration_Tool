        public static DataTable Get_Current_Work_Assigned_Of_Developers_Report(SqlTransaction newTransaction, int Firm, int Developer, int last_dev_endate)
        {
            if (last_dev_endate == 0)
            {
                DataTable dtDetails = new DataTable();
                try
                {
                    SqlParameter par_Firm = new SqlParameter("@Firm", Firm);
                    SqlParameter par_Developer = new SqlParameter("@Developer", Developer);

                    SqlParameter[] parameters = { par_Firm, par_Developer };
                    SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_CURRENT_ASSIGNED_WORKS_OF_THE_DEVELOPERS, dtDetails, 0, parameters);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dtDetails;
            }
            else
            {
                DataTable dtDetails = new DataTable();
                try
                {
                    SqlParameter par_Firm = new SqlParameter("@Firm", Firm);
                    SqlParameter par_Developer = new SqlParameter("@Developer", Developer);


                    SqlParameter[] parameters = { par_Firm, par_Developer };
                    SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_THE_LAST_WORK_END_DATE_OF_DEVELOPER, dtDetails, 0, parameters);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dtDetails;
            }
        }