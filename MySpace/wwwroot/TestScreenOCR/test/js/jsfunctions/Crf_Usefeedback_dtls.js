function Crf_Usefeedback_dtls() {
    // Get the selected crf_id from the <select> element
    var selectedCrfId = $("#crf_for_usrfdback").val();

    $.ajax({
        type: "GET",
        url: "/Home/Crf_dtls_for_userfeedback",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crf_id: selectedCrfId }, // Pass the selected crf_id as a parameter
        success: function (response) {
            var data = JSON.parse(response);

            // Update your labels with the received data
            html = data[0].Description;
          $("#crf_content").text($('<div/>').html(data[0].Description).text() || "null");

        },
        error: function (error) {
            console.log("Error fetching CRF details:", error);
        }
    });
}