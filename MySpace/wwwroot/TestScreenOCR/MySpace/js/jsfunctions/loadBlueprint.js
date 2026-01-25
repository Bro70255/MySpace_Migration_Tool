function loadBlueprint() {
    fetch('/Home/GetBlueprint')
        .then(r => r.json())
        .then(edges => renderBlueprintFromEdges(edges))
        .catch(console.error);
}