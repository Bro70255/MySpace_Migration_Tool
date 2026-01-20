function Save_Tester_Bug_Report() {
    $("#loading").show();
    var Tester_Bug_Report = {};
    var flag = 0;
    var File_Upload = document.getElementsByName('Upload_file')[0].files[0];
    var formData = new FormData();
    formData.append('File_Upload', File_Upload);

    $.ajax({
        type: "POST",
        url: "/Home/Tester_bug_Report_Upload_file",
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        success: function (response) {
            Tester_Bug_Report.Attach_file = response;
        },
    });
    Tester_Bug_Report.Tracker = document.getElementById("tracker").value;
    Tester_Bug_Report.subject = document.getElementById("tester_bug_report_subject").value;
    Tester_Bug_Report.Description = CKEDITOR.instances.editor_.getData();
    Tester_Bug_Report.severity = document.getElementById("severity").value;
    Tester_Bug_Report.priority = document.getElementById("priority_").value;
    Tester_Bug_Report.Environment = document.getElementById("envnt").value;
    Tester_Bug_Report.Developer = document.getElementById("developer_for_bug_report").value;


    if (flag === 0) {
        $.ajax({
            type: "POST",
            url: "/Home/Tester_bug_Report",
            data: JSON.stringify(Tester_Bug_Report),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#loading").hide();
                alert("Submitted Successfully.");
                location.reload(); // Refresh the page
            },
            error: function (xhr, status, error) {
                // Handle error response
            }
        });
    }
}