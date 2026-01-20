function Save_Assit_or_techlead_verification(button) {
    $("#loading").show();
    var DWU_ID = $(button).data('dwu-id');

    $.ajax({
        type: "POST",
        url: "/Home/Save_Assit_or_techlead_verification",
        data: JSON.stringify({ DWU_ID: DWU_ID }), 
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