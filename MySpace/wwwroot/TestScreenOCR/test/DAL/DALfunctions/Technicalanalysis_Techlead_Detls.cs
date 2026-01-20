        public static void Technicalanalysis_Techlead_Detls(SqlTransaction newTransaction, Dictionary<string, string> Technicalanalysis_dtls, List<List<string>> Details, int EMP_CODE, int Techlead_Status)
        {
            try
            {
                string Crf_id = Technicalanalysis_dtls["Crf_id"];
                string Remarks = Technicalanalysis_dtls["Remarks"];
                string Man_hour = Technicalanalysis_dtls["Man_hour"];
                string cost_estmation = Technicalanalysis_dtls["cost_estmation"];

                foreach (var detail in Details)
                {
                    SqlParameter parCrf_id = new SqlParameter("@Crf_id", Crf_id);
                    SqlParameter parRemarks = new SqlParameter("@Remarks", Remarks);
                    SqlParameter parPhase = new SqlParameter("@Phase", detail[0]);
                    SqlParameter parChanges_type = new SqlParameter("@Changes_type", detail[1]);
                    SqlParameter parcrf_complexity = new SqlParameter("@crf_complexity", detail[3]);
                    SqlParameter parRelated_Work = new SqlParameter("@Related_Work", detail[5]);
                    SqlParameter parNumber_of_changes = new SqlParameter("@Number_of_changes", detail[7]);
                    SqlParameter parMan_Hours = new SqlParameter("@Man_Hours", detail[8]);
                    SqlParameter parDeveloper = new SqlParameter("@Developer", detail[9]);
                    SqlParameter parStart_Date = new SqlParameter("@Start_Date", detail[11]);
                    SqlParameter parEnd_Date = new SqlParameter("@End_Date", detail[12]);
                    SqlParameter parCost_Estimation = new SqlParameter("@Cost_Estimation", detail[13]);
                    SqlParameter parEMP_CODE = new SqlParameter("@Techlead", EMP_CODE);
                    SqlParameter parTestlead_Status = new SqlParameter("@Techlead_Status", Techlead_Status);
                    SqlParameter[] detailParameters = { parCrf_id, parRemarks, /*parMan_hour, parcost_estmation,*/parPhase, parChanges_type, parcrf_complexity, parRelated_Work, parNumber_of_changes, parMan_Hours, parDeveloper, parStart_Date, parEnd_Date, parCost_Estimation, parEMP_CODE, parTestlead_Status };

                    SqlHelper.ExecuteNonQuery(newTransaction
                        , CommandType.StoredProcedure
                        , StoreProcedure.INSERT_TECHLEAD_TECHINCALANALYSIS_DTLS
                        , detailParameters
                    );
                }
                SqlParameter parCrf_Id = new SqlParameter("@Crf_id", Crf_id);
                SqlParameter parMan_hour = new SqlParameter("@Man_hrs", Man_hour);
                SqlParameter parcost_estmation = new SqlParameter("@cst_estmation", cost_estmation);
                SqlParameter[] Parameters1 = { parCrf_Id, parMan_hour, parcost_estmation };

                SqlHelper.ExecuteNonQuery(newTransaction
                    , CommandType.StoredProcedure
                    , StoreProcedure.INSERT_MANPOWER_DTLS
                    , Parameters1
                       );
                // Continue with the rest of your code...
            }
            catch (Exception ex)
            {
                throw ex;
                // Handle any exceptions here...
            }

        }