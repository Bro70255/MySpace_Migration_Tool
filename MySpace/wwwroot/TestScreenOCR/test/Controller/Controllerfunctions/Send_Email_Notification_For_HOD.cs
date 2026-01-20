        public ActionResult Send_Email_Notification_For_HOD(string crfid, string description, string unit, string Firm_Name, int Firm_id,int department)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;

            try
            {
                string email;
                string Name;
               

                // Multiple conditional logic
                if (Firm_id == 1 && unit != "3")
                {
                    Name = "Rekha MP";
                    email = "ophead@manappuramfoundation.org";
                    //email = "developer9@manappuramfoundation.org";
                }
                else if (Firm_id == 2 && unit == "4")
                {
                    Name = "Mintu P Mathew";
                    email = "principal@mageetschool.com";
                    //email = "developer9@manappuramfoundation.org";
                }
                else if (Firm_id == 2 && unit == "5")
                {
                    Name = "Jijikrishna NG";
                    email = "principalmps@manappuramschools.com";
                    //email = "developer9@manappuramfoundation.org";
                }
                else if (Firm_id == 1 && unit == "3" && department == 1)
                {

                    Name = "Subhash";
                    email = "hrhead@manappuramfoundation.org";
                    //email = "developer4@manappuramfoundation.org";
                }
                else if (Firm_id == 1 && unit == "3" && department == 2)
                {

                    Name = "Sarika PC";
                    email = "techlead01@manappuramfoundation.org";
                    //email = "developer5@manappuramfoundation.org";
                }
                else if (Firm_id == 1 && unit == "3" && department == 3)
                {

                    Name = "Rekha MP";
                    email = "ophead@manappuramfoundation.org";
                   //  email = "developer7@manappuramfoundation.org";
                }
                else
                {

                    Name = "Arya Sudarsan";
                    email = "accountshead@manappuramfoundation.org";
                    //email = "developer10@manappuramfoundation.org";
                }

                string messageBody = $@"
        Dear {Name},<br/><br/>

        I hope this message finds you well.<br/><br/>
        A new CRF has been created and is currently awaiting your approval.<br/><br/>
        <b>CRF ID: {crfid}</b><br/>
        <b>Description: {description}</b><br/>
        We kindly request you to review and confirm the request by logging into the user portal.<br/>
        Your timely approval will help us proceed with the necessary actions efficiently.<br/>
        Thank you for your attention to this matter. Should you have any questions or require additional information, please do not hesitate to reach out.<br/>
        Best regards,<br/>
        {Firm_Name}";

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