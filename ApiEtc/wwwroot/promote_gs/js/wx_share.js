var id,
    url = window.globalConfig.api,
    uri = 'http://t4.ngrok.wjbjp.cn';  //高速通行助手
    // uri = 'http://sletc.56bs.cn';       //物流行业资讯

$(function () {
    id = getQueryVariable('id');
    get_wx_info()
})

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
                debug: false,   // 测试阶段，可以写为true，主要是为了测试是否配置成功
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
        // var shareUrl = window.location.href;
        var shareUrl = uri+'/promote_wl/sharePage.html?id='+id,
            imgUrl = uri+'/logo.png';
        var obj_firend ={
                title: 'ETC在线轻松办理，全国高速95折，免费赠送OBU设备', // 分享标题
                desc: '三分钟快速办理，免费邮寄设备，让你省省省，数量有限，立即领取', // 分享描述
                link: shareUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl:imgUrl,
                success: function () {
                    // 设置成功
                }
            },
            // 自定义“分享到朋友圈”及“分享到QQ空间”按钮的分享内容（1.4.0）
            obj_firends = {
                title: 'ETC在线轻松办理，全国高速95折，免费赠送OBU设备', // 分享标题
                link: shareUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl:imgUrl,
                success: function () {
                    // 设置成功
                }
            },
            obj_firends1={
                title: 'ETC在线轻松办理，全国高速95折，免费赠送OBU设备', // 分享标题
                link: shareUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl:imgUrl, // 分享图标
                success: function () {
                    // 用户点击了分享后执行的回调函数
                }
            },
            obj_firend1 = {
                title: 'ETC在线轻松办理，全国高速95折，免费赠送OBU设备', // 分享标题
                desc: '三分钟快速办理，免费邮寄设备，让你省省省，数量有限，立即领取', // 分享描述
                link: shareUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl:imgUrl, // 分享图标
                type: '', // 分享类型,music、video或link，不填默认为link
                dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                success: function () {
                    // 用户点击了分享后执行的回调函数
                }
            };
        wx.onMenuShareAppMessage(obj_firend1);
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
