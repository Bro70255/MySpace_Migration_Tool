        public async Task<JsonResult> Call_AI([FromBody] Blue_Print_01 request)
        {
            try
            {
                var apiKey = _configuration["Gemini:ApiKey"];

                var url =
                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

                var prompt = $@"
Screen Name: {request.ScreenName}

Screen Code:
{request.ScreenCode}

Explain what this screen does in simple words.
";

                var requestBody = new
                {
                    contents = new[]
                    {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        status = "Error",
                        message = responseText
                    });
                }

                return Json(new
                {
                    status = "Success",
                    response = responseText
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = "Exception",
                    message = ex.Message
                });
            }
        }