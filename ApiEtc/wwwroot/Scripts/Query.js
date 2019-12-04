﻿var opts;
var pager;
var pageNum = 1;
//是否是简单页面，如果是，则只显示表示
var simple = 0;


//Query实体类
var queryEnt = null
//行配置JSON对象
var cfgJson
/* 所有过虑数组 */
var allFilterArr = [{
    objFiled: "",
    opType: "",
    value: "",
    fieldType: ""
}]
/*
*加载完成的方法,可以重写该方法
*/
function LoadConfigComplete() {

}

function onBeforeLoad() {
    var opts = dataGrid.datagrid('options');
    console.log(opts)
    //第一次加载，设置默认排序
    try
    {
        if ((opts.sortName == null || opts.sortName == "") && defaultSortName!="") {
            opts.sortName = defaultSortName;
            opts.sortOrder = defaultSortOrder;

            dataGrid.datagrid(opts);
            return false;
        }
    }catch(e){
    }
    return true;
}



/**
获取Query实体类，以方便配置,加载完后，调用数据
*/
function LoadConfig() {

    var urlJson = UrlToJson()
    if (urlJson["code"] != null) {
        code = urlJson["code"];
    }


    $.ajax({
        url: singleApi,
        type: 'post',
        dataType: 'json',
        data: JSON.stringify({ 'Key': code }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            queryEnt = data.data;
            console.log(queryEnt)
            //添加列
            var allcolumns = [];
            if (queryEnt.queryCfgJson == null || queryEnt.queryCfgJson == "") queryEnt.queryCfgJson = "[]";
            cfgJson = {};
            eval("cfgJson="+queryEnt.queryCfgJson)
            console.log(cfgJson)

            /* 添加筛选字段 */
            MakeFilterItem(cfgJson)

            /* 配置显示的列 */
            if (queryEnt.showCheckbox) {
                allcolumns.push({ field: 'ck', checkbox: true })
            }

                        //配置行按钮
            if (queryEnt.rowsBtn == null || queryEnt.rowsBtn == "") queryEnt.rowsBtn = "[]";
            rowsBtn = eval(queryEnt.rowsBtn)
            var nameLength = 0;
            for (var i = 0; i < rowsBtn.length; i++) {
                nameLength += rowsBtn[i].Name.length;
            }
            if (nameLength > 0) {
                allcolumns.push({ title: '操作', field: 'action', formatter: ShowRowBtn, width: nameLength * 20 })
            }

            for (var item in cfgJson) {

                if (cfgJson[item].editable == null || cfgJson[item].editable == true || cfgJson[item].editable == 'true') {
                    var cfg = { field: item, title: cfgJson[item].title, sortable: true };
                    if (cfgJson[item].onComponentInitFunction) {
                        var f = function () { }
                        eval("f=" + cfgJson[item].onComponentInitFunction);
                        cfg["formatter"] =f;
                    }

                    if (cfgJson[item].width != null && cfgJson[item].width != '') {
                        cfg["width"] = cfgJson[item].width;
                    }
                    allcolumns.push(cfg)
                }
            }




            /* 配置头按钮 */
            var allBtn = [];
            //"[{"DialogMode":"Div","Name":"添加","IconCls":"icon-add","Url":"../Query/Edit","DialogWidth":"0","DialogHeigth":"0","Parameter":[]}]"
            if (queryEnt.heardBtn == null || queryEnt.HEARD_BTN == "") queryEnt.HEARD_BTN = "[]";
            headBtnList = eval(queryEnt.HEARD_BTN)
            for (var i = 0; i < headBtnList.length; i++) {
                btn = headBtnList[i]
                allBtn.push({
                    text: btn.Name,
                    btnJosn: btn,//按钮值，传到按钮事件里
                    iconCls: btn.IconCls,
                    handler: function (obj) {
                        //取出传进来的按钮JSON
                        btn = $(this).linkbutton("options").btnJosn
                        console.log(btn)
                        var btnUrl = btn.Url;
                        var s = btnUrl.indexOf('@@(');

                        while (s > -1) {
                            e = btnUrl.indexOf(')');
                            var t = btnUrl.substr(s + 3, e - s - 2);
                            console.log (t);
                            if (eval(t) != null) {
                                btnUrl = btnUrl.replace("@@(" + t + ")", eval(t));
                            }
                            s = btnUrl.indexOf('@@(');
                        }

                        console.log(btn.Parameter)
                        

                        if (btn.Parameter != null) {
                            for (var a = 0; a < btn.Parameter.length; a++) {
                                //用于判断是否有选择
                                var tmpParar=getSelections(btn.Parameter[a].ObjValue);
                                if (tmpParar == null) return;
                                if (btnUrl.indexOf('?') > 0) {
                                    btnUrl += "&" + btn.Parameter[a].Para + "=" + tmpParar;
                                }
                                else {
                                    btnUrl += "?" + btn.Parameter[a].Para + "=" + tmpParar;
                                }
                            }
                        }

                        console.log(btnUrl)

                        switch (btn.DialogMode) {
                            case "PromptAjax":
                                PromptAjaxUrl(btn.Name, btnUrl)
                                break;
                            case "Ajax":
                                DelAjaxUrl(btn.Name, btnUrl)
                                break;
                            case "Div":
                                DivEditDialog(btnUrl, btn.Name, btn.DialogWidth, btn.DialogHeigth)
                                break;
                            case "WinOpen":
                                WindowOpen(btn.Name, btnUrl, btn.DialogWidth, btn.DialogHeigth)
                                break;
                            case "DivDialog":
                                DivDialog(btnUrl, btn.Name, btn.DialogWidth, btn.DialogHeigth)
                                break;
                            case "TopDiv":
                                parent.DivOpen(btn.Name, btnUrl, btn.DialogWidth, btn.DialogHeigth)
                                break;
                            case "JsFun":
                                eval(btnUrl);
                                break;
                        }
                    }
                })
                allBtn.push('-')

            }

            if (queryEnt.isDebug == true || queryEnt.isDebug == "true") {
                allBtn.push({
                    text: "调试",
                    iconCls: 'icon-help',
                    handler: function () { ShowDebug() }
                })
                allBtn.push('-')
            }



            if (queryEnt.allowExport == true || queryEnt.allowExport == "true") {
                allBtn.push({
                    text: "导出",
                    iconCls: 'icon-sum',
                    handler: function () { DownData() }
                })
                allBtn.push('-')
            }

            //如果不是自动加载，则不显示对话框
            if (queryEnt.autoLoad == true || queryEnt.autoLoad == "true") {

                $('#div_filte').dialog({
                    closed: true,
                    width: '80%',
                    modal: true,
                    resizable: true,
                    maximizable: true,
                    collapsible: true,
                    top: 80,
                    left: 50,
                })

                allBtn.push({
                    text: "筛选",
                    iconCls: 'icon-filter',
                    handler: function () { $('#div_filte').dialog('open') }
                })
            }
            else {
                $('#div_filte').panel({
                    width: '99%',
                    collapsible: true,
                })
            }



            /* 配置参数 */
            var postJson = GetPostParmJson()

            var gridConfig = {
                url: '',
                method: 'post',
                queryParams: postJson, //配置提交的参数
                columns: [allcolumns],
                toolbar: allBtn, //配置表头按钮
                pageSize: queryEnt.pageSize,
                pageList: [10, 15, 20, 25, 30, 35],
                rownumbers: true,
                //rowStyler: function (index, row) {
                //    if (index % 2 == 0) {
                //        return 'background-color:#eaf2aa;';
                //    }
                //},
                height: ($(window).height() - 90),
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                collapsible: true,
                pagination: true,//分页栏
                remoteSort: true,
                //fitColumns:true,
                singleSelect: queryEnt.showCheckbox != 1,
                onLoadSuccess: onLoadSuccess,
                onBeforeLoad:onBeforeLoad,
                onHeaderContextMenu: function (e, field) {
                    e.preventDefault();
                    if (!cmenu) {
                        createColumnMenu();
                    }
                    cmenu.menu('show', {
                        left: e.pageX,
                        top: e.pageY
                    });
                }
            }
            console.log(gridConfig)
            dataGrid.datagrid(gridConfig);


            if (queryEnt.JS_STR != null) {
                eval(queryEnt.JS_STR) 
            }

            setTimeout(function () {
                dataGrid.datagrid('getPager').pagination({
                    pageSize: 10,
                    pageNumber: 1,
                    pageList: [10, 20, 30, 40, 50],
                    beforePageText: '第',
                    afterPageText: '页    共 {pages} 页',
                    displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                });

                if (queryEnt.autoLoad) {
                    Search();
                }
            }, 100)

            LoadConfigComplete();

        }
    });
}
/* 生成过滤字段,并初始化allFilterArr */
function MakeFilterItem(cfgJson) {
    allFilterArr = [];
    for (var item in cfgJson) {

        if (cfgJson[item].filter == null || cfgJson[item].filter != false) {
            var strHtml = $("#filterItem").html();
            strHtml = strHtml.replace(new RegExp('{title}', "g"), cfgJson[item].title);
            strHtml = strHtml.replace(new RegExp('{fieldName}', "g"), item);
            $("#filterItem").before(strHtml);
            $("#filterItem").hide();

            $(".textbox-label-top").removeAttr("for");



            var menuStr = ""
            var opTypeContent = '<div id="mm_' + item + '" style="width:150px;display:none">';
            opType = ""
            switch (cfgJson[item].type) {
                case "numberbox":
                    $('#s_' + item + '_value').numberbox();
                    opTypeContent += "<div onclick=\"CheckType('" + item + "','=', '等于')\">等于</div>";
                    opTypeContent += "<div onclick=\"CheckType('" + item + "','>', '大于')\">大于</div>";
                    opTypeContent += "<div onclick=\"CheckType('" + item + "','<', '小于')\">小于</div>";
                    opType = "=";
                    $("#type_" + item + "_lable").html('等于')

                    break;
                case "combobox":
                    var t = {};
                    if (cfgJson[item].SearchScript == null || cfgJson[item].SearchScript == '') cfgJson[item].SearchScript = "{}";
                    eval("t=" + cfgJson[item].SearchScript)
                    //修改默认不可编辑
                    if (!t.hasOwnProperty("editable")) t["editable"] = false;

                    $('#s_' + item + '_value').combobox(t)
                    if (t.multiple) {
                        opType = "in";
                        opTypeContent += "<div onclick=\"CheckType('" + item + "','in', '等于')\">等于</div>";
                        $("#type_" + item + "_lable").html('等于')
                    }
                    else {
                        opTypeContent += "<div onclick=\"CheckType('" + item + "','=', '等于')\">等于</div>";
                        opType = "=";
                        $("#type_" + item + "_lable").html('等于')
                    }
                    break;
                case "datetimebox":     //时间段
                    opTypeContent += "<div onclick=\"CheckType('" + item + "','between', '区间')\">区间</div>";
                    opTypeContent += "<div onclick=\"CheckType('" + item + "','not between', '不在区间')\">不在区间</div>";
                    var t = {};
                    try {
                        if (cfgJson[item].SearchScript == null || cfgJson[item].SearchScript == '') cfgJson[item].SearchScript = "{}";
                        eval("t=" + cfgJson[item].SearchScript)
                    } catch (e) { }
                    if (t == null) t = {};
                    if (!t.hasOwnProperty("timePicker")) t.timePicker = false;
                    if (!t.hasOwnProperty("timePicker24Hour")) t.timePicker24Hour = true;
                    if (!t.hasOwnProperty("linkedCalendars")) t.linkedCalendars = false;
                    if (!t.hasOwnProperty("autoUpdateInput")) t.autoUpdateInput = false;
                    if (!t.hasOwnProperty("autoApply")) t.autoApply = true;
                    if (!t.hasOwnProperty("locale")) {
                        t.locale = {
                            "format": 'YYYY-MM-DD',
                            "separator": ' ~ ',
                            "daysOfWeek": ["日", "一", "二", "三", "四", "五", "六"],
                            "monthNames": ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
                        };
                    }
                    else {
                        var locale = t.locale;
                        if (!locale.hasOwnProperty("applyLabel")) locale.applyLabel = "应用";
                        if (!locale.hasOwnProperty("cancelLabel")) locale.cancelLabel = "取消";
                        if (!locale.hasOwnProperty("resetLabel")) locale.resetLabel = "重置";
                        if (!locale.hasOwnProperty("customRangeLabel")) locale.customRangeLabel = "自定义";
                        if (!locale.hasOwnProperty("format")) locale.format = "YYYY-MM-DD";
                        if (!locale.hasOwnProperty("separator")) locale.separator = "~";
                        if (!locale.hasOwnProperty("daysOfWeek")) locale.daysOfWeek = ["日", "一", "二", "三", "四", "五", "六"];
                        if (!locale.hasOwnProperty("monthNames")) locale.monthNames = ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"];
                        t.locale = locale;
                    }

                    //显示样式
                    $('#s_' + item + '_value').textbox()
                    //设置控件
                    $('#s_' + item + '_value').daterangepicker(t, function (start, end, label) {
                        if (!start.isValid()) {
                            this.element.val('');
                            $('#' + this.element.attr('id')).textbox("setValue", '')
                        } else {
                            var v = this.startDate.format(this.locale.format) + this.locale.separator + this.endDate.format(this.locale.format);
                            this.element.val(v);
                            $('#' + this.element.attr('id')).textbox("setValue", v)
                        }
                    });

                    //清空数据
                    $('#s_' + item + '_value').on('cancel.daterangepicker', function (ev, picker) {
                        $('#' + this.id).textbox("setValue", '')
                    });


                    //隐藏Easyui的样式
                    var fieldName = item
                    $('#s_' + fieldName + '_value').show();
                    $('#s_' + fieldName + '_value').next().hide();

                    opType = "between";
                    $("#type_" + item + "_lable").html('区间')
                    break;

                default:
                    $('#s_' + item + '_value').textbox();
                    opTypeContent += "<div onclick=\"CheckType('" + item + "','like', '包含')\">包含</div>";
                    opTypeContent += "<div onclick=\"CheckType('" + item + "','=', '等于')\">等于</div>";

                    opType = "like";
                    $("#type_" + item + "_lable").html('包含')
                    break
            }
            opTypeContent += "<div onclick=\"CheckType('" + item + "','is null', '为空')\">为空</div>";
            opTypeContent += "<div onclick=\"CheckType('" + item + "','is not null', '不为空')\">不为空</div>";
            opTypeContent += " </div>"

            $("body").append(opTypeContent)
            try {
                $(".textbox-label-top").children("a").menubutton();
            } catch (e) { }

            allFilterArr.push({
                objFiled: item,
                opType: opType,
                value: '',
                fieldType: cfgJson[item].type,
            })


        }
    }
}


