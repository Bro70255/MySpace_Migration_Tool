function Update_hod_reject() {
    var selectedCrfId = $("#crf_with_sub").val();
    var remark = document.getElementById("remark").value;
    if (selectedCrfId === "0") {
        alert("Please Select Crf.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Home/Update_hod_reject",
        data: { crf_id: selectedCrfId, Remark: remark },
        dataType: "json",
        success: function (response) {
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