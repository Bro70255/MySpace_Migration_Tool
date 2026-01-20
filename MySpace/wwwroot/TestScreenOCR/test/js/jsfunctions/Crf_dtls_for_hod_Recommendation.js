function Crf_dtls_for_hod_Recommendation() {
    // Get the selected crf_id from the <select> element
    var selectedCrfId = $("#crf_with_sub").val();

    $.ajax({
        type: "GET",
        url: "/Home/Crf_dtls_for_hod_Recommendation",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crf_id: selectedCrfId }, // Pass the selected crf_id as a parameter
        success: function (response) {
            // No need to parse the response if dataType is json
            var data = JSON.parse(response);

            // Update your labels with the received data

            $("#crf_content").text($('<div/>').html(data[0].Description).text() || "null");
            $("#it_team").text(data[0].It_team || "null");
            $("#req_typ").text(data[0].Request_type || "null");
            $("#module_type").text(data[0].Project_name || "null");
            $("#requested_date").text(data[0].Requested_Date ? formatDate(data[0].Requested_Date) : "null");
            $("#target_date").text(data[0].Target_date ? formatDate(data[0].Target_date) : "null");
            $("#impact_nodule").text(data[0].Module_name || "null");
            $("#priority").text(data[0].Priority || "null");
            $("#req_by").text(data[0].Name || "null");
        },
        error: function (error) {
            console.log("Error fetching CRF details:", error);
        }
    });
}