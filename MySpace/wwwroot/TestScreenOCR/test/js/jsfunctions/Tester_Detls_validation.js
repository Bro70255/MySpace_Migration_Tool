function Tester_Detls_validation() {
    var selectedCrf = document.getElementById('CRF').value;
    var testStatus = document.getElementById('ddltststs').value.trim();
    var bugStatusYes = document.getElementById('yes').checked;
    var bugStatusNo = document.getElementById('no').checked;

    if (selectedCrf === '' || selectedCrf === '0') {
        alert('Please select a valid CRF.');
        return false;
    }

    if (!bugStatusYes && !bugStatusNo) {
        alert('Please select Bug Status.');
        return false;
    }

    if (testStatus === '' || testStatus === '0') {
        alert('Please select Test Status.');
        return false;
    }

    //if (remark === '') {
    //    alert('Please enter Remark.');
    //    return false;
    //}



    Tester_Detls();
    // If all validations pass, proceed with confirmation or form submission
    // You can add your logic here to confirm or submit the form
    return true;
}