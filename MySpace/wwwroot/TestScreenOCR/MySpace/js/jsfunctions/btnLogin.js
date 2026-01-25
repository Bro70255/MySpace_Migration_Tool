function btnLogin() {

    let username = $("#username").val().trim();
    let password = $("#password").val().trim();

    if (!username || !password) {
        showError("Please enter username and password.");
        return;
    }

    $.ajax({
        url: "/Home/Sign_In",
        type: "POST",
        data: {
            username: username,
            password: password
        },
        success: function (res) {
            if (res.success) {
                window.location.href = "/Home/MySpace_Dashboard";
            } else {
                showError(res.message);
            }
        },
        error: function () {
            showError("Server error. Try again.");
        }
    });
}