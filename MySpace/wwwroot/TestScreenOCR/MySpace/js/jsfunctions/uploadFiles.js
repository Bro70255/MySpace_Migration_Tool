function uploadFiles() {

    const project = getSelectedProject();

    if (!project.projectId) {
        showMessage("Please select a project", "error");
        return;
    }

    if (!selectedFiles || selectedFiles.length === 0) {
        showMessage("No files selected", "error");
        return;
    }

    const formData = new FormData();
    formData.append("projectId", project.projectId);
    formData.append("projectName", project.projectName);

    Array.from(selectedFiles).forEach(file => {
        formData.append("files", file);
    });

    $.ajax({
        url: "/Home/UploadScreenFolder",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (res) {

            if (res.success) {
                showMessage(res.message || "Upload completed successfully", "success");
                selectedFiles = [];
                $("#uploadInfo").html("");
            } else {
                showMessage(res.message || "Upload failed", "error");
            }
        },
        error: function () {
            showMessage("Server error during upload", "error");
        }
    });
}