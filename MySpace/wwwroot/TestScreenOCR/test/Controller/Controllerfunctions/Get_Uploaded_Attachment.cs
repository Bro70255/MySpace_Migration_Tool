        public JsonResult Get_Uploaded_Attachment(string crf_id)
        {
            DataTable dtDetails = new DataTable();

            try
            {
                string crf_ID = crf_id?.Substring(0, Math.Min(crf_id.Length, 7)); // Use null conditional operator to handle null crf_id

                if (!string.IsNullOrEmpty(crf_ID))
                {
                    dtDetails = CRF_Tracker_bll.Get_Uploaded_Attachment(crf_ID);

                    if (dtDetails != null)
                    {
                        string JsResult = JsonConvert.SerializeObject(dtDetails);
                        return Json(JsResult, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        // Handle the case where Get_Uploaded_Attachment returns null
                        return Json("No data found", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    // Handle the case where crf_id is null or empty
                    return Json("Invalid crf_id", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine("Exception: " + ex.Message);
                return Json("An error occurred", JsonRequestBehavior.AllowGet);
            }
        }