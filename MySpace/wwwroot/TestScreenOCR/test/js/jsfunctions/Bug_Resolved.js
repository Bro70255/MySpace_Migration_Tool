function Bug_Resolved(Tester_Bug_Report_ID) {
    var remark = prompt("Please enter the remark:");
    if (remark !== null) {
        if (remark.trim() === "") {
            alert("Remark cannot be empty. Please enter a remark.");
        } else {
            $("#loading").show();
            $.ajax({
                type: "POST",
                url: "/Home/Save_Bug_Resolved?Tester_Bug_Report_ID=" + Tester_Bug_Report_ID + "&remark=" + encodeURIComponent(remark),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    $("#loading").hide();
                    var data = JSON.parse(response);
                    if (data == 1) {
                        alert("Resolved");
                        location.reload();
                    }
                }
            });
        }
    }
}