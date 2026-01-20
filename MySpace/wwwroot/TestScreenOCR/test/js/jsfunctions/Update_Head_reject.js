function Update_Head_reject() {
    var selectedFirm = $("#firm").val();
    var selectedCrfId = $("#crf_with_sub").val();
    if (selectedFirm === "0") {
        alert("Please Select Firm.");
        return;
    }
    if (selectedCrfId === null) {
        alert("Select Crf.");
        flag = 1;
        return false;
    }
    $("#loading").show();
    var remark = document.getElementById("Head_remark").value;
    $.ajax({
        type: "POST",
        url: "/Home/Update_Head_reject",
        data: { crf_id: selectedCrfId, Remark: remark },
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = response;
            if (data == 1) {
            }
            alert("Rejected Successfull")
            location.reload();
        },
        error: function () {
            alert("Error")
        }
    });
}