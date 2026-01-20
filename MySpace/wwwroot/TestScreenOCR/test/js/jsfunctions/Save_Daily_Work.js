function Save_Daily_Work() {

    var addedData = [];

    // Iterate through the table rows except the header row
    $("#tbtable tr").each(function () {
        var rowData = [];
        $(this).find("td").each(function () {
            rowData.push($(this).text());
        });
        addedData.push(rowData);
    });

    // Convert the addedData array to JSON
    var dataToSend = JSON.stringify({ Details: addedData });
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Save_Daily_Work_Updation",
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#loading").hide();
            alert("Updated Successfully.");
            location.reload(); // Refresh the page
        },
        error: function (xhr, status, error) {
            // Handle error response
            console.error(xhr.responseText);
        }
    });
}