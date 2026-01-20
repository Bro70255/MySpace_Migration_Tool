function Return_To_User() {
    var Crf_id = document.getElementById("ddlCRF").value;
    if (Crf_id == 0) {
        alert("Please Select CRF");
        return;
    }
    $("#loading").show();
    var Returnuser_dtls = {};
    var flag = 0;
    Returnuser_dtls.Crf_id = document.getElementById("ddlCRF").value;
    Returnuser_dtls.Remarks = document.getElementById("remark").value;
    var dataToSend = JSON.stringify({ Returnuser_dtls: Returnuser_dtls });
    $.ajax({
        type: "POST",
        url: "/Home/Return_To_User_Detls",
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#loading").hide();
            alert("Return to User Successfully.");
            location.reload(); // Refresh the page
        },
        error: function (xhr, status, error) {
            // Handle error response
        }
    });
}