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