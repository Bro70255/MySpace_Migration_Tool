        public static DataTable Bind_CRF_for_Dailywork(SqlTransaction newTransaction, int EMP_CODE, int FIRM, int UserType)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter parEMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter par_FIRM = new SqlParameter("@FIRM", FIRM);
                SqlParameter par_UserType = new SqlParameter("@UserType", UserType);

                SqlParameter[] parameters = {

                  parEMP_CODE,
                     par_FIRM,
                 par_UserType,

                };

                SqlHelper.FillDatatable(newTransaction, CommandType.StoredProcedure, StoreProcedure.BIND_CRF_FOR_DAILYWORK_UPDATION, dtDetails, 0, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }