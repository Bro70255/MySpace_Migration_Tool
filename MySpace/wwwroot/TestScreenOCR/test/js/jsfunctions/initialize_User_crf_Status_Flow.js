function initialize_User_crf_Status_Flow() {

    $("#loading").show();
    $.ajax({
        url: "/Home/Get_CRF_Flow_Of_User",
        type: "GET",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            $("#loading").hide();
            try {
                var data = JSON.parse(response);

                if (Array.isArray(data) && data.length > 0) {
                    var timelineItems = document.getElementById('timelineitems');

                    data = data.reverse(); // Reverse the order of data array

                    data.forEach(function (item, index) {
                        var li = document.createElement('li');
                        var div = document.createElement('div');

                        div.classList.add(index % 2 === 0 ? 'left-item' : 'right-item');

                        // Check each property for null values and display empty space if it's null
                        div.innerHTML = `<label class="dtlabel">${formatDateforstatusflow(item.Status_Updated_Date)}</label><label>${item.username ? item.username : ' '}</label><label>${item.Status_Description ? item.Status_Description : ' '}</label><label>${item.Subject ? item.Subject : ' '}</label>`;

                        li.appendChild(div);

                        if (timelineItems) {
                            timelineItems.appendChild(li);
                        } else {
                            console.log("Timeline items container not found.");
                        }
                    });
                } else {
                    console.log("No timeline data received.");
                }
            } catch (error) {
                console.error("Error parsing JSON:", error);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching timeline data:", error);
        }
    });
}