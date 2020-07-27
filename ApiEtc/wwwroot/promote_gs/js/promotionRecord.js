var url = window.globalConfig.api;

var page = 1, //分页码
    off_on = true;//分页开关(滚动加载方法中用的)

var tagType = 0; //查询列表1全部，2已结算，3待结算
var saveTagType = 0;
$(function () {
    $('.promoteSuccess').click(function () {
        tagType = 0;
        page = 1;
        //如果当前页面数据和请求数据相同则不进行请求
        $(this).css({borderBottom:'1px solid #FE4E41'});
        $('.havePay').css({borderBottom: 'none'});
        $('.notPay').css({borderBottom: 'none'});
        $('.bind').css({borderBottom: 'none'});

    });
    $('.havePay').click(function () {
        tagType = 1;
        page = 1;
        $(this).css({borderBottom:'1px solid #FE4E41'});
        $('.promoteSuccess').css({borderBottom: 'none'});
        $('.notPay').css({borderBottom: 'none'});
        $('.bind').css({borderBottom: 'none'});

    })
    $('.notPay').click(function () {
        tagType = 2;
        page = 1;
        $(this).css({borderBottom:'1px solid #FE4E41'});
        $('.havePay').css({borderBottom: 'none'});
        $('.promoteSuccess').css({borderBottom: 'none'});
        $('.bind').css({borderBottom: 'none'});
    });
    $('.bind').click(function () {
        tagType = 3;
        page = 1;
        $(this).css({borderBottom:'1px solid #FE4E41'});
        $('.havePay').css({borderBottom: 'none'});
        $('.promoteSuccess').css({borderBottom: 'none'});
        $('.notPay').css({borderBottom: 'none'});

    });

    //默认查询
    getClientReport();
    getClientList()

    document.querySelector('.promoteSuccess').addEventListener('click',de())
    document.querySelector('.havePay').addEventListener('click',de())
    document.querySelector('.notPay').addEventListener('click',de())
    document.querySelector('.bind').addEventListener('click',de())
});

// 获取推广列表
getClientList=()=>{
    var Data = {
        Key : sessionStorage.getItem('id'),
        page : page,
        rows : 10
    };
    if (tagType!==0){
        Data.payType = saveTagType
    }
    $.ajax({
        type : "post",
        url : url+'/Client/list',
        data:JSON.stringify(Data),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{
            if (res.success==true){
                //判断请求的数据length是否大于page。
                if (res.dataList.length<10){
                    off_on = false; //设置上拉加载状态
                }
                else {
                    off_on = true; //设置上拉加载状态
                }
                if (res.dataList.length>0){
                    for (var i=0;i<res.dataList.length;i++) {
                        var dom = `
                <div class="recordModules">
                    <div class="recordModules_top">
                    <div class="carNum">
                        <p>车牌号</p>:<span>${res.dataList[i].CarNum}</span>
                    </div>
                    <div>
                        <p>姓名</p>:<span>${res.dataList[i].ClientName}</span>
                    </div>
                    <div>
                        <p>电话</p>:<span>${res.dataList[i].ClientPhone}</span>
                    </div>    
                    <div class="createTime">
                        <p>申办时间</p>:<span>${res.dataList[i].SubmitTime}</span>
                    </div>   
                    </div>
                    <div class="recordModules_foot">
                        <p class="state">${res.dataList[i].Status}</p>
                        <p class="money">佣金金额:<span style="color: #FE4E41">${res.dataList[i].Money}</span></p>
                    </div>
                </div>
            `;

                        $('.allRecordList').append(dom)
                    }
                    var stateList = $('.state')
                    //已结算未结算状态展示
                    for (var k = (page-1)*10;k<stateList.length;k++){
                        if (tagType==3){
                            stateList[k].style.color = '#FE4E41';
                            $('.money').hide();  //绑定列表隐藏金额显示
                            $('.recordModules_foot').css({webkitJustifyContent:'flex-start'});//调整flex布局方式
                            if (res.dataList[k-(page-1)*10].Status=='已绑定') {
                                $('.recordModules_top').css({height:'7rem'});//调整高度
                                $('.carNum').hide();//隐藏车牌号
                                $('.createTime').hide()//时间隐藏
                            }
                        }
                        else if (tagType==2) {
                            stateList[k].style.color = '#FE4E41';
                        }
                        else if (tagType==1){
                            stateList[k].style.color = '#00C160';
                        }
                        else if (tagType==0){
                            if (res.dataList[k-(page-1)*10].Status=='待结算') {
                                stateList[k].style.color = '#FE4E41';
                            }
                            else  {
                                stateList[k].style.color = '#00C160';
                            }
                        }

                    }
                }
                else {
                    off_on = false;
                }

            }
            else {
                alert(res.msg)
            }
            $('.loading').hide()
        },
        error:(err)=>{
            alert('请求服务器失败')
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
            if (res.success==true){
                $('#promoteSuccess').text(`(${res.data.allNum})`);
                $('#havePay').text(`(${res.data.paidNum})`);
                $('#notPay').text(`(${res.data.noPaidNum})`);
                $('#bind').text(`(${res.data.bindNum})`)
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

//滚动加载方法1
$(document).scroll(function() {
    if($(window).height()+$(document).scrollTop()>=$(document.body).height()){
        if (off_on==true) {
            off_on=false;
            setTimeout(function(){
                page++;
                $('.loading').show()
                getClientList(); //上拉加载更多请求数据，不是第一次请求数据，不需要传参，采用默认参数
            },500)
        }
    }
});

//防抖
function de(){
    let timer=null;
    return function (){
        clearTimeout(timer);
        timer=setTimeout(function(){
            if (saveTagType!=tagType){
                $('.recordModules').remove();
                saveTagType = tagType;//赋值后请求
                getClientList()
            }
        },800);
    }
}


