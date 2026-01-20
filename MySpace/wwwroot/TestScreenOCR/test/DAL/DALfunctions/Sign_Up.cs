        public static void Sign_Up(SqlTransaction newTransaction, SignUp_model Details)
        {
            try
            {
                SqlParameter par_Name = new SqlParameter("@Name", Details.Name);
                SqlParameter par_Employee_Code = new SqlParameter("@Employee_Code", Details.Employee_Code);
                SqlParameter par_Unit = new SqlParameter("@Unit", Details.Unit);
                SqlParameter par_Firm = new SqlParameter("@Firm", Details.Firm);
                SqlParameter par_UserType = new SqlParameter("@UserType", Details.UserType);
                SqlParameter par_Email = new SqlParameter("@Email", Details.Email);
                SqlParameter par_Phone_No = new SqlParameter("@Phone_No", Details.Phone_No);
                SqlParameter par_Password = new SqlParameter("@Password", Details.Password);
                SqlParameter par_Confirm_Password = new SqlParameter("@Confirm_Password", Details.Confirm_Password);

                SqlParameter[] parameters =
                {
                par_Name,par_Employee_Code,par_Unit,par_Firm,par_UserType,par_Email,par_Phone_No,par_Password,par_Confirm_Password
            };
                SqlHelper.ExecuteNonQuery(newTransaction
                           , CommandType.StoredProcedure
                           , StoreProcedure.INSERT_SIGNUP_DETAILS
                           , parameters
                           );
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }