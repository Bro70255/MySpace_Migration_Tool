        public JsonResult Get_CRFDetails_Send_mail_to_IT_Head(string crfId)
        {
            DataTable dtDetails = new DataTable();
            string msg = string.Empty;

            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(crfId) || !crfId.Contains("-"))
                {
                    return Json(new { success = false, message = "Invalid CRF ID format." }, JsonRequestBehavior.AllowGet);
                }

                // Extract CRF ID (e.g., "CRF1451")
                string CRF_ID = crfId.Split('-')[0].Trim();

                // Get CRF details from BLL
                dtDetails = CRF_Tracker_bll.Get_CRFDetails_Send_mail_to_IT_Head(CRF_ID);

                if (dtDetails.Rows.Count > 0)
                {
                    // ✅ Hardcoded values
                    string email = "it@manappuramfoundation.org";
                    //string email = "developer9@manappuramfoundation.org";
                    string name = "Unni KK";
                    string description = dtDetails.Rows[0]["Description"]?.ToString();

                    // Email content
                    string messageBody = $@"
                Dear {name},<br/><br/>

                I hope this message finds you well.<br/><br/>

                A new CRF (Change Request Form) has been created and is currently awaiting your approval. Below are the details of the request:<br/><br/>

                <b>CRF ID:</b> {CRF_ID}<br/>
                <b>Description:</b> {description}<br/><br/>

                We kindly request you to review and confirm the request by logging into the user portal. Your timely approval will help us proceed with the necessary actions efficiently.<br/><br/>

                Thank you for your attention to this matter. Should you have any questions or require additional information, please do not hesitate to reach out.<br/><br/>

                Best regards,<br/>
                IT Team Name<br/>
            ";

                    // Send the email
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    MailAddress fromAddress = new MailAddress("trackeralert@manappuramfoundation.org", "Manappuram Foundation");

                    message.From = fromAddress;
                    message.To.Add(email);
                    message.Subject = $"CRF Approval Notification - {CRF_ID}";
                    message.IsBodyHtml = true;
                    message.Body = messageBody;

                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);

                    return Json(new { success = true, message = "Email successfully sent to IT Head: " + email }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No CRF details found for ID: " + crfId }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(new { success = false, message = "Error: " + msg }, JsonRequestBehavior.AllowGet);
            }
        }