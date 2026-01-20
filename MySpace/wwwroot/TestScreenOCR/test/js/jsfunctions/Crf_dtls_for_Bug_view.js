function Crf_dtls_for_Bug_view() {
    $("#loading").show();
    $.ajax({
        type: "GET",
        url: "/Home/Get_Crf_dtls_for_Bug_view",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            try {
                var data = JSON.parse(response);
                $("#loading").hide();
                if (data && data.length > 0) {
                    // Update your labels with the received data
                    $("#crf_content").text($('<div/>').html(data[0].Description).text() || "null");
                    $("#it_team").text(data[0].It_team || "null");
                    $("#req_typ").text(data[0].Request_type || "null");
                    $("#developer").text(data[0].DEVELOPER || "null");
                    $("#target_date").text(data[0].Target_date ? formatDate(data[0].Target_date) : "null");
                    $("#priority").text(data[0].Priority || "null");
                    $("#developer_cmpltedate").text(data[0].End_Date ? formatDate(data[0].End_Date) : "null");
                    $("#tester").text(data[0].TESTER || "null");
                } else {
                    console.log("Empty data returned.");
                }
            } catch (error) {
                console.log("Error parsing response:", error);
            }
        },
        error: function (error) {
            console.log("Error fetching data:", error);
        }
    });
}