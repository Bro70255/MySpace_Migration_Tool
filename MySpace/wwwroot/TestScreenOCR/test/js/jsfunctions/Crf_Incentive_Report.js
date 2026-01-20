function Crf_Incentive_Report() {
    var firm = document.getElementById("firm").value;
    if (firm.trim() === '') {
        alert('Please Select Firm');
        return false;
    }
    var Startdate = document.getElementById("startdate_incentive").value;
    if (Startdate.trim() === '') {
        alert('Please enter Startdate.');
        return false;
    }

    var Enddate = document.getElementById("enddate_incentive").value;
    if (Enddate.trim() === '') {
        alert('Please enter Enddate.');
        return false;
    }
    var startdate = new Date(Startdate);
    var enddate = new Date(Enddate)

    if (startdate > enddate) {
        alert('End Date must be greater than or equal to Start Date.');
        return;
    }

    var html = '';

    // Remove unnecessary check for ReportType == 'Full Report' here
    $("#loading").show();
    $.ajax({
        type: "GET",
        url: "/Home/Get_crf_Incentive_Report",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: {
            Startdate: Startdate,
            Enddate: Enddate,
            firm: firm
        },
        success: function (Response) {
            $("#loading").hide();
            var data = JSON.parse(Response);
            if (!data || data.length === 0) {
                alert('Data not found.');
                return;
            }
            $.each(data, function (i, value) {
                // Convert null values to empty strings
                var REQUEST_DATE = (data[i].Created_DateTime === null || data[i].Created_DateTime === '') ? '' : formatDate(data[i].Created_DateTime);
                var Head_Approved_Date = (data[i].Head_Approved_Date === null || data[i].Head_Approved_Date === '') ? '' : formatDate(data[i].Head_Approved_Date);
                var Target_date = (data[i].TA_Target_Date === null || data[i].TA_Target_Date === '') ? '' : formatDate(data[i].TA_Target_Date);
                var Start_Date = (data[i].TA_Start_Date === null || data[i].TA_Start_Date === '') ? '' : formatDate(data[i].TA_Start_Date);
                var End_Date = (data[i].End_Date === null || data[i].End_Date === '') ? '' : formatDate(data[i].End_Date);
                var Tester_Startdt = (data[i].Tester_Startdt === null || data[i].Tester_Startdt === '') ? '' : formatDate(data[i].Tester_Startdt);
                var Tester_Enddt = (data[i].Tester_Enddt === null || data[i].Tester_Enddt === '') ? '' : formatDate(data[i].Tester_Enddt);
                var Project_targetdate = (data[i].Project_Target_Date === null || data[i].Project_Target_Date === '') ? '' : formatDate(data[i].Project_Target_Date);
                var RELEASE_DATE = (data[i].RELEASE_DATE === null || data[i].RELEASE_DATE === '') ? '' : formatDate(data[i].RELEASE_DATE);
                var User_Acceptance_Date = (data[i].User_Acceptance_Date === null || data[i].User_Acceptance_Date === '') ? '' : formatDate(data[i].User_Acceptance_Date);
                var Developer_Date = (data[i].Developer_Date === null || data[i].Developer_Date === '') ? '' : formatDate(data[i].Developer_Date);
                var Developer_Enddate = (data[i].Developer_Enddate === null || data[i].Developer_Enddate === '') ? '' : formatDate(data[i].Developer_Enddate);
                var Tester_StartDate = (data[i].Tester_Start_Date === null || data[i].Tester_Start_Date === '') ? '' : formatDate(data[i].Tester_Start_Date);
                var Tester_EndDate = (data[i].Tester_End_Date === null || data[i].Tester_End_Date === '') ? '' : formatDate(data[i].Tester_End_Date);


                // Concatenate the HTML string with conditional values
                html += '<tr><td>' + (data[i].crf_Id || '') +
                    '</td><td>' + (data[i].Priority || '') +
                    '</td><td>' + REQUEST_DATE +
                    '</td><td>' + (data[i].Subject || '') +
                    '</td><td>' + (data[i].Project_name || '') +
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
                    '</td><td>' + (data[i].STATUS || '0') +
                    '</td><td>' + (RELEASE_DATE || '0') +
                    '</td><td>' + (data[i].CRF_Total_Hrs || '0') +
                    '</td><td>' + (data[i].Request_Type || '0') +
                    '</td><td>' + (User_Acceptance_Date || '0') +
                    '</td><td>' + (data[i].Total_Cost || '0') +
                    '</td><td>' + (Developer_Date || '0') +
                    '</td><td>' + (Developer_Enddate || '0') +
                    '</td><td>' + (Tester_StartDate || '0') +
                    '</td><td>' + (Tester_EndDate || '0') +
                    '</td><td>' + (data[i].QA_Complexity || '0') +
                    '</td><td>' + (data[i].CRF_Complexity || '0') +
                    '</td><td>' + (data[i].Module_Complexity || '0') +
                    '</td><td>' + (data[i].Q1 || '0') +
                    '</td><td>' + (data[i].Q2 || '0') +
                    '</td><td>' + (data[i].Q3 || '0') +
                    '</td><td>' + (data[i].Q4 || '0') +
                    '</td><td>' + (data[i].Q5 || '0') +
                    '</td><td>' + (data[i].Avg_Feedback || '') +
                    '</td><td>' + (data[i].Std_Days || '') +
                    '</td><td>' + (data[i].Actual_days || '') +
                    '</td><td>' + (data[i].Developer_side_delayor_Savings || '0') +
                    '</td><td>' + (data[i].Target_days || '0') +
                    '</td><td>' + (data[i].Total_Delay_Savings_without_38_days || '0') +
                    '</td><td>' + (data[i].delay_38_days || '0') +
                    '</td><td>' + (data[i].Total_Delay_Savings_with_38_days || '0') +
                    '</td><td>' + (data[i].Saving_Percentage_Text || '0') +
                    '</td><td>' + (data[i].Module_Complexity_W || '0') +
                    '</td><td>' + (data[i].Change_in_Critical_Module_Complexity || '0') +
                    '</td><td>' + (data[i].As_per_days_Complexity || '0') +
                    '</td><td>' + (data[i].User_Feedback_W || '0') +
                    '</td><td>' + (data[i].Pull_Back_and_Up_W || '0') +
                    '</td><td>' + (data[i].Bug_W || '0') +
                    '</td><td>' + (data[i].Total_W || '0') +
                    '</td><td>' + (data[i].Incentive_Per_Person || '0') +
                    '</td><td>' + (data[i].Final_Incentive || '0') +
                    '</td><td>' + (data[i].Savings_W_for_Tech || '0') +
                    '</td><td>' + (data[i].Tech_W || '0') +
                    '</td><td>' + (data[i].Tech_Incentive || '0') +
                    '</td><td>' + (data[i].Net_Incentive_Tech || '0') +
                    '</td><td>' + (data[i].Module_Complexity_W || '0') +
                    '</td><td>' + (data[i].QA_Complexity_W || '0') +
                    '</td><td>' + (data[i].QA_Std_Days || '0') +
                    '</td><td>' + formatDate((data[i].Tester_Startdt || '0')) +
                    '</td><td>' + formatDate((data[i].Tester_Date || '0')) +
                    '</td><td>' + (data[i].QA_Actual_Days || '0') +
                    '</td><td>' + (data[i].QA_Delay_Savings || '0') +
                    '</td><td>' + (data[i].QA_Delay_Savings_W || '0') +
                    '</td><td>' + (data[i].QA_User_Feedback_W || '0') +
                    '</td><td>' + (data[i].QA_Total_Weigthage_for_tester || '0') +
                    '</td><td>' + (data[i].tester_incentive || '0') +
                    '</td><td>' + (data[i].QA_Net_Incentive || '0') +
                    '</td></tr>';
            });
            // Append the table to the #tbtable element within #content
            $("#tbtable").empty().append(html);
        }
    });
}