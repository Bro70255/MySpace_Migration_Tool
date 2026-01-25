function Save_Approve_Details(button) {
    $("#loading").show();

    var ID = $(button).data('apprv_id');
    var Account_num = $(button).data('account_num');
    var sign1 = $(button).data('sign1') || null; // Handle null
    var sign2 = $(button).data('sign2') || null;
    var sign3 = $(button).data('sign3') || null;
    var sign4 = $(button).data('sign4') || null;

    // Check if ID and Account_num are present
    if (!ID || !Account_num) {
        alert("ID or Account number is missing.");
        $("#loading").hide();
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Home/Save_Approve_Dtls",
        data: JSON.stringify({ ID: ID, Account_num: Account_num, sign1: sign1, sign2: sign2, sign3: sign3, sign4: sign4 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#loading").hide();
            if (data.success) {
                alert("Approved Successfully.");
                location.reload(); // Refresh the page
            } else {
                alert("Error: " + data.message);
            }
        },
        error: function (xhr, status, error) {
           // console.error("Status: " + status + " | Error: " + error + " | Response: " + xhr.responseText);
            $("#loading").hide();
            alert("There was an error processing the request. Check console for details.");
        }
    });

}