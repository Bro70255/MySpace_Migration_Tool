function updateDevelopmentSum() {
    // Get all values in the "development" column and calculate the sum
    var sum = 0;
    $(".development-column").each(function () {
        var value = parseFloat($(this).text()) || 0; // Parse as float, default to 0 if not a number
        sum += isNaN(value) ? 0 : value;
    });

    // Display the sum in the "development" input field
    $("#development").val(sum);

    var sumCost = 0;
    $(".cost-column").each(function () {
        var value = parseFloat($(this).text()) || 0; // Parse as float, default to 0 if not a number
        sumCost += isNaN(value) ? 0 : value;
    });

    // Display the sum in the "totalcost" input field
    $("#totalcost").val(sumCost);

    // Check if there is only one row
    if ($("#tbtable1 tr").length === 1) {
        // If only one row, display the values without summing
        $("#codereview").val($("#development").val() * 0.40);
        $("#totalworkhrs").val(parseFloat($("#development").val()) + parseFloat($("#codereview").val()));
    }
}