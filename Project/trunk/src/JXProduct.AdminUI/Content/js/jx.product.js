//var temp = 'id|code|barcode|guige|color|size|dushu';
var temp = "{0}|{1}|{2}|{3}|{4}|{5}|{6}";
var product = {}
product.list = function () {
    product.cf();
    product.quickedit();
    product.bindclick();
    product.search();
    product.stock();
}
product.pagechange = function () {
    product.quickedit();
    product.bindclick();
    product.stock();
}
product.search = function () {
    var $form = $('#form1');
    $('.submit').on('click', function () {
        $form.submit();
    })
}
product.get = function (productid, func) {
    $.ajax({
        type: "POST",
        url: "/product/get",
        data: { productid: productid },
        dataType: "json",
        cache: true,
        success: function (response) {
            if (response.status) {
                func(response.data);
            }
            else {
                jx.alert("失败了");
            }
        }
    });
}
product.stock = function () {
    var productIDs = [];
    var $items = $('span[combo=1]');
    if ($items.length == 0)
        return;
    $.each($items, function (i, item) { productIDs.push($(item).attr("pid")); });
    $.ajax({
        type: "POST",
        url: "/product/getcombostock",
        data: { productIDS: productIDs.join(',') },
        dataType: "json",
        success: function (result) {
            if (result.status && result.data.length > 0) {
                $.each(result.data, function (i, item) {
                    var t = $('span[pid=' + item.productid + ']');
                    t.text(item.stock);
                });
            }
        }
    });
}


