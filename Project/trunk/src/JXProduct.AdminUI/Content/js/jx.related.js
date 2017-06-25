/// <reference path="jquery-2.1.3.min.js" >
var related = {};
related.getproduct = function (productid, fun) {
    $.post("/product/get", { productid: productid }, function (result) {
        fun(result);
    });
};
related.init = function () {
    //当前商品数据
    related.productid = parseInt($('#ProductID').val());
    related.productcode = $('#ProductCode').val();
    related.chinesename = $('#ChineseName').val();
    related.unit = $('#Unit').val();
    related.spec = $('#Spec').val();
    related.price = parseFloat($('#Price').val());

    //启用功能
    related.dabaozhuang();
    related.zuhe();
    related.tuijian();

    //切换
    $('.header-comm-table a').on('click', function () {
        $('.header-comm-table a').removeClass('active');
        $(this).addClass('active');
        $('.setCommConterTab').hide().eq($(this).index()).show();
        return false;
    });
};

related.dabaozhuang = function () {
    var $formdbz = $('#formdbz');
    var $table = $formdbz.find(".table");
    var $save = $formdbz.find("a[name=save]");

    //文本框
    var unit = $formdbz.find("span[name=unit]").text();
    var $quan = $formdbz.find("input[name=quantity]");
    var $price = $formdbz.find("input[name=price]");
    var $name = $formdbz.find("input[name=viewname]");
    var $spec = $formdbz.find("input[name=spec]");
    var $chinesename = $('#ChineseName');
    $quan.add($price).on('keyup blur change', function () {
        var str = '';
        var name = $chinesename.val();
        var q = $quan.val();
        var p = $price.val();
        if (q > 0 && p > 0)
            str = name + q + unit + '装(每' + unit + '仅需￥' + (p / q).toFixed(2) + ')';
        $name.val(str);
    })
    //id
    var $hidden_relatedid = $formdbz.find("input[name=relatedid]");
    //提交
    var issubmit = 0;
    $formdbz.validate({
        errorPlacement: function (error, element) { error.insertAfter(element); },
        submitHandler: function () {
            $save.next().removeClass('hide');
            issubmit = 1;
            $.ajax({
                type: "POST", url: "/product/related_largepacking", data: $formdbz.serialize(), dataType: "json", success: function (result) {
                    if (result.status) {
                        setInterval(function () {
                            $save.next().addClass("hide");
                        }, 2000);
                        $formdbz.find(".table-responsive").load('/product/related?isajax=1&type=1&productid=' + related.productid);
                    } else {
                        jx.error("编辑失败!");
                    }
                    resetEdit();
                    issubmit = 0;
                }
            });
            return false;
        }
    });
    $save.on('click', function () {
        if (issubmit == 1) {
            jx.alert("正在处理...");
            return false;
        }
        $formdbz.submit();
        return false;
    });

    //删除
    $formdbz.on("click", ".table a[name=del]", function () {
        var $parent = $(this).parent();
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

    //修改
    var editindex = 0;
    $formdbz.on("click", ".table a[name=edit]", function () {
        var $this = $(this);
        var $parent = $this.parent();
        var $tds = $parent.parent().children();

        //加载编辑数据
        $save.text("编辑");
        editindex = $this.parent().attr('index');
        $hidden_relatedid.val($parent.attr("relatedid"));

        $quan.val($tds.eq(4).text());
        $price.val($tds.eq(5).find("span").text());
        $name.val($tds.eq(1).text());
        $spec.val($tds.eq(6).text());
        return false;
    });

    function resetEdit() {
        $hidden_relatedid.val('0');
        $formdbz.find('input:reset').trigger('click');
        editindex = 0;
        $save.text("保存");
    }
};
related.zuhe = function () {
    var defaulttr = '<tr><td>{0}</td><td>{1}</td><td class="tcName">{2}</td><td>{3}</td><td class="yeMian">{4}</td><td>{5}</td><td>{6}</td><td class="cz"><a href="#" name="edit">编辑</a><a href="#" name="del" class="ml-10">删除</a></td></tr>'.format(related.productid, related.productcode, related.chinesename, related.spec, related.chinesename, related.price, 1);
    var $zuhe = $("#formzuhe");
    var productid = related.productid;
    var productname = related.chinesename;
    var unit = related.unit;

    var $temptab = $zuhe.find('.temptable');
    var $combotable = $('.resulttable');
    var $childid = $zuhe.find("input[name=childid]");
    var $childprice = $zuhe.find("input[name=childprice]");
    var $childcount = $zuhe.find("input[name=childcount]");
    var $viewname = $zuhe.find('input[name=viewname]');
    var $comboname = $zuhe.find('input[name=comboname]');

    var $spec = $zuhe.find('input[name=combospec]');
    var $price = $zuhe.find('input[name=comboprice]');
    var $save = $zuhe.find("a[name=save]");
    var $submit = $zuhe.find("a[name=submitcombo]");

    //设置库存
    setProductStock();

    var t = 0;
    var $showinfo = $('#showinfo');
    $childid.blur(function () {
        $showinfo = $('#showinfo');
        if ($showinfo.length == 0) {
            $showinfo = $('<div class="spInputWrap clear" id="showinfo"></div>');
            $childid.parent().after($showinfo)
        }
        if ($showinfo.length > 0)
            $showinfo.empty();
        var $this = $(this);
        var value = $this.val();
        if ($this.attr("old") == value) {
            return false;
        }
        clearTimeout(t);
        var $next = $viewname.next();
        t = setTimeout(function () {
            $this.attr("old", value);
            related.getproduct(value, function (data) {
                if (data.status) {
                    var p = data.data;
                    $this.attr("pass", "1");
                    $next.attr({ "productcode": p.ProductCode, "chinesename": p.ChineseName, 'spec': p.Specifications });
                    $showinfo.html('<span class="width-150">&nbsp;</span>商品规格:<font class="red">{0}</font>  库存:<font class="red">{1}</font> 成本价:<font class="red">{2}</font> 金象价:<font class="red">{3}</font>'.f(p.Specifications, p.ProductStock / p.LargePacking, p.CostPrice, p.TradePrice));
                    $next.html("<a productcode='" + p.ProductCode + "' spec='" + p.Specifications + "' href='http://www.jxdyf.com/product/" + p.ProductID + ".html'>" + p.ChineseName + "</a>  规格:" + p.Specifications);
                    $viewname.val(p.ChineseName + "(" + p.Specifications + ")");
                }
                else {
                    $next.html("没有找到该商品ID");
                }
            });
        }, 500);
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
    $submit.on("click", function () {
        var mainproductid = related.productid;
        var comboname = $comboname.val();
        var combospec = $spec.val();
        var comboprice = $price.val();
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
            data.push("<quantity>" + tds.eq(6).text() + "</quantity>");
            data.push("</item>");
        });
        data.push("</items>");
        //提交组合,并且生成数据  updatecombo_productid 在编辑时赋值
        if (updatecombo_productid == 0) {
            $.post("/product/combo", { mainproductid: mainproductid, comboname: comboname, combospec: combospec, comboprice: comboprice, data: encodeURIComponent(data.join('')) },
                function (result) {
                    if (result.status) {
                        $submit.next().removeClass("hide");
                        setTimeout(function () { $submit.next().addClass("hide"); }, 2000);
                        $combotable.find("table:last").load("/product/combo?productid=" + mainproductid);
                    }
                    else {
                        jx.alert("失败!" + result.msg);
                    }
                    setProductStock();
                });
        }
        else {
            //修改
            $.post("/product/combo", { productid: updatecombo_productid, comboname: comboname, combospec: combospec, comboprice: comboprice, data: encodeURIComponent(data.join('')) },
                function (result) {
                    if (result.status) {
                        var $gou = $("<p class='gou'>保存成功</p>")
                        $temptab.find("a.cancel").after($gou).hide();
                        setTimeout(function () { $gou.remove(); }, 2000);
                        $combotable.find("table:last").load("/product/combo?productid=" + mainproductid);
                    } else {
                        jx.alert("失败!" + result.msg);
                    }
                    setProductStock();
                });
        }
        //重置信息
        $temptab.find("tbody").html(defaulttr);
        $comboname.val('');
        $price.val('');
        $spec.val('');
        $showinfo.html('');
        return false;
    });

    //删除套餐  判断是否是修改,是修改取消修改状态
    $combotable.on("click", "a[name=del]", function () {
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
    //删除一行
    $zuhe.on("click", "a[name=delrow]", function () {
        var $this = $(this);
        jx.del(function () {
            $this.parent().parent().remove();
        });
        return false;
    });
    //修改套餐
    $combotable.on("click", "a[name=edit]", function () {
        var $this = $(this);
        var productid = $this.parent().attr("productid");
        $.post("/product/ComboDetail", { productid: productid }, function (result) {
            if (result.status) {
                var html = [];
                for (i = 0; i < result.data.length; i++) {
                    var item = result.data[i];
                    html.push('<tr><td>' + item.ComboProductID + '</td><td>' + item.ComboProductCode + '</td><td class="tcName">' + item.ComboProductName + '</td><td>' + item.Spec + '</td><td>' + item.Name + '</td><td>' + item.Price + '</td><td quantity="' + item.Quantity + '">' + item.Quantity + '</td><td><a href="#" name="edit">编辑</a>' + (item.IsMain ? "" : '<a href="#" name="delrow" class="ml-10">删除</a>') + '</td></tr>');
                }
                $temptab.find("tbody").html(html.join(''));
                $temptab.find("a.cancel").show();

                //set update status
                updatecombo_productid = productid;
                isupdate = 0;
                //set name spec price
                var $tds = $this.parent().parent().children();
                var name = $tds.eq(1).text();
                var spec = $tds.eq(5).text();
                var price = $tds.eq(4).find("span").text();
                $comboname.val(name);
                $spec.val(spec);
                $price.val(price);
            }
        });
        return false;
    });

    //修改子商品
    $zuhe.on("click", "a[name=edit]", function () {
        var $this = $(this);
        if ($this.attr("isupdate") == "1") {
            var tds = $this.parent().parent().children();
            var viewname = tds.eq(4).find("input").val();
            var price = parseFloat(tds.eq(5).find('input').val());
            var count = parseInt(tds.eq(6).find("input").val());
            if (viewname.length == 0 || isNaN(price) || isNaN(count) || price <= 0 || count <= 0) {
                jx.alert('请确认参数!');
                return false;
            }
            tds.eq(4).html(viewname);
            tds.eq(5).html(price);
            tds.eq(6).html(count);
            $this.attr("isupdate", "0");
            isupdate--;
            $this.next().show();
            $this.text("编辑")
            setZuHeName();
        }
        else {
            $this.attr("isupdate", "1");
            var tds = $this.parent().parent().children();
            tds.eq(4).html("<input type='text' class='a' value='" + tds.eq(4).html() + "' style='width:260px;' />");
            tds.eq(5).html("<input type='text' class='c' value='" + tds.eq(5).text() + "' />");
            tds.eq(6).html("<input type='text' class='c' value='" + tds.eq(6).text() + "' />");
            $this.next().hide()
            $this.text("保存");
            isupdate++;
        }
        return false;
    });

    //取消修改
    $zuhe.find("a.cancel").click(function () {
        $temptab.find("tbody").html(defaulttr);
        updatecombo_productid = 0;
        $(this).hide();
        return false;
    });

    function setViewTemp() {
        var price = $childprice.val();
        var count = $childcount.val();
        var productname = related.chinesename;
        var $comminfo = $viewname.next();
        var childname = $comminfo.attr('chinesename');
        var childcode = $comminfo.attr('productcode');
        var childspec = $comminfo.attr('spec');
        var html = '<tr><td>' + $childid.val() + '</td><td>' + childcode + '</td><td class="tcName">' + childname + '</td><td>' + childspec + '</td><td class="yeMian">' + $viewname.val() + '</td><td>' + price + '</td><td quantity="' + count + '">' + count + '</td><td><a href="#" name="edit">编辑</a><a href="#" name="del" class="ml-10">删除</a></td></tr>';
        html = $(html).find("a[name=del]").click(function () {
            $(this).parent().parent().remove(); return false;
        }).end();
        $temptab.find('tbody').append(html);
        $childid.val('').next().html('');
        $childcount.val('');
        $childprice.val('');
        $viewname.val('');

        setZuHeName();
    }
    function setZuHeName() {
        var $tds;
        var str = [];
        var totalprice = 0;
        $temptab.find("tbody>tr").each(function (i, item) {
            $tds = $(item).children();
            str.push($tds.eq(4).html());// + tds.eq(6).html()
             
            var quantity = parseInt($tds.eq(6).text());
            var price = parseFloat($tds.eq(5).text());
            if(!isNaN(quantity) && !isNaN(price))
            totalprice += price * quantity;
        });
        $comboname.val(str.join('+'));
        $price.val(totalprice);
    }
    function setProductStock() {
        var ids = [];
        $('.resulttable>table>tbody>tr').each(function (i, item) {
            ids.push($(item).children().eq(2).text());
        });
        $.ajax({
            type: "POST",
            url: "/product/getcombostock",
            data: { productIDS: ids.join(',') },
            dataType: "json",
            success: function (result) {
                if (result.status && result.data.length > 0) {
                    $.each(result.data, function (i, item) {
                        debugger;
                        var t = $('td[productid=' + item.productid + ']').prev();
                        t.text(item.stock);
                    });
                }
            }
        });
    }
}
related.tuijian = function () {
    var productid = related.productid;
    var $tuijian = $('#tuijian');
    var $save = $tuijian.find("a[name=save]");
    // 保存
    $save.on("click", function () {
        var $this = $(this);
        var productIDs = [];
        var $ids = $tuijian.find("input[name=tuijianid]");
        $ids.each(function (i, item) {
            var $item = $(item);
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
                        $save.next().removeClass("hide");
                        setTimeout(function () { $save.next().addClass("hide"); }, 2000);
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
    $tuijian.on("click", "a[name=del]", function () {
        var $this = $(this);
        var relatedid = $this.attr("relatedid");
        jx.del(function () {
            $.post("/product/related_del", { relatedid: relatedid }, function (result) {
                if (result.status) {
                    jx.alert("删除推荐商品成功!");
                    $this.parent().parent().remove();
                    setview();
                }
            });
        });
        return false;
    });

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
                related.getproduct(value, function (data) {
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

    setview();
    function setview() {
        var $tuijianid = $('input[name=tuijianid]');
        $tuijianid.val('').next().text('');
        $tuijian.find("table>tbody>tr").each(function (i, item) {
            var pid = $(item).children().eq(1).text();
            var name = $(item).children().eq(2).text();
            $tuijianid.eq(i).val(pid).next().text(name);
        });
    }
}