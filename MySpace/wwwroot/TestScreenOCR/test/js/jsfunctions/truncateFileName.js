function truncateFileName(fileName, maxLength) {
    if (fileName.length > maxLength) {
        return fileName.substring(0, maxLength) + '...';
    }
    return fileName;
}