function Insert_Usefeedback_dtls() {
    var selectedCrfId = $("#crf_for_usrfdback").val();
    var production_link = document.getElementById("prdctn_link").value;
    var path = document.getElementById("path").value;
    var remark = document.getElementById("remark").value;

    /*Validation*/
   if (selectedCrfId === "0") {
        alert("Please select a CRF.");
        return; // Exit function if CRF is not selected
    }
    if (!production_link.trim()) {
        alert("Please provide a production link.");
        return; // Exit function if production link is not provided
    }
    if (!path.trim()) {
        alert("Please provide a path.");
        return; // Exit function if path is not provided
    }
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Save_Userfeedback_dtls",
        data: JSON.stringify({ crf_id: selectedCrfId, Link: production_link, Path: path, Remark: remark }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#loading").hide();
            alert("User Feedback Requested Successfully.");
            location.reload(); // Refresh the page
        },
        error: function (xhr, status, error) {
            // Handle error response
            console.error("Error:", error);
        }
    });
}