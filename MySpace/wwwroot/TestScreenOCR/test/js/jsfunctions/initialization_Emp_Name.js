function initialization_Emp_Name() {
    $.ajax({
        type: "GET",
        url: "/Home/Get_Emp_Name",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response);

            $("#emp_name").text(data[0].Name || "null");
        }
    });
}