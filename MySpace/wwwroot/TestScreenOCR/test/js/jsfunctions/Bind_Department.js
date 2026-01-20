function Bind_Department() {
    try {
        $.ajax({
            url: "/Home/Get_Bind_Department",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("ddl_department");
                    dropdown.length = 0;

                    // Add default empty option
                    var opt = document.createElement('option');
                    opt.text = '';
                    opt.value = 0;
                    dropdown.options.add(opt);

                    var hasUnit3 = false; // Flag to check if any item has unit == 3
                    $.each(data, function (i, value) {
                        if (value.unit == 3) {
                            hasUnit3 = true;
                            var opt = document.createElement('option');
                            opt.text = value.Department;
                            opt.value = value.Dept_Id;
                            dropdown.options.add(opt);
                        }
                    });

                    if (hasUnit3) {
                        $("#select_department").show();
                    } else {
                        // Optionally hide if no unit 3 found
                        $("#select_department").hide();
                    }

                    dropdown.selectedIndex = 0;
                } else {
                    $('#' + 'ddl_department').empty();
                    $("#select_department").show();
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