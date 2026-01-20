        public void Testcase_File_Upload(string crf_id, string Remark)
        {
            try
            {
                var EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                var UserType = Convert.ToInt32(Session["UserType"]);
                var FIRM = Convert.ToInt32(Session["FIRM"]);
                // Get the files, tags, and category from the request
                HttpFileCollectionBase files = ControllerContext.HttpContext.Request.Files;
                string tag = ControllerContext.HttpContext.Request.Params["tags"];
                string category = ControllerContext.HttpContext.Request.Params["category"];

                // Create a directory to store the files
                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Upload_File/"));

                List<string> uploadedFiles = new List<string>();

                // Process each file in the request
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        // Generate a unique filename for each file
                        string sentFileName = Path.GetFileName(file.FileName);
                        string newFilename = crf_id + "_" + sentFileName + "_" + DateTime.UtcNow.ToString("ddMMyyyy-hhmm-tt") + Path.GetExtension(sentFileName);
                        string savedFileName = Path.Combine(di.FullName, newFilename);

                        // Save the file to the server
                        file.SaveAs(savedFileName);

                        // Store only the file name (without the path)
                        uploadedFiles.Add(newFilename);
                    }
                }

                // Assuming CSR_BLL has a method Kyc_File_Upload that accepts sequence, subject_id, and a list of filenames
                CRF_Tracker_bll.Testcase_File_Upload(crf_id, Remark, uploadedFiles, EMP_CODE, UserType, FIRM);

            }
            catch (Exception ex)
            {
                // Instead of rethrowing the exception, handle it appropriately.
                var errorMsg = new { msg = "Error uploading file", error = ex.Message };
                throw ex;
                // You might want to return this error message to the client or log it for further reference.
            }
        }