        public static DataTable Get_Manpower_Fortesting_Ta(SqlTransaction newTransaction, string crf_id)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter par_crf_id = new SqlParameter("@crf_id", crf_id);


                SqlParameter[] parameters = {

                                            par_crf_id
                };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.GET_MANPOWER_COST_TADETAILS, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }