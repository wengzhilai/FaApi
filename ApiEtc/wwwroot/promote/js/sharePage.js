
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
        url : url+'/WeiXin/GetJsApi',
        async: false,
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
                jsApiList: [
                    // 'updateAppMessageShareData',
                    // 'updateTimelineShareData',
                    'onMenuShareTimeline',
                    'onMenuShareAppMessage'
                ]
            });
        },
        error:(err)=>{
            alert('请求服务器失败')
        }
    });
    wx.ready(function () {   //需在用户可能点击分享按钮前就先调用
        // 自定义“分享给朋友”及“分享到QQ”按钮的分享内容（1.4.0）
        var shareUrl = window.location.href;
        var obj_firend ={
                title: 'ETC在线轻松办理，全国高速通行95折，免费赠送赠送OBU设备，数量有限，赶紧办理', // 分享标题
                desc: '几分钟快速办理，免费邮寄设备,让你省省省', // 分享描述
                link: shareUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                // imgUrl: 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1576665988342&di=d92f32e6fd50e93c87ff3d877d20b401&imgtype=0&src=http%3A%2F%2Fku.90sjimg.com%2Felement_origin_min_pic%2F01%2F40%2F32%2F98573cf75c3bf04.jpg', // 分享图标
                imgUrl:'./images/share.png',
                success: function () {
                    // 设置成功
                }
            },
            // 自定义“分享到朋友圈”及“分享到QQ空间”按钮的分享内容（1.4.0）
            obj_firends = {
                title: 'ETC在线轻松办理，全国高速通行95折，免费赠送赠送OBU设备，数量有限，赶紧办理', // 分享标题
                link: shareUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: 'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1576665988341&di=5f02f262d5f8601a87555af75c662558&imgtype=0&src=http%3A%2F%2Fpic.90sjimg.com%2Fdesign%2F00%2F08%2F16%2F10%2F592185577aeb9.png', // 分享图标
                success: function () {
                    // 设置成功
                }
            },
            obj_firends1={
                title: 'ETC在线轻松办理，全国高速通行95折，免费赠送赠送OBU设备，数量有限，赶紧办理', // 分享标题
                link: shareUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl:'./images/share.png', // 分享图标
                success: function () {
                    // 用户点击了分享后执行的回调函数
                }
        },
            obj_firend1 = {
                title: 'ETC在线轻松办理，全国高速通行95折，免费赠送赠送OBU设备，数量有限，赶紧办理', // 分享标题
                desc: '几分钟快速办理，免费邮寄设备,让你省省省', // 分享描述
                link: shareUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl:'./images/share.png', // 分享图标
                type: '', // 分享类型,music、video或link，不填默认为link
                dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                success: function () {
                    // 用户点击了分享后执行的回调函数
                }
            };
        wx.onMenuShareAppMessage(obj_firend1)
        wx.onMenuShareTimeline(obj_firends1)
        // wx.updateAppMessageShareData(obj_firend);
        // wx.updateTimelineShareData(obj_firends)
    });
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
