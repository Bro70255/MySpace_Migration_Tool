
        public JsonResult Log_In(int employeeCode, string loginPassword)
        {
            DataTable dtDetails = new DataTable();
            try
            {
                //Check details with the API

                JsonResult apiResult = API_Check(employeeCode, loginPassword);
                string apiResponse = apiResult.Data.ToString();
                dynamic jsonResponse = JsonConvert.DeserializeObject(apiResponse);
                int flag = jsonResponse.status.flag;


                //int flag = 1;

                if (flag == 1) // API indicates valid credentials
                {
                    dtDetails = CRF_Tracker_bll.Log_In(employeeCode, loginPassword);

                    if (dtDetails.Rows.Count > 0)
                    {
                        // Get user data from the DataTable
                        Session["EMP_CODE"] = dtDetails.Rows[0]["Employee_Code"];
                        Session["UserID"] = dtDetails.Rows[0]["ID"];
                        Session["NAME"] = dtDetails.Rows[0]["Name"].ToString();
                        Session["FIRM"] = dtDetails.Rows[0]["Firm"].ToString();
                        Session["UNIT"] = dtDetails.Rows[0]["Unit"].ToString();
                        Session["Team_id"] = dtDetails.Rows[0]["Team_id"].ToString();
                        Session["UserType"] = dtDetails.Rows[0]["UserType"].ToString();

                        // Get the result from the DataTable
                        string result = dtDetails.Rows[0]["RESULT"].ToString(); // Assuming RESULT is a string

                        // Generate a secure unique code
                        string uniqueCode = GenerateUniqueCode(result, employeeCode);

                        if (result == "WDSKFHL@7624") // Successful login
                        {
                            return Json(new
                            {
                                success = true,
                                redirectUrl = Url.Action("Dashboard", "Home"),
                                uniqueCode = uniqueCode // Returning unique code
                            });
                        }
                        else if (result == "0") // Invalid credentials
                        {

                            return Json(new
                            {
                                success = false,
                                message = "Invalid login credentials. Please try again."
                            });
                        }
                        else // Other cases
                        {
                            return Json(new
                            {
                                success = false,
                                message = "Unexpected error. Please contact support."
                            });
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = "No data found for the provided credentials."
                        });
                    }
                }
                else // API indicates invalid credentials
                {

                    dtDetails = CRF_Tracker_bll.log_In(employeeCode, loginPassword);

                    if (dtDetails.Rows.Count > 0)
                    {
                        // Get user data from the DataTable
                        Session["EMP_CODE"] = dtDetails.Rows[0]["Employee_Code"];
                        Session["UserID"] = dtDetails.Rows[0]["ID"];
                        Session["NAME"] = dtDetails.Rows[0]["Name"].ToString();
                        Session["FIRM"] = dtDetails.Rows[0]["Firm"].ToString();
                        Session["UNIT"] = dtDetails.Rows[0]["Unit"].ToString();
                        Session["Team_id"] = dtDetails.Rows[0]["Team_id"].ToString();
                        Session["UserType"] = dtDetails.Rows[0]["UserType"].ToString();

                        // Get the result from the DataTable
                        string result = dtDetails.Rows[0]["RESULT"].ToString(); // Assuming RESULT is a string

                        // Generate a secure unique code
                        string uniqueCode = GenerateUniqueCode(result, employeeCode);

                        if (result == "CNSJEX@492") // Successful login
                        {
                            return Json(new
                            {
                                success = true,
                                redirectUrl = Url.Action("Dashboard", "Home"),
                                uniqueCode = uniqueCode // Returning unique code
                            });
                        }
                        else if (result == "0") // Invalid credentials
                        {

                            return Json(new
                            {
                                success = false,
                                message = "Invalid login credentials. Please try again."
                            });
                        }
                        else // Other cases
                        {
                            return Json(new
                            {
                                success = false,
                                message = "Unexpected error. Please contact support."
                            });
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = "No data found for the provided credentials."
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "An error occurred: " + ex.Message
                });
            }
        }