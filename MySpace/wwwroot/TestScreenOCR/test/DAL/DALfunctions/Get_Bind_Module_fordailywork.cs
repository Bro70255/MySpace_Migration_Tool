        public static DataTable Get_Bind_Module_fordailywork(SqlTransaction newTransaction, int Firm)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_Firm_Id = new SqlParameter("@Firm_Id", Firm);
                SqlParameter[] parameters = {
                 par_Firm_Id
                };
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_BIND_MODULE_FORDAILYWORK, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }