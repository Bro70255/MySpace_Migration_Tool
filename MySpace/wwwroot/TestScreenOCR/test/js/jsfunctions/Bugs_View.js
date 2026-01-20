function Bugs_View() {
    $("#loading").show();
    var html = '';
    $.ajax({
        type: "GET",
        url: "/Home/Get_Bugs_View",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = JSON.parse(response);
            $.each(data, function (i, attachment) {
                var Click1 = '<span style="cursor: pointer;" id="close-img2' + i + 'Doc" onclick="OpenImage(' + "'" + 'img' + i + 'Doc' + "'" + ')"><img src="../../images/istockphoto-1302329383-612x612.jpg" width="30" height="20" /></span>' +
                    '<img id="img' + i + 'Doc" hidden src="../../Tester_Bug_Report/' + data[i].Attach_file + '" width="160" height="200" class="img-id-proof" />';
                var Resolved = '<button class="button10" onclick ="Bug_Resolved(' + "'" + data[i].Tester_Bug_Report_ID + "'" + ');" >Resolve</button>';
                html += '<tr><td>' + data[i].Tester_Bug_Report_ID +
                    '</td><td>' + data[i].Priority +
                    '</td><td>' + data[i].Severity +
                    '</td><td>' + data[i].Subject +
                    '</td><td>' + formatDate(data[i].Created_Date_Time) +
                    '</td><td>' + Click1 +
                    '</td><td>' + Resolved +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html);
           
        }

    });
}