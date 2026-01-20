function Bind_Project() {
    var impactmodule = document.getElementById("ddlimpactingmodule").value;
    try {
        $.ajax({
            url: "/Home/Get_Bind_Project",
            type: "GET",
            data: { impactmodule: impactmodule },
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("exmodule");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].Project_name;
                        opt.value = data[i].Project_id;
                    });
                    dropdown.selectedIndex = 0;
                }
                else {
                    $('#' + impactmodule).empty();
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