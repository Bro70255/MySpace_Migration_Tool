function Save_Developer_Change_dtls() {
    var Developer = $("#ddldeveloper").val();
    if (Developer === "0") {
        alert("Select Developer.");
        flag = 1;
        return false;
    }
    var Crf_id = $("#crf").val();
    if (Crf_id === "0") {
        alert("Select Crf_id.");
        flag = 1;
        return false;
    }
    var New_Developer = $("#ddldeveloper1").val();
    if (New_Developer === "0") {
        alert("Select New Developer.");
        flag = 1;
        return false;
    }

    if (Developer == New_Developer) {
        alert("Select Another Developer");
    } else {
        $("#loading").show();
        $.ajax({
            type: "POST",
            url: "/Home/Save_Developer_Change_dtls",
            data: { Developer: Developer, Crf_id: Crf_id, New_Developer: New_Developer },
            success: function (data) {
                $("#loading").hide();
                alert("Confirmed Successfully.");
                location.reload(); // Refresh the page
            },
            error: function (xhr, status, error) {
                // Handle error response
            }
        });
    }
}