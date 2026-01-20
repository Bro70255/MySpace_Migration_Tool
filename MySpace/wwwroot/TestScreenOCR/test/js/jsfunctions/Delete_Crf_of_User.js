function Delete_Crf_of_User(crfId) {
    $("#loading").show();
    var remarks = prompt("Please enter remark"); // Show a prompt for remarks

    if (remarks !== null) { // Check if user entered remarks or clicked cancel
        // Perform deletion with remarks
        $.ajax({
            type: "POST",
            url: "/Home/Delete_Crf_of_User",
            data: JSON.stringify({ crfId: crfId, remarks: remarks }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                  $("#loading").hide();
                var data = response;
                if (data != null) {
                    alert("Crf Deleted successfully");
                    location.reload();
                }
            }
        });
    }
}