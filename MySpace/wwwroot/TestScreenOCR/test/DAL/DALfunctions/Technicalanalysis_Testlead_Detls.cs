        public static void Technicalanalysis_Testlead_Detls(SqlTransaction newTransaction, Dictionary<string, string> Technicalanalysis_Techleaddtls, List<List<string>> Detail, int EMP_CODE, int Testlead_Status)
        {
            try
            {
                string Crf_id = Technicalanalysis_Techleaddtls["Crf_id"];
                string Remarks = Technicalanalysis_Techleaddtls["Remark"];
                string Testing_Hrs = Technicalanalysis_Techleaddtls["Testing_Hrs"];
                string Total_Work_Hrs = Technicalanalysis_Techleaddtls["Total_Work_Hrs"];
                string Total_Cost = Technicalanalysis_Techleaddtls["Total_Cost"];
                string Code_review = Technicalanalysis_Techleaddtls["Code_review"];
                foreach (var detail in Detail)
                {
                    SqlParameter parCrf_id = new SqlParameter("@Crf_id", Crf_id);
                    SqlParameter parRemarks = new SqlParameter("@Remark", Remarks);
                    SqlParameter parTesting_Hrs = new SqlParameter("@Testing_Hrs", Testing_Hrs);
                    SqlParameter parTotal_Work_Hrs = new SqlParameter("@Total_Work_Hrs", Total_Work_Hrs);
                    SqlParameter parTotal_Cost = new SqlParameter("@Total_Cost", Total_Cost);
                    SqlParameter parCode_review = new SqlParameter("@Code_review", Code_review);
                    SqlParameter parProject_Type = new SqlParameter("@Project_Type", detail[1]);
                    SqlParameter parTester_Phase = new SqlParameter("@Tester_Phase", detail[3]);
                    SqlParameter parRelated_Work = new SqlParameter("@Related_Work", detail[5]);
                    SqlParameter parTester_Startdt = new SqlParameter("@Tester_Startdt", detail[6]);
                    SqlParameter parTester_Enddt = new SqlParameter("@Tester_Enddt", detail[7]);
                    SqlParameter parTestcase = new SqlParameter("@Testcases", detail[8]);
                    SqlParameter parTester = new SqlParameter("@Tester", detail[10]);
                    SqlParameter parEMP_CODE = new SqlParameter("@Testlead", EMP_CODE);
                    SqlParameter parTestlead_Status = new SqlParameter("@Testlead_Status", Testlead_Status);
                    SqlParameter[] detailParameters = { parCrf_id, parRemarks, parProject_Type, parTester_Phase, parRelated_Work, parTester_Startdt, parTester_Enddt, parTestcase, parTester, parEMP_CODE, parTestlead_Status, parTesting_Hrs, parTotal_Work_Hrs, parTotal_Cost, parCode_review, };

                    SqlHelper.ExecuteNonQuery(newTransaction
                        , CommandType.StoredProcedure
                        , StoreProcedure.INSERT_TESTLEAD_TECHINCALANALYSIS_DTLS
                        , detailParameters
                    );
                }
                // Continue with the rest of your code...
            }
            catch (Exception ex)
            {
                throw ex;
                // Handle any exceptions here...
            }
        }