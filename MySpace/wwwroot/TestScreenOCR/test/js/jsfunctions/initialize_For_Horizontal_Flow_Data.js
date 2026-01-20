function initialize_For_Horizontal_Flow_Data() {
    $.ajax({
        type: "GET",
        url: "/Home/Get_For_Horizontal_Flow_Data",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (response) {
            var data = JSON.parse(response);

            $("#user_hr_flow").text(data[0].Requester);
            $("#hod_hr_flow").text(data[0].Hod_Name);
            $("#it_head_hr_flow").text(data[0].IT_Head_Name);
            $("#ceo_hr_flow").text(data[0].Head);
            /*##########################################################################################*/
            $("#user_hr_flow_blw").text(data[0].Requester);
            $("#crf_id_hr_flow").text(data[0].CRFID);
            $("#crf_id_created_date_hr_flow").text(formatDate(data[0].Requested_Date)); 
            $("#crf_subject_hr_flow").text(data[0].Subject);
            $("#techlead_hr_flow").text(data[0].TL);
            $("#developer_hr_flow").text(data[0].Developer_Name);
            $("#dvlpmt_srt_dt").text(formatDateforflow(data[0].Developer_Start_date));
            $("#dvlpmt_end_dt").text(formatDateforflow(data[0].Developer_End_date));
            $("#Tstr_nm").text(data[0].Tester_name);
            $("#Tstr_srt_dt").text(formatDateforflow(data[0].Tester_start_date));
            $("#Tstr_end_dt").text(formatDateforflow(data[0].Tester_end_date));
            $("#prjt_trt_dt").text(formatDateforflow(data[0].Prjt_trgt_date));
            
            /*    $("#crf_description").text(data[0].CRF_Description);*/
            $("#crf_description").text($('<div/>').html(data[0].CRF_Description).text() || "null");

            // Adding the 'active' class to all list items
            $("li").addClass("active");
        }
    });
}