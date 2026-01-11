function Add_Bank() {
    var unit = document.getElementById("ddl_unit").value;
    var bank = document.getElementById("bank").value;
    if (unit.trim() === "") {
        alert("Select Unit");
        return false;
    }
    if (bank.trim() === "") {
        alert("Please enter a valid bank");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Home/Add_Newbank",
        data: JSON.stringify({ unit: unit, bank: bank }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var data = JSON.parse(response);
            if (data == 1) {
                alert("Bank added Successfully.");
                location.reload();
            }
        }
    });

}