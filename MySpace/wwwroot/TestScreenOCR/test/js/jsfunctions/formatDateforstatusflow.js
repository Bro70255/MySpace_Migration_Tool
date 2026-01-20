function formatDateforstatusflow(dateString) {
    var date = new Date(dateString);
    var day = date.getDate();
    var month = date.toLocaleString('en', { month: 'long' }); // Get month in full name
    var year = date.getFullYear();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();
    var ampm = hours >= 12 ? 'PM' : 'AM'; // Determine AM or PM

    // Convert hours from 24-hour to 12-hour format
    hours = hours % 12;
    hours = hours ? hours : 12; // Handle midnight (0 hours)

    // Pad day, hours, minutes, and seconds with leading zero if needed
    day = (day < 10) ? '0' + day : day;
    hours = (hours < 10) ? '0' + hours : hours;
    minutes = (minutes < 10) ? '0' + minutes : minutes;
    seconds = (seconds < 10) ? '0' + seconds : seconds;

    return day + ' ' + month + ' ' + year + ' ' + hours + ':' + minutes + ':' + seconds + ' ' + ampm;
}