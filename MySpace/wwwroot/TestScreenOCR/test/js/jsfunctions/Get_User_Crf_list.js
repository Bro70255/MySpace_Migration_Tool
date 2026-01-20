function Get_User_Crf_list() {
    $("#loading").show();
    var html = '';
    $.ajax({
        type: "GET",
        url: "/Home/Get_User_Crf_list",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#loading").hide();
            var data = JSON.parse(response);

            // Sort the data based on status, placing "CRF Created" rows first
            data.sort(function (a, b) {
                if (a.Status_Description === "CRF Created" && b.Status_Description !== "CRF Created") {
                    return -1; // "CRF Created" comes first
                } else if (a.Status_Description !== "CRF Created" && b.Status_Description === "CRF Created") {
                    return 1; // "CRF Created" comes second
                } else {
                    return 0; // No change in order for other statuses
                }
            });

            $.each(data, function (i, attachment) {
                var Click = '<span style="cursor: pointer;" id="close-img' + i + 'Doc" onclick="OpenImage(' + "'" + 'img' + i + 'Doc' + "'" + ')"><img src="../../images/istockphoto-1302329383-612x612.jpg" width="30" height="20" /></span>' +
                    '<img id="img' + i + 'Doc" hidden src="../../File_Upload/' + data[i].File_Name + '" width="160" height="200" class="img-id-proof" />';

                var Delete = '';
                if (data[i].Status_Description === "CRF Created") {
                    Delete = '<a style="color:Red !important; title="Click" data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#contentpanel" href="#" onclick="Delete_Crf_of_User(\'' + data[i].crf_Id + '\')">Delete</a>';
                }

                html += '<tr><td>' + data[i].crf_Id +
                    '</td><td>' + data[i].Subject +
                    '</td><td>' + Click +
                    '</td><td>' + data[i].Status_Description +
                    '</td><td>' + data[i].Created_DateTime +
                    '</td><td>' + Delete +
                    '</td></tr>';
            });

            $("#tbtable").empty();
            $("#tbtable").append(html);
        }
    });
}