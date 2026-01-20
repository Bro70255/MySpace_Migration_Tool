function Update_IT_head_recommendation() {
    var selectedFirm = $("#firm").val();
    var selectedCrfId = $("#crf_with_sub").val();
    if (selectedFirm === "0") {
        alert("Please Select Firm.");
        return;
    }
    if (selectedCrfId === "0") {
        alert("Please Select Crf.");
        return;
    }

    var remark = $("#IT_remark").val(); // Use jQuery for consistency
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Update_IT_head_recommendation",
        data: { crf_id: selectedCrfId, Remark: remark },
        dataType: "json",
        success: function (response) {
            var data = response;
            if (data === 1) { // Use === for comparison
                $("#loading").hide();
            }
            alert("Recommended Successfully");
            location.reload();
        },
        error: function () {
            $("#loading").hide();
        }
    });
}