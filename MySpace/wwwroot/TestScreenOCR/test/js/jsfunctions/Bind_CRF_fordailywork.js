function Bind_CRF_fordailywork() {
    try {
        $.ajax({
            url: "/Home/Bind_CRF_for_Dailywork",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("crf_withsub");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].CRF_ID_With_Subject_dailywork;
                        opt.value = data[i].Crf_id;
                    });
                    dropdown.selectedIndex = 0;
                }
                else {
                    $('#' + 'firm').empty();
                }
            },
            error: function () {
              
            }
        });
    } catch (e) {
        
    }
}