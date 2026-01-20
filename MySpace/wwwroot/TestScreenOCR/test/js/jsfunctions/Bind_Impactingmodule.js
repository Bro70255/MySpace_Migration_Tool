function Bind_Impactingmodule() {
    var team = document.getElementById("ddlTeam").value;
    try {
        $.ajax({
            url: "/Home/Get_Bind_Impactingmodule",
            type: "GET",
            data: { team: team },
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("ddlimpactingmodule");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].Module_name;
                        opt.value = data[i].Module_id;
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