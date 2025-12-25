function Report() {
    try {
        $.ajax({
            type: "GET",
            url: "/Home/Get_Report",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var html = '';

                // No need to parse the response if dataType is set to 'json'
                var data = JSON.parse(response);

                // Check if data exists and has content
                if (!data || data.length === 0) {
                    alert('Data not found.');
                    location.reload();
                    return;
                }

                // Loop through data and construct table rows
                $.each(data, function (i, value) {
                    var Edit = '';
                    // Conditionally display the Edit link if userType == 1
                    if (userType == "1" || userType == "2") {
                        Edit = '<a style="color:Red !important;" title="View" data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#contentpanel" href="Bank_Account_crrction?Id=' + data[i].ID + '&Screen=Bank_Account_crrction">Edit</a>';
                    }


                    html += '<tr><td>' + (i + 1) + '</td>' +
                        '<td style="display:none;">' + (value.ID || '') + '</td>' + // Hides the ID column
                        '<td>' + (value.Unit || '') + '</td>' +
                        '<td>' + (value.BANK || '') + '</td>' +
                        '<td>' + (value.ACCOUNT_NUMBER || '') + '</td>' +
                        '<td>' + (value.SIGNATURE1 || '') + '</td>' +
                        '<td>' + (value.SIGNATURE2 || '') + '</td>' +
                        '<td>' + (value.SIGNATURE3 || '') + '</td>' +
                        '<td>' + (value.SIGNATURE4 || '') + '</td>' +
                        '<td>' + Edit + '</td></tr>';
                });

                // Update the table content
                $("#tbtable").empty().append(html);
            },
            error: function (xhr, status, error) {
                //console.error("Error occurred: " + xhr.responseText);
            }
        });
    } catch (e) {
        console.log("Exception: " + e.message);
    }
}