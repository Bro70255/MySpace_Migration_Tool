function Developerwise_Report() {
    var firm = document.getElementById("firm").value;
    if (firm.trim() === '') {
        alert('Please Select Firm');
        return false;
    }
    var Startdate = document.getElementById("startdate").value;
    if (Startdate.trim() === '') {
        alert('Please enter Startdate.');
        return false;
    }

    var Enddate = document.getElementById("enddate").value;
    if (Enddate.trim() === '') {
        alert('Please enter Enddate.');
        return false;
    }
    var Developer = document.getElementById("ddldeveloper").value;
    if (Developer.trim() === "0") {
        alert('Please Select Developer');
        return false;
    }

    var html = '';

    // Remove unnecessary check for ReportType == 'Full Report' here
    $("#loading").show();
    $.ajax({
        type: "GET",
        url: "/Home/Get_developerwise_Report",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: {
            Startdate: Startdate,
            Enddate: Enddate,
            Developer: Developer,
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
                var REQUEST_DATE = (data[i].Created_DateTime === null || data[i].Created_DateTime === '') ? '' : formatDate(data[i].Created_DateTime);
                var Head_Approved_Date = (data[i].Head_Approved_Date === null || data[i].Head_Approved_Date === '') ? '' : formatDate(data[i].Head_Approved_Date);
                var Target_date = (data[i].Target_date === null || data[i].Target_date === '') ? '' : formatDate(data[i].Target_date);
                var Start_Date = (data[i].Start_Date === null || data[i].Start_Date === '') ? '' : formatDate(data[i].Start_Date);
                var End_Date = (data[i].End_Date === null || data[i].End_Date === '') ? '' : formatDate(data[i].End_Date);
                var Tester_Startdt = (data[i].Tester_Startdt === null || data[i].Tester_Startdt === '') ? '' : formatDate(data[i].Tester_Startdt);
                var Tester_Enddt = (data[i].Tester_Enddt === null || data[i].Tester_Enddt === '') ? '' : formatDate(data[i].Tester_Enddt);
                var Project_targetdate = (data[i].Tester_Enddt === null || data[i].Tester_Enddt === '') ? '' : formatDate(data[i].Tester_Enddt);
                var Dof_Live_Publish = (data[i].Date_of_live_Publish === null || data[i].Date_of_live_Publish === '') ? '' : formatDate(data[i].Date_of_live_Publish);
                var RELEASE_DATE = (data[i].RELEASE_DATE === null || data[i].RELEASE_DATE === '') ? '' : formatDate(data[i].RELEASE_DATE);
                var User_Acceptance_Date = (data[i].User_Acceptance_Date === null || data[i].User_Acceptance_Date === '') ? '' : formatDate(data[i].User_Acceptance_Date);
                var Developer_Date = (data[i].Developer_Date === null || data[i].Developer_Date === '') ? '' : formatDate(data[i].Developer_Date);
                var Developer_Enddate = (data[i].Developer_Enddate === null || data[i].Developer_Enddate === '') ? '' : formatDate(data[i].Developer_Enddate);

                html += '<tr><td>' + data[i].crf_Id +
                    '</td><td>' + data[i].Priority +
                    '</td><td>' + REQUEST_DATE +
                    '</td><td>' + data[i].Subject +
                    '</td><td>' + data[i].Requested_By +
                    '</td><td>' + data[i].Unit_Name +
                    '</td><td>' + data[i].TECHLEAD +
                    '</td><td>' + data[i].DEVELOPER +
                    '</td><td>' + data[i].TESTER +
                    '</td><td>' + Head_Approved_Date +
                    '</td><td>' + Target_date +
                    '</td><td>' + Start_Date +
                    '</td><td>' + End_Date +
                    '</td><td>' + Tester_Startdt +
                    '</td><td>' + Tester_Enddt +
                    '</td><td>' + Project_targetdate +
                    '</td><td>' + (Dof_Live_Publish || '') +
                    '</td><td>' + (data[i].Live_Publish_Remark || '') +
                    '</td><td>' + data[i].STATUS +
                    '</td><td>' + RELEASE_DATE +
                    '</td><td>' + data[i].CRF_Total_Hrs +
                    '</td><td>' + data[i].Request_Type +
                    '</td><td>' + User_Acceptance_Date +
                    '</td><td>' + data[i].Total_Cost +
                    '</td><td>' + Developer_Date +
                    '</td><td>' + Developer_Enddate +
                    '</td></tr>';
            });

            // Append the table to the #tbtable element within #content
            $("#tbtable").empty().append(html);

        }
    });
}