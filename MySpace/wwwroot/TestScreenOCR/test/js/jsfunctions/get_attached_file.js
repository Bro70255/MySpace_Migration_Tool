function get_attached_file(crfId) {
    $.ajax({
        type: "GET",
        url: "../Home/Get_attached_file",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crfId: crfId },
        success: function (Response) {
            var attachmentsData = JSON.parse(Response);

            // Close previously opened file viewer if exists
            if (openedFileViewer !== null) {
                openedFileViewer.remove();
                openedFileViewer = null;
            }

            // Loop through each attachment in the response
            attachmentsData.forEach(function (attachment) {
                // Check if attachment.File_Name is defined and is a string before proceeding
                if (attachment.File_Name && typeof attachment.File_Name === 'string') {
                    // Create a hidden anchor element to trigger the file download
                    var downloadLink = $('<a style="display: none;"></a>');
                    downloadLink.attr('href', '../../File_Upload/' + attachment.File_Name);
                    downloadLink.attr('download', attachment.File_Name); // You can keep this as attachment.File_Name
                    $("body").append(downloadLink);

                    // Trigger the click event to initiate the download
                    downloadLink[0].click();

                    // Remove the download link from the DOM
                    downloadLink.remove();
                }
            });
        },
        error: function (error) {
            console.log("Error fetching attachments:", error);
        }
    });
}