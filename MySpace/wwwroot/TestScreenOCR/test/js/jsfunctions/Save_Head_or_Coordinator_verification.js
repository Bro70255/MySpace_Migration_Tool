function Save_Head_or_Coordinator_verification(button) {
    $("#loading").show();
    var DWU_ID = $(button).data('dwu-id');

    // Rest of your code here
    $.ajax({
        type: "POST",
        url: "/Home/Save_Head_or_Coordinator_verification",
        data: JSON.stringify({ DWU_ID: DWU_ID }), // Pass DWU_ID as data
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#loading").hide();
            alert("Verified Successfully.");
            location.reload(); // Refresh the page
        },
        error: function (xhr, status, error) {
            // Handle error response
            console.error(xhr.responseText);
        }
    });
}