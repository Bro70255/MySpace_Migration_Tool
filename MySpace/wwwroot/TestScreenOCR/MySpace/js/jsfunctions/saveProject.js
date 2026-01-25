function saveProject() {

    const projectName = document.querySelector('input[name="ProjectName"]').value.trim();
    const projectType = document.querySelector('select[name="ProjectType"]').value;

    // Validation
    if (!projectName) {
        alert("Please enter Project Name");
        return;
    }

    if (!projectType) {
        alert("Please select Project Type");
        return;
    }

    if (flow.length === 0) {
        alert("Please define Project Flow");
        return;
    }

    // JSON payload
    const data = {
        ProjectName: projectName,
        ProjectType: projectType,
        ProjectFlow: flow   // array
    };

    fetch('/Home/Create_Project', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        credentials: 'include',   // ✅ IMPORTANT (send cookies)
        body: JSON.stringify(data)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Failed to save project");
            }

            alert("Project created successfully");

            // ✅ Redirect ALWAYS works
            window.location.href = '/Home/Upload';
        })
        .catch(error => {
            console.error(error);
            alert("Error while saving project");
        });

}