/* 选中的类型节点,如果为空则填写值 */
function CheckType(fieldName, type, typeName) {
    //循环所有筛选项，把选择的列，的类型付值
    for (var i = 0; i < allFilterArr.length; i++) {
        var item = allFilterArr[i];
        if (item == fieldName) {
            if (type == "is null" || type == "is not null") {
                switch (item.SearchType) {
                    case "numberbox":
                        $('#s_' + item + '_value').numberbox("setValue", type);
                        break;
                    case "datetimebox":
                        $('#s_' + item + '_value').datetimebox("setValue", type)
                        break;
                    case "combobox":
                        $('#s_' + item + '_value').combobox("setValue", type);
                        break;
                    case "daterangepicker": //时间段
                        $('#s_' + item + '_value').val(type);
                        break;
                    default:
                        $('#s_' + item + '_value').textbox("setValue", type)
                        break;
                }
            }
            item.OpType = type;
            $("#type_" + item + "_lable").html(typeName)
            break;
        }
    }
    console.log(allFilterArr)
}

/*
fieldName:字段名称
type：类型
typeName：类型名称
value：值
*/
function SetFilterValue(fieldName, type, typeName,value) {
    //循环所有筛选项，把选择的列，的类型付值
    for (var i = 0; i < allFilterArr.length; i++) {
        var item = allFilterArr[i];
        if (item.objFiled == fieldName) {
            switch (item.fieldType) {
                case "numberbox":
                    $('#s_' + item.objFiled + '_value').numberbox("setValue", value);
                    break;
                case "datetimebox":
                    $('#s_' + item.objFiled + '_value').datetimebox("setValue", value)
                    break;
                case "combobox":
                    $('#s_' + item.objFiled + '_value').combobox("setValue", value);
                    break;
                case "daterangepicker": //时间段
                    $('#s_' + item.objFiled + '_value').val(value);
                    break;
                default:
                    $('#s_' + item.objFiled + '_value').textbox("setValue", value)
                    break;
            }
            item.OpType = type;
            $("#type_" + item.objFiled + "_lable").html(typeName)
            break;
        }
    }
    console.log(allFilterArr)
}

