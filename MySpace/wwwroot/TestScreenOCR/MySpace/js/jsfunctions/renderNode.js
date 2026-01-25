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