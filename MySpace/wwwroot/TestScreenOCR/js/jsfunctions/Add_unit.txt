function Add_unit() {
    var unit = document.getElementById("addunit").value;

    if (unit.trim() === "") {
        alert("Please enter Unit");
        return false;
    }
    var isDuplicate = false;
    $.ajax({
        url: "/Home/Duplicate_unit?unit=" + unit,
        type: "GET",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (response) {
            var data = JSON.parse(response);
            if (data[0].RESULT == 1) {
                alert("Already saved this Unit.");
                location.reload();
                isDuplicate = true;
            }
        }
    });
    if (!isDuplicate) {
        $.ajax({
            type: "POST",
            url: "/Home/Add_Newunit",
            data: JSON.stringify({ unit: unit }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                var data = JSON.parse(response);
                if (data == 1) {
                    alert("Unit Added Successfully.");
                    location.reload();
                }
            }
        });
    }
}