/* 获取过虑控件的值 */
function GetFieldNameValue(fieldName) {
    try {
        for (var item in cfgJson) {
            if (item == fieldName) {
                switch (cfgJson[item].type) {
                    case "numberbox":
                        return $('#s_' + item + '_value').numberbox("getValue")
                    case "combobox":
                        var valList = $('#s_' + item + '_value').combobox("getValues");
                        if (valList.length == 0) {
                            return "";
                        }
                        return valList.join(",");
                    case "datetimebox": //时间段
                        return $('#s_' + item + '_value').val();
                    default:
                        return $('#s_' + item + '_value').textbox("getValue");
                }
            }
        }
        return $("#s_" + fieldName + "_value").val()
    } catch (e) {
        //alert("获取筛选值[" + fieldName+"]出错");
    }
}

/*
获取请求的参数
orderStr:"排序值" 如:ID DESC
*/
function GetPostParmJson(orderStr) {
    var useWhereList = [];
    //获取所有过滤的字段
    for (var i = 0; i < allFilterArr.length; i++) {
        tmp = allFilterArr[i];
        tmp.value = GetFieldNameValue(tmp.objFiled);
        if (tmp.value) {
            useWhereList.push(tmp);
        }
    }

    //如果没有传值，就取Grid列表的值
    if (orderStr == null) {
        //初始化的时候会报错
        try {
            var options = dataGrid.datagrid('options');
            var sortName = options.sortName;
            var sortOrder = options.sortOrder;
            if (sortName && sortOrder) {
                orderStr = sortName + " " + sortOrder
            }
        } catch (e) {
            orderStr=""
        }
    }

    //获取地址栏的参数
    var paraList = []
    var urlJson=UrlToJson()
    for (var key in urlJson) {
        paraList.push({ 'Key': key, 'Value': urlJson[key] })
        for (var i = 0; i < allFilterArr.length; i++) {
            tmp = allFilterArr[i];
            //表示地址栏名称跟筛选控件一样
            if (tmp.objFiled == key) {
                SetFilterValue(key, "=", "等于", urlJson[key])
            }
        }
    }
    
    var reObj = {
        'code': code,
        'page': pageNum,
        'rows': queryEnt.pageSize,
        'whereList': useWhereList,
    }
    for (var i = 0; i < paraList.length; i++) {
        item = paraList[i];
        reObj[item.Key] = item.Value
    }


    return reObj;
}

