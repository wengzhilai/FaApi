
// 本地
var url = window.globalConfig.api;

$(function () {
    $('.myBtn').hide()
    $('#fallInInfo').hide()
    var open_id = getQueryVariable('openid');
    sessionStorage.setItem('id',open_id)
    getAccountInfo(open_id)
})

// 提交申请信息
// 简单设置请求防抖
var flag = true;
submitUserIfo =()=>{
    var name = $('#name').val(),
        phone = $('#phone').val(),
        idCard = $('#idCard').val()
            if (name!=''&& phone!=''&&idCard!=''){

                if (!(/^1[3456789]\d{9}$/.test(phone))) {
                   return  alert('请正确输入手机号')
                }

                 if (!(/(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/.test(idCard))){
                   return  alert('请正确输入身份证号码')
                }
                    if (!flag) return false;

                    flag = false

                    var Data = {
                        name:name,
                        phone:phone,
                        idNo:idCard,
                        key:sessionStorage.getItem('id')
                    }
                    $.ajax({
                            type : "post",
                            url : url+'/Staff/bindUser',
                            data:JSON.stringify(Data),
                            datatype : 'json',
                            contentType: "application/json; charset=utf-8",
                            success:(res)=>{
                                if (res.success==true){
                                    $('.background').show()
                                }
                                else {
                                    flag = true
                                    alert(res.msg)
                                }

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

// 申请成功
successBtn = ()=>{
    $('.background').hide();
    $('#fallInInfo').hide();
    $('.myBtn').show()
}

//账号是否已申请注册
getAccountInfo = (id)=>{
    $.ajax({
        type : "post",
        url : url+'/Staff/checkIsBind',
        data:JSON.stringify({Key:id}),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{
            if (res.success == true) {
                if (res.data==true){
                    $('#fallInInfo').hide();
                    $('.myBtn').show()
                } else {
                    $('.myBtn').hide();
                    $('#fallInInfo').show()
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
}

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
