function initialization_Teach_Lead_Team() {
    var html = '';
    var sl = 0;
    $.ajax({
        type: "GET",
        url: "/Home/Get_Teach_Lead_Team_Details",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response);
            $.each(data, function (i, attachment) {
                sl++;
                html += '<tr><td>' + sl +
                    '</td><td >' + data[i].Employ_Code +
                    '</td><td>' + data[i].Name +
                    '</td><td >' + data[i].Designation +                   
                    '</td><td>' + formatDate(data[i].Dev_End_Date) +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html);
        }

    });

}