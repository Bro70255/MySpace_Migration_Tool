function Total_crf_Report_IThead(firm) {
//    $("#loading").show();
//    var html = '';
//    $.ajax({
//        type: "GET",
//        url: "/Home/Get_Total_crf_Report_IThead",
//        contentType: "application/json; charset=utf-8",
//        data: { firm: firm },
//        dataType: "json",
//        success: function (response) {
//            $("#loading").hide();
//            var data = JSON.parse(response);
//            if (!data || data.length === 0) {
//                alert('Data not found.');
//                return;
//            }
//            $.each(data, function (i, value) {
//                // Convert null values to empty strings
//                var REQUEST_DATE = (value.Created_DateTime === null || value.Created_DateTime === '') ? '' : formatDate(value.Created_DateTime);
//                var Head_Approved_Date = (value.Head_Approved_Date === null || value.Head_Approved_Date === '') ? '' : formatDate(value.Head_Approved_Date);
//                var Target_date = (value.Target_date === null || value.Target_date === '') ? '' : formatDate(value.Target_date);
//                var Start_Date = (value.Start_Date === null || value.Start_Date === '') ? '' : formatDate(value.Start_Date);
//                var End_Date = (value.End_Date === null || value.End_Date === '') ? '' : formatDate(value.End_Date);
//                var Tester_Startdt = (value.Tester_Startdt === null || value.Tester_Startdt === '') ? '' : formatDate(value.Tester_Startdt);
//                var Tester_Enddt = (value.Tester_Enddt === null || value.Tester_Enddt === '') ? '' : formatDate(value.Tester_Enddt);
//                var Project_targetdate = (value.Project_Target_Date === null || value.Project_Target_Date === '') ? '' : formatDate(value.Project_Target_Date);
//                var RELEASE_DATE = (value.RELEASE_DATE === null || value.RELEASE_DATE === '') ? '' : formatDate(value.RELEASE_DATE);
//                var User_Acceptance_Date = (value.User_Acceptance_Date === null || value.User_Acceptance_Date === '') ? '' : formatDate(value.User_Acceptance_Date);
//                var Developer_Date = (value.Developer_Date === null || value.Developer_Date === '') ? '' : formatDate(value.Developer_Date);
//                var Developer_Enddate = (value.Developer_Enddate === null || value.Developer_Enddate === '') ? '' : formatDate(value.Developer_Enddate);
//                var Attachments = '<a style="color:Red !important;" title="Click" data-ajax-method="GET" data-ajax-mode="replace" data-ajax-update="#contentpanel" href="Attachments?crf_id=' + window.btoa('VIEW' + value.crf_Id) + '">Attachments</a>';

//                var SLNO = i + 1; // Calculate SLNO

//                // Concatenate the HTML string with conditional values
//                html += '<tr><td>' + SLNO +
//                    '</td><td>' + (value.crf_Id || '') +
//                    '</td><td>' + (value.Priority || '') +
//                    '</td><td>' + REQUEST_DATE +
//                    '</td><td>' + (value.Subject || '') +
//                    '</td><td>' + (value.REQUESTEDBY) +
//                    '</td><td>' + (value.Unit_Name || '') +
//                    '</td><td>' + (value.TECHLEAD || '') +
//                    '</td><td>' + (value.DEVELOPER || '') +
//                    '</td><td>' + (value.TESTER || '') +
//                    '</td><td>' + (Head_Approved_Date || '') +
//                    '</td><td>' + (Target_date || '') +
//                    '</td><td>' + (Start_Date || '') +
//                    '</td><td>' + (End_Date || '') +
//                    '</td><td>' + (Tester_Startdt || '') +
//                    '</td><td>' + (Tester_Enddt || '') +
//                    '</td><td>' + (Project_targetdate || '') +
//                    '</td><td>' + (value.STATUS || '') +
//                    '</td><td>' + (RELEASE_DATE || '') +
//                    '</td><td>' + (value.CRF_Total_Hrs || '') +
//                    '</td><td>' + (value.Request_Type || '') +
//                    '</td><td>' + (User_Acceptance_Date || '') +
//                    '</td><td>' + (value.Total_Cost || '') +
//                    '</td><td>' + (Developer_Date || '') +
//                    '</td><td>' + (Developer_Enddate || '') +
//                    '</td><td>' + Attachments + '</td></tr>';
//            });

//            // Append the table to the #tbtable element within #content
//            $("#tbtable").empty().append(html);
//        }
//    });
//}