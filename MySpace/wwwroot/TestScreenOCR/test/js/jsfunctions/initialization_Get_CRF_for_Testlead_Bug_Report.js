function initialization_Get_CRF_for_Testlead_Bug_Report() {

    $("#loading").show();
    var html = '';
    var sl = 0;
    $.ajax({
        type: "GET",
        url: "/Home/Get_CRF_for_Testlead_Bug_Report",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = JSON.parse(response);
         
            $.each(data, function (i, attachment) {
                sl++;
                var Click = '<a style="color:Red !important; title ="Click"  data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#contentpanel" href="Bug_Testlead_View?Crf_id=' + window.btoa('VIEW' + data[i].crf_Id) + '"</a>';
                html += '<tr><td>' + sl +
                    '</td><td >' + Click + data[i].CRF_ID_With_Subject +
                    '</td><td >' + data[i].Bug_Count +
                    '</td><td >' + data[i].Resolved_Bug_Count +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html);
          
        }

    });

}