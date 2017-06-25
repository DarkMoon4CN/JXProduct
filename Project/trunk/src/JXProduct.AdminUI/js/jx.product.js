/// <reference path="base.regex.js" />
/// <reference path="jquery.min.js" />
var product = {}
product.init = function () {
    product.bindClick();
    product.list();
};
product.getProductID = function (this$) {
    return this$.parent().parent().attr("pid");
}
product.getProductByID = function (id, fun) {
    $.ajax({
        type: "post",
        url: "/product/get",
        anysc: false,
        data: { productid: id },
        success: function (data) {
            fun(data);
        }
    });
}
product.bindClick = function () {
    var tr$ = $("#tablelist tr");
    //快速编辑
    product.quickEdit(tr$);

    //活动
    productactivity.init(tr$);

    //库存
    productstock.init(tr$);

    //标签
    producttag.init(tr$);

    producteval.init(tr$);

    //置顶
    tr$.on("click", "a[name=istop]", function () {
        var this$ = $(this);
        var productid = product.getProductID(this$);
        var istop = this$.attr("istop") == "1" ? 0 : 1;
        $.post('/product/SetTop', { productid: productid, istop: istop }, function (result) {
            if (result.status) {
                this$.text(istop == 1 ? "[已置顶]" : "[置顶]");
                this$.attr("istop", istop);
            }
        });
        return false;
    }).on("click", "a[name=isrecommend]", function (data) {
        var this$ = $(this);
        var productid = product.getProductID(this$);
        var isrecommend = this$.attr("isrecommend") == "1" ? 0 : 1;
        $.post('/product/SetRecommend', { productid: productid, isrecommend: isrecommend }, function (result) {
            if (result.status) {
                this$.text(isrecommend == 1 ? "[已推荐]" : "[推荐]");
                this$.attr("isrecommend", isrecommend);
            }
        });
        return false;
    });
}
product.submitsucc = function (data) {
    if (data.status)
        jx.alert("编辑成功!");
    else
        jx.alert("编辑失败!请重试!" + data.msg);
}
product.quickEdit = function (tr$) {
    //赠品
    var t;
    tr$.find("input[name=giftID]").on("keyup change blur", function () {
        var $this = $(this);
        var value = $this.val();
        if ($this.attr("old") == value) {
            return false;
        }
        $this.attr("ispass", "0")
        clearTimeout(t);
        var $next = $this.parent().parent().next();

        if (value == '')
            return false;
        if (!/^\d{1,8}$/.test(value)) {
            $next.html('');
        }
        else {
            t = setTimeout(function () {
                $this.attr("old", value);
                product.getProductByID(value, function (data) {
                    if (data.status) {
                        $this.attr("ispass", "1");
                        $this.parent().next().find("input").val('1');
                        $next.html(data.data.ChineseName + "    库存：1000");
                    }
                    else {
                        $next.html("没有找到该商品ID");
                    }
                });
            }, 500);
        }
    });
    //加减
    var $parent = tr$.find(".setNumber");
    $parent.on("click", '.jian', function () {
        var $this = $(this);
        var quantity$ = $this.next();
        var count = parseInt(quantity$.val());
        if (isNaN(count) || count < 1)
            count = 0
        else
            count--;
        quantity$.val(count);
    });
    $parent.on("click", '.jia', function () {
        var $this = $(this);
        var quantity$ = $this.prev();
        var count = parseInt(quantity$.val());
        if (count > 98)
            count = 99;
        else
            count++;
        quantity$.val(count);
    });
    tr$.find("div.selectSim").each(function (i, item) {
        var v = $(item).find("input").val();
        $(item).select1(v);
    });

    //保存
    tr$.find("a[name=save]").on("click", function () {
        var $this = $(this);
        var tr = $this.parent().parent().parent();
        var productid = $this.attr('productid');
        var gift$ = tr.find("input[name=giftID]");
        var quantity$ = tr.find("input[name=quantity]");
        var isfreeship$ = tr.find("input[name=isfreeship]");
        var selling$ = tr.find('input[name=hidden_selling]');
        var actions$ = tr.find('input[name=hidden_actions]');
        var sort$ = tr.find("input[name=sort]");

        var giftid = gift$.val();
        var giftcount = quantity$.val();
        var isfreeship = isfreeship$.prop('checked');
        var sort = sort$.val();
        var selling = selling$.val();
        var actions = actions$.val();

        $.post("/product/quickedit", {
            productid: productid,
            ProductGiftID: giftid,
            ProductGiftCount: giftcount,
            IsFreeShip: isfreeship ? 1 : 0,
            Selling: selling,
            Actions: actions,
            Sort: sort
        }, function (result) {
            if (result.status) {
                jx.alert('编辑成功!');
            }
            else {
                jx.error("编辑失败!" + result.msg);
            }
        });
        $this.blur();
        return false;
    });
}
product.list = function () {

    $('#StartSelling').on("focus", function () {
        $(this).attr("readonly", "readonly");
        WdatePicker({ double: true, dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-31', minDate: new Date().toLocaleDateString() });
    });
    $('#EndSelling').on("focus", function () {
        $(this).attr("readonly", "readonly");
        WdatePicker({ double: true, dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-31', minDate: '#F{$dp.$D(\'StartSelling\')}' });
    });

}
product.search = function () {
    
}

//商品编辑
product.edit = function () {
    //分类编辑
    var $div = $("#productcf>div");
    $div.each(function (i, item) {
        var $item = $(item);
        var $cf1, $cf2, $cf3;
        var $child = $item.children();
        $cf1 = $child.eq(0);
        $cf2 = $child.eq(1);
        $cf3 = $child.eq(2);
        $cf1.select1($cf1.find("input").val(), function (key, value) {
            product.setSelectCFData($cf2, key);
        });
        $cf2.select1($cf2.find("input").val(), function (key, value) {
            product.setSelectCFData($cf3, key);
        });
        $cf3.select1();
    });

    var $cont = $('.productConter');
    $('.productHeader a').click(function () {
        var $this = $(this).addClass("active");
        $this.siblings().removeClass("active");
        $cont.hide().eq($this.index()).show();
        return false;
    });

    //commXinxi
    $('.commXinxi .imitationSelect').each(function () {
        var $this = $(this);
        $this.select1($this.find("input").val());
    });
    var jxcf = $('#divjxcf');
    jxcf.select1(jxcf.find("input").val(), function (key, value) {
        jxcf.parent().next().load("/product/jxcf?jxcfid=" + key + "&productid=" + productid);
    });

    $('a[name=savejxcfvalue]').on("click", function () { $('#form3').submit(); return false; });
    $('a[name=savedetail]').on("click", function () { $("#form1").submit(); return false; });
}
product.setSelectCFData = function ($this, cfid, isfirst) {
    if (typeof ($this) == "undefined")
        return;
    var arr = [];
    arr.push("<li><a href='#' t='0'>请选择</a></li>");
    if (!isfirst && cfid == 0) {
        $this.find('input').val("0");
        $this.find(".selectTitleLeft").text('请选择');
        return false;
    }

    $.post("/classification/GetCFByParentID", { parentid: cfid }, function (data) {
        if (data.status) {
            for (i = 0; i < data.data.length; i++) {
                var cf = data.data[i];
                arr.push("<li><a href='#' t='" + cf.CFID + "'>" + cf.ChineseName + "</a></li>");
            }
            if ($this.index() == 1 && data.msg == "1") { //key
                $this.hide();
                $this = $this.next();
            }
            else {
                $this.next().find(".imitaSelectList ul").html("<li><a href='#' t='0'>请选择</a></li>").end().find(".selectTitleLeft").text('请选择');
                $this.show();
            }
            $this.find(".imitaSelectList ul").html(arr.join(''));
            $this.find('input').val("0");
            $this.find(".selectTitleLeft").text('请选择');
        }
    })
}
product.setSelectKeywordData = function ($this, cfid) {
    var arr = [];
    arr.push("<li><a href='#' t='0'>请选择</a></li>");
    $this.find('input').val("0");
    $this.find(".selectTitleLeft").text('请选择');
    if (cfid == 0) {
        $this.find("ul").html(arr.join(''));
        return;
    }
    $.ajax({
        type: "post",
        url: "/classification/getkeywordbycfid",
        data: { cfid: cfid },
        async: false,
        success: function (data) {
            if (data.status && data.data.length > 0) {
                for (i = 0; i < data.data.length; i++) {
                    var d = data.data[i];
                    arr.push("<li><a href='#' t='" + d.keywordid + "'>" + d.keyword + "</a></li>");
                }
                $this.find("ul").html(arr.join(''));
            }
        }
    });

}
product.findExists = function (cfid) {
    for (var i = 0; i < cflist.length; i++) {
        if (cflist[i].ParentID == cfid)
            return true;
    }
    return false;
}
product.editJXCFSuccess = function (data) {
    if (data.status) {
        var $this = $("a[name=savejxcfvalue]");
        var $gou = $("<p class='gou'>保存成功</p>");
        $this.after($gou);
        jx.autoHide($gou);
    } else {
        jx.error("更新失败!");
    }
}
product.editDetailSuccess = function (data) {
    if (data.status) {
        var $this = $('a[name=savedetail]');
        var $gou = $("<p class='gou'>保存成功</p>");
        $this.after($gou);
        jx.autoHide($gou);
    } else {
        jx.error("更新失败!");
    }
}

//图片上传
product.upload = function () {
    //product.upload1();
    //return;;
    //商品图片
    var maxImageSize = 5;
    var $imageupload = $("#imageadd");
    var $imagelist = $('#imageListWrap');
    setTimeout(function () {
        $imageupload.uploadify({
            'formData': { productid: productid },
            "height": 120,
            'width': 170,
            'swf': '/js/uploadify3.2.1/uploadify.swf?v=' + new Date().toUTCString(),
            'uploader': '/product/uploadimage',
            'fileSizeLimit': "1024KB",
            'method': "post",
            "checkExisting": 0,
            'fileTypeDesc': '只允许上传bmp,gif,jpg,jpeg图片',
            'fileTypeExts': '*.gif; *.jpg; *.png',
            'buttonImage': '/image/sprites/addImage.png',
            "removeCompleted": false,
            'queueID': 'imageListWrap',
            'wmode': 'transparent',
            'preventCaching': false,
            'auto': true,
            'overrideEvents': ['onUploadProgress'],
            'multi': false,
            'itemTemplate': "<div id='${fileID}' class='imageList'><a href='#'></a><div class='progress'>上传中</div></div>",
            "onUploadSuccess": function (file, data, response) {
                var $this = $('#' + file.id);
                if (data == "" || data == "0") {
                    var div = $this.find("div").html("上传失败");
                    setTimeout(function () { $this.remove(); }, 2000);
                }
                else {
                    $this.find("div").remove();
                    $this.append("<img src='http://img.jxdyf.com/product/" + data + "' />");
                    $this.attr("path", data);
                }
                //判断 如果>5 就停止
                //在删除后解开
                if ($imagelist.children().length > maxImageSize) {
                    $imageupload.uploadify('disable', true);
                }
            },
            'onQueueComplete': function (queueData) {
                queueData.files = [];
            },
            'onSWFReady': function () {
                $imageupload.uploadify('disable', $imagelist.children().length > maxImageSize);
            }
        });
        $imagelist.on("click", "a", function () {
            var $this = $(this);
            $.post("/product/productimagedel", { productid: productid, path: $this.parent().attr("path") }, function (data) {
                if (data.status) {
                    $this.parent().remove();
                    $imageupload.uploadify('disable', false);
                } else {
                    jx.alert("删除失败!");
                }
            });
            return false;
        });
        $imagelist.on('mouseover mouseout', 'div', function (event) {
            var $this = $(this).find("a");
            if ($this.length == 0)
                return false;
            switch (event.type) {
                case "mouseover":
                    $this.show();
                    break;
                case "mouseout":
                    $this.hide();
                    break;
            }
        });


        var $descimages = $("#descimages>.updateImgWrap");
        $descimages.on("click", "a", function () {
            var $parent = $(this).parent();
            var path = $parent.attr("path") || "";
            if (path == "")
                return false;
            var i = $parent.parent().index();
            $.post("/product/descimagedel", { productid: productid, index: i, path: $parent.attr("path") }, function (data) {
                if (data.status) {
                    $parent.remove();
                }
                else {
                    jx.alert("出错了!");
                }
            });
            return false;
        }).on('mouseover mouseout', '.upImage', function (event) {
            var $this = $(this).find("a");
            if ($this.length == 0)
                return false;
            switch (event.type) {
                case "mouseover":
                    $this.show();
                    break;
                case "mouseout":
                    $this.hide();
                    break;
            }
        });
        $descimages.each(function (i, item) {
            initUpload(i, item);
        });

        //添加 
        $('.addImgaeText').on("click", function () {
            var $parent = $(this).parent().parent();
            var nextindex = $parent.children().length;
            if (nextindex > 10) {
                jx.alert("最多只允许添加10张图");
                return false;
            }
            var html = [];
            html.push('<div class="updateImgWrap" id="updateImgWrap' + nextindex + '">');
            html.push('<div class="upImageTop">');
            html.push('<span>* 详情图' + nextindex + '：</span>');
            html.push('<span>要求尺寸大小：90PX*90PX</span>');
            html.push('<div class="resetUpBtn">');
            html.push('<img alt="" src="../image/sprites/resetUp.png" id="upload' + nextindex + '">');
            html.push('</div></div>');
            var $next = $(html.join(''));
            $parent.append($next);
            initUpload(nextindex - 1, $next);
        });

    }, 10);

    function initUpload(i, item) {
        var $this = $(item);
        var currentIndex = (i + 1);
        var $upload = $this.find("#upload" + currentIndex);
        $upload.uploadify({
            'formData': { productid: productid, index: currentIndex },
            "height": 24,
            'width': 80,
            'swf': '/js/uploadify3.2.1/uploadify.swf',
            'uploader': '/product/uploaddescimage',
            'fileSizeLimit': "1024KB",
            'method': "post",
            "checkExisting": 0,
            'fileTypeDesc': '只允许上传bmp,gif,jpg,jpeg图片',
            'fileTypeExts': '*.gif; *.jpg; *.png',
            'buttonImage': '/image/sprites/resetUp.png',
            "removeCompleted": false,
            'queueID': $(this).attr("id"),
            'wmode': 'transparent',
            'preventCaching': false,
            'auto': true,
            'overrideEvents': ['onUploadProgress'],
            'multi': false,
            'itemTemplate': "<div class='upImage'><a href='#' class='removeImgeCha'>×</a><span>上传中</span></div>",
            'onSelect': function (file) {
                var img$ = $("<div class='upImage' style='display:none;'><a href='#' class='removeImgeCha'>×</a><span>上传中</span></div>");
                $this.append(img$);
                $this.find('.upImage').not(":last").remove();
                img$.show();
            },
            "onUploadSuccess": function (file, data, response) {
                if (data == "0" || data == "") {
                    var span = $this.find("pan").html("上传失败");
                }
                else {
                    $this.find(".upImage span").remove();
                    $this.find(".upImage").attr("path", data).append("<img src='http://img.jxdyf.com/product/" + data + "' />");
                }
            },
            'onQueueComplete': function (queueData) {
                queueData.files = [];
            }
        });
    }
}

//活动 true
var productactivity = {
    init: function (tr$) {
        tr$.on("click", "a[name=activity]", function () {
            jx.open("/product/activity?productid=" + product.getProductID($(this)), null, 450, 620);
            return false;
        });
    },
    edit: function () {

        productactivity.$actprice = $('#actprice');
        productactivity.$actquantity = $('#actquantity');
        productactivity.$actdiscount = $('#actdiscount');
        productactivity.$actproductgift = $('#actproductgift');
        productactivity.$actdesc = $('#actdesc');
        productactivity.$actdate = $('div[name=actdate]');
        productactivity.$others = $('#others');
        productactivity.$actcoupon = $('#actcoupon');

        var type = $('#hidden_type').val();
        productactivity.setHide(false);
        productactivity.setShow(type);
        $('div.imitationSelect').select1(type, function (key, value) {
            productactivity.setHide(true);
            productactivity.setShow(key);
        });


        productactivity.bindInfo();
        productactivity.$actprice
            .add(productactivity.$actquantity)
            .add(productactivity.$actdiscount)
            .add(productactivity.$actproductgift)
            .add(productactivity.$actcoupon).on("blur keyup change", "input", function () {
                productactivity.validater();
            });


        productactivity.$actdate.on("focus", "input", function () {
            $(this).attr("readonly", "readonly");
            WdatePicker({ doubleCalendar: true, dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '2099-12-31', minDate: new Date().toLocaleDateString() });
        });

        //提交表单
        $("a.submit").click(function () {
            if (productactivity.validater()) {
                $('#form1').submit();
            }
            else {
                jx.alert("请检查参数!");
            }
        });
    },
    setShow: function (key) {
        if (key == "0")
            return;

        var pa = productactivity;

        /*  包邮 = 1,满赠 = 2,满减 = 3,满返 = 4,满折 = 5,换购 = 6,折扣 = 7,直降 = 8*/
        //  判断有没有_ 选择需要显示的
        var type = 0;
        if (key.indexOf("_") > 0) {
            var arr = key.split('_');
            switch (arr[1]) {
                case "1": pa.$actprice.show(); break;
                case "2": pa.$actquantity.show(); break;
            }
            type = parseInt(arr[0]);
        } else {
            type = parseInt(key);
        }
        switch (type) {
            case 2: //赠品
                pa.$actproductgift.show();
                break;
            case 4: //优惠劵
                pa.$actcoupon.show();
                break;
            case 6: //换购 价格 赠品
                pa.$actproductgift.show();
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写换购价格：").end().find(".discountunit").text("元");
            case 3: //满减
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写减免金额：").end().find(".discountunit").text("元");
                break;
            case 5: //满折
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写折扣：").end().find(".discountunit").text("折");
                break;
            case 7: //折扣
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写折扣：").end().find(".discountunit").text("折");
                break;
            case 8: //直降
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写优惠金额：").end().find(".discountunit").text("元");
                break;
        }
        pa.$actdesc.show();
        pa.$actdate.show();
        pa.$others.show();

    },
    setHide: function (isclear) {
        var pa = productactivity;
        pa.$actprice.hide();
        pa.$actproductgift.hide();
        pa.$actquantity.hide();
        pa.$actdiscount.hide();
        pa.$actdesc.hide();
        pa.$actdate.hide();
        pa.$others.hide();
        pa.$actcoupon.hide();
        if (isclear) {
            pa.$actprice.add(pa.$actproductgift).add(pa.$actquantity).add(pa.$actdiscount).add(pa.$actdesc).find("input").val("");
        }
    },
    bindInfo: function () {

        //商品名称读取
        var t;
        productactivity.$actproductgift.find("input").on("change keyup blur", function () {
            clearTimeout(t);
            var $product = $(this);
            var productid = parseInt($product.val());
            if (productid == $product.attr("productid")) {
                return false;
            }
            var $msg = $product.parent().next().html("");
            if (!isNaN(productid) && productid > 0) {
                t = setTimeout(function () {
                    $product.attr("productid", productid);
                    product.getProductByID(productid, function (data) {
                        if (data.status) {
                            $product.parent().next().html("商品名称：<a href='http://www.jxdyf.com/product/" + data.data.ProductID + ".html' target='_blank'>" + data.data.ChineseName + "</a>");
                            $product.attr("cname", data.data.ChineseName);
                            productactivity.validater();
                            clearTimeout(t);
                        }
                    });
                }, 500);
            }
        });
    },
    validater: function () {
        var ispass = true;
        //活动介绍
        var $actdesc = productactivity.$actdesc.find("textarea");
        var type = parseInt($('#hidden_type').val().substring(0, 1));
        var price = parseInt(productactivity.$actprice.find("input").val());
        var $quantity = productactivity.$actquantity.find("input")

        var quantity = parseInt($quantity.val());
        var unit = $quantity.next().text();

        var $gift = productactivity.$actproductgift.find("input");
        var giftid = parseInt($gift.val());
        var giftname = $gift.attr("cname") || "";

        var $discount = productactivity.$actdiscount.find("input");
        var discount = parseInt($discount.val());

        var couponno = $('#CouponBatchNo').val();
        var couponname = $('#CouponName').val();



        /*  包邮 = 1,满赠 = 2,满减 = 3,满返 = 4,满折 = 5,换购 = 6,折扣 = 7,直降 = 8 */
        var msg = '';
        switch (type) {
            case 1:
                if (price > 0)
                    msg = "满" + price + "元，即可享受包邮";
                else if (quantity > 0)
                    msg = "满" + quantity + unit + "，即可享受包邮";
                break;
            case 2:
                if (giftid > 0 && giftname.length > 0) {
                    if (price > 0)
                        msg = "满" + price + "元，即可获赠" + giftname;
                    else if (quantity > 0)
                        msg = "满" + quantity + unit + "，即可获赠" + giftname;
                }
                break;
            case 3:
                if (discount > 0) {
                    if (price > 0)
                        msg = "满" + price + "元，即可享受减" + discount + "元优惠";
                    else if (quantity > 0)
                        msg = "满" + quantity + unit + "，即可享受减" + discount + "元优惠";
                }
                break;
            case 4:

                if (couponno.length > 0 && couponname.length > 0) {
                    if (price > 0)
                        msg = "满" + price + "元，即可享受返券优惠";
                    else if (quantity > 0)
                        msg = "满" + quantity + unit + "，即可享受返券优惠";
                }
                break;
            case 5:
                if (discount > 0) {
                    if (price > 0)
                        msg = "满" + price + "元，即可享受" + discount + "折优惠";
                    else if (quantity > 0)
                        msg = "满" + quantity + unit + "，即可享受" + discount + "折优惠";
                    break;
                }
            case 6:
                if (giftid > 0 && giftname.length > 0) {
                    if (price > 0)
                        msg = "满" + price + "元，可加价换购商品1件";
                    else if (quantity > 0)
                        msg = "满" + quantity + unit + "，可加价换购商品1件";
                }
                break;
            case 7:
                if (discount > 0 && discount < 10)
                    msg = discount + "折优惠 全网最低";
                break;
            case 8:
                msg = "直降" + $discount.val() + "元，全网最低";
                break;
            default:
                return true;
        }
        $actdesc.text(msg);
        return msg.length > 0;
    }
}

//标签 true
var producttag = {};
producttag.init = function (tr$) {
    tr$.on("click", "a[name=tag]", function () {
        jx.open("/keyword/productwin?productid=" + product.getProductID($(this)), null, 450, 500);
        return false;
    });
}

//评论
var producteval = {
    init: function (tr$) {
        tr$.on("click", "a[name=eval]", function () {
            jx.open("/product/EvalEdit?productid=" + product.getProductID($(this)), null, 450, 500);
            return false;
        });
    },
    edit: function () {
        $('.imitationSelect').select1();
        $('a.submit').on("click", function () {
            $('#form1').submit()
        });
    }
}

//reated 关联
var related = {
    //编辑
    edit: function () {
        var $nav = $('div.setCommAttrHeader');
        var $content = $('div.setCommConterTab');
        $nav.on('click', "span", function () {
            var $this = $(this).addClass("active");
            $this.siblings().removeClass("active");
            $content.find("div.packagingInfo").addClass("hide").eq($this.index()).removeClass("hide");
        });

        this.packaging();
        this.tuijian();
        this.guige();
        this.zuhe();
    },

    //大包装
    packaging: function () {

        var $p = $('#packaging');
        var productid = parseInt($p.find(".pid").text());
        var productname = $p.find(".pname").text()
        var unit = $p.find(".unit").text()

        var $cancel = $p.find("a.cancel");
        var $quantity = $p.find("input[name=quantity]");
        var $price = $p.find("input[name=price]");
        var $viewname = $p.find("input[name=viewname]");
        var $save = $p.find("a[name=save]");
        $quantity.add($price).on("change keyup blur", function () {  //生成 viewname
            var str = '';
            var q = $quantity.val();
            var p = $price.val();
            if (q > 0 && p > 0)
                str = productname + q + unit + '装(每' + unit + '仅需￥' + (p / q).toFixed(2) + ')';
            $viewname.val(str);
        });
        //提交表单
        $p.on("click", "a[name=save]", function () {
            $('#form1').submit();
            return false;
        });
        $("#form1").validate({
            errorPlacement: function (error, element) {
                element.parent().append(error);
            },
            submitHandler: function (form) {
                var quantity = $quantity.val();
                var price = $price.val();
                var viewname = $viewname.val();
                $.post('/product/related_largepacking', { relatedid: 0, productid: productid, price: price, quantity: quantity, viewname: viewname }, function (data) {
                    if (data.status) {
                        if ($save.next().next().length == 0) {
                            var $gou = $("<p class='gou'>保存成功</p>")
                            $save.next().after($gou);
                            jx.autoHide($gou);
                            related.clearItem($p);
                            $p.find(".infoTable").load("/product/Related?type=1&productid=" + productid);
                        }
                    } else {
                        jx.alert("失败了!" + data.msg);
                    }
                    return false;
                });
            }
        });


        var isupdate = false;
        // 修改
        $p.on("click", "a[name=update]", function () {
            if (isupdate) {
                jx.alert("请先编辑完当前数据");
                return false;
            }
            isupdate = true;
            //操作数据
            var tds = $(this).parent().parent().children();
            var name = tds.eq(2).text();
            var price = tds.eq(3).text();
            tds.eq(2).html("<input name='name' style='width:300px;' value='" + name + "' />");
            tds.eq(3).html("<input name='price' style='width:80px;' value='" + price + "' />");
            tds.eq(5).html("<a href='#' name='updatedone'>保存</a>");
            return false;
        });
        $p.on("click", "a[name=updatedone]", function () {
            var $parent = $(this).parent();
            var tds = $(this).parent().parent().children();
            var $name = tds.eq(2).find("input");
            var $price = tds.eq(3).find("input");
            var name = $name.val();
            var price = $price.val();

            if (name.length > 0 && checkDecimal(price)) {
                $.post('/product/related_largepacking', { relatedid: $parent.attr("relatedid"), price: price, quantity: tds.eq(4).attr("q"), viewname: name }, function (data) {
                    if (data.status) {
                        $name.remove();
                        $price.remove();
                        tds.eq(2).text(name);
                        tds.eq(3).text(price);
                        tds.eq(5).html('<a href="#" name="update">编辑</a><a href="#" name="del">删除</a>');
                        isupdate = false;
                    } else {
                        jx.alert("失败了!" + data.msg);
                    }
                    return false;
                });

            }

            return false;
        });

        //删除
        $p.on("click", "a[name=del]", function () {
            var $this = $(this);
            var $parent = $this.parent();
            var relatedid = $parent.attr("relatedid");
            jx.del(function () {
                $.post("/product/related_del", { relatedid: relatedid }, function (result) {
                    if (result.status) {
                        $parent.parent().remove();
                    } else {
                        jx.alert("出错了!" + result.msg);
                    }
                });
            });
            return false;
        });
        $cancel.on("click", function () {
            $p.find(".updateTitle").addClass("hide");
            related.clearItem($p);
            $cancel.hide();
            if ($childproductid)
                $childproductid.val('0');
            return false;
        });
    },

    //推荐
    tuijian: function () {
        var $tuijian = $('#tuijian');
        var productid = parseInt($tuijian.find(".pid").text());

        // 保存
        $tuijian
            .on("click", "a.save", function () {
                var $this = $(this);
                //获取productids数据
                var productIDs = [];
                var $ids = $tuijian.find("input[name=tuijianid]");
                $ids.each(function (i, item) {
                    var $item = $(item);
                    if ($item.attr("pass") != "1")
                        return true;
                    var v = $item.val();
                    if (productIDs.indexOf(v) == -1) {
                        productIDs.push(v);
                    }
                });
                if (productIDs.length > 0) {
                    //提交
                    $.post("/product/Related_BestRecommend", { productid: productid, productIDs: productIDs.join(",") },
                        function (data) {
                            if (data.status) {
                                var $gou = $("<p class='gou'>保存成功</p>");
                                $this.after($gou);
                                jx.autoHide($gou);
                                //成功,刷新table数据
                                $tuijian.find("div.infoTable").load("/product/Related?type=3&productid=" + productid);
                            }
                            else {
                                jx.error("提交失败!" + data.msg);
                            }
                        });
                }
                else {
                    jx.alert("您需要填写数据!");
                }
                return false;
            })
            .on("click", "a[name=del]", function () {
                var $this = $(this);
                var pid = $this.attr("productid");
                var childproductid = $this.attr("childproductid");
                jx.del(function () {
                    $.post("/product/related_del", { productid: pid, childproductid: childproductid, type: 2 }, function (result) {
                        if (result.status) {
                            jx.alert("删除推荐商品成功!");
                            $this.parent().parent().remove();

                            //搜索
                            $tuijian.find(".shortinput").each(function (i, item) {
                                if ($(item).val() == childproductid) {
                                    $(item).val('').attr("old", "0").attr("pass", "0").next().html('');
                                    return false;
                                }
                                return true;
                            });
                        }
                    });
                });
                return false;
            });
        //检查数据
        var t;
        $tuijian.on("change keyup blur", "input[name=tuijianid]", function () {
            var $this = $(this);
            var value = $this.val();
            if ($this.attr("old") == value) {
                return false;
            }
            clearTimeout(t);
            $this.attr("pass", "0");
            var $next = $this.next().html('').removeClass("error");

            if (value == '')
                return false;
            if (!/^\d{2,8}$/.test(value)) {
                $next.html("数据错误!请填写数字!").addClass("error");
            }
            else {
                t = setTimeout(function () {
                    $this.attr("old", value);
                    product.getProductByID(value, function (data) {
                        if (data.status) {
                            $this.attr("pass", "1");
                            $next.html("<a href='http://www.jxdyf.com/product/" + data.data.ProductID + ".html'>" + data.data.ChineseName + "</a>");
                        }
                        else {
                            $next.html("没有找到该商品ID");
                        }
                    });
                }, 500);
            }
        });

        //加载推荐数据到编辑框

        function setItemsShow() {
            var $inputlist = $tuijian.find("input");
            $tuijian.find("table tr").slice(1).each(function (i, item) {
                $inputlist.eq(i).attr("pass", "1").val($(item).children().eq(1).text());
                $inputlist.eq(i).next().html("<a href='#'>" + $(item).children().eq(2).text() + "</a>");
            });
        }
        setItemsShow();
    },

    //规格
    guige: function () {
        var $guige = $("#guige");
        var productid = parseInt($guige.find(".pid").text());

        var $childid = $guige.find("input[name=childid]");
        var $viewname = $guige.find("input[name=viewname]");
        var t;
        //检查数据
        $childid.bind("change blur keyup", function () {
            var $this = $(this);
            var value = $this.val();
            if ($this.attr("old") == value) {
                return false;
            }
            clearTimeout(t);
            $this.attr("pass", "0");
            var $next = $this.next().html('').removeClass("error");

            if (value == '')
                return false;
            if (!/^\d{2,8}$/.test(value)) {
                $next.html("数据错误!请填写数字!").addClass("error");
            }
            else {
                t = setTimeout(function () {
                    $this.attr("old", value);
                    product.getProductByID(value, function (data) {
                        if (data.status) {
                            $this.attr("pass", "1");
                            $next.html("<a href='http://www.jxdyf.com/product/" + data.data.ProductID + ".html'>" + data.data.ChineseName + "</a>  规格:" + data.data.Specifications);
                            $viewname.val(data.data.ChineseName + "(" + data.data.Specifications + ")");
                        }
                        else {
                            $next.html("没有找到该商品ID");
                        }
                    });
                }, 500);
            }
        });
        //保存
        $guige.find("a[name=save]").click(function () {
            var $this = $(this);
            var id = parseInt($childid.val());
            var name = $viewname.val();
            if (isNaN(id) || name.length == 0)
                return false;
            $.post("/product/related_differentspec", { productid: productid, childproductid: id, name: name }, function (data) {
                if (data.status) {
                    var $gou = $("<p class='gou'>保存成功</p>");
                    $this.after($gou);
                    setTimeout(function () { $gou.remove() }, 2000);
                    //成功,刷新table数据
                    $guige.find('.infoTable').load("/product/Related?type=2&productid=" + productid);
                    $childid.val('').next().html('');
                    $viewname.val('');
                }
                else {
                    jx.error("提交失败!" + data.msg);
                }
            });
            return false;
        });
        //修改
        $guige.find("div.infoTable")
            .on("click", "a[name=update]", function () {
                var $this = $(this);
                var tds = $this.parent().parent().children();
                $childid.val(tds.eq(1).text());
                $viewname.val(tds.eq(4).text())
                $childid.next().html("<a href='#'>" + tds.eq(2).text() + "</a>");
            })
            .on("click", "a[name=del]", function () {
                var $this = $(this);
                var $parent = $this.parent();
                var productid = $parent.attr('productid');
                var childproductid = $parent.attr("childproductid");
                jx.del(function () {
                    $.post("/product/related_del", { productid: productid, childproductid: childproductid, type: 2 }, function (data) {
                        if (data.status) {
                            jx.alert("删除成功!");
                            $parent.parent().remove();
                        } else {
                            jx.alert(data.msg);
                        }
                    });
                });
                return false;
            });
    },

    //组合
    zuhe: function () {


        var $zuhe = $("#zuhe");
        var productid = parseInt($zuhe.find(".pid").text());
        var productname = $zuhe.find(".pname").text()
        var unit = $zuhe.find(".unit").text()


        var $temptab = $zuhe.find('#temptab');
        var $childid = $zuhe.find("input[name=childid]");
        var $childprice = $zuhe.find("input[name=childprice]");
        var $childcount = $zuhe.find("input[name=childcount]");
        var $viewname = $zuhe.find('input[name=viewname]');

        var $comboname = $temptab.find('input[name=comboname]');
        var $submitcombo = $temptab.find("a[name=submitcombo]");

        var defaulttr = $temptab.find("tbody").html();  //默认当前商品的tr 先备份 
        //加载当前数据到组合
        //加载商品信息
        var t;
        $childid.on("keyup change blur", function () {
            var $this = $(this);
            var value = $this.val();
            if ($this.attr("old") == value) {
                return false;
            }
            clearTimeout(t);
            $this.attr("pass", "0");
            var $next = $this.next().html('').removeClass("error");

            if (value == '')
                return false;
            if (!/^\d{2,8}$/.test(value)) {
                $next.html("数据错误!请填写数字!").addClass("error");
            }
            else {
                t = setTimeout(function () {
                    $this.attr("old", value);
                    product.getProductByID(value, function (data) {
                        if (data.status) {
                            $this.attr("pass", "1");
                            $next.html("<a productcode='" + data.data.ProductCode + "' spec='" + data.data.Specifications + "' href='http://www.jxdyf.com/product/" + data.data.ProductID + ".html'>" + data.data.ChineseName + "</a>  规格:" + data.data.Specifications);
                            $viewname.val(data.data.ChineseName + "(" + data.data.Specifications + ")");
                        }
                        else {
                            $next.html("没有找到该商品ID");
                        }
                    });
                }, 500);
            }
        });
        $childprice.on("keyup change blur", function () {
            var value = $(this).val();
            var iserror = /^\d*(\d|(\.[1-9]{1,2}))$/.test(value);
            var $msg = $childprice.next().next();
            if (!iserror)
                $msg.html("请输入正确的价格").addClass("error");
            else
                $msg.html('').removeClass("error");
        });
        $childcount.on("keyup change blur", function () {
            var value = $(this).val();
            var iserror = /^\d{1,5}$/.test(value);
            var $msg = $childcount.next().next();
            if (!iserror)
                $msg.html("请输入正确的数量").addClass("error");
            else
                $msg.html('').removeClass("error");
        });
        $zuhe.find('a[name=save]').on("click", function () {
            setViewTemp(); return false;
        });


        var updatecombo_productid = 0;
        var isupdate = 0;
        $submitcombo.on("click", function () {

            var mainproductid = productid;
            var comboname = $comboname.val();

            if (isupdate > 0) {
                jx.alert("您正在编辑中!");
                return false;
            }

            //组合子商品数据
            var data = [];
            data.push("<items>");
            $temptab.find("tbody>tr").each(function (i, item) {
                var tds = $(item).children();
                data.push("<item>");
                data.push("<comboproductid>" + tds.eq(0).text() + "</comboproductid>");
                data.push("<name>" + tds.eq(4).text() + "</name>");
                data.push("<price>" + tds.eq(5).text() + "</price>");
                data.push("<quantity>" + tds.eq(6).attr("quantity") + "</quantity>");
                data.push("</item>");
            });
            data.push("</items>");
            //提交组合,并且生成数据  updatecombo_productid 在编辑时赋值
            if (updatecombo_productid == 0) {
                $.post("/product/combo", { mainproductid: mainproductid, comboname: comboname, data: encodeURIComponent(data.join('')) }, function (result) {
                    if (result.status) {
                        var $gou = $("<p class='gou'>保存成功</p>")
                        $submitcombo.after($gou);
                        setTimeout(function () { $gou.remove(); }, 2000);
                        $zuhe.find("table:last").load("/product/combo?productid=" + mainproductid);
                    }
                    else {
                        jx.alert("失败!" + result.msg);
                    }
                });
            }
            else {
                //修改
                $.post("/product/combo", { productid: updatecombo_productid, comboname: comboname, data: encodeURIComponent(data.join('')) }, function (result) {
                    if (result.status) {
                        var $gou = $("<p class='gou'>保存成功</p>")
                        $temptab.find("a.cancel").after($gou).hide();
                        setTimeout(function () { $gou.remove(); }, 2000);
                        $zuhe.find("table:last").load("/product/combo?productid=" + mainproductid);
                    } else {
                        jx.alert("失败!" + result.msg);
                    }
                });
            }
            //重置信息
            $temptab.find("tbody").html(defaulttr);
            $comboname.val('');
            return false;
        });


        //删除套餐  判断是否是修改,是修改取消修改状态
        $zuhe.on("click", ".taoCanZH a[name=del]", function () {
            var $this = $(this);
            var productid = $(this).parent().attr("productid");
            jx.del(function () {
                $.post("/product/combodel", { productid: productid }, function (result) {
                    if (result.status) {
                        jx.alert("删除套餐成功!");
                        $this.parent().parent().remove();

                        if (productid == updatecombo_productid) {
                            updatecombo_productid = 0;
                            $temptab.find("tbody").html(defaulttr);
                        }
                    } else {
                        jx.alert("失败!" + data.msg);
                    }
                });
            });
            return false;
        });
        $zuhe.on("click", "a[name=delrow]", function () {
            var $this = $(this);
            jx.del(function () {
                $this.parent().parent().remove();
            });
            return false;
        });
        //修改套餐
        $zuhe.on("click", ".taoCanZH a[name=update]", function () {
            var productid = $(this).parent().attr("productid");
            $.post("/product/ComboDetail", { productid: productid }, function (result) {
                if (result.status) {
                    var html = [];
                    for (i = 0; i < result.data.length; i++) {
                        var item = result.data[i];
                        html.push('<tr><td>' + item.ComboProductID + '</td><td>' + item.ComboProductCode + '</td><td>' + item.ComboProductName + '</td><td>' + item.Spec + '</td><td>' + item.Name + '</td><td>' + item.Price + '</td><td quantity="' + item.Quantity + '">*' + item.Quantity + item.Unit + '</td><td><a href="#" name="update">编辑</a>' + (item.IsMain ? "" : '<a href="#" name="delrow">删除</a>') + '</td></tr>');
                    }
                    $temptab.find("tbody").html(html.join(''));
                    $temptab.find("a.cancel").show();
                    updatecombo_productid = productid;
                    setZuHeName();
                }
            });
            return false;
        });

        //修改子商品
        $temptab.on("click", "a[name=update]", function () {
            var $this = $(this);
            if ($this.attr("isupdate") == "1") {
                var tds = $this.parent().parent().children();
                var viewname = tds.eq(4).find("input").val();
                var price = tds.eq(5).find('input').val();
                var count = tds.eq(6).find("input").val();
                if (viewname.length == 0 || !/^\d*(\d|(\.[1-9]{1,2}))$/.test(price) || !/^\d{1,5}$/.test(count) || price <= 0 || count <= 0) {
                    jx.alert('请确认参数!');
                    return false;
                }
                tds.eq(4).html(viewname);
                tds.eq(5).html(price);
                tds.eq(6).html("*" + count + tds.eq(6).text()).attr("quantity", count);
                $this.attr("isupdate", "0");
                isupdate--;
                $this.next().show();
                $this.text("编辑")
                setZuHeName();
            }
            else {
                $this.attr("isupdate", "1");
                var tds = $this.parent().parent().children();
                tds.eq(4).html("<input type='text' value='" + tds.eq(4).html() + "' style='width:260px;' />");
                tds.eq(5).html("<input type='text' value='" + tds.eq(5).html() + "' />");
                tds.eq(6).html("<input type='text' value='" + tds.eq(6).attr("quantity") + "' />" + tds.eq(6).text().substr(-1));
                $this.next().hide()
                $this.text("保存");
                isupdate++;
            }
            return false;
        });

        $temptab.find("a.cancel").click(function () {
            $temptab.find("tbody").html(defaulttr);
            updatecombo_productid = 0;
            $(this).hide();
            return false;
        });

        function setViewTemp() {
            var id = $childid.val();
            var price = $childprice.val();
            var count = $childcount.val();
            var product = $childid.next().find("a");
            var productname = product.html();

            var unit = $childcount.next().html();
            var html = '<tr><td>' + id + '</td><td>' + product.attr("productcode") + '</td><td>' + productname + '</td><td>' + product.attr("spec") + '</td><td>' + $viewname.val() + '</td><td>' + price + '</td><td quantity="' + count + '">*' + count + unit + '</td><td><a href="#" name="del">删除</a></td></tr>';
            html = $(html).find("a[name=del]").click(function () { $(this).parent().parent().remove(); return false; }).end();
            $temptab.find('tbody').append(html);

            $childid.val('').next().html('');
            $childcount.val('');
            $childprice.val('');
            $viewname.val('');

            setZuHeName();
        }
        function setZuHeName() {
            var tds;
            var str = [];
            $temptab.find("tbody>tr").each(function (i, item) {
                tds = $(item).children();
                str.push(tds.eq(4).html() + tds.eq(6).html());
            });
            $comboname.val(str.join('+'));
        }
    },

    clearItem: function ($p) {
        $p.find(".spInput input").val('');
    }
}

//库房
var productstock = {};
productstock.init = function (tr$) {
    tr$.on("click", "a[name=stock]", function () {
        jx.open("/product/stock?productid=" + product.getProductID($(this)), null, 450, 600);
        return false;
    });
}
productstock.edit = function () {
    $('a.submit').on("click", function () {
        $('#form1').submit();
    });
    $('.selecWrap').each(function (i, item) { $(item).select1(); });
}

//报价属性
var paraprice = {}
paraprice.init = function () {

}