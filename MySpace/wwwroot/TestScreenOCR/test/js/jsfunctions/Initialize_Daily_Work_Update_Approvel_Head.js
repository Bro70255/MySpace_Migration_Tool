function Initialize_Daily_Work_Update_Approvel_Head() {
    $("#loading").show();
    var html = '';
    $.ajax({
        type: "GET",
        url: "/Home/Get_Daily_Work_Update_Approvel_Head",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (Response) {
            $("#loading").hide();
            var data = JSON.parse(Response);
            $.each(data, function (i, value) {
                var DWU_ID = data[i].DWU_ID; // Store DWU_ID in a variable
                var APPROVE = `<button class="button10" data-dwu-id="${DWU_ID}" onclick="Save_Head_or_Coordinator_verification(this); return false;">Verify</button>`;

                html += '<tr><td>' + data[i].NAME +
                    '</td><td>' + data[i].ASSIGNED_WORKS +
                    '</td><td>' + data[i].Project_name +
                    '</td><td>' + data[i].Description_work_perform +
                    '</td><td>' + data[i].DETAILED_DESCRIPTION +
                    '</td><td>' + data[i].REMARK +
                    '</td><td>' + formatDate(data[i].DATE) +
                    '</td><td>' + data[i].DISCUSSION_TIME +
                    '</td><td>' + data[i].PERCENTAGE_OF_CPMPLETION +
                    '</td><td>' + APPROVE +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            // Append the table to the #tbtable element within #content
            $("#tbtable").empty();
            $("#tbtable").append(html);
        }
    });
}