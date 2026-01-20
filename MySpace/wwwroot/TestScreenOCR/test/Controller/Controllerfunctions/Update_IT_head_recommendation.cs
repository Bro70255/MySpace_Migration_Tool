        public JsonResult Update_IT_head_recommendation(string crf_id, string Remark)
        {
            try
            {
                int IT_head = 3;
                int EMP_CODE = Convert.ToInt32(Session["EMP_CODE"]);
                var FIRM = Convert.ToInt32(Session["FIRM"]);
                string CRF_ID = crf_id.Substring(0, 7);

                // Update recommendation
                CRF_Tracker_bll.Update_IT_head_recommendation(IT_head, EMP_CODE, CRF_ID, Remark);
             
                // Send email to CEO
                DataTable dtDetails = CRF_Tracker_bll.Get_Email_For_CEO(CRF_ID, FIRM);
                if (dtDetails != null && dtDetails.Rows.Count > 0)
                {
                    DataRow row = dtDetails.Rows[0];

                    string email = row["Email"].ToString();
                    string name = row["Name"].ToString();
                    string description = row["Description"].ToString();
                    string firmName = row["Firm_Name"].ToString();
                    string crfid = row["crf_Id"].ToString();

                    Send_Email_Notification_For_CEO(firmName, email, name, description, crfid);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }