function SignUp() {
    var flag = 0;
    var SignUp_Details = {};
    var Employee_Code = document.getElementById("empcode").value;
    SignUp_Details.Name = document.getElementById("name").value;
    SignUp_Details.Employee_Code = document.getElementById("empcode").value;
    SignUp_Details.Unit = document.getElementById("unit").value;
    SignUp_Details.Firm = document.getElementById("firm").value;
    SignUp_Details.UserType = document.getElementById("usertype").value;
    SignUp_Details.Email = document.getElementById("email").value;
    SignUp_Details.Phone_No = document.getElementById("ph_no").value;
    SignUp_Details.Password = document.getElementById("pass").value;
    SignUp_Details.Confirm_Password = document.getElementById("cpass").value;

    if (flag === 0) {
        $.ajax({
            type: "POST",
            url: "/Home/Sign_Up",
            data: JSON.stringify({ Details: SignUp_Details }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert("SignUp Completed Successfully.");
                location.reload(); // Refresh the page
            },
            error: function (xhr, status, error) {
                // Handle error response
            }
        });
    }
}