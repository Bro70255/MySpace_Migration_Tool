function bindFileTypes(projectFlowJson) {

    $("#fileTypeSelect")
        .empty()
        .append('<option value="">-- Select File Type --</option>');

    if (!projectFlowJson) return;

    let flowArray;

    try {
        flowArray = JSON.parse(projectFlowJson);
    } catch (e) {
        console.error("ProjectFlow parse error:", projectFlowJson);
        alert("Invalid ProjectFlow JSON");
        return;
    }

    flowArray.forEach(flow => {
        $("#fileTypeSelect").append(
            `<option value="${flow}">${flow}</option>`
        );
    });
}