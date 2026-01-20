function Save_Returncrf_dtls_validation() {
    var flag = 0;
    var selectedCrfId = document.getElementById("returncrf").value;
    var File_Upload = document.getElementsByName('Upload_file')[0].files[0];
    var formData = new FormData();


    // Validation for selectedCrfId
    if (selectedCrfId === "0") {
        alert("Select Crfid.");
        flag = 1;
        return false;
    }
    // Validation for file upload
    if (!File_Upload) {
        alert("Please select a file to upload.");
        flag = 1; // Assuming `flag` is defined elsewhere
        return false;
    }

    // If file is selected, append it to the formData
    formData.append('File_Upload', File_Upload);
    // Validation for remark

    if (flag === 0) {
        Save_Returncrf_dtls();
    }

}