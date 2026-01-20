function EndDateValidation() {
    var start_date = new Date(document.getElementById("start_date").value);
    var end_date = new Date(document.getElementById("end_date").value);

    // Ensure end date is not before start date
    if (end_date < start_date) {
        alert("End date cannot be before the start date");
        document.getElementById("end_date").value = '';
    }

    var endDateLimit = new Date(start_date);
    endDateLimit.setDate(start_date.getDate() + 5);

    //if (end_date > endDateLimit) {
    //    alert("End date must be less than start date + 5 days");
    //    document.getElementById("end_date").value = '';
    //    // You can add additional code here to handle the validation failure
    //}
}