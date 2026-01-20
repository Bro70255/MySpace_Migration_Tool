        public static DataTable Get_Bind_Tester_Change_Dtls(SqlTransaction newTransation, int selectedtesterid)
        {
            SqlParameter par_selectedtesterid = new SqlParameter("@selectedtesterid", selectedtesterid);

            DataTable dtDetails = new DataTable();
            SqlHelper.FillDatatable(newTransation, CommandType.StoredProcedure, StoreProcedure.GET_BIND_TESTERCHANGE_DETAILS, dtDetails, 0, par_selectedtesterid);
            return dtDetails;

        }