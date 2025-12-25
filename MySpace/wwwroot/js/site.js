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
    li.classList.add("tree-item");

    if (node.isDirectory) {
        const header = document.createElement("div");
        header.classList.add("tree-folder");

        const caret = document.createElement("span");
        caret.classList.add("tree-caret", "closed");

        const icon = document.createElement("span");
        icon.classList.add("tree-folder-icon");
        icon.textContent = "📁";

        const name = document.createElement("span");
        name.classList.add("tree-name");
        name.textContent = node.name;

        header.appendChild(caret);
        header.appendChild(icon);
        header.appendChild(name);
        li.appendChild(header);

        const childrenUl = document.createElement("ul");
        childrenUl.classList.add("tree-children");
        childrenUl.style.display = "none";

        node.children.forEach(child => {
            renderNode(child, childrenUl);
        });

        header.addEventListener("click", () => {
            const open = childrenUl.style.display === "block";
            childrenUl.style.display = open ? "none" : "block";

            caret.classList.toggle("open", !open);
            caret.classList.toggle("closed", open);
            icon.textContent = open ? "📁" : "📂";
        });

        li.appendChild(childrenUl);
    } else {
        li.classList.add("tree-file");
        li.innerHTML = `
            <span class="tree-file-icon">📄</span>
            <span class="tree-name">${node.name}</span>
        `;
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



