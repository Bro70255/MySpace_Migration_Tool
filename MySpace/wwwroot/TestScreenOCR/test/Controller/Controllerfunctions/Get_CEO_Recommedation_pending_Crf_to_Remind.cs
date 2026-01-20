        public JsonResult Get_CEO_Recommedation_pending_Crf_to_Remind()
        {
            DataTable dtDetails = new DataTable();
            try
            {
                // 1. Check if today's reminder has already been sent
                if (IsReminderAlreadySentTodayCEO())
                {
                    return Json(new { success = false, message = "Reminder email already sent today." }, JsonRequestBehavior.AllowGet);
                }

                // 2. Get pending CRFs
                dtDetails = CRF_Tracker_bll.Get_CEO_Recommedation_pending_Crf_to_Remind();

                if (dtDetails.Rows.Count > 0)
                {
                    List<string> crfIds = new List<string>();
                    foreach (DataRow row in dtDetails.Rows)
                    {
                        string crfId = row["CRF_ID"]?.ToString();
                        if (!string.IsNullOrEmpty(crfId))
                            crfIds.Add(crfId);
                    }

                    string crfIdList = string.Join(", ", crfIds);
                    string email = "ceo@manappuramfoundation.org";
                    //string email = "developer9@manappuramfoundation.org";

                    string name = "GEORGE DE DAS";

                    string messageBody = $@"
                Dear {name},<br/><br/>

                This is a kind reminder that the following CRFs are pending your recommendation:<br/><br/>

                <b>CRF IDs:</b> {crfIdList}<br/><br/>

                Please log in to the portal and provide your recommendations at your earliest convenience.<br/><br/>

                Thank you for your attention.<br/><br/>

                Best regards,<br/>
                IT Team
            ";

                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    MailAddress fromAddress = new MailAddress("trackeralert@manappuramfoundation.org", "Manappuram Foundation");

                    message.From = fromAddress;
                    message.To.Add(email);
                    message.Subject = $"Reminder: Recommendation Pending for CRFs";
                    message.IsBodyHtml = true;
                    message.Body = messageBody;

                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);

                    // 3. Log the sent reminder in DB
                    LogReminderSentTodayCEO();

                    return Json(new { success = true, message = "Reminder email sent to IT Head." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No pending CRFs to remind." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }