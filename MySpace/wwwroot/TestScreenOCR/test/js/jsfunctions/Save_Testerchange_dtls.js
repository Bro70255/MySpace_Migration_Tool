function Save_Testerchange_dtls() {
    var Tester_id = $("#ddltester").val();
    if (Tester_id === "0") {
        alert("Select Tester id.");
        flag = 1;
        return false;
    }
    var selected_crf = $("#CRFtstrchnge").val();
    if (selected_crf === "0") {
        alert("Select Crf.");
        flag = 1;
        return false;
    }
    var Assign_to = $("#testerchange").val();
    if (Assign_to === "0") {
        alert("Select New Tester.");
        flag = 1;
        return false;
    }
    if (Tester_id == Assign_to) {
        alert("Choose Another Tester");
    } else {
        $("#loading").show();
        $.ajax({
            type: "POST",
            url: "/Home/Save_Testerchange_dtls",
            data: { testerid: Tester_id, crf: selected_crf, new_tester: Assign_to },
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
}