function popup(imgUrl) {
    var isIE = (navigator.appName == "Microsoft Internet Explorer");
    window.open("popup.html?" + imgUrl, 'ImageWindow', 'top=50,left=50,width=250,height=250,toolbar=no,menubar=no,scrollbars=no,resizable=yes');
}