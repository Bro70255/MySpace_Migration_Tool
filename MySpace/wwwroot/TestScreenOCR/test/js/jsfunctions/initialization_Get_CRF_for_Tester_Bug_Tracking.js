function initialization_Get_CRF_for_Tester_Bug_Tracking() {
    $("#loading").show();
    var html = '';
    var sl = 0;
    $.ajax({
        type: "GET",
        url: "/Home/Get_CRF_for_Tester_Bug_Tracking",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = JSON.parse(response);
            $.each(data, function (i, attachment) {
                sl++;
                var Click = '<a style="color:Red !important; title ="Click"  data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#contentpanel" href="Add_Bug?Crf_id=' + window.btoa('VIEW' + data[i].crf_Id) + '"</a>';
                var Click1 = '<a style="color:Red !important; title ="Click"  data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#contentpanel" href="Bug_Tester_View?Crf_id=' + window.btoa('VIEW' + data[i].crf_Id) + '"</a>';
                var CommpletdCount = data[i].Completed_Bug_Count + data[i].Resolved_Bug_Count;
                html += '<tr><td>' + sl +
                    '</td><td >' + Click + data[i].CRF_ID_With_Subject +
                    '</td><td >' + data[i].Bug_Count +
                    '</td><td >' + CommpletdCount +
                    '</td><td >' + data[i].Resolved_Bug_Count +
                    '</td><td >' + Click1 + "View" +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html);
        }

    });

}