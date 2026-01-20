function Bugs_Tester_View() {
    $("#loading").show();
    var html = '';
    $.ajax({
        type: "GET",
        url: "/Home/Get_Bugs_Tester_View",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response);
            $.each(data, function (i, attachment) {
                var status;
                var remark;
                if (data[i].Status == 0) {
                    status = 'Not Closed';
                    var Re_Assign = '';
                    var Resolved = '';
                }
                else {
                    status = 'Closed';
                    var Resolved = '<button class="button10" onclick ="Bug_Verified(' + "'" + data[i].Tester_Bug_Report_ID + "'" + ');" >Verify</button>';
                    var Re_Assign = '<button class="button10" onclick ="Bug_Re_Assign(' + "'" + data[i].Tester_Bug_Report_ID + "'" + ');" >Re_Assign</button>';
                }
                if (data[i].Remark == null) {
                    remark = '';
                }
                else {
                    remark = data[i].Remark;
                }
                html += '<tr><td>' + data[i].Tester_Bug_Report_ID +
                    '</td><td>' + data[i].Subject +
                    '</td><td>' + data[i].Priority +
                    '</td><td>' + data[i].Severity +
                    '</td><td>' + formatDate(data[i].Created_Date_Time) +
                    '</td><td>' + status +
                    '</td><td>' + remark +
                    '</td><td>' + Re_Assign +
                    '</td><td>' + Resolved +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html);
            $("#loading").hide();
        }
    });
}