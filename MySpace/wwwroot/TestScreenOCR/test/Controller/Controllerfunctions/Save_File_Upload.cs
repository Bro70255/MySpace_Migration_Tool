        public void Save_File_Upload()
        {
            try
            {
                var id = Convert.ToString(Session["ID"]);
                HttpFileCollectionBase files = ControllerContext.HttpContext.Request.Files;
                string tag = ControllerContext.HttpContext.Request.Params["tags"];
                string category = ControllerContext.HttpContext.Request.Params["category"];

                // Create a directory to store the files
                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/File_Upload/"));

                List<string> uploadedFiles = new List<string>();

                // Process each file in the request
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        // Generate a unique filename for each file
                        string sentFileName = Path.GetFileNameWithoutExtension(file.FileName); // Extract filename without extension
                        string extension = Path.GetExtension(file.FileName); // Extract file extension
                        string newFilename = $"{id}_{sentFileName}_{DateTime.UtcNow:ddMMyyyy-hhmm-tt}{extension}";
                        string savedFileName = Path.Combine(di.FullName, newFilename);

                        file.SaveAs(savedFileName);
                        uploadedFiles.Add(newFilename);
                    }
                }

                // Assuming CSR_BLL has a method Kyc_File_Upload that accepts sequence, subject_id, and a list of filenames
                CRF_Tracker_bll.Save_File_Upload(uploadedFiles, id);

                DataTable dtDetails = CRF_Tracker_bll.Get_dtls_For_Email_HOD(id);
                if (dtDetails != null && dtDetails.Rows.Count > 0)
                {
                    DataRow row = dtDetails.Rows[0];

                    string crfid = row["crf_Id"].ToString();
                    string description = row["Description"].ToString();
                    string unit = row["Unit"].ToString();
                    string Firm_Name = row["Firm_Name"].ToString();
                    int Firm_id = Convert.ToInt32(row["Firm_id"]);
                    int department = Convert.ToInt32(row["department"]);

                    Send_Email_Notification_For_HOD(crfid, description, unit, Firm_Name, Firm_id, department);

                }
                
            }
            catch (Exception ex)
            {
                // Handle the exception and log or return the error message
                var errorMsg = new { msg = "Error uploading file", error = ex.Message };
                // Log the error message or handle it as needed
                // You might want to return this error message to the client or log it for further reference
                // For example, log to a file, database, or any logging mechanism
                // LogError(errorMsg); // Example logging method

                throw new HttpException(500, "Internal Server Error", ex); // Optionally rethrow as an HttpException
            }
        }