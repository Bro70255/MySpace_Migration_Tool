function Hold_request_dtls() {
    // Get the selected crf_id from the <select> element
    var selectedCrfId = $("#crf_with_subject").val();

    $.ajax({
        type: "GET",
        url: "/Home/Get_Hold_request_dtls",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crf_id: selectedCrfId },
        success: function (response) {

            var data = JSON.parse(response);

            // Update your labels with the received data
            $("#crf_id").text(data[0].CRF_ID || "null");
            $("#hold_days").text(data[0].Total_Hold_Days || "null");
            $("#reason").text(data[0].Reason || "null");
            $("#remark").text(data[0].Remark || "null");
            $("#hold_from_date").text(data[0].Hold_start ? formatDate(data[0].Hold_start) : "null");
            $("#hold_to_date").text(data[0].Hold_end ? formatDate(data[0].Hold_end) : "null");

            $(".request-container").removeClass("hidden").css("display", "block");
        },
        error: function (error) {
            console.log("Error fetching CRF details:", error);
        }
    });
}