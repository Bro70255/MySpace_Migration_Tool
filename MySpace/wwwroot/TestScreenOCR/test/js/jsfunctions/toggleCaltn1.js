function toggleCaltn1() {
    $("#start_date").val() = $("#dev_cmpt_dt").val();
    var caltn1 = document.getElementById("caltn1");
    caltn1.style.display = (caltn1.style.display === "none") ? "block" : "none";
}