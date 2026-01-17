
function registerUser() {

    // -------- Collect Form Data --------
    const data = {
        FirstName: $("input[placeholder='John']").val().trim(),
        LastName: $("input[placeholder='Doe']").val().trim(),
        Email: $("input[type='email']").val().trim(),
        Username: $("input[placeholder='username']").val().trim(),
        Password: $("#pwd").val(),
        ConfirmPassword: $("#cpwd").val()
    };

    // -------- Basic Required Field Validation --------
    if (!data.FirstName || !data.LastName || !data.Email ||
        !data.Username || !data.Password) {
        alert("All fields are required");
        return;
    }

    // -------- Password Length Validation --------
    if (data.Password.length < 8) {
        alert("Password must be at least 8 characters");
        return;
    }

    // -------- Password Match Validation --------
    if (data.Password !== data.ConfirmPassword) {
        alert("Passwords do not match");
        return;
    }

    // -------- AJAX Call : Register User --------
    $.ajax({
        url: "/Home/RegisterUser",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (res) {
            if (res.success) {
                alert(res.message);
                window.location.href = "/Home/MySpace_Login";
            } else {
                alert(res.message);
            }
        },
        error: function () {
            alert("Server error. Please try again.");
        }
    });
}

function btnLogin() {

    let username = $("#username").val().trim();
    let password = $("#password").val().trim();

    if (!username || !password) {
        showError("Please enter username and password.");
        return;
    }

    $.ajax({
        url: "/Home/Sign_In",
        type: "POST",
        data: {
            username: username,
            password: password
        },
        success: function (res) {
            if (res.success) {
                window.location.href = "/Home/MySpace_Dashboard";
            } else {
                showError(res.message);
            }
        },
        error: function () {
            showError("Server error. Try again.");
        }
    });
}

function loadBlueprint() {
    fetch('/Home/GetBlueprint')
        .then(res => res.json())
        .then(data => {
            console.log("DB DATA:", data);
            renderBlueprint(data);
        })
        .catch(err => console.error(err));
}

function renderBlueprint(data) {
    const root = document.getElementById('blueprintTree');
    root.innerHTML = '';

    data.forEach((screen, s) => {

        // ================= SCREEN =================
        const view = document.createElement('div');
        view.className = 'view-node';

        const title = document.createElement('div');
        title.className = 'view-title';
        title.textContent = `${s + 1}. ${screen.screenName}`;
        title.onclick = () => view.classList.toggle('active');

        view.appendChild(title);

        // ================= JS CONTAINER =================
        const jsContainer = document.createElement('div');
        jsContainer.className = 'js-container';

        screen.jsFunctions.forEach((js, j) => {

            // -------- JS NODE --------
            const jsNode = document.createElement('div');
            jsNode.className = 'js-node';
            jsNode.textContent = `${s + 1}.${j + 1} ${js.jsFunctionName}`;

            // -------- CONTROLLER LIST --------
            const ctrlList = document.createElement('div');
            ctrlList.className = 'controller-list';

            js.controllers.forEach((c, k) => {

                // 🔑 FIX: Normalize HTTP type
                const type = c.httpType?.toUpperCase().includes('GET')
                    ? 'get'
                    : 'post';

                const ctrl = document.createElement('div');
                ctrl.className = `controller ${type}`;
                ctrl.textContent = `${s + 1}.${j + 1}.${k + 1} ${c.controllerAction}`;

                ctrlList.appendChild(ctrl);
            });

            // Toggle only this JS function
            jsNode.onclick = () => {
                jsNode.classList.toggle('active');
                ctrlList.classList.toggle('active');
            };

            jsContainer.appendChild(jsNode);
            jsContainer.appendChild(ctrlList);
        });

        view.appendChild(jsContainer);
        root.appendChild(view);
    });
}


function loadOCRTreeView() {
    fetch('/Home/List_out_the_Files_in_Folder_ReadOCRFile')
        .then(res => res.json())
        .then(data => {
            if (!data.success) {
                alert("Failed to load files");
                return;
            }

            const treeView = document.getElementById("treeView");
            treeView.innerHTML = "";

            const ul = document.createElement("ul");
            renderNode(data.data, ul);
            treeView.appendChild(ul);
        })
        .catch(err => {
            console.error(err);
            alert("Error loading tree view");
        });
}

function renderNode(node, parentUl) {
    const li = document.createElement("li");

    if (node.isDirectory) {

        const header = document.createElement("div");
        header.className = "tree-folder";

        const caret = document.createElement("span");
        caret.className = "tree-caret";
        caret.textContent = "▶";

        const icon = document.createElement("span");
        icon.className = "tree-folder-icon";
        icon.textContent = "📁";

        const name = document.createElement("span");
        name.className = "tree-name";
        name.textContent = node.name;

        header.append(caret, icon, name);
        li.appendChild(header);

        const childrenUl = document.createElement("ul");
        childrenUl.className = "tree-children";

        node.children.forEach(child => renderNode(child, childrenUl));

        header.addEventListener("click", () => {
            const open = childrenUl.classList.contains("open");

            childrenUl.classList.toggle("open", !open);
            caret.classList.toggle("open", !open);
            icon.textContent = !open ? "📂" : "📁";
        });

        li.appendChild(childrenUl);
    }
    else {
        li.className = "tree-file";
        li.innerHTML = `
                <span class="tree-file-icon">📄</span>
                <span class="tree-name">${node.name}</span>
            `;
    }

    parentUl.appendChild(li);
}

