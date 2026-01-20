function Save_User_Acceptance() {
    $("#loading").show();
    var selectedCrfId = $("#CRF_user_acceptance").val();
    if (selectedCrfId === "0") {
        alert("Select Crf.");
        flag = 1;
        return false;
    }
    var user_acceptance_remark = $("#user_acceptance_rmk").val();

    $.ajax({
        type: "POST",
        url: "/Home/Save_User_Acceptance",
        data: { selectedCrfId: selectedCrfId, Remark: user_acceptance_remark },
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