/* Json转Url
ojson:Json数据
*/
function JsonToUrl(ojson) {
    var s = '', name, key;
    for (var p in ojson) {
        if (!ojson[p]) { continue; }
        if (ojson.hasOwnProperty(p)) { name = p };
        key = ojson[p];
        s += "&" + name + "=" + encodeURIComponent(key);
    };
    return s.substring(1, s.length);
};

/**
查询数据
*/
function Search(orderStr) {

    var postJson = GetPostParmJson(orderStr)
    /* 重置分页 */
    var opts = dataGrid.datagrid('options');
    var pager = dataGrid.datagrid('getPager');
    opts.pageNumber = 1;
    opts.pageSize = opts.pageSize;
    pager.pagination('refresh', {
        pageNumber: 1,
        pageSize: opts.pageSize
    });
    /* 计算参数 */
    if ($('#tfrom').form('validate')) {
        var options = dataGrid.datagrid('options');
        options.queryParams = postJson;



        if (options.url == "") {
            options.url = bindListApi;
            dataGrid.datagrid(options);
        } else {
            dataGrid.datagrid("reload");
        }
    }
};



/* 用于表头按钮,计算每列的指定的值 */
function getSelections(fieldName) {


    if (fieldName == null || fieldName == '') {
        alert('列名不能为空');
        return null;
    }
    var selectRows = dataGrid.datagrid("getSelections");
    var arrayObj = [];　//创建一个数组
    for (var i = 0; i < selectRows.length; i++) {
        arrayObj.push(selectRows[i][fieldName]);
    }

    if (arrayObj.length < 1) {
        alert('请选中需要操作的行');
        return null;
    }
    var reStr = arrayObj.join(',');
    return reStr;
}


