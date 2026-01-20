function Technicalanalysis_Techlead() {
    var Crf_id = document.getElementById("ddlCRF").value;
    if (Crf_id == 0) {
        alert("Please Select CRF");
        return;
    }
    var tbltable = document.getElementById("tbltable1");

    if (tbltable.rows.length == 1) {
        alert("Table is empty.Please add developers");
        return;
    }

    var Technicalanalysis_dtls = {};
    var flag = 0;
    Technicalanalysis_dtls.Crf_id = document.getElementById("ddlCRF").value;
    Technicalanalysis_dtls.Remarks = document.getElementById("remark").value;
    Technicalanalysis_dtls.Man_hour = document.getElementById("totalworkhrs").value;
    Technicalanalysis_dtls.cost_estmation = document.getElementById("totalcost").value;
    var addedData = [];

    $("#tbtable1 tr").each(function () {
        var rowData = [];
        $(this).find("td").each(function () {
            // Include developer ID from hidden input in the last column
            var developerId = $(this).find("input[name='developer-id']").val();

            if (developerId) {
                rowData.push({
                    id: developerId,
                    name: $(this).text()
                });
            } else {
                rowData.push($(this).text());
            }
        });
        addedData.push(rowData);
    });

    var dataToSend = JSON.stringify({ Technicalanalysis_dtls: Technicalanalysis_dtls, Details: addedData });
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Technicalanalysis_Techlead_Detls",
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#loading").hide();
            alert("Confirmed Successfully.");
            location.reload(); // Refresh the page
        },
        error: function (xhr, status, error) {
            // Handle error response
        }
    });
}