function update_Head_Approval() {
  
    var selectedFirm = $("#firm").val();
    var selectedCrfId = $("#crf_with_sub").val();
    if (selectedFirm === "0") {
        alert("Please Select Firm.");
        return;
    }
    if (selectedCrfId === "0") {
        alert("Select Crf.");
        flag = 1;
        return false;
    }
    var remark = document.getElementById("Head_remark").value;
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/update_Head_Approval",
        data: { crf_id: selectedCrfId, Remark: remark },
        dataType: "Json",
        success: function (response) {
            $("#loading").hide();
            var data = response;
            if (data = 1) {
            }
            alert("Approved Successfull")
            location.reload();
        },
        error: function () {
            alert("Error");
        }
    });
}