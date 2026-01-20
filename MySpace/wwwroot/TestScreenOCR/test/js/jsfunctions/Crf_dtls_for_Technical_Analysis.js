function Crf_dtls_for_Technical_Analysis() {
    // Get the selected crf_id from the <select> element
    var selectedCrfId = $("#ddlCRF").val();
    $.ajax({
        type: "GET",
        url: "/Home/Crf_dtls_for_Technical_Analysis",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crf_id: selectedCrfId }, // Pass the selected crf_id as a parameter
        success: function (response) {
            var html;
            var data = JSON.parse(response);

            // Update your labels with the received data
            html = data[0].Description;
            /*$("#descrption").text(data[0].Description || "null");*/
            $("#descrption").text($('<div/>').html(data[0].Description).text() || "null");
            $("#itteam").text(data[0].It_team);
            $("#request_type").text(data[0].Request_type);
            $("#module_type").text(data[0].Module_name);
            $("#requested_by").text(data[0].UserName);
            $("#requested_Date").text(formatDate(data[0].Requested_Date));
            $("#recommended_by").text(data[0].HOD);
            $("#recommended_comments").text(data[0].HOD_Remark);
            $("#approved_by").text(data[0].Head);
            $("#approved_Comments").text(data[0].Head_Remark);
            $("#requested_date").text(formatDate(data[0].Requested_Date));
            $("#target_date").text(formatDate(data[0].Target_date));
            $("#priority").text(data[0].Priority);
            // Clear previous attachments
            $("#div_attachment").empty();

            // Make a new AJAX request for attachments
            $.ajax({
                type: "GET",
                url: "/Home/Get_Uploaded_Attachment",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { crf_id: selectedCrfId },
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
                           //console.log("Reached the attachment appending part");
                            //console.log("HTML to append:", html);
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