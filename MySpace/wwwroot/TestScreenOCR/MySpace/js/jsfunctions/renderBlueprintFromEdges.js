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