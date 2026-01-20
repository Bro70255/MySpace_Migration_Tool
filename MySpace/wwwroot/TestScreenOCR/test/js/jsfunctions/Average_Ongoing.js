function Average_Ongoing(firm) {
    $.ajax({
        type: "GET",
        url: "/Home/Average_ongoing",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { firm: firm },
        success: function (response) {
            if (response.success) {
                // Assuming response has an 'average' property
                $('#avg_days').val(response.average || "null");
            } else {
                console.error("Error fetching data:", response.message);
                $('#avg_days').val("Error: " + response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX error:", status, error);
            $('#avg_days').val("Error");
        }
    });
}