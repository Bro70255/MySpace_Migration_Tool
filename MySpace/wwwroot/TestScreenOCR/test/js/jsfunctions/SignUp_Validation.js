function SignUp_Validation(event) {
    event.preventDefault();
    var flag = 0;

    var Name = document.getElementById("name").value;
    var Employee_Code = document.getElementById("empcode").value;
    var Unit = document.getElementById("unit").value;
    var Firm = document.getElementById("firm").value;
    var UserType = document.getElementById("usertype").selectedIndex;
    var Email = document.getElementById("email").value;
    var Phone_No = document.getElementById("ph_no").value;
    var Password = document.getElementById("pass").value;
    var Confirm_Password = document.getElementById("cpass").value;

    if (Name === '') {
        alert("Enter Name");
        flag = 1;
        return false;
    }
    if (Employee_Code === '') {
        alert("Enter Employee_Code");
        flag = 1;
        return false;
    }
    if (Email === '') {
        alert("Enter Email");
        flag = 1;
        return false;
    }
    if (UserType === 0) {
        alert("Select User Type");
        flag = 1;
        return false;
    }
    if (Firm === '0') {
        alert("Select Firm");
        flag = 1;
        return false;
    }
    if (Unit === '0') {
        alert("Select Unit");
        flag = 1;
        return false;
    }
    if (Phone_No === '') {
        alert("Enter Phone Number");
        flag = 1;
        return false;
    }
    var phoneNumberPattern = /^\d{10}$/;
    if (!phoneNumberPattern.test(Phone_No)) {
        alert("Enter a valid phone number (10 digits).");
        flag = 1;
        return false;
    }
    if (Password === '') {
        alert("Enter password");
        flag = 1;
        return false;
    }
    if (Confirm_Password === '') {
        alert("Enter Confirm Password");
        flag = 1;
        return false;
    }

    if (Password !== Confirm_Password) {
        alert("Password and Confirm Password do not match.");
        flag = 1;
        return false;
    }
    if (flag === 0) {
        SignUp();
    }
    return false;
}