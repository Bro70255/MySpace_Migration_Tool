
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
        .then(r => r.json())
        .then(edges => renderBlueprintFromEdges(edges))
        .catch(console.error);
}

function renderBlueprintFromEdges(edges) {
    const root = document.getElementById('blueprintTree');
    root.innerHTML = '';

    const adj = buildGraph(edges);

    const screens = [...adj.keys()]
        .filter(n => nodeType(n) === 'SCREEN')
        .sort((a, b) => nodeLabel(a).localeCompare(nodeLabel(b)));

    screens.forEach((screenNode, sIdx) => {
        const sNo = `${sIdx + 1}`;
        const viewItem = makeTreeItem(
            sNo, 'VIEW', nodeLabel(screenNode), 'view', true, screenNode
        );

        const jsNodes = (adj.get(screenNode) || [])
            .filter(n => nodeType(n) === 'JS');

        jsNodes.forEach((jsNode, jIdx) => {
            const jNo = `${sNo}.${jIdx + 1}`;
            const jsItem = makeTreeItem(
                jNo, 'JS', nodeLabel(jsNode), 'js', false, jsNode
            );

            const ctrlNodes = (adj.get(jsNode) || [])
                .filter(n => nodeType(n) === 'CTRL');

            ctrlNodes.forEach((ctrlNode, cIdx) => {
                const cNo = `${jNo}.${cIdx + 1}`;
                const ctrlItem = makeTreeItem(
                    cNo, 'CTRL', nodeLabel(ctrlNode), 'ctrl', false, ctrlNode
                );

                const bllNodes = (adj.get(ctrlNode) || [])
                    .filter(n => nodeType(n) === 'BLL');

                bllNodes.forEach((bll, bIdx) => {
                    const bNo = `${cNo}.${bIdx + 1}`;
                    const bllItem = makeTreeItem(
                        bNo, 'BLL', nodeLabel(bll), 'bll', false, bll
                    );

                    const dalNodes = (adj.get(bll) || [])
                        .filter(n => nodeType(n) === 'DAL');

                    dalNodes.forEach((dal, dIdx) => {
                        const dNo = `${bNo}.${dIdx + 1}`;
                        const dalItem = makeTreeItem(
                            dNo, 'DAL', nodeLabel(dal), 'dal', false, dal
                        );

                        const spNodes = (adj.get(dal) || [])
                            .filter(n => nodeType(n) === 'SP');

                        spNodes.forEach((sp, spIdx) => {
                            const spNo = `${dNo}.${spIdx + 1}`;
                            const spItem = makeTreeItem(
                                spNo, 'SP', nodeLabel(sp), 'sp', false, sp
                            );
                            dalItem.body.appendChild(spItem.el);
                        });

                        if (!dalItem.body.hasChildNodes())
                            dalItem.el.classList.add('leaf');

                        bllItem.body.appendChild(dalItem.el);
                    });

                    if (!bllItem.body.hasChildNodes())
                        bllItem.el.classList.add('leaf');

                    ctrlItem.body.appendChild(bllItem.el);
                });

                if (!ctrlItem.body.hasChildNodes())
                    ctrlItem.el.classList.add('leaf');

                jsItem.body.appendChild(ctrlItem.el);
            });

            if (!jsItem.body.hasChildNodes())
                jsItem.el.classList.add('leaf');

            viewItem.body.appendChild(jsItem.el);
        });

        root.appendChild(viewItem.el);
    });
}

/* ================= TREE ITEM ================= */

function makeTreeItem(no, tag, text, kindClass, openByDefault, nodeValue) {
    const el = document.createElement('div');
    el.className = `tree-item ${kindClass}`;

    const header = document.createElement('div');
    header.className = 'tree-header';

    const left = document.createElement('div');
    left.className = 'tree-left';

    const right = document.createElement('div');
    right.className = 'tree-right';

    const twisty = document.createElement('span');
    twisty.className = 'twisty';

    const num = document.createElement('span');
    num.className = 'tree-num';
    num.textContent = no;

    const pill = document.createElement('span');
    pill.className = `pill pill-${tag.toLowerCase()}`;
    pill.textContent = tag;

    const label = document.createElement('span');
    label.className = 'tree-text';
    label.innerHTML = escapeHtml(text);

    const viewBtn = document.createElement('span');
    viewBtn.className = 'view-code-btn';
    viewBtn.innerHTML = '&lt;/&gt;';
    viewBtn.title = 'View Code';

    viewBtn.onclick = e => {
        e.stopPropagation();
        openCodeViewer(tag, nodeValue);
    };

    left.append(twisty, num, pill, label);
    right.appendChild(viewBtn);

    header.append(left, right);

    const body = document.createElement('div');
    body.className = 'tree-children';

    el.append(header, body);

    if (openByDefault) el.classList.add('open');

    header.onclick = () => {
        if (!body.hasChildNodes()) return;
        el.classList.toggle('open');
    };

    return { el, body };
}

/* ================= CODE VIEWER ================= */

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

/* Close viewer */
function closeCodeViewer() {
    document.getElementById('codeViewer')
        .classList.remove('open');
}


/* ================= HELPERS ================= */

