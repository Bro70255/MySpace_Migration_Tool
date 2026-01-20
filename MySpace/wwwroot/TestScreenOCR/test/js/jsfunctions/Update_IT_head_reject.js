function Update_IT_head_reject() {
    var selectedFirm = $("#firm").val();
    var selectedCrfId = $("#crf_with_sub").val();
    var remark = document.getElementById("IT_remark").value;
    if (selectedFirm === "0") {
        alert("Please Select Firm.");
        return;
    }
    if (selectedCrfId === "0") {
        alert("Please Select Crf.");
        return;
    }
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Update_IT_head_reject",
        data: { crf_id: selectedCrfId, Remark: remark },
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = response;
            if (data == 1) {
            }
            alert("Rejected Successfully")
            location.reload();
        },
        error: function () {
            alert("Error")
        }
    });
}