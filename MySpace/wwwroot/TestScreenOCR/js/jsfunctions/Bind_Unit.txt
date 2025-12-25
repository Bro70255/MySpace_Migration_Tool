function Bind_Unit(dropdownId) {
    try {
        $.ajax({
            url: "/Home/Get_Bind_Unit",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById(dropdownId);
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].BranchName;
                        opt.value = data[i].Branch_ID;
                    });
                    dropdown.selectedIndex = 0;
                }
                else {
                    $('#' + dropdownId).empty();
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