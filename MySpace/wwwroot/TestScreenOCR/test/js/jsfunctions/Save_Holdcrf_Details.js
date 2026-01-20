function Save_Holdcrf_Details() {
    var CRFid = document.getElementById("crfID").value.trim();
    var Hold_from = document.getElementById("holdFrom").value.trim();
    var Hold_end = document.getElementById("holdEnd").value.trim();
    var Reason = document.getElementById("reason").value.trim();
    var Remark = document.getElementById("remark").value.trim();

    if (Hold_from === "") {
        alert("Select the Hold From date");
        return;
    }

    if (Hold_end === "") {
        alert("Select the Hold End date");
        return;
    }

    if (Reason === "") {
        alert("Enter the Reason");
        return;
    }

    if (Remark === "") {
        alert("Enter the Remark");
        return;
    }

    var holdCrfDetails = {
        CRFid: CRFid,
        Hold_from: Hold_from,
        Hold_end: Hold_end,
        Reason: Reason,
        Remark: Remark
    };

    $.ajax({
        type: "POST",
        url: "/Home/Save_Hold_CRF_Details",
        data: JSON.stringify(holdCrfDetails), // Send data as JSON string
        contentType: "application/json; charset=utf-8", 
        dataType: "json",
        success: function (data) {
            alert("Request Submitted Successfully");
            
            location.reload(); 
        },
        error: function (xhr, status, error) {
            console.error("Error occurred: " + error);
            alert("An error occurred while saving the details. Please try again.");
        }
    });
}