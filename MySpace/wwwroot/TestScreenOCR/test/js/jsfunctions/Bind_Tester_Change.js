function Bind_Tester_Change() {
    try {
        $.ajax({
            url: "/Home/Get_Bind_Testers",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("testerchange");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].Name;
                        opt.value = data[i].Employ_Code;
                    });
                    dropdown.selectedIndex = 0;
                }
                else {
                    $('#' + 'firm').empty();
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