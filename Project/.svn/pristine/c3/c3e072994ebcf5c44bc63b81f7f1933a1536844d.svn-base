
$(function () {
var cfid = $("#CFID").val();
$("#selectedBrand").each(function(){
        $(this).children().children().each(function () {
            if ($(this).attr("class") == "gb")
            {
                var isSelected = $(this).attr("Id");
                $("#AllAddBrand").each(function () {
                    $(this).children().children().each(function () {
                        if ($(this).attr("class") == "beiMian")
                        {
                            var waitSelected = $(this).attr("Id");
                            if (isSelected == waitSelected)
                            {
                                $(this).html("取消添加");
                            }
                        }
                    });
                }); }  }); });

$(".beiMian").click(function () {
    var brandid = $(this).attr("id");
    var isCretate = $(this).html();//添 加
    var outMegStr = "";
    //获取当前的上一级节点
    var liClone = $(this).parent().clone(true);
    $.ajax({
        url: '/brand/ModifyBrand',
        type: 'POST',
        data: 'cfid=' + cfid + '&brandid=' + brandid,
        async: false,
        error: function () {
            alert('服务器访问失败，请查看网络是否畅通！');
        },
        success: function (data) {
            if (data.status == true) {
                //样式*如果是未添加的状态
                if (isCretate == "添 加")
                {
                    liClone.children().last().remove();
                    liClone.append($("<a style='display: none;' class='gb' id='" + brandid + "' href='javascript:;'></a>"));
                    $("#selectedBrand").append(liClone);
                    outMegStr = "取消添加";
                }
                else
                {
                    $("#selectedBrand").find("#" + brandid).parent().remove();
                    outMegStr = "添 加";
                }
                parent.layer.msg(data.msg + "成功！", { time: 1000 })
                $(".addBranP").html("已添加" + data.data + "个品牌！");
            }
        }
    });
    $(this).html(outMegStr);
});
$('.selecWrap').select1(cfid, function (key, vlue) {
        var url = "/brand/list?cfid=" + key;
        window.location.href = url;
    });
var brandBox = $('.addBranBox');
brandBox.on("click", "a", function () { gbClick(this); });
brandBox.eq(0).on('mouseover mouseout', 'li', function (event) {
        switch (event.type) {
            case "mouseover":
                $(this).find('a').show();
                break;
            case "mouseout":
                $(this).find('a').hide();
                break;
        }
    });
brandBox.eq(1).on('mouseover mouseout', 'li', function (event) {
    switch (event.type) {
        case "mouseover":
            $(this).find('.beiMian').stop().animate({
                top: -1
            }, 200);
            break;
        case "mouseout":
            $(this).find('.beiMian').stop().animate({
                top: 79
            }, 200);
            break;
    }
});
});
function gbClick(obj) {
    var brandid = $(obj).attr("id");
    var cfid = $("#CFID").val();
    var _this = obj;
    var o = parent.layer.confirm('确定要取消品牌吗？', { icon: 1 }, function () {
        $.ajax({
            url: '/brand/ModifyBrand',
            type: 'POST',
            data: 'cfid=' + cfid + '&brandid=' + brandid,
            async: false,
            error: function () {
                jx.alert('服务器访问失败，请查看网络是否畅通！');
            },
            success: function (data) {
                if (data.status == true) {
                    parent.layer.msg(data.msg + "成功！", { time: 1000 })
                    $(".addBranP").html("已添加" + data.data + "个品牌！");
                }
            }
        });
        parent.layer.close(o);
        $(_this).parent().remove();
    })
    $("#AllAddBrand").find("#" + brandid).html("添 加");
}