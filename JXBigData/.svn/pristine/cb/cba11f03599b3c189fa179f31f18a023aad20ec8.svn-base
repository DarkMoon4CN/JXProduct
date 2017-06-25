/// <reference path="jquery-2.1.3.min.js" />
var cf = {};
var section = {};
cf.init = function () {
    var $cfnav = $('.classification');
    $('.header-comm-table a').on('click', function () {
        var $this = $(this);
        $this.siblings().removeClass("active");
        $this.addClass("active");
        $cfnav.addClass("hide").eq($this.index()).removeClass("hide");
    });

    var $cf = $('#cf');
    var $child = $cf.children();

    //选中
    $child.on('click', ".class-s-d a", function () {

        //样式
        var $a = $(this);
        $a.addClass('active').parent().siblings().find("a").removeClass("active");

        //索引
        var $div = $a.parent().parent().parent().parent().parent();
        var cfid = parseInt($a.attr("cfid"));
        var index = $div.index();



        //最后一级不加载数据
        if (index >= 2)
            return false;

        $child.slice(index + 1).find(".class-s-d ul").empty();
        cf.getcf(cfid, function (data) {
            var $next = $child.eq(index + 1);
            if (data.msg == "1" && index == 0) {
                $next.hide();
                $next = $next.next();
            }
            $next.show();
            if (data.status) {
                var arr = [];
                for (var i = 0; i < data.data.length; i++) {
                    var item = data.data[i]
                    arr.push("<li><a href='#' cfid='{0}'>{1}</a></li>".format(item.CFID, item.ChineseName));
                }
                $next.find(".class-s-d ul").append(arr.join(''));
            }
        });
        return false;
    });


    //编辑
    $cf.find('a[name=add]').on('click', function () {
        var $prev = $(this).parent().parent().parent().parent().parent().prev()
        if ($prev.is(":hidden")) {
            $prev = $prev.prev();
            cf.addobj = $prev;
        }
        var $a = $prev.find("a.active");
        if ($a.length > 0) {
            jx.open("/classification/edit?parentid=" + $a.attr('cfid'), { width: 450, height: 550 });
        }
        else {
            jx.alert("请选择上一级目录!");
        }
        return false;
    });
    $cf.find('a[name=edit]').on('click', function () {
        var $a = $(this).parent().parent().parent().prev().find("a.active");
        if ($a.length > 0) {
            cf.editobj = $a;
            jx.open("/classification/edit?cfid=" + $a.attr('cfid'), { width: 450, height: 550 });
        }
        else {
            jx.alert("请选择当前项目");
        }
        return false;
    });
}
cf.getcf = function (parentid, fun) {
    $.ajax({
        type: "POST",
        url: "/classification/getbyparentid",
        data: { parentid: parentid },
        dataType: "json",
        success: function (result) {
            fun(result);
        }
    });
};
cf.getcfbycfid = function (cfid, fun) {
    $.ajax({
        type: "POST",
        url: "/classification/getbycfid",
        data: { cfid: cfid },
        dataType: "json",
        success: function (result) {
            fun(result);
        }
    });
}
cf.getcfbyname = function (cfname, fun) {
    $.ajax({
        type: "POST",
        url: "/classification/getbycfname",
        data: { cfname: cfname },
        dataType: "json",
        success: function (result) {
            fun(result);
        }
    });
}
cf.edit = function () {
    var $name = $('input[name=ChineseName]');
    var t = 0;
    var $down = $name.parent().next();
    var $save = $('a[name=add]');
    var $form = $('#form1');
    var $cancel = $('a[name=cancel]');
    $name.on("change blur keyup", function () {
        var name = $name.val();
        if ($name.attr('o') == name) {
            return false;
        }
        clearTimeout(t);
        //$down.addClass("hide");

        if (name.length == 0)
            return false;
        t = setTimeout(function () {
            cf.getcfbyname(name, function (result) {
                if (result.status) {
                    var arr = [];
                    arr.push('<p class="xingRed noneTag">检索到' + result.data.length + '个结果</p>');
                    if (result.data.length > 0) {
                        arr.push("<ul>");
                        for (var i = 0 ; i < result.data.length; i++) {
                            arr.push('<li>' + result.data[i].ChineseName + '</li>');
                        }
                        arr.push("</ul>");
                    }
                    $down.html(arr.join(''));
                    $down.find("li").click(function () { $name.val($(this).text()); });
                    $down.removeClass("hide");
                    $name.attr('o', name);
                }
            });
        }, 1000);
    });
    $save.on('click', function () {
        clearTimeout(t);
        $.ajax({
            type: "POST",
            url: "/classification/edit",
            data: $form.serialize(),
            dataType: "json",
            success: function (result) {
                if (result.status) {
                    var $r = $('.r' + result.data);
                    $r.removeClass("hide");

                    var cfid = parseInt($('#CFID').val());
                    var name = $('input[name=ChineseName').val();
                    if (cfid > 0) {
                        parent.cf.editobj.text(name);
                    } else {
                        parent.cf.addobj.find("a.active").click();
                    }
                    setTimeout(function () {
                        $cancel.click();
                    }, 2000);

                }
            }
        });
        return false;
    });

}

