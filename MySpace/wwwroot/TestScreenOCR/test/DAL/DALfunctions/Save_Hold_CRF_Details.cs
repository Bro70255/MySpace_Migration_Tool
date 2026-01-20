        public static DataTable Save_Hold_CRF_Details(SqlTransaction newTransation, string CRFid, DateTime Hold_from, DateTime Hold_end, string Reason, string Remark, int Employee_Code)
        {
            try
            {

                SqlParameter par_CRFid = new SqlParameter("@CRFid", CRFid);
                SqlParameter par_Hold_from = new SqlParameter("@Hold_from", Hold_from);
                SqlParameter par_Hold_end = new SqlParameter("@Hold_end", Hold_end);
                SqlParameter par_Reason = new SqlParameter("@Reason", Reason);
                SqlParameter par_Remark = new SqlParameter("@Remark", Remark);
                SqlParameter par_Employee_Code = new SqlParameter("@Employee_Code", Employee_Code);

                SqlParameter[] parameters = {

                                      par_CRFid,
                                      par_Hold_from,
                                      par_Hold_end,
                                      par_Reason,
                                      par_Remark,
                                      par_Employee_Code
                    };
                DataTable dtDetails = new DataTable();
                SqlHelper.FillDatatable(newTransation, CommandType.StoredProcedure, StoreProcedure.SAVE_HOLD_CRF_DETAILS, dtDetails, 0, parameters);
                return dtDetails;

            }
            catch (Exception ex) { throw ex; }
        }