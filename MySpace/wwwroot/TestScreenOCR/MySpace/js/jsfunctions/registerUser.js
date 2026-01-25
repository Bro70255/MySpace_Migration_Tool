function registerUser() {

    // -------- Collect Form Data --------
    const data = {
        FirstName: $("input[placeholder='John']").val().trim(),
        LastName: $("input[placeholder='Doe']").val().trim(),
        Email: $("input[type='email']").val().trim(),
        Username: $("input[placeholder='username']").val().trim(),
        Password: $("#pwd").val(),
        ConfirmPassword: $("#cpwd").val()
    };

    // -------- Basic Required Field Validation --------
    if (!data.FirstName || !data.LastName || !data.Email ||
        !data.Username || !data.Password) {
        alert("All fields are required");
        return;
    }

    // -------- Password Length Validation --------
    if (data.Password.length < 8) {
        alert("Password must be at least 8 characters");
        return;
    }

    // -------- Password Match Validation --------
    if (data.Password !== data.ConfirmPassword) {
        alert("Passwords do not match");
        return;
    }

    // -------- AJAX Call : Register User --------
    $.ajax({
        url: "/Home/RegisterUser",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (res) {
            if (res.success) {
                alert(res.message);
                window.location.href = "/Home/MySpace_Login";
            } else {
                alert(res.message);
            }
        },
        error: function () {
            alert("Server error. Please try again.");
        }
    });
}