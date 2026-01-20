
        private bool IsReminderAlreadySentToday()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CT"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM CRF_Email_Reminder_Log WHERE ReminderDate = CAST(GETDATE() AS DATE) AND IsSent = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }