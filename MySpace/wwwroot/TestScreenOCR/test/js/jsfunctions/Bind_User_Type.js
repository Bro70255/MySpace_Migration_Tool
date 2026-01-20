function Bind_User_Type() {
    try {
        $.ajax({
            url: "/Home/Get_Bind_UserType",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("usertype");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].User_Type;
                        opt.value = data[i].User_Type_Id;
                    });
                    dropdown.selectedIndex = 0;
                }
                else {
                    $('#' + usertype).empty();
                }
            },
            error: function () {
                // Handle error if needed
            }
        });
    } catch (e) {
        // Handle exception if needed
    }
}