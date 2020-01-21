$(function () {
    var dialogStr = "" +
    '<div id="dialog_FullWin" title="添加" class="easyui-dialog" style="height: 250px; width: 550px;display:none" data-options="closed: true,iconCls:\'icon-properties\',modal:true,maximizable:true,resizable:true,onResize:function(){$(this).dialog(\'center\');}">' +
    '    <iframe scrolling="auto" id="openWindowIframe" name="openWindowIframe" frameborder="0" style="width: 100%; height: 98%; padding: 0px;"></iframe>' +
    '</div> ';
    $(dialogStr).appendTo("body");

})

function GetRootPath() {
    console.log(bootPATH);
    return bootPATH;
}


function ShowAddDialog(title, url, width, height) {
    url = url.replace("~/", '');
    url = GetRootPath() + url;
    if (width == 0) width = 800;
    if (height == 0) height = 600;
    var content = '<iframe id="iframeid" scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:98%;"></iframe>';
    openWin = $('<div id="myWinId" class="easyui-dialog" data-options="iconCls:&quot;icon-edit&quot;,closed:true" ></div>').appendTo(document.body);
    openWin.window({
        title: title,
        width: width === undefined ? 360 : width,
        height: height === undefined ? 300 : height,
        content: content,
        modal: true,
        minimizable: false,
        maximizable: false,
        shadow: false,
        cache: false,
        closed: false,
        collapsible: false,
        resizable: false,
        loadingMessage: '正在加载数据，请稍等片刻......',
        onClose: function () {
            openWin.window("destroy"); //后面可以关闭后的事件
        }
    });
}

function DelAjaxUrl(title, url) {
    
    url = url.replace("~/",'');
    url = GetRootPath() + url;
    $.messager.confirm(title + "提示", "确定要执行" + title + "操作？", function (isOk) {
        if (isOk) {
            console.log("执行:" + url)
            $.ajax({
                url: url,
                type: 'post',
                dataType: 'json',
                data: {
                    t: Math.random()
                },
                success: function (data) {
                    if (data.length < 5) {
                        if (data == 1) {
                            $.messager.alert("提示", "操作成功")
                        }
                        else
                        {
                            $.messager.alert("提示", "操作失败")
                        }
                    }
                    else {
                        if (data.IsSucess == true || data.IsSucess == "true") {
                            $.messager.alert("提示", data.RetMsg)
                        }
                        else {
                            $.messager.alert("提示", data.RetMsg)
                        }
                        console.log(data)
                    }
                    try{
                        dataGrid.datagrid('reload');
                    }catch(e){}
                },
                error: function (data) {
                    alert('操作失败')
                    console.log(data)
                }
            });
        }
    })

}

/*
传参数请求
*/
function PromptAjaxUrl(title, url) {

    url = url.replace("~/", '');
    url = GetRootPath() + url;

    $.messager.prompt(title + "提示", "确定要执行" + title + "操作？", function (text) {
        if (text) {
            console.log("执行:" + url)
            $.ajax({
                url: url,
                type: 'post',
                dataType: 'json',
                data: {
                    msg: text,
                    t: Math.random()
                },
                success: function (data) {
                    if (data == 1) {
                        $.messager.alert("提示", "操作成功");
                    }
                    else
                    {
                        if (data.IsSucess == true || data.IsSucess == "true") {
                            $.messager.alert("提示", data.RetMsg)
                        }
                        else {
                            $.messager.alert("提示", data.RetMsg)
                        }
                    }
                    console.log(data)
                    try {
                        dataGrid.datagrid('reload');
                    } catch (e) { }
                },
                error: function (data) {
                    alert('操作失败')
                    console.log(data)
                }
            });
        }
    });
    

}



var nowTop = 0;
function DivEditDialog(url, title, w, h) {
    url = url.replace("~/", '');
    console.log(1111);
    console.log(parent);
    console.log(GetRootPath());
    url = GetRootPath()+url;
    $('#openWindowIframe')[0].src = "";
    $('#openWindowIframe')[0].src = url;

    $('#dialog_FullWin').dialog({
        title: title,
        iconCls:'icon-save',
        closed: false,
        maximized: true,
        onClose: function () {
            $('#openWindowIframe')[0].src = "";
            $('#dg').datagrid('reload');
            isOpen = false;
        },
        buttons: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {

                $.messager.progress({
                    title: '提示',
                    msg: '正在处理中..',
                    text: '处理中...',
                    interval: '1000'
                });

                var myFrame = document.getElementsByName("openWindowIframe"); //获取所有name为myFrame 的iframe
                for (var i = 0; i < myFrame.length; i++) //进行循环
                {
                    var callBack = function (IsClose) {
                        console.log(IsClose)
                        if (IsClose) {
                            CloseWin();
                            try {
                                dataGrid.datagrid("reload");
                            } catch (e) { }
                        }
                    }
                    var FinishBack=function(){
                        $.messager.progress('close')
                    }
                    myFrame[i].contentWindow.onSubmit(callBack, FinishBack); //kindSubmit();为iframe页面的方法
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () {
                CloseWin();
            }
        }]
    });
    if (w == 0 || h == 0) {

        $('#dialog_FullWin').dialog("maximize");
    }
    else {
        $('#dialog_FullWin').dialog({
            width: w,
            height: h,
            maximized: false,
            onMaximize: function () {
                $("#dialog_FullWin").parent().css("top", $(document).scrollTop() + "px");
            }
        });
        $('#dialog_FullWin').dialog("center");
    }
    isOpen = true;
    nowTop = $('#dialog_FullWin').panel('options').top - $(document).scrollTop();
    if (nowTop < 0) nowTop = 0;
}

var isStop = false;
function DivDialog(url, title, w, h) {
    url = url.replace("~/",'');
    url = GetRootPath() + url;
    $('#openWindowIframe')[0].src = "";
    $('#openWindowIframe')[0].src = url;
    $('#dialog_FullWin').dialog({
        title: title,
        closed: false,
        maximized: true,
        buttons: [],
        onClose: function () {
            $('#openWindowIframe')[0].src = "";
            $('#dg').datagrid('reload');
            isOpen = false;
        }
    });
    if (w == 0 || h == 0) {

        $('#dialog_FullWin').dialog("maximize");
    }
    else {
        $('#dialog_FullWin').dialog({
            width: w,
            height: h,
            maximized: false,
            onMaximize: function () {
                $("#dialog_FullWin").parent().css("top", $(document).scrollTop() + "px");
            }
        });
        $('#dialog_FullWin').dialog("center");

    }
    isOpen = true;
    nowTop = $('#dialog_FullWin').panel('options').top - $(document).scrollTop();


    if (nowTop < 0) nowTop = 0;
}

/**
获取地址栏参数
*/
function getUrlParam (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
    
}

function CloseWin() {
    $('#dialog_FullWin').dialog('close');
}

/**
url地址参数转Json
*/
function UrlToJson(url) {
    url = url == null ? window.location.href : url;
    var search = url.substring(url.lastIndexOf("?") + 1);
    var obj = {};
    var reg = /([^?&=]+)=([^?&=]*)/g;
    search.replace(reg, function (rs, $1, $2) {
        var name = decodeURIComponent($1);
        var val = decodeURIComponent($2);
        val = String(val);
        obj[name] = val;
        return rs;
    });
    return obj;
}
