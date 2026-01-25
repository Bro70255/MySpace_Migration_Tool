function Add_Signature() {
    var unitName = parseInt(document.getElementById("ddl_unitt").value);  // Convert to integer
    var bankName = document.getElementById("ddl_bank").value;
    var accountnum = document.getElementById("acc_num").value;
    var signature = document.getElementById("ddl_sign").value;
    var signatureName = document.getElementById("signature_input").value.trim();

    if (signature.trim() === "" || signatureName === "") {
        alert("Select Signature and enter the name.");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Home/Add_Newsignature",
        data: JSON.stringify({
            unitName: unitName,
            bankName: bankName,
            accountnum: accountnum,
            signature: signature,
            signatureName: signatureName
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var data = JSON.parse(response);
            if (data == 1) {
                alert("Signature added Successfully.");
                location.reload();
            }
        }
    });
}