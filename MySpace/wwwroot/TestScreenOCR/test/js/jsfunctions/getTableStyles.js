function getTableStyles(table) {
    // Hardcoded styles for the table headings
    var headingFontWeight = "bold";
    var headingBorderStyle = "1px solid black";

    // Hardcoded styles for the table cells
    var cellBorderStyle = "1px solid black";

    // Format the inline styles as a table
    var stylesTable = "<table><tbody>";

    var headers = table.getElementsByTagName("th");
    var rows = table.rows;

    for (var i = 0; i < rows.length; i++) {
        var cells = rows[i].cells;
        stylesTable += "<tr>";
        for (var j = 0; j < cells.length; j++) {
            var cellContent = cells[j].innerText;
            var cellStyle = "border: " + cellBorderStyle + ";";

            // Check if the cell content is a date
            if (/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(cellContent)) {
                // Align dates to the left
                cellStyle += " text-align: left;";
            } else {
                // Align other content to the right
                cellStyle += " text-align: right;";
            }

            if (i === 0) {
                cellStyle += " font-weight: " + headingFontWeight + ";";
            }
            stylesTable += "<td style='" + cellStyle + "'>" + cellContent + "</td>";
        }
        stylesTable += "</tr>";
    }

    stylesTable += "</tbody></table>";

    return stylesTable;
}