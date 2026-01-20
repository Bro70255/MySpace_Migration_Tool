function User_Feedback_Report() {

    var firm = document.getElementById("firm").value;
    if (firm.trim() === '') {
        alert('Please Select Firm');
        return false;
    }

    var Startdate = document.getElementById("from_date").value;
    if (Startdate.trim() === '') {
        alert('Please Select From Date');
        return false;
    }

    var Enddate = document.getElementById("to_date").value;
    if (Enddate.trim() === '') {
        alert('Please Select To Date');
        return false;
    }


    var html = '';

    // Remove unnecessary check for ReportType == 'Full Report' here
    $("#loading").show();
    $.ajax({
        type: "GET",
        url: "/Home/Get_User_Feedback_Report",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: {
            Startdate: Startdate,
            Enddate: Enddate,
            firm: firm
        },
        success: function (Response) {
            var data = JSON.parse(Response);
            if (!data || data.length === 0) {
                alert('Data not found.');
                return;
            }
            $("#loading").hide();
            var serialNumber = 1; // Initialize serial number

            $.each(data, function (i, value) {
                var Avg = (data[i].Q1 + data[i].Q2 + data[i].Q3 + data[i].Q4 + data[i].Q5) / 5;
                html += '<tr><td>' + serialNumber++ + '</td>' + // Serial number column
                    '<td>' + data[i].crf_Id +
                    '</td><td>' + data[i].Subject +
                    '</td><td>' + data[i].Requested_by +
                    '</td><td>' + data[i].Techlead +
                    '</td><td>' + data[i].Developer +
                    '</td><td>' + formatDate(data[i].LIVE_DATE) +
                    '</td><td>' + data[i].Q1 +
                    '</td><td>' + data[i].Q2 +
                    '</td><td>' + data[i].Q3 +
                    '</td><td>' + data[i].Q4 +
                    '</td><td>' + data[i].Q5 +
                    '</td><td>' + Avg +
                    '</td></tr>';
            });
            // Append the table to the #tbtable element within #content
            $("#tbtable").empty().append(html);
        }
    });
}