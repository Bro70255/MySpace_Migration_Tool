function Bind_CRF_for_Testing_TA() {
    $("#loading").show();
    try {
        $.ajax({
            url: "/Home/Get_Bind_CRF_for_Testing_TA",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                $("#loading").hide();
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("ddlcrf");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].crf_Id + "-" + data[i].Subject;
                        opt.value = data[i].crf_Id;
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