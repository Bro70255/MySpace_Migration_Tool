function Usefeedback_dtls() {
    // Get the selected crf_id from the <select> element
    var selectedCrfId = $("#crfusrfdback").val();

    $.ajax({
        type: "GET",
        url: "/Home/Get_Usefeedback_dtls",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crf_id: selectedCrfId }, // Pass the selected crf_id as a parameter
        success: function (response) {

            var data = JSON.parse(response);
            html = data[0].Description;
            $("#crf_content").text($('<div/>').html(data[0].Description).text() || "null");
            $("#prdctn_link").val(data[0].Link || "null");
            $("#path").val(data[0].Path || "null");
        },
        error: function (error) {
            console.log("Error fetching CRF details:", error);
        }
    });
}