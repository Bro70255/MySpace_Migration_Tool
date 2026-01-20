function toggleInputs(element) {
    var ddlDeveloper = document.getElementById("ddldeveloper");
    var dailyWorkModule = document.getElementById("dailywork_module");
    var allRptRadio = document.getElementById("all_rpt");

    if (element.type === "radio") {
        ddlDeveloper.disabled = element.checked;
        dailyWorkModule.disabled = element.checked;
        ddlDeveloper.selectedIndex = -1;
        dailyWorkModule.selectedIndex = -1;
    } else {
        if (element.id === "ddldeveloper") {
            dailyWorkModule.disabled = true;
            allRptRadio.disabled = true;
            allRptRadio.checked = false;
        } else if (element.id === "dailywork_module") {
            ddlDeveloper.disabled = true;
            allRptRadio.disabled = true;
            allRptRadio.checked = false;
        }
    }
}