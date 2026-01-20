function TestCase_Updation_Report() {
    var startdate = document.getElementById("startdate").value;
    if (startdate.trim() === '') {
        alert('Please enter Startdate.');
        return false;
    }

    var enddate = document.getElementById("enddate").value;
    if (enddate.trim() === '') {
        alert('Please enter Enddate.');
        return false;
    }

    // Convert enddate to Date object
    var endDateObj = new Date(enddate);
    var currentDate = new Date();

    // Compare enddate with current date
    if (endDateObj > currentDate) {
        alert('End date cannot be in the future. Please select an end date equal to or earlier than the current system date.');
        return false;
    }

    // Convert startdate to Date object
    var startDateObj = new Date(startdate);

    // Compare startdate with enddate
    if (startDateObj > endDateObj) {
        alert('Start date cannot be greater than End date.');
        return false;
    }
    $("#loading").show();
    $.ajax({
        type: "GET",
        url: "/Home/Get_TestCase_Updation_Report",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: {
            startdate: startdate,
            enddate: enddate
        },
        success: function (Response) {
            $("#loading").hide();
            var data = JSON.parse(Response);
            if (!data || data.length === 0) {
                alert('Data not found.');
                return;
            }

            var tableHtml = '';

            $.each(data, function (i, value) {
                var downloadLink = '/Upload_File/' + data[i].file_name;
                tableHtml += '<tr><td>' + data[i].Crf_id +
                    '</td><td>' + formatDate(data[i].Created_Datetime) + // Adding Created_Datetime
                    '</td><td>' + data[i].Remark +
                    '</td><td><a href="' + downloadLink + '" download><button class="Download-button"><svg height="16" width="20" viewBox="0 0 640 512"><path d="M144 480C64.5 480 0 415.5 0 336c0-62.8 40.2-116.2 96.2-135.9c-.1-2.7-.2-5.4-.2-8.1c0-88.4 71.6-160 160-160c59.3 0 111 32.2 138.7 80.2C409.9 102 428.3 96 448 96c53 0 96 43 96 96c0 12.2-2.3 23.8-6.4 34.6C596 238.4 640 290.1 640 352c0 70.7-57.3 128-128 128H144zm79-167l80 80c9.4 9.4 24.6 9.4 33.9 0l80-80c9.4-9.4 9.4-24.6 0-33.9s-24.6-9.4-33.9 0l-39 39V184c0-13.3-10.7-24-24-24s-24 10.7-24 24V318.1l-39-39c-9.4-9.4-24.6-9.4-33.9 0s-9.4 24.6 0 33.9z" fill="white"></path></svg><span>Download</span></button></a></td></tr>';
            });

            $("#tbtable").empty().append(tableHtml);

            // Add click event handler for download links
            $(".download-link").click(function (e) {
                e.preventDefault(); // Prevent default link behavior
                var fileName = decodeURIComponent($(this).data("file"));
                downloadFile(fileName);
            });
        }
    });
}