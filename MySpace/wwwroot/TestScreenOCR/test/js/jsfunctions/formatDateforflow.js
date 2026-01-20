function formatDateforflow(dateString) {
    if (!dateString) {
        return ''; // Return an empty string if the date is null or undefined
    }

    var date = new Date(dateString);
    if (isNaN(date.getTime())) {
        return ''; // Return an empty string if the date is invalid
    }

    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    // Pad day and month with leading zero if needed
    day = (day < 10) ? '0' + day : day;
    month = (month < 10) ? '0' + month : month;

    return day + '/' + month + '/' + year;
}