// classification
product.cf = function () {
    product.cflist = null;
    //分类
    product.cflist = $('select.cf');
    product.cflist.not("#cf3").on("change", function () {
        var $this = $(this);
        var index = parseInt($this.attr("id").replace("cf", ""));
        var cfid = parseInt($this.val());
        product.cfclearoption(index);
        if (cfid == 0) {
            return false;
        }
        product.getcfbyid(index, cfid);
    });
    product.getcfbyid(0, 0);
}
product.getcfbyid = function (index, cfid) {
    $.ajax({
        type: "post",
        url: "/classification/getbyparentid",
        data: { parentid: cfid },
        dataType: "json",
        success: function (data) {
            var $cf = product.cflist.eq(index);
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
product.cfclearoption = function (index) {
    product.cflist.each(function (i, item) {
        if (i < index)
            return true;
        product.cflist.eq(i).find("option").slice(1).remove();
    });
}
product.gotop = function () {
    $("html,body").animate({ scrollTop: $("#tablelist").offset().top }, 200);
}

//quickedit
product.quickedit = function () {

    //赠品
    var $gift = $('input[name=giftid]');
    $gift.on("blur", function () {
        var $this = $(this)
        var id = $this.val();
        if (id == "")
            return false;
        if (id.isnum()) {
            product.get(id, function (data) {
                $this.next().remove();
                $this.after("<div class='aotu-info'>自动读取：" + data.ChineseName + "</div>");
            });
        }
    });

    //数量
    $('div.setNumber').each(function (i, item) {
        var $this = $(item);
        var $input = $this.find("input");
        var $jian = $this.find(".jian");
        var $jia = $this.find(".jia");
        $jia.on("click", function () {
            var count = parseInt($input.val());
            if (count < 99)
                count++;
            $input.val(count);
        });
        $jian.on("click", function () {
            var count = parseInt($input.val());
            if (count > 0)
                count--;
            $input.val(count);
        });
        $input.on("blur keyup change", function () {
            var count = parseInt($input.val());
            if (!count.toString().isnum()) {
                count = 1;
            }
            else {
                if (count > 99) count = 99;
                if (count < 0) count = 0;
            }
            $input.val(count);
        });
    });
    //保存
    $('a.save').on("click", function () {

        var $td = $(this).parent();
        var productid = $(this).attr("productid");
        var giftid = $td.find("input[name=giftid]").val();
        var giftcount = $td.find("input[name=giftcount]").val();
        var freeship = $td.find("input[name=freeship]").prop('checked') ? 1 : 0;
        var top = $td.find("input[name=top]").prop('checked') ? 1 : 0;
        var recommend = $td.find("input[name=recommend]").prop('checked') ? 1 : 0;
        var selling = $td.find("select[name=selling]").val();
        var actions = $td.find("select[name=actions]").val();
        var sort = $td.find("input[name=sort]").val();

        $.ajax({
            type: "POST",
            url: "/product/quickedit",
            data: {
                productid: productid,
                productgiftid: giftid,
                productgiftcount: giftcount,
                IsFreeShip: freeship,
                IsTop: top,
                IsRecommend: recommend,
                Selling: selling,
                Actions: actions,
                Sort: sort
            },
            dataType: "json",
            success: function (result) {
                if (result.status) {
                    jx.alert("编辑成功!");
                }
                else {
                    jx.error("编辑失败!");
                }
            }
        });
    });
}

//list.open
product.bindclick = function () {
    $('a[name=activty]').on("click", function () {
        var $this = $(this);
        var productid = $this.parent().parent().attr("productid");
        jx.open("/product/activity?productid=" + productid, { height: 620, width: 450 });
        return false;
    });
    $('a[name=price]').on("click", function () {
        var $this = $(this);
        var productid = $this.parent().parent().attr("productid");
        jx.open("/product/price?productid=" + productid, { height: 400, width: 500 });
        return false;
    });
}

//file
product.uploadfile = function () {
    //默认参数
    var defaultSetting = {
        "height": 24,
        'width': 80,
        'swf': '/js/uploadify3.2.1/uploadify.swf?v=' + new Date().getMilliseconds(),
        'fileSizeLimit': "1024KB",
        'method': "post",
        'fileTypeDesc': '只允许上传gif,jpg,jpeg,png图片',
        'fileTypeExts': '*.gif; *.jpg; *.png',
        "removeCompleted": false,
        'wmode': 'transparent',
        'preventCaching': false,
        'auto': true,
        'overrideEvents': ['onUploadProgress'],
        'multi': false,
        'buttonText': '上传图片'
    };

    $('.flooSpecial>div').each(function (i, item) {
        var index = i + 1;
        var $imageupload = $("#upload" + index);
        var $imagelist = $('#updateImgWrap' + index);
        setTimeout(function () {
            //专业参数
            var prosetting = {
                'uploader': '/floor/upload',
                'queueID': 'updateImgWrap' + index,
                'itemTemplate': '<div  id="${fileID}" class="upImage"><span>上传中</span><a style="display: none;" href="javascript:;" class="removeImgeCha">×</a></div>',
                'onSelect': function () {
                    if ($imagelist.children().length > 1)
                        $imagelist.find(".upImage").eq(0).remove();
                },
                "onUploadSuccess": function (file, data, response) {
                    data = $.parseJSON(data);
                    var $f = $('#' + file.id);
                    if (!data.status) {
                        $f.find("span").text("上传失败!");
                        return false;
                    }
                    $f.find("span").replaceWith("<img src='" + data.data + "' />");
                    $f.parent().prev().find("input:hidden").val(data.data);
                    //$imageupload.uploadify('disable', true);
                    //$imageupload.uploadify('settings', 'buttonText', '已上传');
                    $f.find("a").on("click", function () {
                        if (!confirm("确定要删除图片吗?"))
                            return false;
                        var $this = $(this);
                        $this.parent().remove();
                        $imageupload.uploadify('disable', false);
                        $imageupload.uploadify('settings', 'buttonText', '上传图片');
                        return false;
                    });
                }
            };
            //合并上传参数
            $.extend(prosetting, defaultSetting);
            $imageupload.uploadify(prosetting);
        }, 10);

    });
    //显示/隐藏
    $('.updateImgWrap').on('mouseover mouseout', 'div', function (event) {
        var $this = $(this).find("a");
        if ($this.length == 0) return false;
        switch (event.type) {
            case "mouseover": $this.show(); break;
            case "mouseout": $this.hide(); break;
        }
    });
}
//price
product.price = function () {
    var $productid = $('i[nid=productid]');
    var $productode = $('i[nid=productcode]');
    var $chinseeName = $('i[nid=chinesename]');
    var $checkprice = $('#checkprice');
    var $marketprice = $('input[name=marketprice]');
    var $tradeprice = $('input[name=tradeprice]');
    var $mobileprice = $('input[name=mobileprice]');
    var $costprice = $('input[nid=costprice]');
    var $hidden_productid = $('#ProductID');

    var $form = $('#form1');
    $form.validate({
        errorPlacement: function (error, element) {
            error.insertAfter(element.next());
        },
        submitHandler: function (form) {
            $.ajax({
                type: "POST",
                url: "/product/price",
                data: $form.serialize(),
                dataType: "json",
                success: function (result) {
                    if (result.status) {
                        jx.alert("编辑成功!");
                    } else {
                        jx.error("编辑失败!" + result.msg);
                    }
                }
            });
        }
    });
    $('a.save').on('click', function () { $form.submit(); });

    $('#packlist li').on("click", function () {
        var $this = $(this);
        if ($this.hasClass("active"))
            return false;

        var productid = $this.attr("productid");
        product.get(productid, function (data) {
            $productid.text(data.ProductID);
            $productode.text(data.ProductCode);
            $chinseeName.html(data.ChineseName + (data.Specifications.length > 0 ? " [" + data.Specifications + "]" : ""));
            $costprice.text(data.CostPrice);
            console.log(data.CheckPrice);
            $checkprice.val(data.CheckPrice);
            $marketprice.val(data.MarketPrice);
            $tradeprice.val(data.TradePrice);
            $mobileprice.val(data.MobilePrice);
            $hidden_productid.val(data.ProductID);
            $this.siblings().removeClass();
            $this.addClass("active");
        });
    });
}

var productedit = {};
productedit.common = function () {
    //属性
    var $paras = $('input[name=ParameterType]');
    var $drugtype = $('select[name=DrugType]');
    var $prescriptiontype = $('select[name=PrescriptionType]');
    var $drug = $('.drug').hide();
    $paras.on('click', function () {
        var $this = $(this);
        $paras.prop("checked", false);
        $this.prop("checked", true);
        if ($this.val() == "1") {
            $drug.show();
        }
        else {
            $drug.hide();
            $drug.find('select').val('');
        }
    });
    if ($(".ptype :checked").val() == "1") {
        $drug.show();
        setshowtype();
    }
    $drugtype.change(function () {
        setshowtype();
    });

    var $name = $('input[name=CADN]');
    $name.on('blur', function () {
        var $pyname = $('input[name=PinyinName]');
        $.ajax({
            type: "post",
            url: "/product/getpy",
            data: { name: $name.val() },
            dataType: "json",
            success: function (result) {
                $pyname.val(result.data);
            }
        });
    });

    function setshowtype() {
        if ($drugtype.val() == "1") {
            $prescriptiontype.removeClass();
        }
        else {
            $prescriptiontype.addClass("hide").next().hide();
            $prescriptiontype.val('');
        }
    }
}


productedit.init = function () {
    productedit.common();
    //商品主体信息
    $("a.save").on('click', function () { $form.submit(); });
    var $form = $('#formproduct');
    $form.validate({
        errorPlacement: function (error, element) { error.insertAfter(element); },
        ignore: ":hidden:not('#BrandID,#SupplierID,.img,#ManufacturerID')",
        submitHandler: function () {
            if ($(".ptype :checked").length > 0) {

            }
            else {
                jx.error("请选择分类!");
                return false;
            }

            $.ajax({
                type: "POST", url: "/product/edit", data: $form.serialize(), dataType: "json", success: function (result) {
                    if (result.status) { jx.alert("编辑成功!"); } else { jx.error("编辑失败!"); }
                }
            });
            return false;
        }
    });

    //商品说明图片+促销
    $('a.savedesc').on('click', function () {
        $.ajax({
            type: "POST",
            url: "/product/editpromotion",
            data: { productid: productid, Promotion: encodeURIComponent(editor.html()) },
            dataType: "json",
            success: function (result) {
                if (result.status) { jx.alert("编辑成功!"); } else { jx.error("编辑失败!"); }
            }
        });
    });

    //seo数据
    $('a.saveseo').on('click', function () {
        $.ajax({
            type: "POST",
            url: "/product/editseo",
            data: "productid=" + productid + "&" + $('#formseo').serialize(),
            dataType: "json",
            success: function (result) {
                if (result.status) { jx.alert("编辑成功!"); } else { jx.error("编辑失败!"); }
            }
        });
    });

    //价格
    var $formprice = $('#formprice');
    $formprice.validate();
    $('a.saveprice').on('click', function () {
        if (!$formprice.valid())
            return false;
        $.ajax({
            type: "POST",
            url: "/product/editprice",
            data: "productid=" + productid + "&" + $formprice.serialize(),
            dataType: "json",
            success: function (result) {
                if (result.status) { jx.alert("编辑成功!"); } else { jx.error("编辑失败!" + result.msg); }
            }, error: function () {
                jx.error("出错了,请重新输入!");
            }
        });
    });

    $('input[name=CADN],input[name=TradeName],input[name=Manufacturer],input[name=ManufacturerAddress]').on("blur change keyup", function () {
        var $this = $(this);
        var $other = $('input[name=' + $this.attr("name") + "1");
        $other.val($this.val());
    });

    $.get("/product/getmanufacturer", function (result) {
        var datas = result.data;
        var $m = $('input[name=Manufacturer]');
        var $maddrss = $('input[name=ManufacturerAddress]');
        var $hidden_m = $m.prev();
        $m.autocomplete(datas, {
            formatItem: function (row, i, max) {
                return "<table width='400px'><tr><td align='left'>" + row.mname + "</td><td align='right'></td></tr></table>";
            },
            formatMatch: function (row, i, max) {
                return row.mname.toString();
            }
        }).result(function (event, data, formatted) {
            $hidden_m.val(data.mid);
            $maddrss.val(data.maddress);
        });
    });
}
productedit.operate = function () {
    var $table = $('table.table');
    //规格编辑
    //添加信息到数据
    var $productids = $('#ProductIDs');
    var $hidden_count = $('input[name=hidden_count]');
    var $save = $('a[name=save]');

    var $productcode = $('input[name=productcode]');
    var $barcode = $('input[name=barcode]');
    var $guige = $('input[name=guige]');
    var $color = $('input[name=color]');
    var $size = $('input[name=size]');
    var $dushu = $('input[name=dushu]');

    var $cadn = $('input[name=CADN]');
    var $chinesename = $('input[name=TradeName]');

    var count = parseInt($hidden_count.val());
    var codes = [];
    var isedit = false;
    var editproductid = 0;
    var editindex = 0;
    $save.on('click', function () {
        if (isedit) {
            var $td = $('td[index=' + editindex + ']');
            var $hidden = $td.find("input");
            $hidden.val(temp.format(editproductid, $productcode.val(), $barcode.val(), $guige.val(), $color.val(), $size.val(), $dushu.val()));
            var $tds = $td.parent().find("td");
            $tds.eq(3).text($chinesename.val());
            $tds.eq(4).text($cadn.val());
            $tds.eq(5).text($barcode.val());
            $tds.eq(6).text($guige.val());
            $tds.eq(7).text($color.val());
            $tds.eq(8).text($size.val());
            $tds.eq(9).text($dushu.val());
            isedit = false;
            editproductid = 0;
            $save.text("添加");
            cleartextbox();
        }
        else {
            var productcode = $productcode.val();
            if (productcode == '' && codes.indexOf(productcode) == -1) {
                jx.error("您需要填写正确的金象码!");
                return false;
            }
            $.post("/product/checkproductcode", { productcode: productcode }, function (result) {
                if (!result.status) {
                    codes.push(productcode);
                    var index = parseInt($table.find("tr:last").find("td:first").text());
                    var str = '<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td index="' + index + '"><a href="#" name="edit">编辑</a><a href="#" name="del" class="ml-10">删除</a><input type="hidden" name="newproduct{10}" value="{11}"></td></tr>';
                    str = str.format(++index, "", productcode, $chinesename.val(), $cadn.val(), $barcode.val(), $guige.val(), $color.val(), $size.val(), $dushu.val(),
                        index,
                        temp.format(0, productcode, $barcode.val(), $guige.val(), $color.val(), $size.val(), $dushu.val()));
                    $table.find("tbody").append(str);
                    cleartextbox();
                    $hidden_count.val(index);
                } else {
                    jx.error("金象码已经存在!");
                }
            });
        }
        return false;
    });
    $table.on('click', "a[name=edit]", function () {
        isedit = true;
        var $this = $(this);
        editindex = parseInt($this.parent().attr("index"));
        var arr = $this.parent().find("input").val().split('|');
        editproductid = parseInt(arr[0]);
        $productcode.val(arr[1]).attr("readonly", true);
        $barcode.val(arr[2]);
        $guige.val(arr[3]);
        $color.val(arr[4]);
        $size.val(arr[5]);
        $dushu.val(arr[6]);
        $save.text('编辑');
        return false;
    });
    $table.on('click', "a[name=del]", function () {
        var $this = $(this);
        var id = parseInt($this.parent().find("input").val().split('|')[0]);
        if (productid == id) {
            jx.alert("该行不能删除!");
            return false;
        }
        jx.confirm("确定要删除该行吗?", function () {
            var productids = $productids.val().split(',');
            var index = productids.indexOf(id.toString());
            if (index > -1) {
                productids.remove(index);
                $productids.val(productids.join(','));
            }
            $this.parent().parent().remove();
        });
        return false;
    });
    function cleartextbox() {
        $productcode.val('').removeAttr("readonly");
        $guige.val('');
        $color.val('');
        $size.val('');
        $dushu.val('');
        $barcode.val('');
    }
}
productedit.keyword = function () {
    //关键字
    var url = "/product/getkeywords";
    var $keyword = $('#keyword');
    var $keylist = $('#keywordlist');
    var $exists = $('#existskeyword');
    var t = 0;
    //seach
    $keyword.on("blur keyup", function () {
        var q = $keyword.val();
        if (q == $keyword.attr("tip")) {
            return false;
        }
        clearTimeout(t);
        t = setTimeout(function () {
            $keylist.find("p").remove().end().find("ul>li").remove();
            $keyword.attr("tip", q);
            if (q == '') {
                $keylist.append("<p class='xingRed noneTag'>请输入标签！</p>")
                return;
            }
            productedit.getkeyword(q, function (data) {
                if (data.status) {
                    $keylist.prepend("<p class='xingRed noneTag'>检索到相关匹配标签，您可以直接点击贴上标签，或直接添加此标签！</p>");
                    var lis = [];
                    for (var i = 0; i < data.data.length; i++) {
                        lis.push("<li><a href='#' keywordid='" + data.data[i].KeywordID + "'>" + data.data[i].ChineseName + "</a><span>贴上</span></li>");
                    }
                    $keylist.find("ul").append(lis.join(''));
                } else {
                    $keylist.append("<p class='xingRed noneTag'>未检索到标签，可以直接添加此标签！</p>")
                }
            });
        }, 1000);
    });
    //add /related
    $keylist.on('click', 'span', function () {
        var $a = $(this).prev();
        var kid = $a.attr("keywordid");
        var kw = $a.text();
        $.ajax({
            type: "POST",
            url: "/product/addkeyword",
            data: { productid: productid, keywordid: kid },
            dataType: "json",
            success: function (data) {
                if (data.status) {
                    $exists.append(" <li><a href='#' keywordid='" + kid + "'>" + kw + "</a><i>×</i></li>");
                    $a.parent().remove();
                }
                else {
                    jx.error('失败了!');
                }
            }
        });
    });
    //add
    $('a[name=addkeyword]').on("click", function () {
        var $this = $(this);
        var name = $this.prev().val();
        if (name.length != 0) {
            $.ajax({
                type: "POST",
                url: "/product/addkeyword",
                data: { productid: productid, keywordid: 0, chinesename: name },
                dataType: "json",
                success: function (data) {
                    if (data.status) {
                        $exists.append(" <li><a href='#' keywordid='" + data.data + "'>" + name + "</a><i class='del'>×</i></li>");
                        $keylist.find("p").remove().end().find("li").remove();
                        $this.val('');
                    }
                    else {
                        jx.error('失败了!');
                    }
                }
            });
        }
        return false;
    });
    //del
    $exists.on("click", "i.del", function () {
        var $this = $(this);
        var keywordid = $this.prev().attr("keywordid");
        jx.del(function () {
            $.ajax({
                type: "POST",
                url: "/product/DelKeyword",
                data: { productid: productid, keywordid: keywordid },
                dataType: "json",
                success: function (data) {
                    if (data.status) {
                        $this.parent().remove();
                    }
                    else {
                        jx.error(data.msg);
                    }
                }
            });
        })
    });
}
productedit.getkeyword = function (q, func) {
    $.ajax({
        type: "POST",
        url: "/product/getkeywords",
        data: { q: q },
        dataType: "json",
        success: function (data) {
            func(data);
        }
    });
}
productedit.upload = function () {
    //默认参数

    //说明书
    var $shuoming = $('#shuoming');
    productedit.baseupload($shuoming, "shuomingimg", "/product/uploadshuoming", { productid: productid }, function (file, data, response) {
        data = $.parseJSON(data);
        var $f = $('#' + file.id);
        if (!data.status) {
            $f.find("span").text("上传失败!");
            return false;
        }
        $f.find("span").replaceWith("<img src='" + data.data + "' />");
    }, function () {
        var $this = $(this);
        jx.del(function () {
            productedit.delimage($this, productid, 3, 0);
        });
        return false;
    });

    //商品5张图
    $('#uploadimage>div').each(function (i, item) {
        var $this = $(item);
        var $btn = $this.find("#image" + i);
        var $img = $this.find("#imagequeue" + i);
        productedit.baseupload($btn, "imagequeue" + i, "/product/uploadimage", { productid: productid, index: (i + 1) }, function (file, data, response) {
            var $f = $('#' + file.id);
            data = $.parseJSON(data);
            if (!data.status) {
                $f.find("span").text("上传失败!");
                return false;
            }
            $f.find("span").replaceWith("<img src='" + data.data + "' />");
            $f.parent().removeClass('hide').prev().find(":hidden").val(data.msg);
        }, function () {
            var $this = $(this);
            jx.del(function () {
                productedit.delimage($this, productid, 1, i);
                $this.parent().prev().find(":hidden").val('');
            });
            return false;
        });
    });

    $('#descuploadimage>div').each(function (i, item) {
        var $this = $(item);
        i++;
        var $btn = $this.find("#descimage" + i);
        var $img = $this.find("#descimagequeue" + i);
        productedit.baseupload($btn, "descimagequeue" + i, "/product/uploadotherimage", { productid: productid, index: i }, function (file, data, response) {
            var $f = $('#' + file.id);
            data = $.parseJSON(data);
            if (!data.status) {
                $f.find("span").text("上传失败!");
                return false;
            }
            $f.find("span").replaceWith("<img src='" + data.data + "' />");
            $f.parent().removeClass('hide');

        }, function () {
            var $this = $(this);
            jx.del(function () {
                productedit.delimage($this, productid, 2, i);
            });
            return false;
        });
    });

    //显示/隐藏
    $('.every-one-info').on('mouseover mouseout', '.informa-imgWrap', function (event) {
        var $this = $(this).find(".closeBtn");
        if ($this.length == 0) return false;
        switch (event.type) {
            case "mouseover": $this.show(); break;
            case "mouseout": $this.hide(); break;
        }
    });

    //添加商品图
    var desccount = 0;
    $('.addImg-btn').on('click', function () {
        var $this = $(this);
        var child = $this.parent().next().children();
        var count = child.length;
        var $new = child.last().clone();
        desccount = parseInt($new.find(".informa-imgWrap").attr('id').replace("descimagequeue", ""));
        desccount++;
        if (desccount > 20)
            return false;
        var html = '<div class="operAdverW clear"><div class="InformaUpload clear"><span class="fl">详情图{0}：</span><div class="resetUpBtn fl"><span id="descimage{1}"><img src="/content/images/upImg.png" alt=""></span></div><span class="imgInfo fl">大小：1024PX*1024PX</span></div><div class="informa-imgWrap hide" id="descimagequeue{1}"></div></div>';
        html = html.format(desccount, desccount, desccount);
        var $html = $(html);
        child.last().after($html);
        var ttop = $this.offset().top;
        var top = $html.offset().top;
        var height = $(window).height();
        if ((top - ttop) > 600) {
            $('html,body').animate({ scrollTop: top - height + 80 });
        }
        var $btn = $html.find("#descimage" + desccount);
        productedit.baseupload($btn, "descimagequeue" + desccount, "/product/uploadotherimage", { productid: productid, index: desccount }, function (file, data, response) {
            var $f = $('#' + file.id);
            data = $.parseJSON(data);
            if (!data.status) {
                $f.find("span").text("上传失败!");
                return false;
            }
            $f.find("span").replaceWith("<img src='" + data.data + "' />");
            $f.parent().removeClass('hide');
        }, function () {
            var $this = $(this);
            jx.del(function () {
                productedit.delimage($this, productid, 2, i);
            });
            return false;
        });

        return false;
    });
}

productedit.baseupload = function ($btn, id, url, data, fun, delfun) {
    //默认参数
    var def = {
        "height": 24,
        'width': 80,
        'swf': '/js/uploadify3.2.1/uploadify.swf?v=' + new Date().getMilliseconds(),
        'fileSizeLimit': "1024KB",
        'method': "post",
        'fileTypeDesc': '只允许上传gif,jpg,jpeg,png图片',
        'fileTypeExts': '*.gif; *.jpg; *.png',
        "removeCompleted": false,
        'wmode': 'transparent',
        'preventCaching': false,
        'auto': true,
        'overrideEvents': ['onUploadProgress'],
        'multi': false,
        'buttonText': '上传图片'
    };
    var $content = $btn.parent().parent().next();
    var pro = {
        'formData': data,
        'uploader': url,
        'queueID': id,
        'itemTemplate': '<div  id="${fileID}" class="upImage"><span>上传中</span><a href="#" class="closeBtn" style="display:none;">X</a></div>',
        'onSelect': function () {
            if ($content.children().length > 1)
                $content.find(".upImage").eq(0).remove();
        },
        "onUploadSuccess": function (file, data, response) {
            fun(file, data, response);
        },
        'onUploadError': function (a, b, c, d) {
        }
    }
    $content.on('click', 'a', delfun);
    $.extend(pro, def);
    $btn.uploadify(pro);
}
productedit.delimage = function ($this, productid, t, index) {
    $.ajax({
        type: "POST",
        url: "/product/delimage",
        data: { productid: productid, t: t, index: index },
        dataType: "json",
        success: function (data) {
            if (data.status) {
                $this.parent().empty().parent().addClass('hide');
            } else {
                jx.error("删除失败了!");
            }
        }
    });
}
productedit.audit = function () {
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
                            location.reload();
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

var productadd = {};
productadd.init = function () {
    productedit.common();
    productadd.operate();
    $.get("/product/getmanufacturer", function (result) {
        var datas = result.data;
        var $m = $('input[name=Manufacturer]');
        var $maddrss = $('input[name=ManufacturerAddress]');
        var $hidden_m = $m.prev();
        $m.autocomplete(datas, {
            formatItem: function (row, i, max) {
                return "<table width='400px'><tr><td align='left'>" + row.mname + "</td><td align='right'></td></tr></table>";
            },
            formatMatch: function (row, i, max) {
                return row.mname.toString();
            }
        }).result(function (event, data, formatted) {
            $hidden_m.val(data.mid);
            $maddrss.val(data.maddress);
        });
    });

    var $suppliername = $('#SupplierName');
    $('#SupplierID').change(function () {
        $suppliername.val($(this).find("option:selected").text());
    });
}
productadd.operate = function () {
    //添加信息到数据
    var productcodes = [];
    var count = 0;
    var $hidden_count = $('input[name=hidden_count]');
    var $save = $('a[name=save]');
    var $guige = $('input[name=guige]');
    var $color = $('input[name=color]');
    var $size = $('input[name=size]');
    var $dushu = $('input[name=dushu]');
    var $productcode = $('input[name=productcode]');
    var $barcode = $('input[name=barcode]');
    var $cadn = $('input[name=CADN]');
    var $chinesename = $('input[name=TradeName]');

    var $table = $('#tablelist');
    var $form = $('#formproduct');
    var $submit = $('a[name=submit]');
    $submit.on('click', function () { $form.submit(); return false; });
    $form.validate({
        rules: { BrandID: { required: true }, SupplierID: { required: true } },
        messages: { BrandID: { required: "请选择品牌" }, SupplierID: { required: "请选择供应商" } },
        ignore: ":hidden:not('#BrandID,#SupplierID,#ManufacturerID')",
        errorPlacement: function (error, element) { error.insertAfter(element); },
        submitHandler: function () {
            if ($(".ptype :checked").length > 0) {

            }
            else {
                jx.error("请选择分类!");
                return false;
            }

            if ($submit.attr("isajax") == "1") {
                return false;
            }
            $submit.attr("isajax", "1").text("提交中...");
            $.ajax({
                type: "POST", url: "/product/addproduct", data: $form.serialize(), dataType: "json", success: function (result) {
                    if (result.status) {
                        var $next = $submit.next();
                        $next.removeClass("hide");
                        $table.find("tbody").empty().html("<tr class='empty'><td colspan='11' align=center>无</td></tr>")
                        $hidden_count.val(0);
                        count = 0;
                        productcodes = [];
                        productadd.submitSuccess(result.data);
                        $(':input', $form).not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');
                        setTimeout(function () { $next.addClass("hide"); }, 2000);
                    } else {
                        jx.error("编辑失败!");
                    }
                    $submit.attr("isajax", "0").text("提交");
                }
            });
            return false;
        }
    });

    var count = parseInt($hidden_count.val());
    var codes = [];
    var isedit = false;
    var editproductid = 0;
    var editindex = 0;
    $table.on('click', "a[name=edit]", function () {
        isedit = true;
        var $this = $(this);
        editindex = parseInt($this.parent().attr("index"));
        editindex = isNaN(editindex) ? 1 : editindex;
        var arr = $this.parent().find("input").val().split('|');
        editproductid = parseInt(arr[0]);
        $productcode.val(arr[1]);
        $barcode.val(arr[2]);
        $guige.val(arr[3]);
        $color.val(arr[4]);
        $size.val(arr[5]);
        $dushu.val(arr[6]);
        $save.text('编辑');
        return false;
    });
    $save.on('click', function () {
        var productcode = $productcode.val();
        if (productcode == '' && codes.indexOf(productcode) == -1) {
            jx.error("您需要填写正确的金象码!");
            return false;
        }
        $.post("/product/checkproductcode", { productcode: productcode }, function (result) {
            if (!result.status) {
                if (isedit) {
                    var $td = $('td[index=' + editindex + ']');
                    var $hidden = $td.find("input");
                    $hidden.val(temp.format(editproductid, productcode, $barcode.val(), $guige.val(), $color.val(), $size.val(), $dushu.val()));
                    var $tds = $td.parent().find("td");
                    $tds.eq(2).text(productcode);
                    $tds.eq(3).text($chinesename.val());
                    $tds.eq(4).text($cadn.val());
                    $tds.eq(5).text($barcode.val());
                    $tds.eq(6).text($guige.val());
                    $tds.eq(7).text($color.val());
                    $tds.eq(8).text($size.val());
                    $tds.eq(9).text($dushu.val());
                    isedit = false;
                    editproductid = 0;
                    $save.text("添加");
                    cleartextbox();
                }
                else {
                    codes.push(productcode);
                    $table.find(".empty").remove();
                    var index = parseInt($table.find("tr:last").find("td:first").text());
                    index = isNaN(index) ? 0 : index;
                    var str = '<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td index="' + index + '"><a href="#" name="edit">编辑</a><a href="#" name="del" class="ml-10">删除</a><input type="hidden" name="newproduct{10}" value="{11}"></td></tr>';
                    str = str.format(++index, "", productcode, $chinesename.val(), $cadn.val(), $barcode.val(), $guige.val(), $color.val(), $size.val(), $dushu.val(),
                        index,
                        temp.format(0, productcode, $barcode.val(), $guige.val(), $color.val(), $size.val(), $dushu.val())
                        );
                    $table.find("tbody").append(str);
                    cleartextbox();
                    $hidden_count.val(index);
                }
            } else {
                jx.error("金象码已经存在!");
            }
        });
        return false;
    });
    $table.on('click', "a[name=del]", function () {
        var $this = $(this);
        jx.confirm("确定要删除该行吗?", function () {
            $this.parent().parent().remove();
        });
        return false;
    });
    function cleartextbox() {
        $productcode.val('');
        $guige.val('');
        $color.val('');
        $size.val('');
        $dushu.val('');
        $barcode.val('');
    }
}

productadd.submitSuccess = function (data) {
    var $tableresult = $('#succtable');
    if (data.length > 0) {
        $tableresult.removeClass("hide");
    }
    var temp = '<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>';
    var str = [];
    for (var i = 0; i < data.length; i++) {
        str.push(temp.format(i + 1, data[i].ProductID, data[i].ProductCode, data[i].TradeName, data[i].CADN, data[i].Specifications, data[i].Manufacter, data[i].SupplierName));
    }
    $tableresult.find("tbody").html(str.join(''));

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
            case 3: //满减
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写减免金额：").end().find(".discountunit").text("元");
                break;
            case 4: //优惠劵
                pa.$actcoupon.show();
                break;
            case 5: //换购 价格 赠品
                pa.$actproductgift.show();
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写换购价格：").end().find(".discountunit").text("元");
                break;
            case 6: //满折
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写折扣：").end().find(".discountunit").text("折");
                break;
            case 7: //直降
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写优惠金额：").end().find(".discountunit").text("元");
                break;
            case 8: //折扣
                pa.$actdiscount.show().find(".discountmsg").text("* 请填写折扣：").end().find(".discountunit").text("折");
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
        productactivity.$actproductgift.find("input").on("change blur", function () {

            var $product = $(this);
            var productid = parseInt($product.val());
            if (productid == $product.attr("productid")) {
                return false;
            }
            var $msg = $product.parent().next().html("");
            if (!isNaN(productid) && productid > 0) {
                t = setTimeout(function () {
                    $product.attr("productid", productid);
                    $product.attr("cname", '');
                    product.get(productid, function (data) {
                        $product.parent().next().html("商品名称：<a href='http://www.jxdyf.com/product/" + data.ProductID + ".html' target='_blank'>" + data.ChineseName + "</a>");
                        $product.attr("cname", data.ChineseName);
                        productactivity.validater();
                        clearTimeout(t);
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
                if (giftid > 0 && giftname.length > 0) {
                    if (price > 0)
                        msg = "满" + price + "元，可加价换购商品1件";
                    else if (quantity > 0)
                        msg = "满" + quantity + unit + "，可加价换购商品1件";
                }
                break;
            case 6:
                if (discount > 0) {
                    if (price > 0)
                        msg = "满" + price + "元，即可享受" + discount + "折优惠";
                    else if (quantity > 0)
                        msg = "满" + quantity + unit + "，即可享受" + discount + "折优惠";
                    break;
                }
            case 7:
                msg = "直降" + $discount.val() + "元，全网最低";
                break;
            case 8:
                if (discount > 0 && discount < 10)
                    msg = discount + "折优惠 全网最低";
                break;
            default:
                return true;
        }
        $actdesc.text(msg);
        return msg.length > 0;
    }
}

productactivity.successedit = function (data) {
    if (data.status) {
        jx.alert("编辑成功", function () {
            jx.closeAll();
        });
    } else {
        jx.error("编辑失败");
    }
}

var brand = {};
brand.edit = function () {
    var brandid = 0;

};