section.init = function () {

    var $section = $('#section');
    var $child = $section.children();

    //选中
    $child.on('click', ".class-s-d a", function () {

        //样式
        var $a = $(this);
        $a.addClass('active').parent().siblings().find("a").removeClass("active");

        //索引
        var $div = $a.parent().parent().parent().parent().parent();
        var sectionid = parseInt($a.attr("sectionid"));
        var index = $div.index();
        if (index >= 2)
            return false;

        $child.slice(index + 1).find(".class-s-d ul").empty();
        section.getsection(sectionid, function (data) {
            var $next = $child.eq(index + 1);
            if (data.msg == "1" && index == 0) {
                $next.hide();
                $next = $next.next();
            }
            $next.show();
            if (data.status) {
                var arr = [];
                for (var i = 0; i < data.data.length; i++) {
                    var item = data.data[i]
                    arr.push("<li><a href='#' sectionid='{0}'>{1}</a></li>".format(item.SectionID, item.SectionName));
                }
                $next.find(".class-s-d ul").append(arr.join(''));
            }
        });
        return false;
    });


    //编辑
    $section.find('a[name=add]').on('click', function () {
        var $prev = $(this).parent().parent().parent().parent().parent().prev();
        var $a = $prev.find("a.active");
        if ($a.length > 0) {
            jx.open("/section/edit?parentid=" + $a.attr('sectionid'), { width: 450, height: 550 });
        }
        else {
            jx.alert("请选择上一级目录!");
        }
        return false;
    });
    $section.find('a[name=edit]').on('click', function () {
        var $a = $(this).parent().parent().parent().prev().find("a.active");
        if ($a.length > 0) {
            section.editobj = $a;
            jx.open("/section/edit?sectionid=" + $a.attr('sectionid'), { width: 450, height: 550 });
        }
        else {
            jx.alert("请选择当前项目");
        }
        return false;
    });
}
section.getsection = function (parentid, fun) {
    $.ajax({
        type: "POST",
        url: "/section/getbyparentid",
        data: { parentid: parentid },
        dataType: "json",
        success: function (result) {
            fun(result);
        }
    });
};
section.getsectionbysectionid = function (sectionid, fun) {
    $.ajax({
        type: "POST",
        url: "/section/getbysectionid",
        data: { sectionid: sectionid },
        dataType: "json",
        success: function (result) {
            fun(result);
        }
    });
}
section.getsectionbyname = function (sectionname, fun) {
    $.ajax({
        type: "POST",
        url: "/section/getbysectionname",
        data: { sectionname: sectionname },
        dataType: "json",
        success: function (result) {
            fun(result);
        }
    });
}
section.edit = function () {
    var $name = $('input[name=ChineseName]');
    var t = 0;
    var $down = $name.parent().next();
    var $save = $('a[name=add]');
    var $form = $('#form1');
    var $cancel = $('a[name=cancel]');
    $name.on("change blur keyup", function () {
        var name = $name.val();
        if ($name.attr('o') == name) {
            return false;
        }
        clearTimeout(t);
        //$down.addClass("hide");

        if (name.length == 0)
            return false;
        t = setTimeout(function () {
            section.getsectionbyname(name, function (result) {
                if (result.status) {
                    var arr = [];
                    arr.push('<p class="xingRed noneTag">检索到' + result.data.length + '个结果</p>');
                    if (result.data.length > 0) {
                        arr.push("<ul>");
                        for (var i = 0 ; i < result.data.length; i++) {
                            arr.push('<li>' + result.data[i].SectionName + '</li>');
                        }
                        arr.push("</ul>");
                    }
                    $down.html(arr.join(''));
                    $down.find("li").click(function () { $name.val($(this).text()); });
                    $down.removeClass("hide");
                    $name.attr('o', name);
                }
            });
        }, 1000);
    });
    $save.on('click', function () {
        clearTimeout(t);
        $.ajax({
            type: "POST",
            url: "/section/editmodel",
            data: $form.serialize(),
            dataType: "json",
            success: function (result) {
                if (result.status) {
                    var $r = $('.r' + result.data);
                    $r.removeClass("hide");
                    var sectionid = parseInt($('#SectionID').val());
                    var name = $name.val();
                    if (sectionid > 0) {
                        parent.section.editobj.text(name);
                    } else {
                        parent.section.addobj.find("a.active").click();
                    }
                    setTimeout(function () {
                        $cancel.click();
                    }, 2000);
                }
            }
        });
        return false;
    });
}