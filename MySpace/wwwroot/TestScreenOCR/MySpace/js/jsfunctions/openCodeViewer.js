function openCodeViewer(tag, nodeValue) {

    let filename = '';
    const parts = nodeValue.split('|');

    switch (tag) {

        case 'VIEW':
            filename = parts[2] || parts[1];
            break;

        case 'JS':
            filename = (parts[2] || parts[1]) + '.js';
            break;

        case 'CTRL':
        case 'BLL':
        case 'DAL':
            filename = parts.slice(1).join('|') + '.cs';
            break;

        case 'SP':
            filename = parts.slice(1).join('|');
            break;

        default:
            filename = parts.slice(1).join('|');
    }

    // Title
    document.getElementById('codeTitle').textContent =
        `${tag} : ${filename}`;

    const codeBox = document.getElementById('codeContent');
    codeBox.textContent = 'Loading...';

    // API Call
    $.ajax({
        url: "/Home/Get_File_Path_For_View_Code",
        type: "GET",
        dataType: "json",   // 👈 important
        data: { filename: filename },

        success: function (data) {

            codeBox.textContent = data?.textContent || 'No code found';
            codeBox.scrollTop = 0;

            document.getElementById('codeViewer')
                .classList.add('open');
        }
    });

}