function togglePassword() {
    const input = $("#password");
    input.attr("type", input.attr("type") === "password" ? "text" : "password");
}

function showError(msg) {
    $("#errorBox").text(msg).fadeIn();
}

function validateForm() {
    let fullName = document.getElementById("FullName").value.trim();
    let phone = document.getElementById("Phone").value.trim();
    let email = document.getElementById("Email").value.trim();
    let address = document.getElementById("Address").value.trim();
    let place = document.getElementById("Place").value.trim();
    let pinCode = document.getElementById("PinCode").value.trim();

    if (fullName === "") {
        alert("Full Name is required");
        return false;
    }
    if (phone === "" || phone.length < 10) {
        alert("Valid Phone Number is required");
        return false;
    }
    if (email === "") {
        alert("Email is required");
        return false;
    }

    let emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(email)) {
        alert("Invalid email format");
        return false;
    }

    if (address === "") {
        alert("Address is required");
        return false;
    }
    if (place === "") {
        alert("Place / City is required");
        return false;
    }
    if (pinCode === "" || pinCode.length < 6) {
        alert("Valid Pin Code is required");
        return false;
    }

    // If validation passes → send data
    saveUser();
    return false; // STOP FORM SUBMIT
}

function saveUser() {
    let user = {
        FullName: document.getElementById("FullName").value,
        Phone: document.getElementById("Phone").value,
        Email: document.getElementById("Email").value,
        Address: document.getElementById("Address").value,
        Place: document.getElementById("Place").value,
        PinCode: document.getElementById("PinCode").value
    };

    fetch('/Home/Register', {   // <-- FIXED URL
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    })
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                alert(data.message);
                document.getElementById("regForm").reset();
            } else {
                alert(data.message);
                console.log(data.errors);
            }
        })
        .catch(err => console.error(err));
}

function Initialize_Registration_Report_Details() {
   let search = $("#txtSearch").val();

    $.ajax({
        url: "/home/Get_Registration_Report_Details",
        type: "GET",
        data: { search: search },
        success: function (data) {

            $("#tdtable").empty();

            if (data.length === 0) {
                $("#tdtable").append(`<tr><td colspan="6" class="text-center">No records found</td></tr>`);
                return;
            }

            data.forEach(function (item) {
                $("#tdtable").append(`
                        <tr>
                            <td>${item.fullName}</td>
                            <td>${item.phone}</td>
                            <td>${item.email}</td>
                            <td>${item.address}</td>
                            <td>${item.place}</td>
                            <td>${item.pinCode}</td>
                        </tr>
                    `);
            });
        },
        error: function (err) {
            console.error(err);
        }
    });
}

/* ================= UPLOAD ================= */
/* ================= UPLOAD ================= */
function uploadFiles() {

    const project = getSelectedProject();

    if (!project.projectId) {
        showMessage("Please select a project", "error");
        return;
    }

    if (!selectedFiles || selectedFiles.length === 0) {
        showMessage("No files selected", "error");
        return;
    }

    const formData = new FormData();
    formData.append("projectId", project.projectId);
    formData.append("projectName", project.projectName);

    Array.from(selectedFiles).forEach(file => {
        formData.append("files", file);
    });

    $.ajax({
        url: "/Home/UploadScreenFolder",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (res) {

            if (res.success) {
                showMessage(res.message || "Upload completed successfully", "success");
                selectedFiles = [];
                $("#uploadInfo").html("");
            } else {
                showMessage(res.message || "Upload failed", "error");
            }
        },
        error: function () {
            showMessage("Server error during upload", "error");
        }
    });
}

function Sent_Data_To_AI() {

    const screenName = document.getElementById("ScreenName").value;
    const screenCode = document.getElementById("ScreenCode").value.replace(/\s/g, '');


    if (!screenName.trim()) {
        alert("Please enter Screen Name");
        return;
    }

    if (!screenCode.trim()) {
        alert("Please enter screen code");
        return;
    }

    document.getElementById("AIResponse").value = "Processing...";

    const requestData = {
        ScreenName: screenName,
        ScreenCode: screenCode
    };

    fetch('/Home/Call_AI', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestData)
    })
        .then(res => res.json())
        .then(data => {
            if (data.status === "Success") {
                document.getElementById("AIResponse").value = data.response;
            } else {
                document.getElementById("AIResponse").value =
                    data.message || "AI processing failed";
            }
        })
        .catch(err => {
            console.error(err);
            document.getElementById("AIResponse").value = "Error calling AI";
        });
}

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
        body: JSON.stringify(data)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Failed to save project");
            }
            return response.json(); // expecting JSON response
        })
        .then(result => {
            alert("Project created successfully");

            // Optional redirect
            window.location.href = '/Home/Upload';
        })
        .catch(error => {
            console.error(error);
            alert("Error while saving project");
        });
}

/* ================= LOAD PROJECTS ================= */
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

/* ================= BIND FILE TYPES ================= */
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




