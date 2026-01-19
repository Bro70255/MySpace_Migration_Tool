        public static DataTable Get_Bank_approved_dtls(SqlTransaction newTransaction,int usertype)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                SqlParameter parusertype = new SqlParameter("@usertype", usertype);
                Sqlhelper.FillDatatable(newTransaction, CommandType.StoredProcedure, Storedprocedure.GET_APPROVED_DTLS, dtDetails, 0, parusertype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDetails;
        }