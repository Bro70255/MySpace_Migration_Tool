function Save_Testcase_Updation() {
    var crf_id = $("#CRF").val();
    if (crf_id === "0") {
        alert("Select CRF ID.");
        return false;
    }

    var Remark = $("#remark").val();
    if (Remark === "") {
        alert("Enter Remark.");
        return false;
    }
    if (selectedFilesArrayTester.length === 0) {
        alert("Please select a file.");
        return false;
    }
    var formData2 = new FormData();

    // Append files to formData2
    for (var i = 0; i < selectedFilesArrayTester.length; ++i) {
        const file = selectedFilesArrayTester[i];
        if (file) {
            formData2.append("file_" + i, file);
        }
    }

    // Add additional form data if needed
    formData2.append("CRF_ID", crf_id);
    formData2.append("Remark", Remark);
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "/Home/Testcase_File_Upload",
        data: formData2,
        contentType: false,
        cache: false,
        processData: false,
        success: function (response) {
            $("#loading").hide();
           // console.log(response);
            if (response && response.length) {
                $.each(response, function (i, attachment) {
                    var html;
                    // Your existing code to process each attachment
                });
            } else {
                console.error("No attachments found in response.");
            }
            alert("Details saved Successfully.");
            location.reload(); // Refresh the page
           
        },

        error: function (error) {
            console.error("Error uploading documents:", error);
            alert("Error uploading documents. Please try again.");
        }
    });
}