        public static string Crf_Detls(SqlTransaction newTransaction, Crfdtls Crf_Details, int EMP_CODE)
        {
            try
            {
                SqlParameter parSubject = new SqlParameter("@Subject", Crf_Details.subject);
                SqlParameter parDescription = new SqlParameter("@Description", Crf_Details.Description);
                SqlParameter parIt_team = new SqlParameter("@It_team", Crf_Details.It_team);
                SqlParameter parRequest_type = new SqlParameter("@Request_type", Crf_Details.Request_type);
                SqlParameter parTarget_date = new SqlParameter("@Target_date", Crf_Details.Target_date);
                SqlParameter parPriority = new SqlParameter("@Priority", Crf_Details.Priority);
                SqlParameter parSelect_module = new SqlParameter("@Select_module", Crf_Details.Select_module);
                SqlParameter parChoose_impctmodule = new SqlParameter("@Choose_impactmodule", Crf_Details.Choose_impctmodule);
                SqlParameter pardepartment = new SqlParameter("@department", Crf_Details.department);
                SqlParameter parEMP_CODE = new SqlParameter("@EMP_CODE", EMP_CODE);
                SqlParameter parCrfId = new SqlParameter("@Crf_Id", SqlDbType.VarChar, 100);
                parCrfId.Direction = ParameterDirection.Output;

                SqlParameter[] parameters = {
            parSubject,
            parDescription,
            parIt_team,
            parRequest_type,
            parTarget_date,
            parPriority,
            parSelect_module,
            parChoose_impctmodule,
            pardepartment,
            parEMP_CODE,
            parCrfId // adding output parameter for CRF ID
        };

                SqlHelper.ExecuteNonQuery(newTransaction,
                    CommandType.StoredProcedure,
                    StoreProcedure.INSERT_CRF_DETAILS,
                    parameters
                );

                // Retrieving CRF ID from output parameter
                string crfId = parCrfId.Value.ToString();
                return crfId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }