function Bind_Module_fordailywork() {

    try {
        $.ajax({
            url: "/Home/Get_Bind_Module_fordailywork",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var Module = document.getElementById('dailywork_module');
                    Module.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    Module.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        Module.options.add(opt);
                        opt.text = data[i].Project_name;
                        opt.value = data[i].Project_id;
                    });
                    Module.selectedIndex = 0;
                }
                else {
                    $('#' + firm).empty();
                }
            },
            error: function () {

            }
        });
    }
    catch (e) { }
}