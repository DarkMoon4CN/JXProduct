﻿/// <reference path="base.js" />
var activity = {};

//列表
activity.list = function () {

    $('#StartDate').on("focus", function () {
        $(this).attr("readonly", "readonly");
        WdatePicker({ doubleCalendar: true, dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-31', minDate: '2015-01-01' });
    });
    $('#EndDate').on("focus", function () {
        $(this).attr("readonly", "readonly");
        WdatePicker({ doubleCalendar: true, dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-31', minDate: '#F{$dp.$D(\'StartDate\')}' });
    });
    $('div.selecWrap').each(function (i, item) {
        $(item).select1();
    });
    $("#addactivity").on("click", function () {
        jx.open('/activity/edit', "活动编辑", 450, 600);
        return false;
    });
    var $tablist = $('#tablist');
    $tablist.on("click", "a[name=update]", function () {
        var $this = $(this);
        jx.open('/activity/edit?actid=' + $this.parent().attr("actid"), "活动编辑", 450, 600);
    });
    $tablist.on("click", 'a[name=del]', function () {
        var $this = $(this);
        var $parent = $this.parent();
        $.post('/activity/setstatus', { actid: $parent.attr("actid"), status: $this.attr("status") }, function (data) {
            if (data.status) {
                jx.alert("操作成功!")
                activity.reloadlist();
            } else {
                jx.error("失败了!" + data.msg);
            }
        });
    });
    $tablist.on("click", "a[name=rule]", function () {
        var $this = $(this);
        jx.open('/activity/rule?actid=' + $this.parent().attr("actid"), "规则编辑", 450, 600);
    });
    $tablist.on("click", "a[name=product]", function () {
        location.href = "/activity/product?actid=" + $(this).parent().attr("actid");
    });
    $('#search').on("click", function () {
        $('#form0').submit();
        return false;
    });
}

//刷新列表,用于弹出层回调
activity.reloadlist = function () {
    $('#tablist').load("/activity/list", { t: 1 });
};

//编辑
activity.edit = function () {
    var $select = $('div.selecWrap');
    var $activity = $select.eq(0);
    var $limit = $select.eq(1);
    var acttype = $activity.find("input").val();
    $activity.select1(acttype, function (key, value) {
        if (key == 7 || key == 8) {
            $limit.hide();
            $limit.find(".selectTitleLeft").text("请选择");
            $limit.find("input").val(0);
        }
        else {
            $limit.show();
        }
    });
    $limit.select1($limit.find("input").val());
    if (acttype == 7 || acttype == 8)
        $limit.hide();

    var curDate = new Date();
    var newDate = new Date(curDate.setDate(curDate.getDate() + 1));
    $('#StartTime').on("focus", function () {
        $(this).attr("readonly", "readonly");
        WdatePicker({ doubleCalendar: true, dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-31', minDate: newDate.toLocaleDateString() });
    });
    $('#EndTime').on("focus", function () {
        $(this).attr("readonly", "readonly");
        WdatePicker({ doubleCalendar: true, dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-31', minDate: '#F{$dp.$D(\'StartTime\')}' });
    });
    var $frm = $('#form1');
    $('.submit').on("click", function () {
        $frm.submit();
    });
}

//规则
activity.rule = function () {

    var $submit = $('a.submit');
    var $form = $('#form1');
    //加载商品
    var $edit = $('.kuang');
    var $msg = $('#msg');
    $edit.on("blur", "#ProductID", function (data) {
        var $this = $(this);
        if ($msg.length == 0) {
            $msg = $("<div id='msg' class='spInputWrap fl'><div id='content'></div>");
            $this.parent().after($msg);
        }
        var id = $this.val();
        if (/^\d+$/.test(id)) {
            $.post("/product/get", "productid=" + id, function (data) {
                if (data.status) {
                    $msg.find("#content").html("商品名称：" + data.data.ChineseName);
                } else {
                    $msg.find("#content").html('输入的ID 不正确');
                    $this.val('');
                }
                $this.parent().after($msg);
            });
        }
    });


    //提交
    $submit.on("click", function () {
        $form.submit();
    });

    $table = $('.generalTable');

    //删除
    $table.on("click", "a[name=del]", function () {
        var $this = $(this);
        jx.del(function () {
            $.ajax({ type: "post", url: "/activity/ruledel", data: { ruleid: $this.parent().attr("ruleid") } }).done(function (data) {
                if (data.status) {
                    jx.alert("删除成功!");
                    $this.parent().parent().remove();
                } else {
                    jx.error("失败了!");
                }
            });
        });
    });

    //修改 
}

//关联商品
activity.product = function () {

    $('#search').on("click", function () {
        $('#form1').submit();
        return false;
    });

    var $cf1, $cf2, $cf3;
    var $items = $('div.selecWrap');
    $cf1 = $items.eq(0);
    $cf2 = $items.eq(1);
    $cf3 = $items.eq(2);

    getcf(0, $cf1, true);
    $cf1.select1(0, function (key, value) {
        clearitem($cf2);
        clearitem($cf3);
        getcf(key, $cf2);
    });
    $cf2.select1(0, function (key, value) {
        clearitem($cf3);
        getcf(key, $cf3);
    });
    $cf3.select1();


    var $tablist = $('div.productdata');
    $('#nav span').on("click", function () {
        var $this = $(this);
        $this.addClass("active").siblings().removeClass("active");
        $tablist.addClass("hide");
        $tablist.eq($this.index()).removeClass("hide");
    });

    $tablist.on("click", "[name='allchecked']", function () {
        var $this = $(this);
        $this.parent().parent().parent().next().find("input").prop("checked", $this.is(":checked"));
    });

    $tablist.on("click", "a[name=add]", function () {
        var ids = [];
        $(this).parent().parent().parent().next().find(":checked").each(function (i, item) { ids.push(item.value); });
        if (ids.length == 0) {
            jx.alert("请选择商品ID");
            return false;
        }
        var actid = $('#ActID').val();
        $.post("/activity/AddProduct", { actid: actid, productIDs: ids.join(',') }, function (data) {
            if (data.status) {
                jx.alert("添加商品成功!")
                $tablist.eq(0).load("/activity/searchproduct?actid=" + actid + "&sindex=1", function () {
                    $tablist.eq(0).find("[data-mvcpager=true]").initMvcPagers();
                });
                $tablist.eq(1).load("/activity/actproduct?actid=" + actid + "&pindex=1", function () {
                    $tablist.eq(1).find("[data-mvcpager=true]").initMvcPagers();
                });
            } else {
                jx.error("失败了!");
            }
        });
        return false;
    });
    $tablist.on("click", "a[name=addall]", function () {
        var actid = $('#ActID').val();
        var cf1 = parseInt($('input[name=cf1]').val());
        var cf2 = parseInt($('input[name=cf2]').val());
        var cf3 = parseInt($('input[name=cf3]').val());

        var cfid = cf3 > 0 ? cf3 : (cf2 > 0 ? cf2 : (cf1 > 0 ? cf1 : 0));
        if (cfid == 0) {
            jx.alert("没有选择分类!");
            return false;
        }
        $.post("/activity/AddProduct", $('#form1').serialize(), function (data) {
            if (data.status) {
                jx.alert("关联商品成功!" + data.msg)
                $tablist.eq(0).load("/activity/searchproduct?actid=" + actid + "&sindex=1", function () {
                    $tablist.eq(0).find("[data-mvcpager=true]").initMvcPagers();
                });
                $tablist.eq(1).load("/activity/actproduct?actid=" + actid + "&pindex=1", function () {
                    $tablist.eq(1).find("[data-mvcpager=true]").initMvcPagers();
                });
            } else {
                jx.error("失败了!" + data.msg);
            }
        });
        return false;
    });
    $tablist.on("click", "a[name=cancel]", function () {
        var ids = [];
        $tablist.find("tbody :checked").each(function (i, item) {
            ids.push(item.value);
        });
        var actid = $('#ActID').val();
        $.post("/activity/DelProduct", { actid: actid, productIDs: ids.join(',') }, function (data) {
            if (data.status) {
                jx.alert("已取消关联商品!")
                $tablist.eq(0).load("/activity/searchproduct?actid=" + actid + "&sindex=1", function () {
                    $tablist.eq(0).find("[data-mvcpager=true]").initMvcPagers();
                });
                $tablist.eq(1).load("/activity/actproduct?actid=" + actid + "&pindex=1", function () {
                    $tablist.eq(1).find("[data-mvcpager=true]").initMvcPagers();
                });
            } else {
                jx.error("失败了!");
            }
        });
        return false;
    });
    function getcf(parentid, $this, isfirst) {
        $.ajax({
            type: "post",
            url: "/classification/getbyparentid",
            data: { parentid: parentid },
            success: function (data) {
                if (data.status) {
                    var items = data.data || [];
                    if ($this.index() == 1 && data.msg == "1") {
                        $this.hide();
                        $this = $this.next();
                    } else {
                        $this.show();
                    }
                    var arr = [];
                    arr.push("<li><a href='#' t='0'>请选择</a></li>")
                    for (i = 0; i < items.length; i++) {
                        arr.push("<li><a href='javascript:void(0);' t='" + items[i].CFID + "'>" + items[i].ChineseName + "</a></li>");
                    }
                    $this.find("ul").html(arr.join(''));
                    $this.find("input").val(0);
                }
            }
        });
    }
    function clearitem($cf) {
        $cf.find("ul>li").slice(1).remove();
        $cf.find("input").val(0);
        $cf.find(".selectTitleLeft").html("请选择")
    }
}

//edit post form
activity.editsuccess = function (data) {
    if (data.status) {

        $('#ActID').val(data.msg);
        jx.alert("编辑成功!", function () {
            parent.location.href = '/activity/list';
            $('.closewin_p').click();
        });
    }
    else {
        jx.error("失败了!" + data.msg);
    }
}

//rule post form 
activity.rulesuccess = function (data) {
    if (data.status) {
        $('.spInputWrap input:text').val('');
        $('.generalTable>tbody').load('/activity/rulelist?actid=' + $('#ActID').val());
        jx.alert("编辑成功!");
    } else {
        jx.error("失败了!" + data.msg);
    }
}
