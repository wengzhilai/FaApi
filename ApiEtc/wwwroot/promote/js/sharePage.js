
var id,
    url = window.globalConfig.api;
$(function () {
    id = getQueryVariable('id');
    get_wx_info()
    getQrCode();
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

//设置微信分享自定义
get_wx_info = ()=>{
    $.ajax({
        type : 'post',
        url : url+'WeiXin/GetJsApi',
        data:JSON.stringify({Key: location.href.split('#')[0]}),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{
            wx.config({
                debug: true,   // 测试阶段，可以写为true，主要是为了测试是否配置成功
                appId: res.data.appid,
                timestamp: res.data.timestamp,
                nonceStr: res.data.noncestr,
                signature: res.data.signature,
                jsApiList: ['updateAppMessageShareData','updateTimelineShareData']
            });
            // 自定义“分享给朋友”及“分享到QQ”按钮的分享内容（1.4.0）
            wx.ready(function () {   //需在用户可能点击分享按钮前就先调用
                wx.updateAppMessageShareData({
                    title: '分享给朋友', // 分享标题
                    desc: '分享给朋友', // 分享描述
                    link: location.href.split('#')[0], // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    // imgUrl: 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1576665988342&di=d92f32e6fd50e93c87ff3d877d20b401&imgtype=0&src=http%3A%2F%2Fku.90sjimg.com%2Felement_origin_min_pic%2F01%2F40%2F32%2F98573cf75c3bf04.jpg', // 分享图标
                    imgUrl:'./images/share.png',
                    success: function () {
                        alert('123')
                        // 设置成功
                    }
                });
                // 自定义“分享到朋友圈”及“分享到QQ空间”按钮的分享内容（1.4.0）
                wx.updateTimelineShareData({
                    title: '分享到朋友圈', // 分享标题
                    link: location.href.split('#')[0], // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    imgUrl: 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1576665988341&di=5f02f262d5f8601a87555af75c662558&imgtype=0&src=http%3A%2F%2Fpic.90sjimg.com%2Fdesign%2F00%2F08%2F16%2F10%2F592185577aeb9.png', // 分享图标
                    success: function () {
                        // 设置成功
                    }
                })
            });
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
