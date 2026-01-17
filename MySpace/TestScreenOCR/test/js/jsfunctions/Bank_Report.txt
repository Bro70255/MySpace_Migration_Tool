function Bank_Report() {
    var unit = document.getElementById('unit').value; // Get the selected unit value
    var bank = document.getElementById("ddl_bank").value; // Correctly get the selected bank value
    var html = '';
    var counter = 1; // Initialize counter for Sl No

    // Log unit and bank for debugging
    ////console.log("Unit:", unit);
    ////console.log("Bank:", bank);
    // Check if Unit is 0 and Bank is "Select Bank"
    if (unit === "0" && bank === "0") {
        location.reload(); // Refresh the page if both conditions are met
        return; // Stop the function to prevent unnecessary AJAX call
    }
    $.ajax({
        type: "GET",
        url: "/Home/Get_Report_dtls",
        data: { bank: bank, unit: unit },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            // Assuming the response is already a JSON object
            var data = JSON.parse(response);
            $.each(data, function (i, value) {
                html += '<tr><td>' + counter++ + // Increment counter for each row
                    '</td><td>' + value.UNIT +
                    '</td><td>' + value.BANK +
                    '</td><td>' + value.ACCOUNT_NUMBER +
                    '</td><td>' + value.SIGNATURE1 +
                    '</td><td>' + value.SIGNATURE2 +
                    '</td><td>' + value.SIGNATURE3 +
                    '</td><td>' + value.SIGNATURE4 +
                    '</td></tr>';
            });

            $("#tbtable").empty().append(html); // Clear existing table and append new rows
        },
        error: function (xhr, status, error) {
            console.error(error); // Log any errors
        }
    });
}