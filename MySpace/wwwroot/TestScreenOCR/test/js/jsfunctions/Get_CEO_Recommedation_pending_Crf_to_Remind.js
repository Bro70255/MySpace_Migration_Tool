function Get_CEO_Recommedation_pending_Crf_to_Remind() {

    $.ajax({
        url: "/Home/Get_CEO_Recommedation_pending_Crf_to_Remind",
        type: 'POST',
        data: {},
        success: function (response) {
            // Try parsing if response is a string
            if (typeof response === "string") {
                try {
                    response = JSON.parse(response);
                } catch (e) {
                    console.error("Invalid JSON response:", e);
                    return;
                }
            }

            // Check if valid array
            if (Array.isArray(response) && response.length > 0) {
                response.forEach(item => {
                    const crfId = item.crf_Id ?? "N/A";
                    const description = item.Description ?? "No description";

                    console.log(`CRF ID: ${crfId}, Description: ${description}`);
                });
            } else {
                console.warn(`No CRF data found for ID: ${selectedCrfId}`);
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX request failed:", status, error);
        }
    });

}