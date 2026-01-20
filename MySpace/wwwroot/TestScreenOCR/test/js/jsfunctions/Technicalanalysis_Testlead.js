function Technicalanalysis_Testlead() {
    var Crf_id = document.getElementById("ddlcrf").value;
    if (Crf_id == 0) {
        alert("Please Select CRF");
        return;
    }
    var tbltable = document.getElementById("tbltable");

    //if (tbltable.rows.length == 1) {
    //    alert("Table is empty.Please add tester");
    //    return;
    //}
    var Technicalanalysis_Techleaddtls = {};
    var flag = 0;
    Technicalanalysis_Techleaddtls.Crf_id = document.getElementById("ddlcrf").value;
    Technicalanalysis_Techleaddtls.Remark = document.getElementById("remark").value;
    Technicalanalysis_Techleaddtls.Testing_Hrs = document.getElementById("testing").value;
    Technicalanalysis_Techleaddtls.Total_Work_Hrs = document.getElementById("wrk_hrs").value;
    Technicalanalysis_Techleaddtls.Total_Cost = document.getElementById("cost").value;
    Technicalanalysis_Techleaddtls.Code_review = document.getElementById("code_review").value;
    var addeddata = [];
    $("#tbtable2 tr").each(function () {
        var rowData = [];
        $(this).find("td").each(function () {
            rowData.push($(this).text());
        });
        addeddata.push(rowData);
    });
    var dataToSend = JSON.stringify({ Technicalanalysis_Techleaddtls: Technicalanalysis_Techleaddtls, Detail: addeddata });
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Technicalanalysis_Testlead_Detls",
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