function loadProjects() {

    $.ajax({
        url: "/Home/Get_Project_Details",
        type: "GET",
        success: function (data) {

            console.log("AJAX RESPONSE:", data);

            // 🔴 HARD CHECK
            if (!Array.isArray(data)) {
                alert("ERROR: Backend is NOT returning JSON array.\nCheck Home/Get_Project_Details");
                return;
            }

            // ---------- PROJECT DROPDOWN ----------
            $("#projectSelect")
                .empty()
                .append('<option value="">-- Select Project --</option>');

            data.forEach(p => {
                $("#projectSelect").append(
                    `<option value="${p.projectId}">${p.projectName}</option>`
                );
            });

            // Auto-load file types for first project
            if (data.length > 0) {
                bindFileTypes(data[0].projectFlow);
            }

            // On project change → update file types
            $("#projectSelect").off("change").on("change", function () {
                let selectedId = $(this).val();
                let proj = data.find(x => x.projectId == selectedId);
                if (proj) {
                    bindFileTypes(proj.projectFlow);
                }
            });
        },
        error: function (err) {
            console.error("AJAX ERROR:", err);
            alert("AJAX call failed. Check console.");
        }
    });
}