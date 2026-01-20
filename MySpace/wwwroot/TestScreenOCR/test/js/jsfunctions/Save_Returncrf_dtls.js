function Save_Returncrf_dtls() {
    $("#loading").show();
    var return_details = {};
    var flag = 0;
    var File_Upload = document.getElementsByName('Upload_file')[0].files[0];
    var formData = new FormData();
    formData.append('File_Upload', File_Upload);

    $.ajax({
        type: "POST",
        url: "/Home/Upload_returnfile",
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        success: function (response) {
          
            return_details.Attach_file = response;
        },
    });
    return_details.selectedCrfId = document.getElementById("returncrf").value;
    return_details.remark = document.getElementById("usrremark").value;

    if (flag === 0) {
        $.ajax({
            type: "POST",
            url: "/Home/User_Edit_Detls",
            data: JSON.stringify(return_details),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#loading").hide();
                alert("Details saved Successfully.");
                location.reload(); // Refresh the page
            },
            error: function (xhr, status, error) {
                // Handle error response
            }
        });
    }
}