function buildGraph(edges) {
    const adj = new Map();
    edges.forEach(e => {
        if (!e.fromNode || !e.toNode) return;
        if (!adj.has(e.fromNode)) adj.set(e.fromNode, []);
        adj.get(e.fromNode).push(e.toNode);
    });
    for (const [k, v] of adj)
        adj.set(k, [...new Set(v)]);
    return adj;
}

function nodeType(n) {
    return (n || '').split('|')[0];
}

function nodeLabel(n) {
    return (n || '').split('|').slice(1).join('|');
}

function escapeHtml(str) {
    return String(str)
        .replaceAll('&', '&amp;')
        .replaceAll('<', '&lt;')
        .replaceAll('>', '&gt;');
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

function toggleZooZooChat() {
    const chat = document.getElementById("zoozooChat");
    chat.style.display = chat.style.display === "flex" ? "none" : "flex";
}

async function sendZooZooMsg() {
    const input = document.getElementById("chatInput");
    const body = document.getElementById("chatBody");
    const text = input.value.trim();
    if (!text) return;

    // User bubble
    body.innerHTML += `<div class="user-msg">${text}</div>`;
    input.value = "";

    // Typing indicator
    const typing = document.createElement("div");
    typing.className = "bot-msg";
    typing.innerText = "🤖 thinking...";
    body.appendChild(typing);
    body.scrollTop = body.scrollHeight;

    try {
        const res = await fetch('/Home/ZooZooAsk', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({
                message: text,
                page: window.location.pathname
            })
        });

        const json = await res.json();
        typing.innerText = json.reply || "🤖 I’m learning…";

    } catch (e) {
        typing.innerText = "⚠️ Something went wrong.";
    }

    body.scrollTop = body.scrollHeight;
}

function loadProjects() {
    fetch('/Home/GetExistingProjects')
        .then(r => r.json())
        .then(res => {

            const box = document.getElementById("projectList");
            box.innerHTML = "";

            res.data.forEach((p, index) => {

                // PROJECT CONTAINER
                const project = document.createElement("div");
                project.className = "project";

                // PROJECT TITLE (NUMBERED)
                const title = document.createElement("div");
                title.className = "project-title clickable";
                title.innerHTML = `${index + 1}. ${p.projectName}`;

                // VERSION LIST (HIDDEN INITIALLY)
                const versionList = document.createElement("div");
                versionList.className = "version-list";
                versionList.style.display = "none";

                p.versions.forEach(v => {
                    const row = document.createElement("div");
                    row.className = "version";
                    row.textContent = v.versionName;

                    row.onclick = () => {
                        loadFiles(p.projectName, v.versionName);

                        // enable download button
                        document.getElementById("downloadBtn").disabled = false;
                        document.getElementById("repoPathText").innerText =
                            `${p.projectName} / ${v.versionName}`;
                    };

                    versionList.appendChild(row);
                });

                // TOGGLE VERSIONS ON PROJECT CLICK
                title.onclick = () => {
                    const isOpen = versionList.style.display === "block";

                    document
                        .querySelectorAll(".version-list")
                        .forEach(v => v.style.display = "none");

                    versionList.style.display = isOpen ? "none" : "block";
                };

                project.appendChild(title);
                project.appendChild(versionList);
                box.appendChild(project);
            });
        });
}


let currentProject = "";
let currentVersion = "";
let currentPath = "";

function loadFiles(project, version, path = "") {
    currentProject = project;
    currentVersion = version;
    currentPath = path;

    document.getElementById("repoPathText").innerText =
        `${project} / ${version}${path ? " / " + path : ""}`;

    // ✅ enable download button
    document.getElementById("downloadBtn").disabled = false;

    fetch(`/Home/GetVersionFiles?projectName=${project}&version=${version}&path=${encodeURIComponent(path)}`)
        .then(r => r.json())
        .then(res => {
            const list = document.getElementById("fileList");
            list.innerHTML = "";

            if (path) {
                const back = document.createElement("li");
                back.textContent = "⬅ Back";
                back.onclick = () => {
                    const parent = path.split("/").slice(0, -1).join("/");
                    loadFiles(project, version, parent);
                };
                list.appendChild(back);
            }

            res.files.forEach(f => {
                const li = document.createElement("li");
                li.className = f.isDirectory ? "folder" : "file";
                li.textContent = f.name;

                if (f.isDirectory)
                    li.onclick = () => loadFiles(project, version, f.path);
                else
                    li.onclick = () => viewFile(project, version, f.path);

                list.appendChild(li);
            });
        });
}
function downloadVersion() {
    if (!currentProject || !currentVersion) return;

    const url = `/Home/DownloadVersionZip?projectName=${encodeURIComponent(currentProject)}&version=${encodeURIComponent(currentVersion)}`;
    window.location.href = url;
}


function viewFile(project, version, path) {
    fetch(`/Home/ViewFile?projectName=${project}&version=${version}&path=${encodeURIComponent(path)}`)
        .then(r => r.text())
        .then(txt => {
            const viewer = document.getElementById("fileViewer");
            viewer.classList.remove("empty");
            viewer.textContent = txt;
        });
}

