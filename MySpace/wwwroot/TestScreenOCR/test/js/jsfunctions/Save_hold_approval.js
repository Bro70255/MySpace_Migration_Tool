function Save_hold_approval() {

    var selectedCrfId = $("#crf_with_subject").val();
  
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Save_hold_approval_crf",
        data: { crf_id: selectedCrfId },
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = response;
            if (data == 1) {
            }
            alert("Recommended Successfull")
            location.reload(); 
        },
        error: function () {
            // Handle errors if needed
            $("#loading").hide(); 
        }
    });
}