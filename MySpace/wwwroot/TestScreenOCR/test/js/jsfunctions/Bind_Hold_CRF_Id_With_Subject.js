function Bind_Hold_CRF_Id_With_Subject() {
    try {
        $.ajax({
            url: "/Home/Bind_Hold_CRF_Id_With_Subject",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("crf_with_subject");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].CRF_ID_With_Subject;
                        opt.value = data[i].CRF_ID_With_Subject;
                    });
                    dropdown.selectedIndex = 0;
                }
                else {
                    $('#' + usertype).empty();
                }
            },
            error: function () {
               
            }
        });
    } catch (e) {
       
    }
}