
window.globalConfig = {
    api:'http://192.168.2.34:100/' //本地
};

//rem 单位换算
function window_width() {
    var myHtml = document.documentElement;
    var VisualWindow = myHtml.getBoundingClientRect().width;
    // var VisualWindow = myHtml.clientWidth;
    rem = VisualWindow / 37.5;
    myHtml.style.fontSize = rem + "px"
}
window_width();
window.addEventListener("resize", function() {
    window_width()
}, false);



