
var url = window.globalConfig.api;

var shenState = false
$(function () {
    getQrCode()
    $('.btn_1').click(function () {
        $('#qrCode').show();
        $('#qrCode_2').hide();
        $('.btn_1 p').css({backgroundColor:'#FE4E41',color:'#FFFFFF'})
        $('.btn_2 p').css({backgroundColor:'#F5F5F5',color:'#333333'})
        $('.operate').text('(长按可保存二维码到手机)')
        $('.explain').text('这是您的专属分享二维码，其他用户通过微信扫描该二维码将和您形成绑定关系。当扫描者办理ETC卡并成功安装激活后，推广佣金将计入您的收入。')
    })
    $('.btn_2').click(function () {
        $('#qrCode').hide();
        $('#qrCode_2').show();
        $('.btn_2 p').css({backgroundColor:'#FE4E41',color:'#FFFFFF'});
        $('.btn_1 p').css({backgroundColor:'#F5F5F5',color:'#333333'});
        if (shenState==true){
            $('.operate').text('(长按可保存二维码到手机)');
            $('.explain').text('这是您的专属分享二维码，其他用户通过支付宝扫描该二维码将和您形成绑定关系。当扫描者办理ETC卡并成功安装激活后，推广佣金将计入您的收入。')
        }
        else {
            $('.operate').text('正在审核中...');
            $('.explain').text('审核周期预计为1个工作日，审核通过后可进行3-6类货车的ETC推广。')
        }
    })
})

//获取二维码
getQrCode = ()=>{
    $.ajax({
        type : "post",
        url : url+'/Staff/getStaff',
        data:JSON.stringify({Key:sessionStorage.getItem('id')}),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{
            if (res.success == true){
                $('#qrCode').attr('src',res.data.qrCode);

                if (res.data.etcNoPic!='/PromotePic/'&&res.data.etcNoPic!='') {
                    shenState = true
                    $('#qrCode_2').attr('src',res.data.etcNoPic)
                    $('#qrCode_2').css({height:'26.9rem',width:'23.9rem'})
                    $('.operate').text('(长按可保存二维码到手机)')
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

