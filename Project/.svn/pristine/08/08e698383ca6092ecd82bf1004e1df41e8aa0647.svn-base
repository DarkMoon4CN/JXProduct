/// <reference path="jquery.min.js" />
var classification = {};
//分类列表数据
classification.init = function () {

    var $cf1, $cf2, $cfkeyword;
    $cf1 = $('#cf1');
    $cf2 = $('#cf2');
    $cf3 = $('#cf3');

    // 疾病标签商品种类切换
    $('.classMainNav span').on('click', function () {
        $('.classMainNav span').removeClass();
        $(this).addClass('active');
        $('.MainWrap').hide().eq($(this).index()).show();
    });

    //编辑滑动按钮
    $('div.editor a').hover(
        function () { var $this = $(this); $this.parent().find("div").eq($this.index()).show(); },
        function () { $(this).siblings("div").hide(); }
    );

    //三级联动
    $cf1.add($cf2).add($cf3).on("click", ".detailsList a", function () {
        //选中样式
        var $this = $(this);
        if ($this.hasClass("active"))
            return false;
        var $cf = $this.parent().parent().parent().parent();
        $cf.find("a").removeClass("active");
        $this.addClass("active");

        //保存当前点击cfid
        $cf.find(".editor").attr('cfid', $this.attr("cfid"));
        if ($cf.index() == 2)
            return false;

        //加载异步数据
        var cfid = parseInt($this.attr("cfid"));
        classification.getListByParentID(cfid, $cf);

        return false;
    });
    $cf2.add($cf3).find(".editor").on("click", 'a.add', function () {
        var $this = $(this);
        var parentid = $this.parent().attr("parentid");
        jx.open("/classification/edit?parentid=" + parentid, null, 450, 500, function () {
            classification.reloadCF($this.parent().parent());
        });
        return false;
    }).on("click", 'a.edi', function () {
        var $this = $(this);
        var cfid = parseInt($this.parent().attr("cfid"));
        if (!isNaN(cfid) && cfid > 0) {
            jx.open("/classification/edit?cfid=" + cfid, null, 450, 500, function () {
                classification.reloadCF($this.parent().parent());
            });
        }
        else
            jx.alert("请点击一个选项为选中状态");
        return false;
    });
};
classification.getListByParentID = function (cfid, $cf) {

    $.ajax({
        type: "post",
        url: "/classification/getcfbyparentid",
        async: false,
        data: { parentid: cfid },
        success: function (data) {
            var $next = $cf.next();
            if (data.status && data.data.length > 0) {
                if ($cf.index() == 0 && data.msg == "1") {
                    $next.hide();
                    $next = $next.next();
                } else {
                    $next.show();
                    $next.next().find(".detailsList>ul").empty();
                    $next.next().find(".editor").hide();
                }
                $next.find(".detailsList>ul").html(classification.joinHtml(data.data)).css("top", 0);
            }
            else {
                $next.find(".detailsList>ul").empty();
            }
            var $edit = $next.find("div.editor");
            $edit.attr("parentid", cfid);
            $edit.attr("cfid", 0);
            $edit.show();
            return false;
        }
    });
}
classification.joinHtml = function (items) {
    var html = [];
    for (i = 0 ; i < items.length; i++) {
        var item = items[i];
        html.push("<li><a href='#' cfid='" + item.CFID + "'>" + item.ChineseName + "</a></li>");
    }
    return html.join('');
}
classification.reloadCF = function ($cf) {
    var parentid = $cf.find(".editor").attr("parentid");
    classification.getListByParentID(parentid, $cf.prev());

}
//分类编辑操作
classification.edit = function () {
    var $form = $('#form1');
    var $submit = $("input:submit");
    $form.validate({
        errorPlacement: function (error, element) {
            $submit.parent().parent().append(error);
        }, submitHandler: function (form) {
            var data = $(form).serialize();
            $.post('/classification/editmodel', data, function (result) {
                var $result = "";
                if (result.status) {
                    $result = $("<p class='gou'>成功编辑标签</p>");
                    $submit.parent().append($result);
                    jx.autoHide($result);
                }
                else {
                    jx.alert("编辑失败!");
                }

            }, 'json');
            return false;
        }
    });
    var t;
    var $input = $("input:text").eq(0);
    var iskeyparent = $('#IsKeyParent').val();
    $input.on("keyup blur change", function () {
        clearTimeout(t);
        t = setTimeout(function () {
            if (iskeyparent == "1") {
                keyword.getkeyword($input);
            }
            else {
                classification.getcf($input);
            }
        }, 500);
    });

    //点击事件
    $('.seachKey').on("click", ".keyCenter a", function () {
        $input.val($(this).text())
    });
}
classification.getCFByName = function (name) {
    var s = '';
    $.ajax({
        type: "post",
        url: "/classification/getcfbyname",
        data: { name: name },
        async: false,
        success: function (data) {
            if (data.status) {
                var arr = [];
                arr.push('<div class="keyCenter"><p class="redColor">检索到到' + data.data.length + '个结果</p>');
                if (data.data.length > 0) {
                    arr.push("<ul>");
                    for (i = 0; i < data.data.length; i++) {
                        arr.push("<li><a href='#' cfid='" + data.data[i].CFID + "'>" + data.data[i].ChineseName + "</a></li>");
                    }
                    arr.push("</ul>");
                } else {
                    arr.push('<p class="redColor">对不起，没有检索到相应的 标签，您可以直接添加</p>')
                }
                arr.push("</div>");
                s = arr.join('');
            }
        }
    });
    return s;
}
classification.getcf = function ($this) {
    var name = $this.val();
    if (name == $this.attr("old"))
        return false;
    $this.attr("old", name)
    var result = "";
    if (name.length != "") {
        var result = classification.getCFByName(name);
    }
    var $next = $this.nextAll();
    if ($next.length > 0) {
        $next.remove();
    }
    $this.after(result);
    $this.parent().parent().next().children().eq(4).remove();
}

