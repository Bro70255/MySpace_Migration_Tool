function Tester_Detls() {
    $("#loading").show();
    var flag = 0;
    var selectedCrfId = $("#CRF").val();
    var remark = $("#remark").val();
    var Tester_status = $("#ddltststs").val();

    if (flag === 0) {
        $.ajax({
            type: "POST",
            url: "/Home/Insert_Tester_dtls",
            data: JSON.stringify({ crf_id: selectedCrfId, status: Tester_status, Remark: remark }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#loading").hide();
                var data = JSON.parse(response);
                if (data[0].Result == 1) {
                    alert("Confirmed Successfully.");
                }
                else {
                    alert("Bugs are not Fixed");
                }
                location.reload(); // Refresh the page
            },
            error: function (xhr, status, error) {
                // Handle error response
                console.error("Error:", error);
            }
        });
    }
}