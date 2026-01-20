function Bind_Developers_For_Dev_Wise_Report() {
    var selectElement = document.getElementById("firm");
    var Firm_Id = selectElement.options[selectElement.selectedIndex].value;

    // Now, the variable 'selectedOptionValue' holds the value of the selected option.
    try {
        $.ajax({
            url: "/Home/Bind_Developers_For_Dev_Wise_Report?Firm_Id=" + Firm_Id,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var Names = document.getElementById('ddldeveloper');
                    Names.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    Names.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        Names.options.add(opt);
                        opt.text = data[i].Name;
                        opt.value = data[i].Employee_Code;
                    });
                    Names.selectedIndex = 0; 
                }
                else {
                    $('#ddldeveloper').empty();
                }
            },
            error: function () {

            }
        });

    }
    catch (e) { }
}