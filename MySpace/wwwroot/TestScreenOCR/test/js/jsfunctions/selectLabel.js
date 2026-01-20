function selectLabel(selectedLabel) {

    // Set visibility of the appropriate div and highlight the selected label
    if (selectedLabel === 'mash') {
        document.getElementById('mashlab').classList.add('highlight');
        document.getElementById('mafoundlab').classList.remove('highlight');
        document.getElementById('mashdiv').style.display = 'block';
        document.getElementById('mafounddiv').style.display = 'none';
        document.getElementById('notification-container_mash').style.display = 'block';
        document.getElementById('notification-container').style.display = 'none';
        document.getElementById('mashlab').checked = true;

    } else if (selectedLabel === 'mafound') {
        document.getElementById('mafoundlab').classList.add('highlight');
        document.getElementById('mashlab').classList.remove('highlight');
        document.getElementById('mafounddiv').style.display = 'block';
        document.getElementById('mashdiv').style.display = 'none';
        document.getElementById('notification-container').style.display = 'block';
        document.getElementById('notification-container_mash').style.display = 'none';
        document.getElementById('mafoundlab').checked = true;

    }

    //   Dim the other label
    document.getElementById('mashlab').classList.add('dim');
    document.getElementById('mafoundlab').classList.add('dim');
    document.getElementById(selectedLabel + 'lab').classList.remove('dim');




    // AJAX call to record the selected label
    $.ajax({
        url: "/Home/Set_Firm",
        type: "GET",
        data: { selectedLabel: selectedLabel },
        dataType: 'json',
        success: function (response) {
           // console.log("Selected label sent to server: ", selectedLabel);
        },
        error: function () {
            console.error("Error sending the selected label.");
        }
    });
}