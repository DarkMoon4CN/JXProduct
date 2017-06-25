/// <reference path="jquery-2.1.3.min.js" >

var _defaultData;
var _isSuccess = true;
function defaultAjax(url, data, callback) {
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        async: false,
        error: function () {
            _isSuccess = false;
            jx.alert('服务器访问失败，请查看网络是否畅通！');
        },
        success: defaultCallback
    });
}
function defaultCallback(data) {
    _defaultData = data;
}

var trade = {}
trade.init = function ()
{
    var startTime ;
    var endTime;
    var typeID = 0; //时间分类
    var orderType = 0;//综合等排序
    var sourceID = 0;//来源
    var cfId = 0; //分类
    var hidMJ = $(".s_lm");
    if ($("#startTime").val() != "") {
        hidMJ.show();
        $("#sTime").show();
        $("#sTime").val($("#startTime").val());
        $("#eTime").val($("#endTime").val());
        $("#eTime").show();
        $("#submit").show();
        $("#other").text("收起日历");
    } else
    {
        hidMJ.hide();
        $("#sTime").hide();
        $("#eTime").hide();
        $("#submit").hide();
        $("#other").text("双日历");
    }
    //typeID
    $(".date a").click(function () {
       
        var aid = $(this).attr("id");
        typeID = 0;
        if (aid != "other") {
            if (aid == "tm") //昨天
            {
                typeID = 1;
            }
            else if (aid == "sw") //7天
            {
                typeID = 2;
            }
            else if (aid == "st")//30天
            {
                typeID = 3;
            }
            else if (aid == "submit")//双日历
            {
                startTime = $("#sTime").val().toString();
                endTime = $("#eTime").val().toString();
                typeID = 4;
            }
            $("#typeID").val(typeID);
            trade.GetDefaultData();
        }
        else
        {
            //打开日历
            if (hidMJ.is(":hidden")) {
                hidMJ.show();
                $("#sTime").show();
                $("#eTime").show();
                $("#submit").show();
                $(this).text("收起日历");
            } else {
                hidMJ.hide();
                $("#sTime").hide();
                $("#eTime").hide();
                $("#submit").hide();
                $(this).text("双日历");
            }
        }
    });

    //orderType
    $(".li_con .list_px").click(function () {
        var id = $(this).attr("id");
        if (id == "zh")
        {
            orderType = 0;
        }
        else if (id == "fl")
        {
            orderType = 1;
        }
        else
        {
            orderType = 2;
        }
        $("#orderType").val(orderType);
        
        trade.GetDefaultData();
    });

    //cfid
    $("#cfDiv .tagcon").click(function () {
        orderType = 1;
        sourceID = 0;
        cfid = $(this).attr("id");
        $("#cfid").val(cfid);
        trade.GetDefaultData();
    });

    //sourceID
    $("#sourceDiv .tagcon").click(function () {
        orderType = 2;
        cfId = 0;
        sourceID = $(this).attr("id");
        $("#sourceID").val(sourceID);
        trade.GetDefaultData();
    });
   
}

trade.GetDefaultData=function()
{
    var typeID = $("#typeID").val();
    var orderType = $("#orderType").val();
    var cfid = $("#cfid").val();
    var sourceID = $("#sourceID").val();
    var startTime;
    var endTime;
    if (typeID == 4) //抓取时间
    {
        startTime = $("#sTime").val().toString();
        endTime = $("#eTime").val().toString();
    }
    var url = '/ProductTrade/List';
    url += '?pageIndex=1&orderType=' + orderType + '&cfid=' + cfid + '&typeID=' + typeID;
    url += '&sourceID=' + sourceID;
    if(startTime!=undefined)
    {
        url += '&startTime=' + startTime + '&endTime=' + endTime;
    }
    window.location.href = url;

}
