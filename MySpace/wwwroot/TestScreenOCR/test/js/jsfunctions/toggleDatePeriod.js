function toggleDatePeriod() {

    var Dev_Complete_Date = document.getElementById("dev_cmpt_dt").textContent;
    var parts = Dev_Complete_Date.split('/'); // Split the string into day, month, and year
    var day = parseInt(parts[0], 10); // Convert day part to integer
    var month = parseInt(parts[1], 10); // Convert month part to integer
    var year = parseInt(parts[2], 10); // Convert year part to integer

    // Check if it's the last day of the month
    if (day === new Date(year, month, 0).getDate()) {
        day = 1; // Reset to the first day of the next month
        if (month === 12) {
            month = 1; // Reset to January of the next year
            year++;
        } else {
            month++; // Move to the next month
        }
    } else {
        day++; // Increment the day
    }

    // Format the date
    var formattedDate = day.toString().padStart(2, '0') + '/' + month.toString().padStart(2, '0') + '/' + year;

    var Complete_Date = formattedDate;
    var parts = Complete_Date.split('/'); // Split the string into day, month, and year
    var dateObj = new Date(parts[2], parts[1] - 1, parts[0]); // Create a Date object (month is 0-indexed)
    dateObj.setDate(dateObj.getDate() + 1); // Add one day
    var formattedDate0 = dateObj.toISOString().split('T')[0]; // Convert date object to ISO string and format as YYYY-MM-DD
    document.getElementById("start_date").value = formattedDate0; // Set the value of the input field

    var ddlTester = document.getElementById('ddltester');
    var devDatePeriod = document.getElementById('dev_date_period');

    // Check if any option is selected in the dropdown
    if (ddlTester.selectedIndex !== 0) {
        devDatePeriod.style.display = 'block';
    } else {
        devDatePeriod.style.display = 'none';
    }
}