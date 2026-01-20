function Get_Current_Work_Assigned_Of_Developers_Report() {
    $("#loading").show();
    var Developer = document.getElementById("ddldeveloper").value;
    var last_dev_endate = document.getElementById("greatest_date").checked ? 1 : 0;

    var html = '';

    $.ajax({
        type: "GET",
        url: "/Home/Get_Current_Work_Assigned_Of_Developers_Report",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: {
            Developer: Developer,
            last_dev_endate: last_dev_endate
        },
        success: function (Response) {
            $("#loading").hide();
            var data = JSON.parse(Response);
            if (!data || data.length === 0) {
                alert('Data not found.');
                return;
            }
            $.each(data, function (i, value) {
                // Create a link for the file name
                var fileNameLink = '<a href="#" onclick="get_attached_file(\'' + (data[i].crf_Id || '') + '\')">' + (data[i].File_Name || '') + '</a>';
                var Developer_Start_Date = (data[i].Developer_Start_Date === null || data[i].Developer_Start_Date === '') ? '' : formatDate(data[i].Developer_Start_Date);
                var Developer_End_Date = (data[i].Developer_End_Date === null || data[i].Developer_End_Date === '') ? '' : formatDate(data[i].Developer_End_Date);
                var Project_Target_Date = (data[i].Project_Target_Date === null || data[i].Project_Target_Date === '') ? '' : formatDate(data[i].Project_Target_Date);

                html += '<tr><td>' + (data[i].crf_Id || '') +
                    '</td><td>' + (data[i].Subject || '') +
                    '</td><td>' + fileNameLink +
                    '</td><td>' + (data[i].Developer_Name || '') +
                    '</td><td>' + Developer_Start_Date +
                    '</td><td>' + Developer_End_Date +
                    '</td><td>' + Project_Target_Date +
                    '</td><td>' + (data[i].current_Status || '') +
                    '</td></tr>';
            });

            // Append the table to the #tbtable element within #content
            $("#tbtable").empty().append(html);
        }
    });
}