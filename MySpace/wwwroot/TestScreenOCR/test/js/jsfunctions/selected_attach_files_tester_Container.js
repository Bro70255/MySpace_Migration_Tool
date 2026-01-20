function selected_attach_files_tester_Container(event) {
    var files = event.target.files;
    var container = document.getElementById('selected_attach_files_tester_Container');

    if (files.length > 0) {
        var existingList = container.querySelector('ul');

        if (!existingList) {
            existingList = document.createElement('ul');
            existingList.style.listStyle = 'none';
            container.appendChild(existingList);
        }

        for (var i = 0; i < files.length; i++) {
            var listItem = document.createElement('li');
            listItem.style.disp
            lay = 'flex';
            listItem.style.alignItems = 'center';

            var fileName = document.createElement('span');
            fileName.textContent = truncateFileName(files[i].name, 15); // Truncate to 15 characters
            fileName.style.marginRight = '10px';
            fileName.style.color = 'black';

            var fileSize = files[i].size / (1024 * 1024); // Convert size to MB
            if (fileSize > 1) {
                alert('File size exceeds 1MB limit for: ' + files[i].name);
                continue; // Skip this file
            }

            var deleteButton = document.createElement('button');
            deleteButton.textContent = 'X';
            deleteButton.style.marginRight = '10px';
            deleteButton.style.backgroundColor = 'red';

            // Use let to create a new variable scope for each iteration
            let currentItem = listItem;
            let currentFile = files[i];

            deleteButton.onclick = function () {
                currentItem.remove();

                // Remove the deleted file from the array
                selectedFilesArrayTester = selectedFilesArrayTester.filter(file => file !== currentFile);
            };

            // Push the file into the array
            selectedFilesArrayTester.push(currentFile);

            listItem.appendChild(fileName);
            listItem.appendChild(deleteButton);
            existingList.appendChild(listItem);
        }
    } else {
        container.innerHTML = 'No files selected';
    }
}