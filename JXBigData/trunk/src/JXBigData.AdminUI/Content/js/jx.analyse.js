// <reference path="jquery-2.1.3.min.js" >

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
var search = {}
search.init = function ()
{
    var startTime;
    var endTime;
    var typeID = 0; //时间分类
    var orderType = 0;//综合等排序
    var sourceID = 0;//来源
    var hidMJ = $(".s_lm");
    if ($("#startTime").val() != "") {
        hidMJ.show();
        $("#sTime").show();
        $("#sTime").val($("#startTime").val());
        $("#eTime").val($("#endTime").val());
        $("#eTime").show();
        $("#submit").show();
        $("#other").text("收起日历");
    } else {
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
            search.GetDefaultData();
        }
        else {
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
        if (id == "rm") {
            orderType = 0;
        }
        else if (id == "xt") {
            orderType = 1;
        }
        $("#orderType").val(orderType);
        search.GetDefaultData();
    });

    $(".m_tab span").click(function () {
        var sourceID=$(this).text().toString().trim();
        if (sourceID == "web")
        {
            sourceID = 0;
        }
        else if (sourceID == "h5")
        {
            sourceID = 4;
        }
        else if (sourceID == "ios") {
            sourceID = 101;
        } else
        {
            sourceID = 100;
        }
        $("#sourceID").val(sourceID);
        search.GetDefaultData();
    });
}

search.GetDefaultData = function ()
{
    var typeID = $("#typeID").val();
    var orderType = $("#orderType").val();
    var sourceID = $("#sourceID").val();
    var startTime;
    var endTime;
    if (typeID == 4) //抓取时间
    {
        startTime = $("#sTime").val().toString();
        endTime = $("#eTime").val().toString();
    }
    var url = '/Analyse/SearchList';
    url += '?orderType=' + orderType + '&typeID=' + typeID;
    url += '&sourceID=' + sourceID;
    if (startTime != undefined) {
        url += '&startTime=' + startTime + '&endTime=' + endTime;
    }
    window.location.href = url;
}

var region = {}
region.init = function ()
{
    var hidMJ = $(".s_lm");
    hidMJ.hide();
    $("#msg").hide();
    $("#msg1").hide();
    $("#sTime").hide();
    $("#eTime").hide();
    $("#submit").hide();
    if ($("#startTime").val() != "") {
        hidMJ.show();
        $("#sTime").show();
        $("#sTime").val($("#startTime").val());
        $("#eTime").val($("#endTime").val());
        $("#eTime").show();
        $("#submit").show();
        $("#other").text("收起日历");
    } else {
        hidMJ.hide();
        $("#sTime").hide();
        $("#eTime").hide();
        $("#submit").hide();
        $("#other").text("双日历");
    }
    var startTime;
    var endTime;
    $(".date a").click(function () {
        var typeID = 0;
        var aid = $(this).attr("id");
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
            region.mapData();
        }
        else {
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

}

region.mapData = function ()
{



    var typeID = $("#typeID").val();
    var orderType = $("#orderType").val();
    var sourceID = $("#sourceID").val();
    var startTime;
    var endTime;
    if (typeID == 4) //抓取时间
    {
        startTime = $("#sTime").val().toString();
        endTime = $("#eTime").val().toString();
    }
    var url = '/Analyse/GetProvinceData';
    var data = { startTime: startTime, endTime: endTime, typeID: typeID };
    defaultAjax(url, data, defaultCallback);
    var rdInfoList =$.parseJSON(_defaultData.data);
    //map 
    Highcharts.setOptions({
        lang: {
            drillUpText: " => {series.name}"
        }
    });
    var data = Highcharts.geojson(Highcharts.maps['countries/cn/custom/cn-all-china']), small = $('#map').width() < 400;
    // 给城市设置数据
    
    for (var i = 0; i < data.length; i++) {
        data[i].drilldown = data[i].properties['drill-key'];
        var isZero = false;
        for (var j = 0; j < rdInfoList.length; j++)
        {
            if (data[i].drilldown == rdInfoList[j].Province)
            {
                data[i].value = rdInfoList[j].TotalQuan;
                isZero = true;
                break;
            }
        }
        if (isZero==false)
        {
            data[i].value = 0;
        }
    }
    //初始化地图
    $('#map').highcharts('Map', {
        chart: {
            events: {
                drilldown: function (e) {
                    if (!e.seriesOptions) {
                       
                        var chart = this;
                        var cname = e.point.properties["cn-name"];
                        chart.showLoading('<i class="icon-spinner icon-spin icon-3x"></i>');

                        // 加载城市数据
                        var url = '/Analyse/GetCityData';
                        var data = { startTime: startTime, endTime: endTime, typeID: typeID, province: e.point.drilldown };
                        defaultAjax(url, data, defaultCallback);
                        var cityInfoList = $.parseJSON(_defaultData.data);
                          
                        var mapUrl = '/Analyse/GetCityMap';
                        var mapData = { province: e.point.drilldown };
                        defaultAjax(mapUrl, mapData, defaultCallback);
                        data = Highcharts.geojson($.parseJSON(_defaultData.data));
                        if (cityInfoList == null || cityInfoList.length == 0) {
                            $.each(data, function (i) {
                                this.value = 0;
                            });
                        } else {
                            for (var m = 0; m < data.length; m++) {
                                var isZero = false;
                                for (var n = 0; n < cityInfoList.length; n++) {
                                    if (data[m].name.indexOf(cityInfoList[n].City) != -1) {
                                        data[m].value = cityInfoList[n].TotalQuan;
                                        isZero = true;
                                        break;
                                    }
                                }
                                if (isZero == false) {
                                    console.log(data[m].name);
                                    data[m].value = 0;
                                }
                            }
                        }
                        chart.hideLoading();
                        chart.addSeriesAsDrilldown(e.point, {
                            name: e.point.name,
                            data: data,
                            dataLabels: {
                                enabled: true,
                                format: '{point.name}'
                            }
                        });
                    }


                    this.setTitle(null, { text: cname });
                },
                drillup: function () {
                    this.setTitle(null, { text: '中国' });
                }
            }
        },
        credits: {
            href: "#",
            text: "金象网所有"
        },
        title: {
            text: '地域分布'
        },

        subtitle: {
            text: '中国',
            floating: true,
            align: 'right',
            y: 50,
            style: {
                fontSize: '16px'
            }
        },

        legend: small ? {} : {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle'
        },
        //tooltip:{
        //pointFormat:"{point.properties.cn-name}:{point.value}"
        //},
        colorAxis: {
            min: 0,
            minColor: '#FFF8DC',
            maxColor: '#FF8247'
        },

        mapNavigation: {
            enabled: false,
            buttonOptions: {
                verticalAlign: 'bottom'
            }
        },

        plotOptions: {
            map: {
                states: {
                    hover: {
                        color: '#EEDD66'
                    }
                }
            }
        },

        series: [{
            data: data,
            name: '中国',
            dataLabels: {
                enabled: true,
                format: '{point.properties.cn-name}'
            }
        }],

        drilldown: {
            activeDataLabelStyle: {
                color: '#FFFFFF',
                textDecoration: 'none',
                textShadow: '0 0 3px #000000'
            },
            drillUpButton: {
                relativeTo: 'spacingBox',
                position: {
                    x: 0,
                    y: 60
                }
            }
        }
    });





}
