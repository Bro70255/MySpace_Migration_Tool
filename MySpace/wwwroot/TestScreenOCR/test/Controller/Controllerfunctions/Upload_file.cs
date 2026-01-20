        public ActionResult Upload_file(HttpPostedFileBase File_Upload)
        {
            if (File_Upload != null && File_Upload.ContentLength > 0)
            {
                string extension = Path.GetExtension(File_Upload.FileName);
                string uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid().ToString("N") + extension;
                string savePath = Path.Combine(Server.MapPath("~/File_Upload"), uniqueFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                File_Upload.SaveAs(savePath);

                return Content(uniqueFileName); // Return the uniqueFileName as plain text
            }

            return Content(""); // If there was no file uploaded, return an empty response or an appropriate message
        }