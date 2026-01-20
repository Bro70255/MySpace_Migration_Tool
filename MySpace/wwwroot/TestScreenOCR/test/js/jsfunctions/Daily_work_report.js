function Daily_work_report() {
    var value = 0;
    if (document.getElementById("all_rpt").checked) {
        value = 1;
    }
    else if (document.getElementById("dailywork_module").value != "0") {
        value = 2;
    }
    else {
        value = 0;
    }

    var from_date = document.getElementById("from_date").value;
    var to_date = document.getElementById("to_date").value;

    if (from_date === "" || to_date === "") {
        alert("Please select both the From Date and To Date");
        return;
    }

    var developer = document.getElementById("ddldeveloper").value == "" ? 0 : document.getElementById("ddldeveloper").value;
   
    var module = document.getElementById("dailywork_module").value;

    try {
        $("#loading").show();
        $.ajax({
            url: "/Home/Get_Developer_Daily_Report?value=" + value + "&From_date=" + from_date + "&To_date=" + to_date + "&Developer=" + developer + "&Module=" + module,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                $("#loading").hide();
                var data = JSON.parse(response);
                if (!data || data.length === 0) {
                    // Show a message when data is not found
                    $("#tbtable").empty();
                    alert('Data not found');
                    return;
                }
                var html = '';
                var Status = '';
                $.each(data, function (i, item) {
                    if (item.TECH_OR_ASST_LEAD === null && item.HEAD_OR_COORDINATOR === null) {
                        Status = 'Not verified' 
                    }
                    if (item.TECH_OR_ASST_LEAD === 1 && item.HEAD_OR_COORDINATOR === null) {
                        Status = 'Verified by' + item.Status_Tech_lead;
                    }
                    if (item.HEAD_OR_COORDINATOR === 1) {
                        Status = 'Verified by' + item.Status_Tech_lead + ' and ' + item.Status_Head;
                    }

                    html += '<tr><td>' + item.NAME +
                        '</td><td>' + item.ASSIGNED_WORKS +
                        '</td><td>' + item.Description_work_perform +
                        '</td><td>' + item.DETAILED_DESCRIPTION +
                        '</td><td>' + item.REMARK +
                        '</td><td>' + formatDate(item.DATE) +
                        '</td><td>' + item.DISCUSSION_TIME +
                        '</td><td>' + item.PERCENTAGE_OF_CPMPLETION +
                        '</td><td>' + Status +
                        '</td></tr>';
                });
                $("#tbtable").empty().append(html);

                // Show the "Export to Excel" button
                $("#exportBtn").show();
            },
            error: function (xhr, status, error) {
                console.error("Error: " + error);
            }
        });
    }
    catch (e) {
        console.error("Exception: " + e);
    }
}