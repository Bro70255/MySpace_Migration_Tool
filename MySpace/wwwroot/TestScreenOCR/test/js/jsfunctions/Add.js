function Add() {
    var selectedOption = document.getElementById("crf_withsub").options[document.getElementById("crf_withsub").selectedIndex];
    var assignedWorkValue = selectedOption.value;
    var assignedWorkText = selectedOption.text;

    // Check if a value is selected
    if (!assignedWorkValue) {
        alert("Assigned Work is required");
        return;
    }

    var module_typeDropdown = $("#dailywork_module");
    var module_id = module_typeDropdown.val();
    if (module_id === "0") {
        alert("Module is required");
        return;
    }
    var Module_Type = module_typeDropdown.find("option:selected").text();

    var Descptionwrk_dropdown = $("#decp_of_work_Perfm");
    var descrption_id = Descptionwrk_dropdown.val();
    if (descrption_id === "0") {
        alert("Description of Work Performed is required");
        return;
    }
    var Descrption_Type = Descptionwrk_dropdown.find("option:selected").text();

    var completionPercentage = $("#pofc").val().trim();
    if (!completionPercentage) {
        alert("Completion Percentage is required");
        return;
    }

    // Check if completionPercentage is a valid number
    if (isNaN(completionPercentage) || parseFloat(completionPercentage) < 0 || parseFloat(completionPercentage) > 100) {
        alert("Completion Percentage must be a number between 0 and 100");
        return;
    }

    var detailedDescription = $("#DD_textarea").val().trim();
    if (!detailedDescription) {
        alert("Detailed Description is required");
        return;
    }

    // Validate Remark
    var remark = $("#dwu_remark").val().trim();
    if (!remark) {
        alert("Remark is required");
        return;
    }

    // Validate Date
    var date = document.getElementById("dwu_date").value.trim();
    if (!date) {
        alert("Date is required");
        return;
    }

    var time = $("#dwu_hours option:selected").text();
    if (time === "00") {
        alert("Hour '00' is not allowed. Please select a valid Time.");
        return;
    }

    // Construct Time Combined
    var time_min = $("#dwu_minutes option:selected").text();
    var time_combined = time + ':' + time_min;

    // Construct new row HTML
    var newRow = '<tr>' +
        '<td>' + assignedWorkText + '</td>' +
        '<td>' + Module_Type + '</td>' +
        '<td style="display: none">' + module_id + '</td>' +
        '<td>' + Descrption_Type + '</td>' +
        '<td style="display: none">' + descrption_id + '</td>' +
        '<td>' + completionPercentage + '</td>' +
        '<td>' + detailedDescription + '</td>' +
        '<td>' + remark + '</td>' +
        '<td>' + date + '</td>' +
        '<td>' + time_combined + '</td>' +
        '<td><button class="btn btn-danger btn-remove">Remove</button></td>' +
        '</tr>';

    // Append new row to the table body
    $("#tbtable").append(newRow);

    // Clear input fields after adding row
    $("#crf_withsub").val("");
    $("#dailywork_module").val("");
    $("#decp_of_work_Perfm").val("");
    $("#pofc").val("");
    $("#DD_textarea").val("");
    $("#dwu_remark").val("");

    // Remove row functionality
    $("#tbtable").on("click", ".btn-remove", function () {
        $(this).closest("tr").remove();
    });
}