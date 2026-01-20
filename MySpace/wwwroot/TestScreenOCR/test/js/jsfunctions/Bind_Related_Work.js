function Bind_Related_Work() {
    try {
        var change_type = document.getElementById("ddlchange_type").value;

        $.ajax({
            url: "/Home/Get_Bind_Related_Work",
            type: "GET",
            data: { change_type: change_type },
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("ddlrelated_work");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].Related_Works;
                        opt.value = data[i].Related_Works_Id;
                    });
                    dropdown.selectedIndex = 0;
                }
                else {
                    $('#' + firm).empty();
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