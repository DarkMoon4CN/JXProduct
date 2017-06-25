var audit = {};
audit.init = function () {

    $('a[name=save]').click(function () {
        var items = $('div.every-select');
        var cflist = [];
        $.each(items, function (i, item) {
            var cf = [];
            var $this = $(item);
            var $select = $this.find("select");
            for (var i = 0; i < $select.length; i++) {
                if ($select.eq(i).is(":hidden")) {
                    continue;
                }
                cf.push($select.eq(i).val());
            }
            cflist.push(cf.join(','));
        });

        var msg = "";
        var morecf = [];
        $.each(cflist, function (i, item) {
            var count = 0;
            var cfids = item.split(',');
            if (cfids[0] == '0')
                return true;
            var mcf = 0;
            for (var index = 0; index < cfids.length; index++) {
                if (cfids[index] != '0') {
                    count++;
                    mcf = cfids[index];
                }
            }
            if (count != cfids.length) {
                msg = "分类" + (i + 1).toString() + "必须选择到最后一级列表";
                return true;
            }
            morecf.push(mcf);
        });
        if (msg.length > 0) {
            jx.error(msg);
            return false
        }
        $.ajax({
            type: "POST", url: "/audit/editaudit",
            data: { productid: productid, cfids: morecf.join(',') },
            dataType: "json",
            success: function (result) {
                if (result.status) {
                    jx.alert("保存成功!");
                }
                else {
                    jx.alert("保存失败!" + result.msg);
                }
            }
        });
        return false;
    });

    $('#manufacturerContent,#productContent').on('focus', 'input[name=startdate],input[name=enddate]', function () {
        WdatePicker({ doubleCalendar: true, dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-31', minDate: '2000-01-01' });
    });

    audit.cf();
    audit.category();
    audit.addqual();
    audit.expire();
    audit.edit();
};
//分类操作
audit.cf = function () {
    audit.cflist = null;
    var $content = $('.addComm-select');
    var items = $('div.every-select');
    var count = items.length;
    items.each(function (i, item) {
        //分类
        var $cf = $(item).find("select");
        audit.initcf($cf);
    });
    //拷贝一份
    $('.add').on('click', function () {
        if (count >= 10) {
            return false;
        }
        var $new = items.last().clone();
        $new.attr("cfid", 0);
        $new.removeAttr("cfpath");
        $new.find(".num-text").text(++count);
        $new.find("select").slice(1).each(function (i, item) {
            $(item).find("option").slice(1).remove();
        });
        $content.append($new);
        audit.initcf($new.find('select'));
    });
}
audit.initcf = function ($cf) {
    $cf.not($cf.last()).on("change", function () {
        var $this = $(this);
        var index = parseInt($this.attr("name").replace("cf", ""));
        var cfid = parseInt($this.val());
        audit.cfclearoption($cf, index);
        if (cfid == 0) {
            return false;
        }
        audit.getcfbyid($cf, index, cfid);
    });
    if ($cf.eq(0).val() == "0")
        audit.getcfbyid($cf, 0, 0);
}
audit.getcfbyid = function ($cflist, index, cfid) {
    $.ajax({
        type: "post",
        url: "/classification/getbyparentid",
        data: { parentid: cfid },
        dataType: "json",
        success: function (data) {
            var $cf = $cflist.eq(index);
            if (data.status) {
                var arr = [];
                $.each(data.data, function (i, item) {
                    arr.push("<option value='" + item.CFID + "'>" + item.ChineseName + "</option>");
                });
                $cf.find("option").slice(1).remove();
                $cf.append(arr.join(''));
                if (data.msg == "1")
                    $cf.next().hide();
                else
                    $cf.next().show();

            }
        }
    });
}
audit.cfclearoption = function ($cf, index) {
    $cf.each(function (i, item) {
        if (i < index)
            return true;
        $cf.eq(i).find("option").slice(1).remove();
    });
}

audit.edit = function () {
    var $pass = $('a[name=pass]');
    var $notpass = $('a[name=notpass]');
    $pass.on("click", function () {
        jx.confirm("确定要通过审核吗?", function () {
            var $parent = $pass.parent();
            var productids = $parent.attr("productids");
            var type = $parent.attr("audittype");
            $.ajax({
                type: "POST",
                url: "/audit/product",
                data: { productids: productids, type: type, status: 1 },
                dataType: "json",
                success: function (result) {
                    if (result.status) {
                        jx.alert("审核通过!", function () {
                            location.href = location.href;
                            return false;
                        });
                    }
                    else {
                        jx.error("审核失败!" + result.msg);
                    }
                }
            });
        });
        return false;
    });
    var $notpass = $('a[name=notpass]');
    $notpass.on("click", function () {
        var $parent = $notpass.parent();
        var productids = $parent.attr("productids");
        var type = $parent.attr("audittype");
        jx.open("/audit/notpass?productids=" + productids + "&type=" + type, {
            width: 400, height: 400
        });
        return false;
    });
}

//点击切换分类
audit.category = function () {
    var mid = $('#MID').val();
    var pid = $('#PID').val();

    //企业资质
    var $nav = $('.enter-nav');
    var $manucontent = $('#manufacturerContent');
    //默认显示
    var $list = $manucontent.find('.every-one-info').children();
    $list.removeClass('hide');
    audit.uploadAll($list, mid, 0);
    //点击分类切换
    $nav.on("click", "li", function () {
        var $this = $(this);
        $this.addClass("active").siblings().removeClass("active");
        var $a = $this.find("a");
        var cid = $a.attr('cid');
        $manucontent.load("/audit/partialedit?mid=" + mid + "&cid=" + cid, function () {
            $list = $manucontent.find('.every-one-info').children();
            audit.uploadAll($list, mid, pid);
            //绑定图片上传
        });
        return false;
    });
    $manucontent.on("click", "#childnav li", function () {
        var $this = $(this);
        var currindex = $this.index();
        $this.addClass("active").siblings().removeClass("active");
        $list.addClass("hide").eq(currindex).removeClass("hide");
        return false;
    });
    $manucontent.on('click', '.save', function () {
        var $this = $(this);
        var $form = $this.parent().parent();
        $.post('/audit/Insert', $form.serialize() + "&productid=" + pid, function (result) {
            if (result.status) {
                jx.alert('提交成功!');
            } else {
                jx.error('失败了!' + result.msg);
            }
        });
        return false;
    });


    var $productContent = $('#productContent');
    var $productItem = $('#productItems');
    $productItem.on('click', 'a', function () {
        var $this = $(this);
        var index = $this.parent().index();
        $this.parent().siblings().find('a').removeClass('active');
        $this.addClass("active");
        $productContent.children().addClass('hide').eq(index).removeClass('hide');
        return false;
    });
    $productContent.find('a.save').click(function () {
        var $this = $(this);
        var $form = $this.parent().parent().parent();
        $.post('/audit/Insert', $form.serialize() + "&" + 'productid=' + pid, function (result) {
            if (result.status) {
                jx.alert('提交成功!');
            } else {
                jx.error('失败了!' + result.msg);
            }
        });
        return false;
    });
    audit.uploadAll($productContent.children(), 0, pid);
}
audit.uploadAll = function ($list, mid, pid) {
    $list.each(function (i, item) {
        var $this = $(this);
        var $btn = $this.find('.resetUpBtn span');
        var $imgcontent = $this.find('.informa-imgWrap');
        var qid = $this.find('input[name=qid]').val();
        var data = { qid: qid, mid: mid, pid: pid };
        audit.upload($btn, $imgcontent, data);
    });
}
audit.upload = function ($btn, $imgcontent, formdata) {
    //默认参数
    var defaultSetting = {
        "height": 24, 'width': 80, 'swf': '/js/uploadify3.2.1/uploadify.swf?v=' + new Date().getMilliseconds(), 'fileSizeLimit': "1024KB", 'method': "post", 'fileTypeDesc': '只允许上传gif,jpg,jpeg,png图片', 'fileTypeExts': '*.gif; *.jpg; *.png', "removeCompleted": false, 'wmode': 'transparent', 'preventCaching': false, 'auto': true, 'overrideEvents': ['onUploadProgress'], 'multi': false, 'buttonText': '上传图片'
    };
    setTimeout(function () {
        var queueid = $imgcontent.attr("id");
        //专业参数
        var prosetting = {
            'uploader': '/audit/upload',
            'formData': formdata,
            'queueID': queueid,
            'itemTemplate': '<div  id="${fileID}" class="upImage"><span>上传中</span><a style="display: none;" href="javascript:;" class="removeImgeCha">×</a></div>',
            'onSelect': function () {
                if ($imgcontent.children().length > 1)
                    $imgcontent.find(".upImage").eq(0).remove();
            },
            "onUploadSuccess": function (file, data, response) {
                var $f = $('#' + file.id);
                data = $.parseJSON(data);
                if (!data.status) {
                    $f.find("span").text("上传失败!");
                    return false;
                }
                var url = data.data.Url + data.data.ImagePath;
                $f.find("span").replaceWith("<img src='" + url + "' />");
                $imgcontent.removeClass("hide").prev().find("input:hidden").val(data.data.ImagePath);
                var $a = $f.find("a");
                $a.on("click", function () {
                    if (!confirm("确定要删除图片吗?"))
                        return false;
                    var $this = $(this);
                    $this.parent().remove();
                    $imgcontent.addClass('hide').prev().find("input:hidden").val('');
                    return false;
                });
                $imgcontent.hover(function () { $a.show(); }, function () { $a.hide(); });
            }
        };
        //合并上传参数
        $.extend(prosetting, defaultSetting);
        $btn.uploadify(prosetting);
    }, 10);
}
//添加资质
audit.addqual = function () {

}

audit.email = function () {
    var $add = $('.addBtn');
    $add.on("click", function () {
        jx.open("/audit/emailedit", { width: 450, height: 350 });
    });

    $('a[name=del]').on('click', function () {
        var $this = $(this);
        jx.del(function () {
            var emailid = $this.attr("emailid");
            $.post('/audit/emaildel', { emailid: emailid }, function (result) {
                if (result.status) {
                    jx.alert('删除成功!', function () {
                        location.reload();
                    });
                }
            });
        });
        return false;
    });
    return false;
}
audit.emailedit = function () {
    var $form = $('#form1');
    var vali = $form.validate({
        submitHandler: function () {
            $.ajax({
                type: "POST", url: "/audit/emailedit", data: $form.serialize(), dataType: "json", success: function (result) {
                    if (result.status) {
                        jx.alert("编辑成功!", function () {
                            parent.parent.location.reload();
                            parent.jx.closeAll();
                        });
                    } else {
                        jx.error("编辑失败!");
                    }
                }
            });
        }
    });
    $('a.save').on("click", function () {
        $form.submit();
    });
}
audit.expire = function () {
    var mid = $('#MID').val();
    $.ajax({
        type: "POST",
        url: "/audit/expire",
        data: { mid: mid },
        dataType: "json",
        success: function (result) {
            if (result.status) {
                var items = result.data;
                for (var i = 0; i < items.length; i++) {
                    if (items[i].count > 0) {
                        $('a[cid=' + i + ']').prev().addClass("curr");
                    }
                }
            }
        }
    });
}