/**
加载成功
*/
function onLoadSuccess(data) {
    $(".datagrid-header-check").html("");
    if (data.total + "" == '0') {
        $(".datagrid-view2 .datagrid-body").append('<div style="text-align:center;color:red">没有相关记录！</div>')
    }
    else {
        $(this).closest('div.datagrid-wrap').find('div.datagrid-pager').show();
    }
}


/* 显示调试信息 */
function ShowDebug() {
    $.ajax({
        url: "../Query/GetDubug?t=" + Math.random(),
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        data: {
            code: code
        },
        success: function (data) {
            $.messager.show({
                title: "查询语句：" + code,
                msg: "<textarea style='width: 100%;height:100%;border-width: 1;'>" + data + "</textarea>",
                showType: 'slide',
                width: 300,
                height: 200
            });
        },
        error: function (data) {
            alert('失败:' + data.responseText);
        }
    });
}

/* 导出数据 */
function DownData() {
    $.messager.progress({
        title: '下载提示',
        msg: '正在下载..',
        text: '努力中...',
        interval: '1000'
    });
    setTimeout(function () {
        $.messager.progress('close')
    }, 10000)
    var urlParm = JsonToUrl(GetPostParmJson())
    window.location = '../Query/DownFile?' + urlParm;
}

/** 清除所有输入 */
function ClearFilter() {
    for (var i = 0; i < allFilterArr.length; i++) {
        allFilterArr[i].value = '';
    }
    for (var item in cfgJson) {
        if (cfgJson[item].filter == null || cfgJson[item].filter != false) {
            switch (cfgJson[item].type) {
                case "numberbox":
                    $('#s_' + item + '_value').numberbox("setValue", "")
                    break;
                case "datetimebox":
                    $('#s_' + item + '_value').datetimebox("setValue", "")
                    break;
                case "combobox":
                    $('#s_' + item + '_value').combobox("clear")
                    break;
                default:
                    $('#s_' + item + '_value').textbox("setValue", "")
                    break
            }

        }
    }

}

