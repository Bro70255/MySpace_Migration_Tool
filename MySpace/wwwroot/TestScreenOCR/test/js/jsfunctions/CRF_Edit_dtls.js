function CRF_Edit_dtls() {
    $("#loading").show();
    try {
        $.ajax({
            url: "/Home/Get_Crf_edit_dtls",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                $("#loading").hide();
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("returncrf");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].CRF_Edit_Dtls;
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