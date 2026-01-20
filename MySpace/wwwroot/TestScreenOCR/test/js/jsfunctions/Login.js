function Login() {
    var Login_Details = {};
    Login_Details.Empcode = document.getElementById("username").value;
    Login_Details.Login_password = document.getElementById("password").value;
    var flag = 0;
    var Username = document.getElementById("username").value;
    var Login_password = document.getElementById("password").value;

    if (Username === '') {
        alert("Enter Username");
        flag = 1;
        return false;
    }
    if (Login_password === '') {
        alert("Enter Password");
        flag = 1;
        return false;
    }
    // AJAX call to send login details
    $.ajax({
        type: "POST",
        url: "/Home/Log_In",
        data: {
            employeeCode: Login_Details.Empcode,
            loginPassword: Login_Details.Login_password
        },
        success: function (response) {
            try {
                // Check if the response is a valid object
                if (response && typeof response === "object") {
                    if (response.success) {
                        window.location.href = response.redirectUrl; // Redirect to the Dashboard
                    } else {
                        alert(response.message || "Invalid login credentials."); // Show error message
                    }
                } else {
                    throw new Error("Invalid response format.");
                }
            } catch (e) {
                alert(e.message || "Unexpected response format.");
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX error:", {
                status: status,
                error: error,
                response: xhr.responseText
            });
            alert("An error occurred while logging in. Please try again later.");
        }
    });

    return false; // Prevent default form submission
}