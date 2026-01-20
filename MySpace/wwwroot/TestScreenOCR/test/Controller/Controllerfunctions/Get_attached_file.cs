        public JsonResult Get_attached_file(String crfId)
        {
            try
            {
                DataTable dtDetails = CRF_Tracker_bll.Get_attached_file(crfId); // Assuming Get_file expects an int parameter

                if (dtDetails != null && dtDetails.Rows.Count > 0)
                {
                    string JsResult = JsonConvert.SerializeObject(dtDetails);
                    return Json(JsResult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Handle the case where Get_file returns null or empty data table
                    return Json("No data found", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine("Exception: " + ex.Message);
                return Json("An error occurred", JsonRequestBehavior.AllowGet);
            }
        }