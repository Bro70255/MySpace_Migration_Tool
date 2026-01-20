function Bind_Unit() {

    var firm = document.getElementById("firm").value;
    try {
        $.ajax({
            url: "/Home/Get_Bind_Unit",
            type: "GET",
            data: { firm: firm },
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("unit");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].Unit_Name;
                        opt.value = data[i].Unit_Id;
                    });
                    dropdown.selectedIndex = 0;
                }
                else {
                    $('#' + unit).empty();
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