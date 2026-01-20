function OpenImage(img) {
    var img = document.getElementById(img);
    window.open(img.src, 'Image', 'width=largeImage.stylewidth,height=largeImage.style.height,resizable=1');

}