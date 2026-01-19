        public static void Insert_bank_account_correction_dtls(SqlTransaction newTransaction, int correction_id, string acc_number, string Signature1, string Signature2, string Signature3, string Signature4, int Emp_Code,int Crrction_edit)
        {
            try
            {
                SqlParameter par_correction_id = new SqlParameter("@correction_id", correction_id);
                SqlParameter par_acc_number = new SqlParameter("@acc_number", acc_number);
                SqlParameter par_Signature1 = new SqlParameter("@Signature1", Signature1);
                SqlParameter par_Signature2 = new SqlParameter("@Signature2", Signature2);
                SqlParameter par_Signature3 = new SqlParameter("@Signature3", Signature3);
                SqlParameter par_Signature4 = new SqlParameter("@Signature4", Signature4);
                SqlParameter par_Emp_Code = new SqlParameter("@Emp_Code", Emp_Code);
                SqlParameter par_Crrction_edit = new SqlParameter("@Crrction_edit", Crrction_edit);
                SqlParameter[] parameters = {
            par_correction_id,
            par_acc_number,
            par_Signature1,
            par_Signature2,
            par_Signature3,
            par_Signature4,
            par_Emp_Code,
            par_Crrction_edit
        };

                Sqlhelper.ExecuteNonQuery(newTransaction, CommandType.StoredProcedure, Storedprocedure.INSERT_BANK_CORRECTION_DTLS, 0, parameters);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw; // Rethrow the exception for higher-level handling
            }
        }