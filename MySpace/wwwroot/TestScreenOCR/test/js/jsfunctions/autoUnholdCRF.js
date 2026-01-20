function autoUnholdCRF() {
    $.ajax({
        type: "GET",
        url: "/Home/Auto_Unhold_CRF",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //var data = JSON.parse(response);
        }
    });
}