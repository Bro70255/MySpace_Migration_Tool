function Update_hod_recommendation() {
    var selectedCrfId = $("#crf_with_sub").val();
    if (selectedCrfId === "0") {
        alert("Please Select Crf.");
        flag = 1;
        return false;
    }
    var remark = document.getElementById("remark").value;
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Update_hod_recommendation",
        data: { crf_id: selectedCrfId, Remark: remark },
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = response;
            if (data == 1) {
            }
            Get_CRFDetails_Send_mail_to_IT_Head(selectedCrfId);
            alert("Recommended Successfull")
           
            location.reload(); // Reload the page or perform other actions
        },
        error: function () {
            // Handle errors if needed
            $("#loading").hide(); // Hide the loading element in case of an error
        }
    });
}