        public static DataTable Get_Bind_Unit(SqlTransaction newTransation)
        {

            DataTable dtDetails = new DataTable();
            Sqlhelper.FillDatatable(newTransation, CommandType.StoredProcedure, Storedprocedure.GET_BIND_UNIT, dtDetails, 0);
            return dtDetails;

        }