function Get_CRF_for_Developer_Bug_Tracking() {
    $("#loading").show();
    var html = '';
    $.ajax({
        type: "GET",
        url: "/Home/Get_CRF_for_Developer_Bug_Tracking",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = JSON.parse(response);

            $.each(data, function (i, attachment) {
                var Click = '<a style="color:Red !important; title ="Click"  data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#contentpanel" href="Bug_View?crf_id=' + window.btoa('VIEW' + data[i].crf_Id) + '"</a>';
                var CommpletdCount = data[i].Completed_Bug_Count + data[i].Verified_Bug_Count;
                html += '<tr><td>' + Click + data[i].crf_Id +
                    '</td><td>' + data[i].Bug_Count +
                    '</td><td>' + CommpletdCount +
                    '</td><td>' + data[i].Verified_Bug_Count +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html);
        }

    });
}