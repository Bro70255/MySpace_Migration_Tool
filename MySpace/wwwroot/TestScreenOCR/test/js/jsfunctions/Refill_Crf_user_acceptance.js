function Refill_Crf_user_acceptance() {
    // Get the selected crf_id from the <select> element
    var selectedCrfId = $("#CRF_user_acceptance").val();
    $.ajax({
        type: "GET",
        url: "/Home/Get_Crf_dtls_for_User_Acceptance",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crf_id: selectedCrfId }, // Pass the selected crf_id as a parameter
        success: function (response) {

            var data = JSON.parse(response);

            // Update your labels with the received data
            html = data[0].Description;
            $("#crf_content").text($('<div/>').html(data[0].Description).text() || "null");
            $("#refill_path").text(data[0].UAT_Path || "null");
            $("#refill_link").text(data[0].UAT_Link || "null");

            // Clear previous attachments
            $("#div_attachment").empty();

            // Make a new AJAX request for attachments
            $.ajax({
                type: "GET",
                url: "/Home/Get_Uploaded_Attachment_user_acceptance",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { crf_ID: selectedCrfId },
                success: function (Response) {
                    var attachmentsData = JSON.parse(Response);

                    $.each(attachmentsData, function (i, attachment) {
                        var html;

                        // Check if Attach_file is defined and is a string before using split
                        if (attachment.Attach_file && typeof attachment.Attach_file === 'string') {
                            var fileExtension = attachment.Attach_file.split('.').pop().toLowerCase();

                            if (fileExtension === 'pdf') {
                                // PDF file
                                html = '<div id="div' + i + 'Doc" class="" style="margin-left: 50px;border-style: solid;border-color: coral;width: 20% !important">' +
                                    '<span style="cursor: pointer;" id="close-img' + i + 'Doc" onclick="OpenImage(' + "'" + 'pdf' + i + 'Doc' + "'" + ')">View</span>' +
                                    '<embed id="pdf' + i + 'Doc" src="../../File_Upload/' + attachment.Attach_file + '" type="application/pdf" width="200" height="200">' +
                                    '</div>';
                            } else if (fileExtension === 'docx') {
                                // Word (docx) file using Office Online Viewer
                                html = '<div id="div' + i + 'Doc" class="" style="margin-left: 50px;border-style: solid;border-color: coral;width: 20% !important">' +
                                    '<span style="cursor: pointer;" id="close-img' + i + 'Doc" onclick="OpenImage(' + "'" + 'docx' + i + 'Doc' + "'" + ')">View</span>' +
                                    '<iframe id="docx' + i + 'Doc" src="https://docs.google.com/gview?url=https://crftracker.manappuramfoundation.com/File_Upload/' + attachment.Attach_file + '" width="200" height="200"></iframe>' +
                                    '</div>';
                            } else {
                                // Image file or other formats
                                html = '<div id="div' + i + 'Doc" class="" style="margin-left: 50px;border-style: solid;border-color: coral;width: 20% !important">' +
                                    '<span style="cursor: pointer;" id="close-img' + i + 'Doc" onclick="OpenImage(' + "'" + 'img' + i + 'Doc' + "'" + ')">View</span>' +
                                    '<img id="img' + i + 'Doc" src="../../File_Upload/' + attachment.Attach_file + '" width="200" height="200" class="img-id-proof"   />' +
                                    '</div>';
                            }

                            $("#div_attachment").append(html);
                        }
                    });
                },
                error: function (error) {
                    console.log("Error fetching attachments:", error);
                }
            });

        },
        error: function (error) {
            console.log("Error fetching CRF details:", error);
        }
    });
}