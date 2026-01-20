function Save_Tester_Bug_Report_Validation() {
    // Get references to the input fields
    var tracker = document.getElementById('tracker');
    var subject = document.getElementById('tester_bug_report_subject');
    var description = CKEDITOR.instances.editor_;
    var severity = document.getElementById('severity');
    var priority = document.getElementById('priority_');
    var environment = document.getElementById('envnt');
    var developer = document.getElementById('developer_for_bug_report');
    var uploadFile = document.getElementById('Upload_file');

    // Get the values of the input fields
    var trackerValue = tracker.value.trim();
    var subjectValue = subject.value.trim();
    var descriptionValue = description.getData().trim(); // Use getData() to get the content of CKEditor
    var severityValue = severity.value.trim();
    var priorityValue = priority.value.trim();
    var environmentValue = environment.value.trim();
    var developerValue = developer.value.trim();
    var uploadFileValue = uploadFile.value.trim();

    // Perform validation
    if (trackerValue === '0') {
        alert('Please select a Tracker.');
        return false;
    }
    if (subjectValue === '') {
        alert('Please enter a Subject.');
        return false;
    }
    if (descriptionValue === '') {
        alert('Please enter the Description.');
        return false;
    }
    if (severityValue === '0') {
        alert('Please select a Severity.');
        return false;
    }
    if (priorityValue === '0') {
        alert('Please select a Priority.');
        return false;
    }
    if (environmentValue === '0') {
        alert('Please select an Environment.');
        return false;
    }
    if (developerValue === '0') {
        alert('Please select a Developer.');
        return false;
    }
    if (uploadFileValue === '') {
        alert('Please attach a file.');
        return false;
    }

    // If all validations pass, you can call the Save_Tester_Bug_Report() function
    Save_Tester_Bug_Report();
    return true; // Return true to allow form submission
}