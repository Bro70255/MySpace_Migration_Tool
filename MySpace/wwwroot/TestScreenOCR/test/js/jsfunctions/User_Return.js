function User_Return() {
    $("#loading").show();
    var selectedCrfId = $("#returncrf").val();
    if (selectedCrfId === "0") {
        alert("Select Crf.");
        $("#loading").hide();
        flag = 1;
        return false;

    }
    var user_rejected_remark = $("#usrremark").val();
    $.ajax({
        type: "POST",
        url: "/Home/Save_User_Return",
        data: { selectedCrfId: selectedCrfId, Remark: user_rejected_remark },
        success: function (data) {
            $("#loading").hide();
            alert("Submitted Successfully.");
            location.reload(); // Refresh the page
        },
        error: function (xhr, status, error) {
            // Handle error response
        }
    });
}