function Bugs_Testlead_View() {
    $("#loading").show();
    var html = '';
    $.ajax({
        type: "GET",
        url: "/Home/Get_Bugs_Testlead_View",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = JSON.parse(response);
            $.each(data, function (i, attachment) {
                var status;
                var Resolve_date;
                if (data[i].Status == 0) {
                    status = 'Not Closed';
                    Resolve_date = '';
                }
                else if (data[i].Status == 1) {
                    status = 'Developer Closed';
                    Resolve_date = '';
                }
                else {
                    status = 'Resolved';
                    Resolve_date = (data[i].Created_Date_Time === null || data[i].Target_date === '') ? '' : formatDate(data[i].Created_Date_Time);
                }
                //  var Resolve_date = (data[i].Created_Date_Time === null || data[i].Target_date === '') ? '' : formatDate(data[i].Created_Date_Time);
                html += '<tr><td>' + data[i].Tester_Bug_Report_ID +
                    '</td><td>' + data[i].Subject +
                    '</td><td>' + data[i].Priority +
                    '</td><td>' + data[i].Severity +
                    '</td><td>' + formatDate(data[i].Created_Date_Time) +
                    '</td><td>' + status +
                    '</td><td>' + Resolve_date +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html);
            
        }
    });
}