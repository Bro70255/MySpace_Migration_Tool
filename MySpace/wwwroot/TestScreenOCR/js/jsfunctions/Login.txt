function Login() {
    var empcode = document.getElementById("usrname").value.trim();
    var password = document.getElementById("paswrd").value.trim();

    // Basic validation
    if (!empcode) {
        alert("Please enter your Employee code.");
        return false;
    }

    if (!password) {
        alert("Please enter your Password.");
        return false;
    }

    // AJAX call to send login details
    $.ajax({
        type: "POST",
        url: "/Home/Login",
        data: { employeeCode: empcode, loginPassword: password },
        success: function (response) {
            if (response && response.success) {
                window.location.href = response.redirectUrl; // Redirect to the Dashboard
            } else {
                alert(response.message || "Invalid login credentials.");
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