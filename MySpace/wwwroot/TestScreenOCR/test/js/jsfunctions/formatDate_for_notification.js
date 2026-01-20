function formatDate_for_notification(dateTimeString) {
    var date = new Date(dateTimeString);
    var now = new Date();
    var diff = Math.floor((now - date) / 1000); 

    if (diff < 60) {
        return diff + ' sec ago';
    } else if (diff < 3600) {
        return Math.floor(diff / 60) + ' min ago';
    } else if (diff < 86400) {
        return Math.floor(diff / 3600) + ' hr ago';
    } else if (diff < 2592000) {
        return Math.floor(diff / 86400) + ' day ago';
    } else if (diff < 31536000) {
        return Math.floor(diff / 2592000) + ' month ago';
    } else
    {
        return Math.floor(diff / 31536000) + ' year ago';
    }
}