
        public JsonResult Get_HOD_Recommendation_Pending_Crf_To_Remind()
        {
            try
            {
                if (IsReminderAlreadySentToday_HOD())
                {
                    return Json(new { success = false, message = "Reminder email already sent today." }, JsonRequestBehavior.AllowGet);
                }

                DataTable dtDetails = CRF_Tracker_bll.Get_HOD_Recommendation_Pending_Crf_To_Remind();

                if (dtDetails.Rows.Count == 0)
                {
                    return Json(new { success = false, message = "No pending CRFs to remind." }, JsonRequestBehavior.AllowGet);
                }

                // Grouping logic
                Dictionary<string, List<string>> groupedCRFs = new Dictionary<string, List<string>>();
                Dictionary<string, (string Name, string Email)> groupRecipients = new Dictionary<string, (string, string)>();

                foreach (DataRow row in dtDetails.Rows)
                {
                    int firmId = Convert.ToInt32(row["Firm"]);
                    string unit = row["Unit"]?.ToString() ?? "";
                    string crfId = row["crf_Id"]?.ToString() ?? "";
                    int department = Convert.ToInt32(row["Dept_Id"]);
                    string key = "";

                    string name, email;

                    if (firmId == 1 && unit != "3")
                    {
                        key = "Group1";
                        name = "Rekha MP";
                        email = "ophead@manappuramfoundation.org";
                        //email = "developer7@manappuramfoundation.org";
                    }

                  
                    else if (firmId == 2 && unit == "4")
                    {
                        key = "Group2";
                        name = "Mintu P Mathew";
                        email = "principal@mageetschool.com";
                       // email = "developer9@manappuramfoundation.org";
                    }
                    else if (firmId == 2 && unit == "5")
                    {
                        key = "Group3";
                        name = "Jijikrishna NG";
                        email = "principalmps@manappuramschools.com";
                       // email = "developer9@manappuramfoundation.org";
                    }
                    else if (firmId == 1 && unit == "3" && department == 1)
                    {
                        key = "Group4";
                        name = "Subhash";
                        email = "hrhead@manappuramfoundation.org";
                       // email = "developer4@manappuramfoundation.org";
                    }
                    else if (firmId == 1 && unit == "3" && department == 2)
                    {
                        key = "Group5";
                        name = "Sarika PC";
                        email = "techlead01@manappuramfoundation.org";
                     //   email = "developer5@manappuramfoundation.org";
                    }
                    else if (firmId == 1 && unit == "3" && department == 3)
                    {
                        key = "Group6";
                        name = "Rekha MP";
                        email = "ophead@manappuramfoundation.org";
                        //email = "developer7@manappuramfoundation.org";
                    }
                    else {
                        key = "Group7";
                        name = "Arya Sudarsan";
                        email = "accountshead@manappuramfoundation.org";
                       // email = "developer10@manappuramfoundation.org";
                    }

                    // Add CRF to group
                    if (!groupedCRFs.ContainsKey(key))
                        groupedCRFs[key] = new List<string>();

                    groupedCRFs[key].Add(crfId);

                    // Store recipient only once per group
                    if (!groupRecipients.ContainsKey(key))
                        groupRecipients[key] = (name, email);
                }

                // Send emails
                foreach (var group in groupedCRFs)
                {
                    string key = group.Key;
                    List<string> crfIds = group.Value;
                    string crfIdList = string.Join(", ", crfIds);

                    var (name, email) = groupRecipients[key];

                    string messageBody = $@"
                Dear {name},<br/><br/>

                This is a kind reminder that the following CRFs (Change Request Forms) are pending your recommendation:<br/><br/>

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
                }

                LogReminderSentToday_HOD();

                return Json(new { success = true, message = "Reminder emails sent to IT Heads." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }