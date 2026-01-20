function Bind_CRF_Id_for_IT_head() {
    $("#crf_content").text("");
    $("#it_team").text("");
    $("#req_typ").text("");
    $("#module_type").text("");
    $("#requested_date").text("");
    $("#target_date").text("");
    $("#impact_nodule").text("");
    $("#priority").text("");
    $("#req_by").text("");

    var firm = document.getElementById("firm").value;
    $("#crf_with_sub").text("");

    try {
        $.ajax({
            type: "GET",
            url: "/Home/Bind_CRF_Id_for_IT_head",
            datatype: "json",  // 'datatype' should be 'dataType'
            data: { firm: firm },
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (response) {
                if (response !== "[]") {  // Fix the condition to check if response is not an empty array
                    var data = JSON.parse(response);  // 'json.parse' should be 'JSON.parse'
                    var dropdown = document.getElementById("crf_with_sub");
                    dropdown.length = 0;
                    var option;
                    option = document.createElement('option');
                    dropdown.options.add(option);
                    option.text = '';
                    option.value = 0;
                    $.each(data, function (i, value) {
                        option = document.createElement('option');
                        dropdown.options.add(option);
                        option.text = data[i].CRF_ID_With_Subject_for_IT_head;
                        option.value = data[i].CRF_ID_With_Subject_for_IT_head;
                    });
                    dropdown.selectedIndex = 0;
                } else {
                    // It seems like you have an incomplete block here, please add your intended code.
                    // For now, I'll just add a comment.
                    // $('#' + usertype).empty();
                }
            },
            error: function () {
                // Handle the error here if needed
            }
        });
    } catch (e) {
        // Catch any exception that might occur
    }
}