
var url = window.globalConfig.api;
$(function () {
    //页面数据年月日更新（当前日期前一天）
    var nowDay = new Date();
    nowDay.setTime(nowDay.getTime()-24*60*60*1000);
     var yesterday = nowDay.getFullYear()+"年" + (nowDay.getMonth()+1) + "月" + nowDay.getDate()+'日';
     var newText = '数据更新至'+yesterday
    $('.dateTime').text(newText)

    getClientReport()
    getQrCode()
})
//获取推广明细
getClientReport=()=>{
    $.ajax({
        type : "post",
        url : url+'/Client/clientReport',
        data:JSON.stringify({Key:sessionStorage.getItem('id')}),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{
            if (res.success==true){
                $('#notPayMoney').text(`${res.data.noPaidMoney}.00`)
                $('#allPerson').text(res.data.allNum)
                $('#allMoney').text(`${res.data.allMoney}.00`)
                $('#havePayMoney').text(`${res.data.paidMoney}.00`)
            }
            else {
                alert(res.msg)
            }

        },
        error:(err)=>{
            alert('请求服务器失败')
        }
    })
}

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
                $('#qrCode').attr('src',res.data.qrCode) //设置推广二维码
            }
            else {
                alert(res.msg)
            }
        },
        error:(err)=>{
            alert('请求服务器失败')
        }
    })
}

