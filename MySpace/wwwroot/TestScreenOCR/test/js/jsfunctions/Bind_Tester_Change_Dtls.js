function Bind_Tester_Change_Dtls() {

    var selectedtesterid = $("#ddltester").val();


    // Now, the variable 'selectedOptionValue' holds the value of the selected option.
    try {
        $.ajax({
            url: "/Home/Get_Bind_Tester_Change_Dtls?selectedtesterid=" + selectedtesterid,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var Names = document.getElementById('CRFtstrchnge');
                    Names.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    Names.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        Names.options.add(opt);
                        opt.text = data[i].CRF_FOR_TESTERCHANGE;
                        opt.value = data[i].crf_Id;
                    });
                    Names.selectedIndex = 0;
                }
                else {
                    $('#' + 'firm').empty();
                }
            },
            error: function () {

            }
        });

    }
    catch (e) { }
}