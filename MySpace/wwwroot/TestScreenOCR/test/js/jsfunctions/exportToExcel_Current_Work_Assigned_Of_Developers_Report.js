function exportToExcel_Current_Work_Assigned_Of_Developers_Report() {
    // Fetch the table element
    var table = document.getElementById("tbltable");

    // Create an HTML string with the table and its styles and data
    var htmlContent = '<html><head><style>' + getTableStyles(table) + '</style></head></html>';

    // Create a Blob from the HTML content
    var blob = new Blob([htmlContent], { type: "application/vnd.ms-excel" });

    // Generate a file name
    var fileName = "Current_Work_Assigned_Of_Developers_Report.xls";

    // Create a download link
    var link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = fileName;

    // Append the link to the document body
    document.body.appendChild(link);

    // Trigger the download
    link.click();

    // Clean up
    document.body.removeChild(link);
}