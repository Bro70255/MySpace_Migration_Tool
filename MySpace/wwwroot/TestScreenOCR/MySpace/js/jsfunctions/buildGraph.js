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