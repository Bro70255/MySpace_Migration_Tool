function Bank_approved_dtls() {
    $.ajax({
        type: "GET",
        url: "/Home/Get_Bank_approved_dtls",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var html = '';
            var counter = 1; // Initialize counter correctly
            var data = JSON.parse(response);

            if (data.length > 0) {
                $.each(data, function (i, value) {
                    var Apprve_Id = value.ID;
                    var Account_num = value.ACCOUNT_NUMBER;
                    var sign1 = value.SIGNATURE1;
                    var sign2 = value.SIGNATURE2;
                    var sign3 = value.SIGNATURE3;
                    var sign4 = value.SIGNATURE4;
                    var APPROVE = `<button class="button10" 
                        data-apprv_id="${Apprve_Id}" 
                        data-account_num="${Account_num}" 
                        data-sign1="${sign1}" 
                        data-sign2="${sign2}" 
                        data-sign3="${sign3}" 
                        data-sign4="${sign4}" 
                        style="background-color: #0db50d; color: white;" 
                        onclick="Save_Approve_Details(this); return false;">
                        Approve
                    </button>`;

                    html += `<tr>
                        <td>${counter++}</td> <!-- Increment counter -->
                        <td style="display:none;">${value.ID}</td>
                        <td>${value.UNIT}</td>
                        <td>${value.BANK}</td>
                        <td>${value.ACCOUNT_NUMBER}</td>
                        <td>${value.SIGNATURE1}</td>
                        <td>${value.SIGNATURE2}</td>
                        <td>${value.SIGNATURE3}</td>
                        <td>${value.SIGNATURE4}</td>
                        <td>${APPROVE}</td>
                    </tr>`;
                });
            }
            // No else statement to display "No data found"

            $("#tbtable1").empty().append(html); // Empty the table and append new HTML
        },
        error: function (xhr, status, error) {
            console.log("Error: " + error);
        }
    });
}