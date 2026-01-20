function openFileUploadDialogue1() {
    var input = document.createElement('input');
    input.type = 'file';
    input.multiple = true;
    input.style.display = 'none';

    input.addEventListener('change', function (event) {
        selected_attach_files_tester_Container(event);
        document.body.removeChild(input); // Remove the input element after use
    });

    document.body.appendChild(input);
    input.click();
}