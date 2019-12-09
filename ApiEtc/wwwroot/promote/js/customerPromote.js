
var url = window.globalConfig.api;
var open_id,ticket;
$(function () {
     open_id = getQueryVariable('openid');//获取openid，可能为undefined
     ticket = getQueryVariable('ticket');//获取ticket，可能为undefined
})

var flag = true
submitCustomerInfo=()=>{
    var name = $('#name').val(),
        phone = $('#phone').val();
    if (name!=''&& phone!=''){
        if (!(/^1[3456789]\d{9}$/.test(phone))) {
            alert('请输入正确的手机号')
        }
        else {
            if (!flag){
                alert('请勿重复提交')
                return false;
            }
            flag = false;
            var Data = {
                name:name,
                phone:phone,
            };
            if (open_id!=undefined){
                Data.Key = open_id
            }
             if (ticket!=undefined){
                Data.ticket = ticket
            }
            $.ajax({
                type : "post",
                url : url+'/Client/regClient',
                data:JSON.stringify(Data),
                datatype : 'json',
                contentType: "application/json; charset=utf-8",
                success:(res)=>{
                    if (res.success==true){
                        // $('#name').val('');
                        // $('#phone').val('');
                        window.location.href = `http://ssl.hltgz.com/web/qtk/etc/applyOpenCard/v3/main/selectPage?promoterNum=${res.code}&promoterChannelNum=fkn00001`
                    }
                    else {
                        flag = true
                        alert(res.msg)
                    }
                },
                error:(err)=>{
                    flag = true
                }
            })
        }
    }
    else {
        alert('请将信息填写完整')
    }
};
//获取url参数
getQueryVariable=(variable)=> {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i=0;i<vars.length;i++) {
        var pair = vars[i].split("=");
        if(pair[0] == variable){return pair[1];}
    }
    // return(false);
}
