        public static DataTable Average_Ongoing(SqlTransaction newTransaction, int firm, int Employee_Code, int UserType)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlParameter par_Employee_Code = new SqlParameter("@Employee_Code", Employee_Code);
                SqlParameter par_UserType = new SqlParameter("@UserType", UserType);

                SqlParameter[] parameters = { par_firm, par_Employee_Code, par_UserType };

                // Execute the stored procedure to fill the DataTable with average day difference
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.AVERAGE_ONGOING, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }