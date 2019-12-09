
var url = window.globalConfig.api;
var notPayMoney; //未提现金额
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
            notPayMoney = res.data.noPaidMoney
           $('.getMoney').text(`${res.data.noPaidMoney}.00`)
        },
        error:(err)=>{
            alert('网络繁忙')
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
            AllMoney: notPayMoney,
            walletAccountType: 1,
            walletaAcount: account,
            walletAccountName: name,
            Key: sessionStorage.getItem('id')
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
                    flag = true
                    getClientReport()
                    $('#account').val('')
                    $('#name').val('')
                },
                error:(err)=>{
                    flag = true
                }
            })
        }
        else {
            alert('请将信息填写完整')
        }


}

