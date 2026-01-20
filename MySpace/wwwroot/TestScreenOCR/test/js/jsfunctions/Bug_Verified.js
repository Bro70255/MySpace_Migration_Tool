function Bug_Verified(Tester_Bug_Report_ID) { 
    // Display confirmation dialog
    if (confirm("Are you sure you want to confirm bug verification?")) {
        $("#loading").show();
        $.ajax({
            type: "POST",
            url: "/Home/Save_Bug_Verify?Tester_Bug_Report_ID=" + Tester_Bug_Report_ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#loading").hide();
                if (response === 1) {                   
                    alert("Verified Successfully.");
                    location.reload();
                }
            }
        });
    }
}