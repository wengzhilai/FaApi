
var url = window.globalConfig.api;
var notPayMoney; //未提现金额
var noPaidNum;//未提现人数
$(function () {
    getClientReport()
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
                notPayMoney = res.data.noPaidMoney;
                noPaidNum  = res.data.noPaidNum
                $('.getMoney').text(`${res.data.noPaidMoney}.00`)
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

// 申请提现
var flag = true;//简单防抖
withdrawal = ()=>{
    if (notPayMoney<=0) return alert('可提现金额不足'); //判断提现金额
    var name = $('#name').val(),
        account = $('#account').val()
    var Data = {
            walletAccountType: 1,
            walletaAcount: account,
            walletAccountName: name,
            Key: sessionStorage.getItem('id'),
            clientNum:noPaidNum
        }
        if(name!=''&&account!=''&&flag==true){
            flag = false
            $.ajax({
                type : "post",
                url : url+'/Wallet/submitWallet',
                data:JSON.stringify(Data),
                datatype : 'json',
                contentType: "application/json; charset=utf-8",
                success:(res)=>{
                    if (res.success==true){
                        alert('提交成功')
                        getClientReport()
                        $('#account').val('')
                        $('#name').val('')
                    }
                    else {
                        alert(res.msg)
                    }
                    flag = true
                },
                error:(err)=>{
                    flag = true
                    alert('请求服务器失败')
                }
            })
        }
        else {
            alert('请将信息填写完整')
        }


}

