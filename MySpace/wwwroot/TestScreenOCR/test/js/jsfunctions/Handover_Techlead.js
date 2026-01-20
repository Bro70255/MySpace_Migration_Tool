function Handover_Techlead() {
    var Crf_id = document.getElementById("ddlCRF").value;
    if (Crf_id == 0) {
        alert("Please Select CRF");
        return;
    }
    var ddltechlead = document.getElementById("ddltechlead").value;
    if (ddltechlead == "0") {
        alert("Please select tech lead");
        return;
    }
    $("#loading").show();
    var Handover_dtls = {};
    var flag = 0;
    Handover_dtls.Crf_id = document.getElementById("ddlCRF").value;
    Handover_dtls.Remarks = document.getElementById("remark").value;
    Handover_dtls.Techlead2 = document.getElementById("ddltechlead").value;
    var dataToSend = JSON.stringify({ Handover_dtls: Handover_dtls });

    $.ajax({
        type: "POST",
        url: "/Home/Handover_Techlead_Detls",
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#loading").hide();
            alert("Handover Successfull.");
            location.reload(); // Refresh the page
        },
        error: function (xhr, status, error) {
            // Handle error response
        }
    });

}