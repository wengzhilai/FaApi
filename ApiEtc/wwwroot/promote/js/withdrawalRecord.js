

var url = window.globalConfig.api;

var page = 1, //分页码
    off_on = true;//分页开关(滚动加载方法中用的)
$(function () {
    getWalletList()
})

var httpsState = true; //设置第一次请求的状态来控制是否展示没有记录时展示的dom
getWalletList=()=>{
    var Data = {
        Key : sessionStorage.getItem('id'),
        page : 1,
        rows : 15
    };
    $.ajax({
        type : "post",
        url : url+'/Wallet/list',
        data:JSON.stringify(Data),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{

            if (res.dataList.length<15){
                off_on = false
            }
            else {
                off_on = true
            }
            if(res.dataList.length>0){
                httpsState = false//判断这次查询的数据长度是否大于15，否则就为最后的数据
                for (var i=0;i<res.dataList.length;i++){
                            var dom = `
                        <div class="modules">
                           <div>
                                <p class="state">${res.dataList[i].Status}</p>
                                <p>${res.dataList[i].Money}</p>
                           </div>
                           <div>
                                 <p>${res.dataList[i].CreateTime}</p>
                           </div>
                        </div>
                    `
                    $('.content').append(dom)
                }
                var stateList = $('.state')
                for(var k=(page-1)*15;k<stateList.length;k++){
                    if(res.dataList[(k/15)-page+1].Status=='已结算'){
                        stateList[k].style.color = '#FE4E41'
                    }
                    else {
                        stateList[k].style.color = '#333333'
                    }
                }
            }
            else {
                console.log(httpsState)
                if (httpsState==true){
                    var dom = `<div class="noList"><p>暂无提现记录</p></div>`
                    $('.content').append(dom)
                }
            }
        },
        error:(err)=>{

        }
    })
};


//滚动加载方法1
$(document).scroll(function() {
    if($(window).height()+$(document).scrollTop()>=$(document.body).height()){
        if (off_on) {
            off_on=false
            setTimeout(function(){
                page++
                getWalletList(); //上拉加载更多请求数据，不是第一次请求数据，不需要传参，采用默认参数
            },500)
        }
    }
})
