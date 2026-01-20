function Bind_Description_of_work() {
    try {
        $.ajax({
            url: "/Home/Get_Bind_Description_of_work",
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response != "[]") {
                    var data = JSON.parse(response);
                    var dropdown = document.getElementById("decp_of_work_Perfm");
                    dropdown.length = 0;
                    var opt;
                    opt = document.createElement('option');
                    dropdown.options.add(opt);
                    opt.text = '';
                    opt.value = 0;
                    $.each(data, function (i, value) {
                        opt = document.createElement('option');
                        dropdown.options.add(opt);
                        opt.text = data[i].Description_work_perform;
                        opt.value = data[i].des_id;
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