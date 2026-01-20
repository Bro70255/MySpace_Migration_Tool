function updateCodeReviewAndTotalWorkHrs() {
    // Get the value of development
    var developmentValue = parseFloat($("#development").val()) || 0;

    // Calculate CodeReview value
    var codeReviewValue = developmentValue * 0.40;

    // Display the CodeReview value in the "codereview" input field
    $("#codereview").val(codeReviewValue);

    // Calculate totalworkhrs value
    var totalWorkHrs = developmentValue + codeReviewValue;

    // Display the totalworkhrs value in the "totalworkhrs" input field
    $("#totalworkhrs").val(totalWorkHrs);
}