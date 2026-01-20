function Create_Crf() {
    $("#loading").show();
    var formData1 = new FormData();
    var Crf_Details = {};
    var flag = 0;
    Crf_Details.subject = document.getElementById("subject").value;
    Crf_Details.Description = CKEDITOR.instances.editor.getData();
    Crf_Details.It_team = document.getElementById("ddlTeam").value;
    Crf_Details.Request_type = document.getElementById("ddlReqType").value;
    Crf_Details.Target_date = document.getElementById("TarDt").value;
    Crf_Details.Priority = document.getElementById("ddlpriority").value;
    Crf_Details.Select_module = document.getElementById("exmodule").value;
    Crf_Details.Choose_impctmodule = document.getElementById("ddlimpactingmodule").value;
    Crf_Details.department = document.getElementById("ddl_department").value;

    if (flag === 0) {
        $.ajax({
            type: "POST",
            url: "/Home/Crf_Detls",
            data: JSON.stringify(Crf_Details),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
               // console.log(response);
                // Assuming selectedFilesArray is defined globally and contains the selected files
                for (var i = 0; i < selectedFilesArray.length; ++i) {
                    const file = selectedFilesArray[i];
                    if (file) {
                        // Append each file to FormData
                        formData1.append("file_" + i, file);
                    }
                }

                // Assuming formData is defined elsewhere
                $.ajax({
                    type: "POST",
                    url: "/Home/Save_File_Upload",
                    data: formData1,
                    contentType: false,
                    cache: false,
                    processData: false,
                    success: function (response) {
                        //console.log(response);
                        $("#loading").hide();
                        alert("Details saved Successfully.");
                        location.reload(); // Refresh the page
                    },
                    error: function (error) {
                        console.error("Error uploading documents:", error);
                        alert("Error uploading documents. Please try again.");
                    }
                });
            },
            error: function (xhr, status, error) {
                console.error("Error saving details:", error);
                alert("Error saving details. Please try again.");
            }
        });
    }
}