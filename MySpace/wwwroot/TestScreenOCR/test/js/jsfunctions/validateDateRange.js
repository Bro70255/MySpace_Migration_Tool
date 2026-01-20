function validateDateRange() {
    var holdFromDate = document.getElementById('holdFrom').value;
    var holdEndDate = document.getElementById('holdEnd').value;

    if (holdFromDate && holdEndDate) {
        var fromDate = new Date(holdFromDate);
        var endDate = new Date(holdEndDate);

        // Calculate the difference in time (milliseconds) and convert to days
        var diffTime = Math.abs(endDate - fromDate);
        var diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

        // Check if the difference exceeds 30 days
        if (diffDays > 30) {
            alert("The date range between 'Hold From' and 'End' cannot exceed 30 days.");
            document.getElementById('holdEnd').value = ''; // Clear the invalid end date
        }
    }
}