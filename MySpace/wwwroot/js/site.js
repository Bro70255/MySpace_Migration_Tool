function togglePassword() {
    const input = $("#password");
    input.attr("type", input.attr("type") === "password" ? "text" : "password");
}

function showError(msg) {
    $("#errorBox").text(msg).fadeIn();
}

function btnLogin() {

    let employeeCode = $("#username").val().trim();
    let password = $("#password").val().trim();

    if (employeeCode === "" || password === "") {
        showError("Please enter username and password.");
        return;
    }

    $.ajax({
        url: "/Home/Sign_In",
        type: "POST",
        data: {
            employeeCode: employeeCode,
            password: password
        },
        success: function (res) {
            if (res.success) {
                window.location.href = "/Home/MySpace_Dashboard";
            } else {
                showError(res.message || "Invalid login details");
            }
        },
        error: function () {
            showError("Server error. Try again.");
        }
    });
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
        li.innerHTML = `📁 <strong>${node.name}</strong>`;
        const childUl = document.createElement("ul");

        node.children.forEach(child => {
            renderNode(child, childUl);
        });

        li.appendChild(childUl);
    } else {
        li.textContent = `📄 ${node.name}`;
        li.classList.add("tree-file");
    }

    parentUl.appendChild(li);
}

function uploadFiles() {
    if (!selectedFiles || selectedFiles.length === 0) {
        alert("No files selected");
        return;
    }

    const formData = new FormData();

    for (let file of selectedFiles) {
        // preserve folder info if available
        formData.append("files", file, file.webkitRelativePath || file.name);
    }

    fetch("/Home/UploadScreenFolder", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            uploadInfo.innerHTML += res.success
                ? `<div style="color:green;margin-top:10px;">✅ ${res.message}</div>`
                : `<div style="color:red;margin-top:10px;">❌ ${res.message}</div>`;
        })
        .catch(err => {
            console.error(err);
            uploadInfo.innerHTML += `<div style="color:red;margin-top:10px;">❌ Upload failed</div>`;
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

function Initialize_OCR_From_File() {

    fetch('/Home/ReadOCRFile')
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                document.getElementById("ScreenCode").value = data.data;
            } else {
                alert("Failed to read file");
            }
        })
        .catch(err => {
            console.error(err);
        });
}

//////////////////////////////////////////////////////////////////
let __bpLinks = []; // { from: HTMLElement, to: HTMLElement }

function loadBlueprint() {
    fetch('/Home/List_out_the_Files_in_Folder_ReadOCRFile')
        .then(r => r.json())
        .then(res => {
            if (!res.success) return alert("Failed");

            const project = Array.isArray(res.data) ? res.data[0] : res.data;

            document.getElementById("projectTitle").innerText =
                project.name || project.fileName || "PROJECT";

            renderTree(project.children || []);

            const stage = document.getElementById("stage");
            stage.addEventListener("scroll", () => window.__bpDrawWires?.());
            window.addEventListener("resize", () => window.__bpDrawWires?.());
        });
}

function renderTree(groups) {
    const tree = document.getElementById("tree");
    tree.innerHTML = "";
    __bpLinks = [];

    const projectEl = document.getElementById("projectTitle");

    groups.forEach((g, i) => {
        const row = document.createElement("div");
        row.className = "bp-row";

        // GROUP BOX
        const groupEl = document.createElement("div");
        groupEl.className = "bp-group";
        groupEl.innerHTML = `<span>SL.${i + 1}</span> ${g.name || g.fileName}`;

        // SUB TREE CONTAINER
        const subTree = document.createElement("div");
        subTree.className = "bp-sub-tree";

        row.appendChild(groupEl);
        row.appendChild(subTree);
        tree.appendChild(row);

        // Link: PROJECT -> GROUP
        __bpLinks.push({ from: projectEl, to: groupEl });

        // Render subnodes recursively and link GROUP -> first-level nodes
        renderSubNodes(g.children || [], subTree, 0, groupEl);
    });

    requestAnimationFrame(drawWires);
}

/**
 * nodes: current node list
 * container: where to render
 * level: depth
 * parentEl: element to connect FROM (group or sub item)
 */
function renderSubNodes(nodes, container, level, parentEl) {
    nodes.forEach((n, idx) => {
        const item = document.createElement("div");
        item.className = "bp-sub-item";
        item.style.marginLeft = (level * 20) + "px";
        item.innerText = `${idx + 1}. ${n.name || n.fileName}`;

        container.appendChild(item);

        // Link: parent -> this item
        __bpLinks.push({ from: parentEl, to: item });

        // If children exist, render and link this item -> children
        if (n.children && n.children.length > 0) {
            renderSubNodes(n.children, container, level + 1, item);
        }
    });
}

/* =========================
   WIRES (PROJECT→GROUP→SUB→SUBSUB)
========================= */

function drawWires() {
    const svg = document.getElementById("bpWires");
    const stage = document.getElementById("stage");

    svg.innerHTML = "";

    svg.setAttribute("width", stage.scrollWidth);
    svg.setAttribute("height", stage.scrollHeight);
    svg.style.width = stage.scrollWidth + "px";
    svg.style.height = stage.scrollHeight + "px";

    __bpLinks.forEach(link => {
        if (!link.from || !link.to) return;

        const from = anchorRight(link.from, stage);
        const to = anchorLeft(link.to, stage);

        // Mid X (signal elbow)
        const midX = from.x + Math.max(40, (to.x - from.x) / 2);

        const path = document.createElementNS("http://www.w3.org/2000/svg", "path");

        /* STRAIGHT SIGNAL PATH */
        path.setAttribute(
            "d",
            `M ${from.x} ${from.y}
             L ${midX} ${from.y}
             L ${midX} ${to.y}
             L ${to.x} ${to.y}`
        );

        path.setAttribute("stroke", "#38bdf8");
        path.setAttribute("stroke-width", "2");
        path.setAttribute("fill", "none");
        path.setAttribute("stroke-linejoin", "round");
        path.setAttribute("stroke-linecap", "round");

        svg.appendChild(path);
    });
}


function anchorRight(el, stage) {
    const r = el.getBoundingClientRect();
    const s = stage.getBoundingClientRect();

    return {
        x: r.left - s.left + stage.scrollLeft + r.width,
        y: r.top - s.top + stage.scrollTop + r.height / 2
    };
}

function anchorLeft(el, stage) {
    const r = el.getBoundingClientRect();
    const s = stage.getBoundingClientRect();

    return {
        x: r.left - s.left + stage.scrollLeft,
        y: r.top - s.top + r.height / 2 + stage.scrollTop
    };
}


window.__bpDrawWires = drawWires;

