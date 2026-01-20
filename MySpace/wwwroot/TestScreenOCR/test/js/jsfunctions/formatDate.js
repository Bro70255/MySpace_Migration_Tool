function formatDate(dateString) {
    var date = new Date(dateString);
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    // Pad day and month with leading zero if needed
    day = (day < 10) ? '0' + day : day;
    month = (month < 10) ? '0' + month : month;
    return day + '/' + month + '/' + year;
}