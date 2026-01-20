function Check_Request_Date() {
    // Parse requested date
    var requested_Date_str = document.getElementById("requested_Date").textContent.trim();
    var requested_Date_parts = requested_Date_str.split('/');
    var requested_Date = new Date(requested_Date_parts[2], requested_Date_parts[1] - 1, requested_Date_parts[0]); // Format: dd/mm/yyyy

    // Parse end date
    var end_date = new Date(document.getElementById("end_date").value);

    // Adjust requested date by adding 30 days
    var adjusted_requested_Date = new Date(requested_Date);
    adjusted_requested_Date.setDate(adjusted_requested_Date.getDate() + 30);

    // Compare end date to adjusted requested date
    //if (end_date > adjusted_requested_Date) {
    //    // End date exceeds requested date + 30 days
    //    alert("End date exceeds requested date + 30 days");
    //    document.getElementById("end_date").value = '';
    //    return false; // Or handle the validation failure in your desired way
    //}
}