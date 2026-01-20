function Create_Crf_validation() {
    var flag = 0;

    var subject = document.getElementById("subject").value;
    var Description = CKEDITOR.instances.editor.getData();
    var It_team = document.getElementById("ddlTeam").value;
    var Request_type = document.getElementById("ddlReqType").value;
    var Choose_impctmodule = document.getElementById("ddlimpactingmodule").value;
    var Target_date = document.getElementById("TarDt").value;
    var Priority = document.getElementById("ddlpriority").value;
    /*var Attach_file = document.getElementById("Upload_file").value;*/
    // Check if 'select_department' div is visible
    var selectDeptDiv = document.getElementById("select_department");
    var isDeptDivVisible = window.getComputedStyle(selectDeptDiv).display !== "none";

    // Validate the fields
    if (subject === "") {
        alert("Please Enter Subject.");
        flag = 1;
        return false;
    }

    if (Description === "") {
        alert("Please Enter Description.");
        flag = 1;
        return false;
    }

    if (It_team === "0") {
        alert("Please Select It team.");
        flag = 1;
        return false;
    }

    if (Request_type === "0") {
        alert("Please Enter Request Type.");
        flag = 1;
        return false;
    }
    if (Choose_impctmodule === "0") {
        alert("Please Select ImpactModule.");
        flag = 1;
        return false;
    }
    
    if (Target_date === "") {
        alert("Please Enter Target date.");
        flag = 1;
        return false;
    }

    if (Priority === "SelectPriority") {
        alert("Please Select the Priority.");
        flag = 1;
        return false;
    }

    // If visible, validate department
    if (isDeptDivVisible) {
        var department = document.getElementById("ddl_department").value;
        if (department === "0") {
            alert("Select department");
            flag = 1;
            return false;
        }
    }

    //if (Attach_file === "") {
    //     alert("Please Select File.");
    //     flag = 1;
    //     return false;
    // }
    if (flag === 0) {
        Create_Crf();
    }
}