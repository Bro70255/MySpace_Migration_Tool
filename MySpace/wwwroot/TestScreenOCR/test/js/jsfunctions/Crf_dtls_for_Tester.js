function Crf_dtls_for_Tester() {
    // Get the selected crf_id from the <select> element
    var selectedCrfId = $("#CRF").val();
    $.ajax({
        type: "GET",
        url: "/Home/Get_Crf_dtls_for_Tester",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crf_id: selectedCrfId }, // Pass the selected crf_id as a parameter
        success: function (response) {
            var html;
            var data = JSON.parse(response);
            var html1 = '';

            // Update your labels with the received data
            html = data[0].Description;
            $("#crf_content").text($('<div/>').html(data[0].Description).text() || "null");
            $("#itteam").text(data[0].It_team);
            $("#req_typ").text(data[0].Request_type);
            $("#developer").text(data[0].Developer);
            $("#usr_expcted_dt").text(formatDate(data[0].Target_date));
            $("#priority").text(data[0].Priority);
            $("#dvlopmnt_cmpleted_dt").text(formatDate(data[0].End_Date));

            $.each(data, function (i, attachment) {

                html1 += '<tr><td>' + data[i].Testlead +
                    '</td><td >' + data[i].Project_Type_Name +
                    '</td><td >' + data[i].Phase_name +
                    '</td><td >' + formatDate(data[i].Tester_Startdt) +
                    '</td><td >' + formatDate(data[i].Tester_Enddt) +
                    '</td><td >' + data[i].Man_Hours +
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html1);
            $("#div_attachment").empty();

            $.ajax({
                type: "GET",
                url: "/Home/Get_Uploaded_Attachment",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { crf_id: selectedCrfId },
                success: function (attachmentResponse) {
                    var attachmentsData = JSON.parse(attachmentResponse);

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