var section = {};
section.init = function () {
    var $section1, $section2, $section3;
    $section1 = $('#section1');
    $section2 = $('#section2');
    $section3 = $('#section3');

    //三级联动
    $section1.add($section2).add($section3).on("click", ".detailsList a", function () {
        //选中样式
        var $this = $(this);
        if ($this.hasClass("active"))
            return false;
        var $section = $this.parent().parent().parent().parent();
        $section.find("a").removeClass("active");
        $this.addClass("active");

        //保存当前点击sectionid
        $section.find(".editor").attr('sectionid', $this.attr("sectionid"));
        if ($section.index() == 2)
            return false;

        //加载异步数据
        var sectionid = parseInt($this.attr("sectionid"));
        section.getListBySectionID(sectionid, $section);

        return false;
    });
    $section2.add($section3).find(".editor").on("click", 'a.add', function () {
        var $this = $(this);
        var parentid = $this.parent().attr("parentid");
        jx.open("/section/edit?parentid=" + parentid, null, 450, 500, function () {
            section.reloadSection($this.parent().parent());
        });
        return false;
    }).on("click", 'a.edi', function () {
        var $this = $(this);
        var sectionid = parseInt($this.parent().attr("sectionid"));
        if (!isNaN(sectionid) && sectionid > 0) {
            jx.open("/section/edit?sectionid=" + sectionid, null, 450, 500, function () {
                section.reloadSection($this.parent().parent());
            });
        }
        else
            jx.alert("请点击一个选项为选中状态");
        return false;
    });


}
section.getListBySectionID = function (sectionid, $section) {
    var s = '';
    $.ajax({
        type: "post",
        url: "/section/getsectionbyparentid",
        data: { parentid: sectionid },
        async: true,
        success: function (data) {
            var $next = $section.next();
            if (data.status && data.data.length > 0) {
                var s = '';
                if ($section.index() == 0 && data.msg == "1") {
                    $next.hide();
                    $next = $next.next
                } else {
                    $next.show();
                    $next.next().find(".detailsList>ul").empty();
                    $next.next().find(".editor").hide();
                }
                s = section.joinSectionHtml(data.data);
                $next.find(".detailsList>ul").html(s);
            }
            else {
                $next.find(".detailsList>ul").empty();
            }
            var $edit = $next.find("div.editor");
            $edit.attr("parentid", sectionid);
            $edit.attr("sectionid", 0);
            $edit.show();
            return false;
        }
    });
    return s;
}
section.joinSectionHtml = function (items) {
    var html = [];
    for (i = 0 ; i < items.length; i++) {
        var item = items[i];
        html.push("<li><a href='#' sectionid='" + item.SectionID + "'>" + item.SectionName + "</a></li>");
    }
    return html.join('');
}
section.reloadSection = function ($section) {
    var parentid = $section.find(".editor").attr("parentid");
    section.getListBySectionID(parentid, $section.prev());

}
section.edit = function () {
    var $form = $('#form1');
    var $submit = $("input:submit");
    $form.validate({
        errorPlacement: function (error, element) {
            $submit.parent().parent().append(error);
        }, submitHandler: function (form) {
            var data = $(form).serialize();
            $.post('/section/editmodel', data, function (result) {
                var $result = "";
                if (result.status) {
                    $result = $("<p class='gou'>成功编辑标签</p>");
                    $submit.parent().append($result);
                    jx.autoHide($result);
                }
                else {
                    jx.alert("编辑失败!");
                }
            }, 'json');
            return false;
        }
    });

    var t;
    var $input = $("input:text").eq(0);
    var iskeyparent = $('#IsKeyParent').val();
    $input.on("keyup blur change", function () {
        clearTimeout(t);
        t = setTimeout(function () {
            if (iskeyparent == "1") {
                section.getsection($input);
            } else {
                keyword.getkeyword($input);
            }
        }, 500);
    });
}

