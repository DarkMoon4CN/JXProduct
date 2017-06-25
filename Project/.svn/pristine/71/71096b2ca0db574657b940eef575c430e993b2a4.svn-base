var common = {
    scroll: function () {
        $('.detailsList').on("mousewheel DOMMouseScroll", function (e) {
            var detailsH = $(this).height();
            var detailsUlH = $(this).find('ul').height();
            if (detailsH > detailsUlH) return;
            var delta = (e.originalEvent.wheelDelta && (e.originalEvent.wheelDelta > 0 ? 1 : -1)) ||  // chrome & ie
						(e.originalEvent.detail && (e.originalEvent.detail > 0 ? -1 : 1));              // firefox
            var t = parseInt($(this).find('ul').css('top'));
            if (delta > 0) {
                // 向上滚
                t += 10;
                if (t >= 0) t = 0;
                $(this).find('ul').css('top', t);
            } else if (delta < 0) {
                // 向下滚
                t -= 10;
                if (Math.abs(t) >= detailsUlH - detailsH) t = -(detailsUlH - detailsH);
                $(this).find('ul').css('top', t);
            }
            return false;
        });
    },
    select: function () {
        // 模拟select
        $('.selectTitleLeft').attr({ bBtn: '0' });
        var selectZindex = 6;
        $('.imitationSelect .selectTitleLeft').on('click', function () {
            var obj = $(this).parents('.imitationSelect');
            obj.find('.imitaSelectList').hide();
            var imitaSelectList = obj.find('.imitaSelectList');
            obj.find('.imitaSelectList').css('zIndex', 55555);
            selectZindex++
            imitaSelectList.parent().css('zIndex', selectZindex);
            if ($(this).attr('bBtn') == 0) {
                imitaSelectList.show();
                $(this).attr('bBtn', '1')
            } else {
                imitaSelectList.hide();
                $(this).attr('bBtn', '0');
            }
        });
        //  选中select
        $('.imitaSelectList').on('click', 'a', function () {
            var obj = $(this).parents('.imitationSelect');
            var value = $(this).attr('value');
            obj.find('.selectTitleLeft').html($(this).html()).attr('value', value);
            obj.find('.imitaSelectList').hide();
            obj.find('.selectTitleLeft').attr({ bBtn: '0' });
            //  加载数据
            obj.find('input').val(value);
        });
    },
    delAll: function (url, data, obj) {
        layer.confirm('你确定要删除吗？', { icon: 3 }, function (index) {
            layer.close(index);
            //  统一POST删除
            $.post(url, data, function (result) {
                if (typeof result == 'undefined' || result == null) {
                    jx.alert("服务器出错啦~");
                    return false;
                }
                if (result == "0") {
                    if (obj == null) {
                        jx.alert("删除成功！");
                        setTimeout(function () {
                            window.location.reload();
                        }, 1000);
                    }
                    else
                        obj.remove();
                }
                else {
                    jx.alert("删除失败，请稍后再试！");
                }
            }, "text");
        });
    },
    isURL: function (str_url) {
        // 验证url
        var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
		+ "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?"    // ftp的user@
		+ "(([0-9]{1,3}\.){3}[0-9]{1,3}"    // IP形式的URL- 199.194.52.184
		+ "|"                               // 允许IP和DOMAIN（域名）
		+ "([0-9a-z_!~*'()-]+\.)*"          // 域名- www.
		+ "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // 二级域名
		+ "[a-z]{2,6})"                     // first level domain- .com or .museum
		+ "(:[0-9]{1,4})?"                  // 端口- :80
		+ "((/?)|"                          // a slash isn't required if there is no file name
		+ "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
        var re = new RegExp(strRegex);
        return re.test(str_url);
    },
    beforeunload: function () {
        //  离开页面提示
        var hasSaved = true;
        $(window).bind('beforeunload', function () {
            if (!hasSaved) {
                return '您输入的内容尚未保存';
            }
            else {
            }
        })
        $('input').blur(function () {
            hasSaved = false;
        });
        $('textarea').blur(function () {
            hasSaved = false;
        });
    },
    uploadify: function (obj) {
        obj.uploadify({
            'uploader': '/js/uploadify/uploadify.swf',
            'script': '/marketing/uploadasyn',
            'cancelImg': '/js/uploadify/cancel.png',
            'sizeLimit': 1024 * 1024 * 2,
            'queueSizeLimit': 50,
            'fileDesc': '只允许上传bmp,gif,jpg,jpeg图片',
            'fileExt': '*.bmp;*.gif;*.jpg;*.jpeg',
            'method': 'Post',
            'buttonImg': '/image/sprites/upImg.png',
            'wmode': 'transparent',
            'auto': true,
            'multi': true,
            'onComplete': function (e, q, f, data, d) {
                if (data.indexOf(".") < 0) {
                    jx.alert(data);
                }
                else {
                    $('#uploadify').parents('.updateImgWrap').find('.upImage').html('<img style="max-width: 220px;" src="' + data + '" />');
                    $('#Picture').val(data);
                }
            }
        });
    },
    uploadifyAll: function () {
        //  批量上传
        $(".uploadify").each(function (i, item) {
            var n = (i + 1);
            $('#uploadify' + n).uploadify({
                'uploader': '/js/uploadify/uploadify.swf',
                'script': '/marketing/uploadasyn',
                'cancelImg': '/js/uploadify/cancel.png',
                'sizeLimit': 1024 * 1024 * 2,
                'queueSizeLimit': 50,
                'fileDesc': '只允许上传bmp,gif,jpg,jpeg图片',
                'fileExt': '*.bmp;*.gif;*.jpg;*.jpeg',
                'method': 'Post',
                'buttonImg': '/image/sprites/upImg.png',
                'wmode': 'transparent',
                'auto': true,
                'multi': true,
                'onComplete': function (e, q, f, data, d) {
                    if (data.indexOf(".") < 0) {
                        jx.alert(data);
                    }
                    else {
                        if ($('#uploadify' + n).parents('.operAdverW').find('.updateImgWrap').length > 0) {
                            $('#uploadify' + n).parents('.operAdverW').find('.updateImgWrap img').attr('src', data);
                        }
                        else {
                            $('#uploadify' + n).parents('.operAdverW').append('<div class="updateImgWrap"><div class="upImage"><img src="' + data + '" /><a class="removeImgeCha" href="javascript:;">×</a></div></div>');
                        }
                        $('#Picture' + n).val(data);
                    }
                }
            });
        });
    },
    uploadifyHtml: function (obj, html) {
        obj.uploadify({
            'uploader': '/js/uploadify/uploadify.swf',
            'script': '/marketing/uploadasyn',
            'cancelImg': '/js/uploadify/cancel.png',
            'sizeLimit': 1024 * 1024 * 2,
            'queueSizeLimit': 50,
            'fileDesc': '只允许上传bmp,gif,jpg,jpeg图片',
            'fileExt': '*.bmp;*.gif;*.jpg;*.jpeg',
            'method': 'Post',
            'buttonImg': '/image/sprites/upImg.png',
            'wmode': 'transparent',
            'auto': true,
            'multi': true,
            'onComplete': function (e, q, f, data, d) {
                if (data.indexOf(".") < 0) {
                    jx.alert(data);
                }
                else {
                    html.append('<li><img class="part1" src="' + data + '" /><img src="/image/pic21.jpg" class="part2" onclick="$(this).parent().remove();" /></li>');
                }
            }
        });
    }
}
//  统一插件
var jx = {
    init: function () {
        $('.closewin').css("cursor", "pointer").click(function () { jx.closeAll(); });
        $('.closewin_p').css("cursor", "pointer").click(function () { parent.jx.closeAll(); });
    },
    open: function (url, title, w, h, fun) {
        title = title || false;
        w = w || 500;
        h = h || 300;
        var t = parent.layer.open({
            type: 2,
            title: false,
            closeBtn: [1, true],
            shade: [0.5, '#fff', false],
            area: [w + 'px', h + 'px'],
            zIndex: 19891014,
            shift: 2,
            content: [url, 'no'],
            end: fun || function () { }
        });
        return t;
    },
    close: function (index) {
        parent.layer.close(index);
    },
    closeAll: function () {
        window.parent.layer.closeAll();
    },
    tips: function (content, id) {
        id = id || 0;
        if (id > 0)
            layer.tips(content, id);
        else
            layer.tips(content);
    },
    alert: function (content, fun) {
        parent.layer.alert(content, { icon: 9 }, function (index) {
            parent.layer.close(index);
            fun = fun || function (index) { };
            fun(index);
        });
    },
    error: function (content) {
        parent.layer.alert(content, { icon: 8 });
    },
    del: function (fun) {
        parent.layer.confirm('你确定要删除吗？', { icon: 3 }, function (index) {
            layer.close(index);
            (fun || function () { })();
        });
    },
    confirm: function (content, fun) {
        parent.layer.confirm(content, { icon: 3 }, function (index) {
            parent.layer.close(index);
            (fun || function () { })();
        });
    },
    autoHide: function ($this, t) {
        t = t || 2000;
        setTimeout(function () {
            $this.remove()
        }, 2000);
    },
    parentReload: function () {
        parent.window.location.reload();
    }
};
//下拉插件 fun 参与回调,点击的数据作为参数
(function ($) {
    $.fn.select1 = function (val, fun) {
        var thiselect = this;
        var selectZindex = 6;
        var $select = $(this);
        var $selectlist = $select.find('.imitaSelectList');
        var $hidden = $select.find("input");
        $select.find('.selectTitleLeft').attr("isshow", '0');
        val = val || -1;
        if (val.length > 0) {
            var update = $($selectlist).find("a[t=" + val + "]");
            var key = update.attr("t");
            var value = update.text();
            $select.attr("isshow", "0").find('.selectTitleLeft').html(value);
        }

        $select.on('click', ".selectTitle", function () {
            $selectlist.hide().css('zIndex', 55555);
            selectZindex++
            //$select.css('zIndex', selectZindex);
            var isshow = $select.attr('isshow') || 0;
            if (isshow == 0) {
                $selectlist.show();
                $select.attr('isshow', '1')
            } else {
                $selectlist.hide();
                $select.attr('isshow', '0');
            }
            return false;
        });
        $selectlist.on('click', 'a', function () {
            var this$ = $(this);
            var key = this$.attr("t");
            var value = this$.text();
            $select.attr("isshow", "0").find('.selectTitleLeft').html(value);
            $selectlist.hide();
            $hidden.val(key);
            (fun || function (key, value) { })(key, value);
            return false;
        }).find("li").hover(
			function () { $(this).css("background", "#eee"); },
			function () { $(this).css("background", ""); }
		);

        this.set = function (t) {
            $selectlist.find("a[t=" + t + "]").click();
            return thisselect;
        }
        return thiselect;;
    }
})(jQuery);

//  初始化关闭
$(function () {
    jx.init();
});