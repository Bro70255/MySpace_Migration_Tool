function Timeline_Comparison_Report() {
    var financial_year = document.getElementById("financial_year").value;
    if (financial_year.trim() === '') {
        alert('Please Select Financial Year');
        return false;
    }

    var html = '';

    $("#loading").show();
    $.ajax({
        type: "GET",
        url: "/Home/Get_Timeline_Comparison_Report",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: {

            financial_year: financial_year

        },
        success: function (Response) {
            $("#loading").hide();
            var data = JSON.parse(Response);
            if (!data || data.length === 0) {
                alert('Data not found.');
                return;
            }
            $.each(data, function (i, value) {

                // Concatenate the HTML string with conditional values
                html += '<tr><td>' + (data[i].Month || '') +
                    '</td><td>' + (data[i].Total_Employees || '') +
                    '</td><td>' + (data[i].Total_CRF || '') +
                    '</td><td>' + ((data[i].Total_Employees && data[i].Total_CRF) ?
                        (data[i].Total_CRF / data[i].Total_Employees).toFixed(2) : '') +
                    '</td><td>' + (data[i].Closed_CRF) +
                    '</td><td>' + (data[i].Closed_CRF && data[i].Total_Employees ?
                        (data[i].Closed_CRF / data[i].Total_Employees).toFixed(2) : '') +
                    '</td><td>' + (data[i].Total_Assigned_Days || '') +
                    '</td><td>' + (data[i].Actual_Days_Taken || '') +
                    '</td><td>' + (data[i].Closed_CRF ? Math.round(data[i].Actual_Days_Taken / data[i].Closed_CRF) : '') +
                    '</td><td>' + ((data[i].Actual_Days_Taken || 0) - (data[i].Total_Assigned_Days || 0)) +
                    '</td><td>' + ((data[i].Actual_Days_Taken ? Math.round(
                        ((data[i].Actual_Days_Taken - (data[i].Total_Assigned_Days || 0)) / data[i].Actual_Days_Taken) * 100) + '%' : '')) +
                    '</td></tr>';
            });


            // Append the table to the #tbtable element within #content
            $("#tbtable").empty().append(html);

        }
    });
}