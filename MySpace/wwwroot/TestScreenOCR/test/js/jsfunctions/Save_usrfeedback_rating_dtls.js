function Save_usrfeedback_rating_dtls() {
   
    var flag = 0;
    var selectedCrfId = $("#crfusrfdback").val();
    if (selectedCrfId === "0") {
        alert("Select Crf.");
        flag = 1;
        return false;
    }
    var remark = document.getElementById("remark").value;
    var rating1 = document.getElementById("ratingValue1").value; // Assuming rating1 is an input field
    if (rating1 === "0") {
        alert("Select Rating.");
        flag = 1;
        return false;
    }
    var rating2 = document.getElementById("ratingValue2").value;
    if (rating2 === "0") {
        alert("Select Rating.");
        flag = 1;
        return false;
    }// Assuming rating2 is an input field
    var rating3 = document.getElementById("ratingValue3").value;
    if (rating3 === "0") {
        alert("Select Rating.");
        flag = 1;
        return false;
    }// Assuming rating3 is an input field
    var rating4 = document.getElementById("ratingValue4").value;
    if (rating4 === "0") {
        alert("Select Rating.");
        flag = 1;
        return false;
    }// Assuming rating4 is an input field
    var rating5 = document.getElementById("ratingValue5").value;
    if (rating5 === "0") {
        alert("Select Rating.");
        flag = 1;
        return false;
    }// Assuming rating5 is an input field

    // Check if any rating is empty
    if (rating1 === "" || rating2 === "" || rating3 === "" || rating4 === "" || rating5 === "") {
        flag = 1; // Set flag to 1 if any rating is empty
    }

    if (flag === 0) {
        $("#loading").show();
        $.ajax({
            type: "POST",
            url: "/Home/Save_usrfeedback_rating_dtls",
            data: JSON.stringify({
                crf_id: selectedCrfId,
                Remark: remark,
                ratingValue1: rating1,
                ratingValue2: rating2,
                ratingValue3: rating3,
                ratingValue4: rating4,
                ratingValue5: rating5 // Pass all ratings to the server
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#loading").hide();
                alert("Confirmed Successfully.");
                location.reload(); // Refresh the page
            },
            error: function (xhr, status, error) {
                // Handle error response
                console.error("Error:", error);
            }
        });
    } else {
        alert("Please provide ratings for all questions.");
    }
}