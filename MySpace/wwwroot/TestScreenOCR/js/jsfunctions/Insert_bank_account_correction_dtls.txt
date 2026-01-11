function Insert_bank_account_correction_dtls() {
    var correction_id = document.getElementById("id").value;// Ensure this is the correct ID for your input
    var acc_number = document.getElementById("num").value;
    var Signature1 = document.getElementById("sign1").value;
    var Signature2 = document.getElementById("sign2").value; // Ensure this is the correct type
    var Signature3 = document.getElementById("sign3").value;
    var Signature4 = document.getElementById("sign4").value;

    $.ajax({
        type: "POST",
        url: "/Home/Insert_bank_account_correction_dtls",
        data: JSON.stringify({
            correction_id: correction_id,
            acc_number: acc_number,
            Signature1: Signature1,
            Signature2: Signature2,
            Signature3: Signature3,
            Signature4: Signature4
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.success) {
                alert("Edited successfull");
                window.location.href = "/Home/Bank_Account_Management"; // Fixed URL path
            } else {
                alert(response.message || "Update failed. Please try again.");
            }
        },
        error: function (xhr, status, error) {
            alert("Error occurred while updating. Please try again.");
        }
    });
}