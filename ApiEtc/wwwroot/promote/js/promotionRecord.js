var url = window.globalConfig.api;

var page = 1, //分页码
    off_on = true;//分页开关(滚动加载方法中用的)

var tagType = 0; //查询列表1全部，2已结算，3待结算
var saveTagType = 0;
$(function () {
    $('.promoteSuccess').click(function () {
        $(this).css({borderBottom:'1px solid #FE4E41'});
        $('.havePay').css({borderBottom: 'none'});
        $('.notPay').css({borderBottom: 'none'});
        $('.allRecordList').show()
        $('.notPayList').hide()
        $('.havePayList').hide()
        tagType = 0;
        page = 1;
        //如果当前页面数据和请求数据相同则不进行请求
        if (saveTagType==tagType){
            return false
        }
        else {
            $('.recordModules').remove();
            saveTagType = tagType;//赋值后请求
            getClientList()
        }
    });
    $('.havePay').click(function () {
        $(this).css({borderBottom:'1px solid #FE4E41'});
        $('.promoteSuccess').css({borderBottom: 'none'});
        $('.notPay').css({borderBottom: 'none'});
        $('.allRecordList').hide()
        $('.notPayList').hide()
        $('.havePayList').show()
        tagType = 1;
        page = 1;
        if (saveTagType==tagType){
            return false
        }
        else {
            $('.recordModules').remove();
            saveTagType = tagType
            getClientList()
        }
    })
    $('.notPay').click(function () {
        $(this).css({borderBottom:'1px solid #FE4E41'});
        $('.havePay').css({borderBottom: 'none'});
        $('.promoteSuccess').css({borderBottom: 'none'});
        $('.allRecordList').hide()
        $('.notPayList').show()
        $('.havePayList').hide()
        tagType = 2;
        page = 1;
        if (saveTagType==tagType){
            return false
        }
        else {
            $('.recordModules').remove();
            saveTagType = tagType
            getClientList()
        }

    })

    //默认查询
    getClientReport()
    getClientList()
})

// 获取推广列表
getClientList=()=>{
    var Data = {
        Key : sessionStorage.getItem('id'),
        payType : saveTagType,
        page : page,
        rows : 10
    };
    $.ajax({
        type : "post",
        url : url+'/Client/list',
        data:JSON.stringify(Data),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{

            //判断请求的数据length是否大于page。
            if (res.dataList.length<10){
                off_on = false; //设置上拉加载状态
            }
            else {
                off_on = true; //设置上拉加载状态
            }
            for (var i=0;i<res.dataList.length;i++) {
                var dom = `
                <div class="recordModules">
                    <div class="recordModules_top">
                    <div>
                        <p>车牌号</p>:<span>${res.dataList[i].CarNum}</span>
                    </div>
                    <div>
                        <p>姓名</p>:<span>${res.dataList[i].ClientName}</span>
                    </div>
                    <div>
                        <p>电话</p>:<span>${res.dataList[i].ClientPhone}</span>
                    </div>    
                    <div>
                        <p>申办时间</p>:<span>${res.dataList[i].SubmitTime}</span>
                    </div>   
                    </div>
                    <div class="recordModules_foot">
                        <p class="state">${res.dataList[i].Status}</p>
                        <p>佣金金额:<span style="color: #FE4E41">${res.dataList[i].Money}</span></p>
                    </div>
                </div>
            `
                if (tagType==0){
                    $('.allRecordList').append(dom)
                }
                else if (tagType==1){
                    $('.havePayList').append(dom)
                }
                else if (tagType==2){
                    $('.notPayList').append(dom)
                }


            }
            var stateList = $('.state')

            //已结算未结算状态展示
            for (var k = (page-1)*10;k<stateList.length;k++){
                if (res.dataList[(k/10)-page+1].Status=='已结算') {
                    stateList[k].style.color = '#00C160'
                }
                else {
                    stateList[k].style.color = '#FE4E41'
                }
            }



        },
        error:(err)=>{

        }
    })
};

//获取推广明细
getClientReport=()=>{
    $.ajax({
        type : "post",
        url : url+'/Client/clientReport',
        data:JSON.stringify({Key:sessionStorage.getItem('id')}),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{
            $('#promoteSuccess').text(`(${res.data.allNum})`)
            $('#havePay').text(`(${res.data.paidNum})`)
            $('#notPay').text(`(${res.data.noPaidNum})`)
        },
        error:(err)=>{
            alert('网络繁忙')
        }
    })
}

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


