function Save_Live_Publish() {
    $("#loading").show();
    var selectedCrfId = $("#CRF_Publish_confirm").val();
    if (selectedCrfId === "0") {
        alert("Select Crf.");
        flag = 1;
        return false;
    }
    var publish_date = $("#publish_date").val();
    var Remark = $("#live_publish_rmk").val();

    $.ajax({
        type: "POST",
        url: "/Home/Save_Live_Publish",
        data: { selectedCrfId: selectedCrfId, publish_date: publish_date, Remark: Remark},
        success: function (data) {
            $("#loading").hide();
            alert("Submitted Successfully.");
            location.reload();
        },
        error: function (xhr, status, error) {
            // Handle error response
        }
    });
}