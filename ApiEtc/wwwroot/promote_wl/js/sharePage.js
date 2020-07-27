
var id,
    url = window.globalConfig.api;
$(function () {
    id = getQueryVariable('id');
    getQrCode();
    $('.mask').click(function () {
        $('.mask').hide()
        $('.content').css({height:'auto',overflow:'scroll'})
    });
    $('.shareBtn').click(function () {
        $('.mask').show()
        $('.content').css({height:'100vh',overflow:'hidden'})
    })
})

//获取二维码
getQrCode = ()=>{
    $.ajax({
        type : "post",
        url : url+'/Staff/getStaff',
        data:JSON.stringify({Key:id}),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{
            if (res.success == true){
                $('#wx_qrCode').attr('src',res.data.qrCode)
                if (res.data.etcNoPic!=''&&res.data.etcNoPic!='/PromotePic/') {
                    $('#zfb_img').show()
                    $('.zfb').show()
                    $('#zfb_qrCode').attr('src',res.data.etcNoPic)
                }
            }
            else {
                alert(res.msg)
            }
        },
        error:(err)=>{
            alert('请求服务器失败')
        }
    })
};


//获取url参数
getQueryVariable=(variable)=> {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i=0;i<vars.length;i++) {
        var pair = vars[i].split("=");
        if(pair[0] == variable){return pair[1];}
    }
    return(false);
}