section.getsection = function ($this) {
    var name = $this.val();
    if (name == $this.attr("old"))
        return false;
    $this.attr("old", name);
    section.getSectionByName(name, $this);
}
section.getSectionByName = function (name, $this) {
    $.ajax({
        type: "post",
        url: "/section/getsectionbyname",
        data: { name: name },
        success: function (data) {
            var arr = [];
            if (data.status) {
                arr.push('<div class="keyCenter"><p class="redColor">检索到到' + data.data.length + '个结果</p>');
                if (data.data.length > 0) {
                    arr.push("<ul>");
                    for (i = 0; i < data.data.length; i++) {
                        arr.push("<li><a href='#' sectionid='" + data.data[i].SectionID + "'>" + data.data[i].SectionName + "</a></li>");
                    }
                    arr.push("</ul>");
                } else {
                    arr.push('<p class="redColor">对不起，没有检索到相应的 标签，您可以直接添加</p>')
                }
                arr.push("</div>");
            }
            var $next = $this.nextAll();
            if ($next.length > 0) {
                $next.remove();
            }
            $this.after(arr.join(''));
            $this.parent().parent().next().children().eq(4).remove();
        }
    });
}


//关键字搜索
var keyword = {
    dataurl: "/classification/GetKeywordComplete",
    edit: function () {
        var t;
        var $input = $("input:text").eq(0);
        $input.on("keyup blur change", function () {
            clearTimeout(t);
            t = setTimeout(function () {
                keyword.getkeyword($input);
            }, 500);
        });
        $("input:submit").click(function () {
            var $submit = $(this);
            var $next = $submit.next();
            var isExists = false;
            $("div.keyCenter a").each(function (i, item) {
                if ($(item).text() == $input.val()) {
                    isExists = true;
                    return false;
                }
            });
            if (isExists) {
                var $parent = $next.parent();
                $parent.children().eq(4).remove();
                $parent.append("<p class='dateBtn redColor'>对不起此分类下已存在添加项！</p>");
            }
            return !isExists;
        });
    },
    getkeyword: function ($this) {
        var name = $this.val();
        if (name == $this.attr("old"))
            return false;
        $this.attr("old", name)
        if (name.length == '') {
            $this.next().remove();
            return false;
        }
        keyword.getKeywordByKeywordName(name, $this);
    },
    getKeywordByKeywordName: function (name, $this) {
        $.ajax({
            type: "post",
            url: "/classification/getkeywordbyname",
            async: true,
            data: { name: name },
            success: function (data) {
                var arr = [];
                if (data.status) {
                    arr.push('<div class="keyCenter"><p class="redColor">检索到到' + data.data.length + '个结果</p>');
                    if (data.data.length > 0) {
                        arr.push("<ul>");
                        arr.push(keyword.joinKeyword(data.data))
                        arr.push("</ul>");
                    }
                    else {
                        arr.push('<p class="redColor">对不起，没有检索到相应的 标签，您可以直接添加</p>')
                    }
                    arr.push('</div>')
                }
                var result = arr.join('');
                var $next = $this.nextAll();
                if ($next.length > 0) {
                    $next.remove();
                }
                var $result = $(result).find("a").click(function () { $this.val($(this).text()); }).end();
                $this.after($result);
                $this.parent().parent().next().children().eq(4).remove();
            }
        });
    },
    joinKeyword: function (items) {
        var html = [];
        for (i = 0 ; i < items.length; i++) {
            var item = items[i];
            html.push("<li><a href='#' keyword='" + item.KeywordID + "'>" + item.ChineseName + "</a></li>");
        }
        return html.join('');
    }
}