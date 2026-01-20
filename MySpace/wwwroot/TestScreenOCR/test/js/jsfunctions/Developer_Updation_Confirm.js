function Developer_Updation_Confirm() {
    var selectedCrfId = $("#crf_with_sub").val();
   
    if (selectedCrfId == 0) {
        alert("Please Select CRF");
        return;
    }
    var remark = document.getElementById("remark").value;
    var dev_status = $("#workstatus").val();
    if (dev_status == 0) {
        alert("Choose Status");
        return;
    }
    var module_name = $("#mod_name").val();
    var Tfs_name = $("#tfs_name").val();
    var Uat_link = $("#uat_link").val();
    var Uat_path = $("#uat_path").val();

    if (dev_status == 11) {

        if (!selectedCrfId) {
            alert("Please select CRF ID.");
            return;
        }
        if (!dev_status) {
            alert("Please select work status.");
            return;
        }
        $("#loading").show();
        $.ajax({
            type: "POST",
            url: "/Home/Insert_Developer_Updation",
            data: { crf_id: selectedCrfId, status: dev_status, Remark: remark },
            dataType: "json",
            success: function (response) {
                $("#loading").hide();
                var data = response;
                if (data == 1) {
                }
                alert("Updated Successfull")
                location.reload(); // Reload the page or perform other actions
            },
            error: function () {
                // Handle errors if needed
                $("#loading").hide(); // Hide the loading element in case of an error
            }
        });
    }
    else {

        if (!selectedCrfId) {
            alert("Please select CRF ID.");
            return;
        }
        if (!dev_status) {
            alert("Please select work status.");
            return;
        }
        if (!module_name) {
            alert("Please Enter module name.");
            return;
        }
        if (!Tfs_name) {
            alert("Please Enter TFS name.");
            return;
        }
        if (!Uat_link) {
            alert("Please Enter UAT link.");
            return;
        }
        if (!Uat_path) {
            alert("Please Enter UAT path.");
            return;
        }
        $("#loading").show();
        $.ajax({
            type: "POST",
            url: "/Home/Insert_Developer_complete_Updation",
            data: { crf_id: selectedCrfId, status: dev_status, Remark: remark, module_name: module_name, Tfs_name: Tfs_name, Uat_link: Uat_link, Uat_path: Uat_path },
            dataType: "json",
            success: function (response) {
                $("#loading").hide();
                var data = response;
                if (data == 1) {
                }
                alert("Updated Successfull")
                location.reload(); // Reload the page or perform other actions
            },
            error: function () {
                // Handle errors if needed
                $("#loading").hide(); // Hide the loading element in case of an error
            }
        });
    }
}