function initialization_Get_Notification() {
    $.ajax({
        type: "GET",
        url: "/Home/Get_Notification",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response);
            var i = 0;
            if (data[i].user_type == 2 || data[i].user_type == 3 || data[i].user_type == 8) {
                var notificationContainer = document.getElementById("notification-container");
                notificationContainer.innerHTML = '';
                var notificationContainer_mash = document.getElementById("notification-container_mash");
                notificationContainer_mash.innerHTML = '';
                for (i = 0; i < data.length; i++) {
                    if (data[i].Firm == 1) {
                        var CRF_id = data[i].crf_Id;
                        var techlead_name = data[i].techlead_name;
                        var subject_crf = data[i].Subject;
                        var time_update = formatDate_for_notification(data[i].Last_Status_Updated_Date);

                        var newNotification = document.createElement('div');
                        newNotification.className = 'not_box';
                        newNotification.innerHTML = '<ul>' +
                            '<li><label>CRF Id:</label><label>' + CRF_id + '</label></li>' +
                            '<li><label>Techlead:</label><label>' + techlead_name + '</label></li>' +
                            '<li><label>' + subject_crf + '</label></li>' +
                            '<li><label>' + time_update + '</label></li>' +
                            '</ul>';
                        newNotification.querySelector('li:first-child label').setAttribute('data-crfid', CRF_id);
                        notificationContainer.appendChild(newNotification);
                    } else {
                        var CRF_id = data[i].crf_Id;
                        var techlead_name = data[i].techlead_name;
                        var subject_crf = data[i].Subject;
                        var time_update = formatDate_for_notification(data[i].Last_Status_Updated_Date);

                        var newNotification_mash = document.createElement('div');
                        newNotification_mash.className = 'not_box';
                        newNotification_mash.innerHTML = '<ul>' +
                            '<li><label>CRF Id:</label><label>' + CRF_id + '</label></li>' +
                            '<li><label>Techlead:</label><label>' + techlead_name + '</label></li>' +
                            '<li><label>' + subject_crf + '</label></li>' +
                            '<li><label>' + time_update + '</label></li>' +
                            '</ul>';
                        newNotification_mash.querySelector('li:first-child label').setAttribute('data-crfid', CRF_id);
                        notificationContainer_mash.appendChild(newNotification_mash); // Append to mash container
                    }
                }
            } else {
                var notificationContainer = document.getElementById("notification-container");
                notificationContainer.innerHTML = '';
                for (var i = 0; i < data.length; i++) {
                    var CRF_id = data[i].crf_Id;
                    var techlead_name = data[i].techlead_name;
                    var subject_crf = data[i].Subject;
                    var time_update = formatDate_for_notification(data[i].Last_Status_Updated_Date);

                    var newNotification = document.createElement('div');
                    newNotification.className = 'not_box';
                    newNotification.innerHTML = '<ul>' +
                        '<li><label>Crf Id:</label><label>' + CRF_id + '</label></li>' +
                        '<li><label style="font-size: 15px;">Techlead:</label><label>' + techlead_name + '</label></li>' +
                        '<li><label style="font-size: 15px;">' + subject_crf + '</label></li>' +
                        '<li><label>' + time_update + '</label></li>' +
                        '</ul>';
                    newNotification.querySelector('li:first-child label').setAttribute('data-crfid', CRF_id);
                    notificationContainer.appendChild(newNotification);
                }
            }
        }
    });
}