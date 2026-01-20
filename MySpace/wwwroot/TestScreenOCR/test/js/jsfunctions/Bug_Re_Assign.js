function Bug_Re_Assign(Tester_Bug_Report_ID) {   
    var confirmation = confirm("Are you sure you want to reassign?");
    if (confirmation) {
        $("#loading").show();
        $.ajax({
            type: "POST",
            url: "/Home/Save_Re_Assign?Tester_Bug_Report_ID=" + Tester_Bug_Report_ID,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#loading").hide();
                if (response === 1) {                  
                    alert("Re Assigned Successfully.");
                    location.reload();
                }
            }
        });
    }
}