//把行按钮转换成字符串，作为按钮显示
function ShowRowBtn(value, row, index) {
    var jsonObj = eval(queryEnt.rowsBtn);

    var btnStr = '<div class="div-btn-deal">';
    var butnTxtLeng = 0;
    for (var i = 0; i < jsonObj.length; i++) {

        var btn = jsonObj[i];
        var allAuthStr = "," + $("#NoAuthority").val() + ",";
        if (allAuthStr.indexOf("," + btn.Name + ",") > -1) {
            continue;
        }

        if (jsonObj[i].Url == null || jsonObj[i].Url == "" || jsonObj[i].Url == 'null') {
            continue;
        }

        var s = jsonObj[i].Url.indexOf('@@(');
        while (s > -1) {
            e = jsonObj[i].Url.indexOf(')');
            var t = jsonObj[i].Url.substr(s + 2, e - s - 2);
            var evelV = null;
            try {
                evelV = eval(t);
            }
            catch (e) {
            }
            if (evelV == null) {
                jsonObj[i].Url = jsonObj[i].Url.replace("@@(" + t + ")", eval("row." + t));
            }
            else {
                jsonObj[i].Url = jsonObj[i].Url.replace("@@(" + t + ")", evelV);
            }
            s = jsonObj[i].Url.indexOf('@@(');
        }
        if (jsonObj[i].Url == null || jsonObj[i].Url == "" || jsonObj[i].Url == 'null') {
            continue;
        }
        jsonObj[i].Url = jsonObj[i].Url.replace("~/", '');


        var isShow = false;
        if (jsonObj[i].ShowCondition == null || jsonObj[i].ShowCondition.length == 0) isShow = true;
        for (var x = 0; x < jsonObj[i].ShowCondition.length; x++) {
            var v = jsonObj[i].ShowCondition[x].Value;
            str = "row." + jsonObj[i].ShowCondition[x].ObjFiled + jsonObj[i].ShowCondition[x].OpType + v;
            try {
                isShow = eval(str);
            } catch (e) { }
            if (isShow) break;
        }
        if (!isShow) continue;
        var paraStr = "?";
        if (jsonObj[i].Url.indexOf('?') > 0) {
            paraStr = "&";
        }
        if (jsonObj[i].Parameter != null) {
            for (var x = 0; x < jsonObj[i].Parameter.length; x++) {
                var v = jsonObj[i].Parameter[x].ObjValue;
                if (v == null) continue;
                if (v == "{input}") {
                    filedV = "{input:" + jsonObj[i].Parameter[x].Para + "}";
                }
                else {
                    var t_str = "";
                    if (jsonObj[i].Parameter[x].ObjValue.indexOf("@@(") == 0) {
                        var t = jsonObj[i].Parameter[x].ObjValue.replace("@@(", "").replace(")", "")
                        if (eval(t) != null) {
                            t_str = t;
                        }
                    }
                    else {
                        t_str = "row." + jsonObj[i].Parameter[x].ObjValue;
                    }
                    var filedV = eval(t_str);
                }
                paraStr += jsonObj[i].Parameter[x].Para + "=" + filedV + "&"
            }
        }


        var tmp = '[<a  href="#" ';
        if (jsonObj[i].DialogWidth == '') jsonObj[i].DialogWidth = '0';
        if (jsonObj[i].DialogHeigth == '') jsonObj[i].DialogHeigth = '0';
        switch (jsonObj[i].DialogMode) {
            case "PromptAjax":
                tmp += ' onclick="PromptAjaxUrl(\'' + jsonObj[i].Name + '\',\'' + jsonObj[i].Url + paraStr + '\')">';
                break;
            case "Ajax":
                tmp += ' onclick="DelAjaxUrl(\'' + jsonObj[i].Name + '\',\'' + jsonObj[i].Url + paraStr + '\')">';
                break;
            case "Div":
                tmp += ' onclick="DivEditDialog(\'' + jsonObj[i].Url + paraStr + '\',\'' + jsonObj[i].Name + '\',' + jsonObj[i].DialogWidth + ',' + jsonObj[i].DialogHeigth + ')"> '
                break;
            case "WinOpen":
                tmp += ' onclick="WindowOpen(\'' + jsonObj[i].Url + paraStr + '\',\'' + jsonObj[i].Name + '\',' + jsonObj[i].DialogWidth + ',' + jsonObj[i].DialogHeigth + ')"> '
                break;
            case "DivDialog":
                tmp += ' onclick="DivDialog(\'' + jsonObj[i].Url + paraStr + '\',\'' + jsonObj[i].Name + '\',' + jsonObj[i].DialogWidth + ',' + jsonObj[i].DialogHeigth + ')"> '
                break;
            case "TopDiv":
                tmp += ' onclick="parent.DivOpen(\'' + jsonObj[i].Url + paraStr + '\',\'' + jsonObj[i].Name + '\',' + jsonObj[i].DialogWidth + ',' + jsonObj[i].DialogHeigth + ')"> '
                break;
            case "JsFun":
                tmp += ' onclick="' + jsonObj[i].Url + '"> '
                break;
        }
        butnTxtLeng += jsonObj[i].Name.length;
        tmp += '<span class="btn-deal">' + jsonObj[i].Name + '</span></a>]  '
        btnStr += tmp;
    }
    btnStr += '</div>'
    return btnStr;
}


/**
获取地址栏参数
*/
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
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