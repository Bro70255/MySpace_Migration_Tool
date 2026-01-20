function total_live_closed_this_month(firm) {
    $("#loading").show();
    var html = '';
    $.ajax({
        type: "GET",
        url: "/Home/Get_Total_live_closed_this_month",
        contentType: 'application/json; charset=utf-8',
        data: { firm: firm },
        async: false,
        success: function (Response) {
            $("#loading").hide();
           // console.log(Response); // Check the raw response
            var data = JSON.parse(Response);
            if (!data || data.length === 0) {
                alert('Data not found.');
                return;
            }
            $.each(data, function (i, value) {
                // Convert null values to empty strings
                var REQUEST_DATE = (data[i].Created_DateTime === null || data[i].Created_DateTime === '') ? '' : formatDate(data[i].Created_DateTime);
                var Head_Approved_Date = (data[i].Head_Approved_Date === null || data[i].Head_Approved_Date === '') ? '' : formatDate(data[i].Head_Approved_Date);
                var Target_date = (data[i].Target_date === null || data[i].Target_date === '') ? '' : formatDate(data[i].Target_date);
                var Start_Date = (data[i].Start_Date === null || data[i].Start_Date === '') ? '' : formatDate(data[i].Start_Date);
                var End_Date = (data[i].End_Date === null || data[i].End_Date === '') ? '' : formatDate(data[i].End_Date);
                var Tester_Startdt = (data[i].Tester_Startdt === null || data[i].Tester_Startdt === '') ? '' : formatDate(data[i].Tester_Startdt);
                var Tester_Enddt = (data[i].Tester_Enddt === null || data[i].Tester_Enddt === '') ? '' : formatDate(data[i].Tester_Enddt);
                var Project_targetdate = (data[i].Project_Target_Date === null || data[i].Project_Target_Date === '') ? '' : formatDate(data[i].Project_Target_Date);
                var RELEASE_DATE = (data[i].RELEASE_DATE === null || data[i].RELEASE_DATE === '') ? '' : formatDate(data[i].RELEASE_DATE);
                var User_Acceptance_Date = (data[i].User_Acceptance_Date === null || data[i].User_Acceptance_Date === '') ? '' : formatDate(data[i].User_Acceptance_Date);
                var Developer_Date = (data[i].Developer_Date === null || data[i].Developer_Date === '') ? '' : formatDate(data[i].Developer_Date);
                var Developer_Enddate = (data[i].Developer_Enddate === null || data[i].Developer_Enddate === '') ? '' : formatDate(data[i].Developer_Enddate);
                var Attachments = '<a style="color:Red !important; title ="Click"  data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#contentpanel" href="Attachments?crf_id=' + window.btoa('VIEW' + data[i].crf_Id) + '"</a>';

                var SLNO = i + 1; // Calculate SLNO

                // Concatenate the HTML string with conditional values
                html += '<tr><td>' + SLNO +
                    '</td><td>' + (data[i].crf_Id || '') +
                    '</td><td>' + (data[i].Priority || '') +
                    '</td><td>' + REQUEST_DATE +
                    '</td><td>' + (data[i].Subject || '') +
                    '</td><td>' + (data[i].REQUESTEDBY) +
                    '</td><td>' + (data[i].Unit_Name || '') +
                    '</td><td>' + (data[i].TECHLEAD || '') +
                    '</td><td>' + (data[i].DEVELOPER || '') +
                    '</td><td>' + (data[i].TESTER || '') +
                    '</td><td>' + (Head_Approved_Date || '') +
                    '</td><td>' + (Target_date || '') +
                    '</td><td>' + (Start_Date || '') +
                    '</td><td>' + (End_Date || '') +
                    '</td><td>' + (Tester_Startdt || '') +
                    '</td><td>' + (Tester_Enddt || '') +
                    '</td><td>' + (Project_targetdate || '') +
                    '</td><td>' + (data[i].STATUS || '') +
                    '</td><td>' + (RELEASE_DATE || '') +
                    '</td><td>' + (data[i].CRF_Total_Hrs || '') +
                    '</td><td>' + (data[i].Request_Type || '') +
                    '</td><td>' + (User_Acceptance_Date || '') +
                    '</td><td>' + (data[i].Total_Cost || '') +
                    '</td><td>' + (Developer_Date || '') +
                    '</td><td>' + (Developer_Enddate || '') +
                    '</td><td>' + Attachments + data[i].crf_Id +
                    '</td></tr>';
            });

            // Append the table to the #tbtable element within #content
            $("#tbtable").empty().append(html);
        }
    });
}