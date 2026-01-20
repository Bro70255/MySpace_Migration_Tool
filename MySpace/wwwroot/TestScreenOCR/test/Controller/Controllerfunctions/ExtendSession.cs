        public ActionResult ExtendSession()
        {
            // This action will be called via AJAX to extend the session
            Session["ExtendSession"] = DateTime.Now; // Update session state to keep it alive
            return new EmptyResult();
        }