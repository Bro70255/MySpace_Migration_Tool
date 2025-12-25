function Initialize_Get_Bank_Information() {
    // Get the 'Id' parameter from the URL
    const urlParams = new URLSearchParams(window.location.search);
    const id = urlParams.get('Id'); // This will return the integer Id directly
    $.ajax({
        type: "GET",
        url: "/Home/Get_bankaccount_crrection_dtls", // Pass the id as a parameter
        contentType: "application/json; charset=utf-8",
        data: { id: id },
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response);
            $("#unit").val(data[0].UNIT || "");
            $("#bank").val(data[0].BANK || "");
            $("#num").val(data[0].ACCOUNT_NUMBER || "");
            $("#sign1").val(data[0].SIGNATURE1 || "");
            $("#sign2").val(data[0].SIGNATURE2 || "");
            $("#sign3").val(data[0].SIGNATURE3 || "");
            $("#sign4").val(data[0].SIGNATURE4 || "");
        },
        error: function (error) {
            console.log("Error fetching bank details:", error);
        }
    });
}