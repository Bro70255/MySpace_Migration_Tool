
        private void LogReminderSentToday_HOD()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CT"].ConnectionString))
            {
                string query = "INSERT INTO CRF_Email_Reminder_Log_HOD (ReminderDate, IsSent) VALUES (CAST(GETDATE() AS DATE), 1)";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }