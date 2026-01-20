function Crf_dtls_for_Developer_Updation() {
    // Get the selected crf_id from the <select> element
    var selectedCrfId = $("#crf_with_sub").val();


    $.ajax({
        type: "GET",
        url: "/Home/Crf_Dtls_for_Developer_Updation",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { crf_id: selectedCrfId }, // Pass the selected crf_id as a parameter
        success: function (response) {
            var html;
            var data = JSON.parse(response);
            var html1 = '';

            // Update your labels with the received data
            html = data[0].Description;
            $("#descrption").text($('<div/>').html(data[0].Description).text() || "null");
            $("#it_team").text(data[0].It_team);
            $("#req_typ").text(data[0].Request_type);
            $("#requester").text(data[0].UserName);
            $("#requested_date").text(formatDate(data[0].Requested_Date));
            $("#priority").text(data[0].Priority);
            $("#user_expected_date").text(formatDate(data[0].Target_date));
            $("#developer_start_date").text(formatDate(data[0].Start_Date));
            $("#developer_completion_date").text(formatDate(data[0].End_Date));
            // Clear previous attachments

            $.each(data, function (i, attachment) {

                html1 += '<tr><td>' + data[i].crf_Id +
                    '</td><td >' + data[i].Techlead +
                    '</td><td >' + data[i].Developer +
                    '</td><td >' + formatDate(data[i].Start_Date) +
                    '</td><td >' + formatDate(data[i].End_Date) +
                    '</td><td >' + data[i].Phase +
                    '</td><td >' + data[i].Changes_type +
                    '</td><td >' + data[i].Related_Works +
                    '</td><td>' + data[i].Number_of_changes +
                    '</td><td>' + data[i].Man_Hours +        
                    '</td></tr>';
                // Perform further operations with the received data
            });
            $("#tbtable").empty();
            $("#tbtable").append(html1);
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
    $.ajax({
        url: "/Home/Bind_Work_Status_Dvlpr_updation",
        type: "GET",
        dataType: 'json',
        data: { crf_id: selectedCrfId },
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (response) {
            if (response != "[]") {
                var data = JSON.parse(response);
                var dropdown = document.getElementById("workstatus");
                dropdown.length = 0;
                var opt;
                opt = document.createElement('option');
                dropdown.options.add(opt);
                opt.text = '';
                opt.value = 0;
                $.each(data, function (i, value) {
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = data[i].Status_Description;
                    opt.value = data[i].Status_id;
                });
                dropdown.selectedIndex = 0;
            }
            else {
                $('#' + 'firm').empty();
            }
        },
        error: function () {
            // Handle error if needed
        }
    });
}