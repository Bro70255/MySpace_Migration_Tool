function Details() {
    $.ajax({
        type: "GET",
        url: "/Home/Get_Details",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var html = '';
            var counter = 1; // Initialize counter correctly
            var data = JSON.parse(response);

            if (data.length > 0) {
                $.each(data, function (i, value) {
                    html += `<tr>
                                    <td>${counter++}</td> <!-- Increment counter -->
                                    <td style="display:none;">${value.ID}</td>
                                    <td>${value.Unit}</td>
                                    <td>${value.BANK}</td>
                                    <td>${value.ACCOUNT_NUMBER}</td>
                                    <td>${value.SIGNATURE1}</td>
                                    <td>${value.SIGNATURE2}</td>
                                    <td>${value.SIGNATURE3}</td>
                                    <td>${value.SIGNATURE4}</td>
                              </tr>`;
                });
            } else {
                html = '<tr><td colspan="9">No data found</td></tr>'; // Adjust colspan to cover all columns
            }

            $("#tbtable").empty().append(html);
        },
        error: function (xhr, status, error) {
            console.log("Error: " + error);
        }
    });
}