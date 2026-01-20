        public ActionResult Send_Email_Notification_For_CEO(string firmName, string email, string Name, string description, string crfId)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;

            try
            {
                string messageBody = $@"
        Dear {Name},<br/><br/>

        I hope this message finds you well.<br/><br/>
        A new CRF has been created and is currently awaiting your approval.<br/><br/>
        <b>CRF ID: {crfId}</b><br/>
        <b>Description: {description}</b><br/>
        We kindly request you to review and confirm the request by logging into the user portal.<br/>
        Your timely approval will help us proceed with the necessary actions efficiently.<br/>
        Thank you for your attention to this matter. Should you have any questions or require additional information, please do not hesitate to reach out.<br/>
        Best regards,<br/>
        IT Team Name<br/>
        {firmName}";

                MailAddress fromAddress = new MailAddress("trackeralert@manappuramfoundation.org", "Manappuram Foundation");
                message.From = fromAddress;
                message.To.Add(email);
                message.Subject = "CRF Approval Notification";
                message.IsBodyHtml = true;
                message.Body = messageBody;

                smtpClient.EnableSsl = true;
                smtpClient.Send(message);


                return Json(new { success = true, message = "Email successfully sent to " + email + "." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(new { success = false, message = msg }, JsonRequestBehavior.AllowGet);
            }
        }