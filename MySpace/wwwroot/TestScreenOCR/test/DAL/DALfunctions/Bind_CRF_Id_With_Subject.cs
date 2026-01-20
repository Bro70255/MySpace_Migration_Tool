        public static DataTable Bind_CRF_Id_With_Subject(SqlTransaction newTransaction, int firm, int unit, int Hod)
        {
            DataTable dtDetails = new DataTable();
            try
            {

                SqlParameter par_firm = new SqlParameter("@firm", firm);
                SqlParameter par_unit = new SqlParameter("@unit", unit);
                SqlParameter par_Hod = new SqlParameter("@Hod", Hod);
                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.BIND_CRF_ID_WITH_SUBJECT, dtDetails, 0, par_Hod, par_firm, par_unit);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }