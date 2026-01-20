        public static void Save_Daily_Work_Updation(SqlTransaction newTransation, List<List<string>> Details, int EMP_CODE, int FIRM, int UserType)
        {
            try
            {
                foreach (var detail in Details)
                {
                    SqlParameter parempId = new SqlParameter("@empId", EMP_CODE);
                    SqlParameter parfirmId = new SqlParameter("@firm", FIRM);
                    SqlParameter parusertype = new SqlParameter("@usertype", UserType);
                    SqlParameter parassignedWork = new SqlParameter("@assignedWork", detail[0]);
                    SqlParameter par_module = new SqlParameter("@module_id", detail[2]);
                    SqlParameter pardescription = new SqlParameter("@descrption_id", detail[4]);
                    SqlParameter parcompletionPercentage = new SqlParameter("@completionPercentage", detail[5]);
                    SqlParameter pardetailedDescription = new SqlParameter("@detailedDescription", detail[6]);
                    SqlParameter parremark = new SqlParameter("@remark", detail[7]);
                    SqlParameter pardate = new SqlParameter("@date", detail[8]);
                    SqlParameter partime = new SqlParameter("@time", detail[9]);
                    SqlParameter[] parameters = {
                parempId,
                parfirmId,
                parusertype,
                parassignedWork,
                par_module,
                pardescription,
                parcompletionPercentage,
                pardetailedDescription,
                parremark,
                pardate,
                partime
            };

                    SqlHelper.ExecuteNonQuery(newTransation
                        , CommandType.StoredProcedure
                        , StoreProcedure.SAVE_DAILY_WORK
                        , 0
                        , parameters
                    );
                }
            }
            catch (Exception ex) { throw ex; }
        }