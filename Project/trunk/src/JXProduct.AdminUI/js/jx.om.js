﻿/// <reference path="jx.om.js" />
//start om ajax
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
//end om  ajax
var om =
{
    //start notice
    noticeListInit: function () { om.listInit(); }
    , noticeAddInit: function () { om.noticeAddOrNoticeEidtInit(); }
    , noticeEidtInit: function () { om.noticeAddOrNoticeEidtInit(); }
    , noticeAddSubmit: function () { om.noticeAddSubmit(); }
    , noticeModifySubmit: function () { om.noticeModifySubmit(); }
    //end notice

    //start FeedBackList
    , feedBackInit: function () { om.feedBackInit(); }
    , feedBackSubmit: function () { om.feedBackSubmit(); }
    //end FeedBackList

    //start roleMessages
    , roleMessageInit: function () { om.roleMessageInit(); }
    //end roleMessages

    //start promotion
    , promotionInit: function () { om.promotionInit(); }
    //end promotion
}
om.listInit = function () {
    $(".noticAddBtn").children().click(function () {
        jx.open("/OM/NoticeAdd", null, 450, 500);
    });
    $(".caozuo").on("click", "a", function () {
        var className = $(this).attr("class");
        var newsID = $(this).parent().attr("id");
        var data = { newsID: newsID }
        var url = "/OM/";
        if (className == "zd") {
            var zdStr = $(this).html();
            var stick;
            var stickMsg;
            if (zdStr == "置顶") {
                var stick = 1;
                stickMsg = "取消置顶";
            } else {
                var stick = 0;
                stickMsg = "置顶";
            }
            url += "UpdateStick";
            data = {
                newsID: newsID,
                stick: stick
            }
            defaultAjax(url, data, defaultCallback);
            if (_isSuccess == true) {
                $(this).html(stickMsg);
                parent.layer.msg(_defaultData.msg, { time: 1000 });
            }
        }
        else if (className == "th") {
            var thStr = $(this).html();
            var red;
            var redMsg;
            if (thStr == "套红") {
                var red = 1;
                redMsg = "取消套红";
            } else {
                var red = 0;
                redMsg = "套红";
            }
            url += "UpdateRed";
            data = {
                newsID: newsID,
                red: red
            }
            defaultAjax(url, data, defaultCallback);
            if (_isSuccess == true) {
                if (red == 1) {
                    $(this).parent().prev().prev().children().css('color', 'red');
                } else {
                    $(this).parent().prev().prev().children().css('color', 'black');;
                }
                $(this).html(redMsg);
                parent.layer.msg(_defaultData.msg, { time: 1000 });
            }
        }
        else if (className == "sc") {
            url += "NoticeRemove";
            var o = parent.layer.confirm('确定要删除公告吗？', { icon: 1 }, function () {
                defaultAjax(url, data, defaultCallback);
                parent.layer.msg(_defaultData.msg, { time: 1000 }, function () {
                    window.location.reload();
                });
            });
        }
        else if (className == "bj") {
            jx.open("/OM/NoticeEidt?newsID=" + newsID, null, 450, 500);
        }
    });
}
om.noticeAddOrNoticeEidtInit = function () {
    var evalType = $("#evalType").val();
    $('.selecWrap').select1(evalType, function (key, value) {
        $("#evalType").val(key);
        if (key == "2") {
            $("#showLine").html("链接：");
            $("#TransType").html($("<input id='Content' name='Content' class='addContent' placeholder='请输入链接' value='' type='text' />"));
        } else {
            $("#showLine").html("内容：");
            $("#TransType").html($("<textarea cols='20' id='Content' name='Content' rows='2' placeholder='请输入内容'></textarea>"));
        }
    });
}
om.noticeAddSubmit = function () {
    var titleStr = $('#Title').val();
    var contentStr = $('#Content').val();
    var evalType = $("#evalType").val();
    if (titleStr == null || titleStr == "") {
        jx.alert("请完成标题填写！");
        return;
    } else if (contentStr == null || contentStr == "") {
        jx.alert("请完成内容填写！");
        return;
    } else if (evalType == 2 && contentStr.toLowerCase().indexOf('http://') < 0) {
        jx.alert("请正确添写链接URL！");
        return;
    }
    var addInfo = {
        type: evalType,
        title: titleStr,
        content: contentStr
    };
    $.ajax({
        url: '/OM/NoticeAddInfo',
        type: 'POST',
        data: addInfo,
        async: false,
        error: function () {
            jx.alert('服务器访问失败，请查看网络是否畅通！');
        },
        success: function (data) {
            parent.layer.msg(data.msg, { time: 1500 }, function () {
                top.document.location.reload();
            });
        }
    });
}
om.noticeModifySubmit = function () {
    var titleStr = $('#Title').val();
    var contentStr = $('#Content').val();
    var evalType = $("#evalType").val();
    var newsID = $("#newsID").val();
    if (titleStr == null || titleStr == "") {
        jx.alert("请完成标题填写！");
        return;
    } else if (contentStr == null || contentStr == "") {
        jx.alert("请完成内容填写！");
        return;
    } else if (evalType == 2 && contentStr.toLowerCase().indexOf('http://') < 0) {
        jx.alert("请正确添写链接URL！");
        return;
    }
    var modifyInfo = {
        newsID: newsID,
        type: evalType,
        title: titleStr,
        content: contentStr
    };
    $.ajax({
        url: '/OM/ModifyNotice',
        type: 'POST',
        data: modifyInfo,
        async: false,
        error: function () {
            jx.alert('服务器访问失败，请查看网络是否畅通！');
        },
        success: function (data) {

            parent.layer.msg(data.msg, { time: 1500 }, function () {
                top.document.location.reload();
            });
        }
    });
}
om.feedBackInit = function () {
    var statusTypeKey = $("#statusTypeKey").val();
    var sourceTypeKey = $("#sourceTypeKey").val();
    $('#statusType').select1(statusTypeKey, function (key, value) {
        statusTypeKey = key;
    });
    $('#sourceType').select1(sourceTypeKey, function (key, value) {
        sourceTypeKey = key;
    });
    $(".addNoticeTab1").on("click", "a", function () {
        $(" #addTemplate").remove();
        
        var aId = $(this).attr("Id");
        var trid = $(this).parent().parent().attr("id");
        if (aId.indexOf('cc_') != -1 || aId.indexOf('hf_') != -1) {
            var addTemplate = $("<tr id='addTemplate'><td colspan='8'></td></tr>");
            var atid = $("#addTemplate").attr("id");
            var rightHeight = 500;
            if (atid == undefined) {
                var data = { feedBackId: aId.split('_')[1] };
                parent.layer.msg("正在加载反馈...", { time: 1000 });
                defaultAjax("/OM/FeedBackReplyDetail", data, defaultCallback);
                if (_defaultData.status == false) {
                    parent.layer.msg(_defaultData.msg, { time: 1000 });
                    $(" #addTemplate").remove();
                    $(" .sid_top6").css("margin-top", "120px");
                    $(" .right1").css("height", "1157px");
                    return;
                }

                //加载tr
                $(" #dataTemplate #" + trid).after(addTemplate);
                //加载tr 下的 div
                var tboyDiv = "<div class='tboy' id='tboy'>";
                tboyDiv += "<div class='tboy_1'>";
                var bfInfo = $.parseJSON(_defaultData.data);
                var userName = bfInfo.userName;
                var isDisabled = "";
                var placeholder = "回复内容...！";
                if (bfInfo.userName == null) {
                    userName = "未填写用户名";
                }
                tboyDiv += "  <p>◆ 用户" + userName + "的意见反馈：</p>";
                tboyDiv += "  <dl>  ";
                tboyDiv += "     <dt><img src='" + bfInfo.image + "' style='width:60px; height:60px;'></dt> ";
                tboyDiv += "     <dd>";
                tboyDiv += "         <b>用户名：" + userName + "<i>类型：" + bfInfo.typeName + "</i></b> ";
                tboyDiv += "           <u>" + bfInfo.contents + "</u>";
                tboyDiv += "             <span>" + bfInfo.createTime + "</span>";
                tboyDiv += "     </dd>";
                tboyDiv += "   </dl>";

                if (bfInfo.replyUserName != undefined) {
                    placeholder = "已对 用户 " + userName + " 完成了回复 ！";
                    isDisabled = " disabled='disabled' ";
                    tboyDiv += "  <dl id='replyUserName'>  ";
                    tboyDiv += "     <dt><img src='" + bfInfo.replyImage + "'  style='width:60px; height:60px;'></dt> ";
                    tboyDiv += "     <dd>";
                    tboyDiv += "         <b>客服：" + bfInfo.replyUserName + "</i></b> ";
                    tboyDiv += "           <u>" + bfInfo.replyContents + "</u>  ";
                    tboyDiv += "             <span>" + bfInfo.replyTime + "</span>";
                    tboyDiv += "     </dd>";
                    tboyDiv += "   </dl>";
                }

                tboyDiv += "</div>";
                tboyDiv += "<div class='tboy_feedBack'>";
                tboyDiv += "   <div>";
                tboyDiv += "      <textarea cols='20' id='Content_feedBack' name='Content_feedBack' rows='2' " + isDisabled + " placeholder='" + placeholder + "'></textarea>  ";
                tboyDiv += "       <input type='hidden' id='SelectedFBID' value=" + aId.split('_')[1] + ">";
                tboyDiv += "   </div>";
                tboyDiv += "   <button class='tboy21_feedBack' Id='FeedBackSubmit'" + isDisabled + " onClick='om.feedBackSubmit()'>提交</button>";
                tboyDiv += "   <button class='tboy21_feedBack' onClick='om.closeTemplate()'>收起评论</button>";
                tboyDiv += "</div>";
                tboyDiv += "</div>";
                addTemplate.children().append($(tboyDiv));
                $(" .sid_top6").css("margin-top", "1000px");
                $(" .right1").height(915+ rightHeight);
                if (aId.indexOf('hf_') != -1) {
                    var heiDiv = $("#Content_feedBack").offset().top;
                    $("html,body").animate({ scrollTop: $("#Content_feedBack").offset().top - 250 }, 500);
                }
            }
            else {
                $(" #addTemplate").remove();
                $(" .right1").css("height", "915px");
                $(" .sid_top6").css("margin-top", "120px");
            }

        }
        else if (aId.indexOf('sc_') != -1) {
            var feedBackId = aId.split('_')[1];
            var url = "/OM/FeedBackRemove";
            var data = { feedBackId: feedBackId }
            defaultAjax(url, data, defaultCallback);
            var o = parent.layer.confirm('确定要删除此反馈信息吗？', { icon: 1 }, function () {
                defaultAjax(url, data, defaultCallback);
                if (_isSuccess == true) {
                    parent.layer.msg(_defaultData.msg, { time: 1000 }, function () {
                        window.location.reload();
                    });
                }
            });
        }
    });
    $(".btn_feedBack").click(function () {
        var sTime = $("#sTime").val();
        var eTime = $("#eTime").val();
        if (sTime != "" && eTime != "") {
            if (sTime > eTime) {
                jx.alert("日期开始时间必须小于结束时间！");
                return;
            }
        }
        if (sTime == "") {
            sTime = "null";
        }
        if (eTime == "") {
            eTime = "null";
        }
        var url = "/OM/FeedBackList?statusType=" + statusTypeKey;
        url += "&sourceType=" + sourceTypeKey;
        url += "&sTime=" + sTime;
        url += "&eTime=" + eTime;
        window.location.href = url;
    });
}
om.feedBackSubmit = function () {
    var content = $('#Content_feedBack').val();
    var feedBackId = $('#SelectedFBID').val();
    var data = {
        feedBackId: feedBackId
       , replyContent: content
    };
    parent.layer.msg("正在提交回复", { time: 1000 });
    defaultAjax("/OM/FeedBackReply", data, defaultCallback);
    parent.layer.msg(_defaultData.msg, { time: 1000 });
    if (_isSuccess == true && _defaultData.data != undefined) {
        var bfInfo = $.parseJSON(_defaultData.data);
        var tboyDiv = "  <dl>  ";
        tboyDiv += "     <dt><img src='/image/pic4.jpg'></dt> ";
        tboyDiv += "     <dd>";
        tboyDiv += "         <b>客服：" + bfInfo.replyUserName + "</i></b> ";
        tboyDiv += "           <u>" + bfInfo.replyContents + "</u>  ";
        tboyDiv += "             <span>" + bfInfo.replyTime + "</span>";
        tboyDiv += "     </dd>";
        tboyDiv += "   </dl>";
        if ($("#replyUserName").attr("id") != undefined) {
            $(".tboy_1").children().eq(1).remove();
        }
        $(".tboy_1").append($(tboyDiv));
        $('#Content_feedBack').attr('disabled', 'disabled');
        $('#FeedBackSubmit').attr('disabled', 'disabled');
        $('#Content_feedBack').val("");
        $("#cc_" + feedBackId).parent().prev().html(bfInfo.replyContents);
        $("#cc_" + feedBackId).parent().prev().prev().html(bfInfo.replyUserName);
    }
}
om.closeTemplate = function () {
    $("html,body").animate({ scrollTop: 152 }, 500);
    $(" #addTemplate").remove();
    $(" .right1").height($(" .right1").height()-500);
    $(" .sid_top6").css("margin-top", "120px");
}
om.roleMessageInit = function () {
    $(".span_bj").click(function () {
        var id = $(this).attr("id");
        parent.layer.msg("正在处理中...", { time: 1000 });
        var url = "/OM/RoleMessageUpdate";
        var data = { messageID: id, status: 1 };
        defaultAjax(url, data, defaultCallback);
        parent.layer.msg(_defaultData.msg, { time: 1500 }, function () {
            top.document.location.reload();
        });
    });

    //5秒后,自动处理已经审核的消息通知
    setTimeout(function () {
        var $shenghe = $('.shenghe');
        if ($shenghe.length > 0) {
            $.ajax({
                type: "POST", url: "/om/rolemessage_auditmessagestatus",
                dataType: "json",
                success: function (result) {
                    if (result.status) {
                        $shenghe.each(function (i, item) {
                            var $this = $(item);
                            if ($this.text().indexOf('已通过审核') > -1) {
                                var $bj = $this.parent().prev().prev().find('.span_bj');
                                $bj.removeClass().addClass("span_ycl").text('已处理').off('click');
                            }
                        });
                    }
                }, error: function (a, b, c) {
                    jx.error("出错了!");
                }
            });
        }
    }, 5000);
}
om.promotionInit = function () {
        $('a[id=addlinkbtn]').on('click', function () {
            var tName = $("#trueName").val();
            if (tName == null || tName == "") {
                parent.layer.msg("推广名不可为空！", { time: 1000 });
            } else {
                parent.layer.msg("正在处理中...", { time: 1000 });
                var url = "/OM/PromotionInsert";
                var data = { trueName: tName };
                defaultAjax(url, data, defaultCallback);
                parent.layer.alert(_defaultData.msg, function () {
                    top.document.location.reload();
                });
            }
    });
}

var eval =
{
    evalListInit: function () { eval.evalListInfo(); }
    , tdOver: function () { eval.tdOver(); }
    , tdOut: function () { eval.tdOut(); }
    , audit: function () { eval.audit(obj, evalId); }
}
eval.evalListInit = function () {
    var statusTypeKey = $("#statusTypeKey").val();
    var sourceTypeKey = $("#sourceTypeKey").val();
    $('#statusType').select1(statusTypeKey, function (key, value) {
        statusTypeKey = key;
    });
    $('#sourceType').select1(sourceTypeKey, function (key, value) {
        sourceTypeKey = key;
    });
    $(".btn_lm3").click(function () {
        var sTime = $("#sTime").val();
        var eTime = $("#eTime").val();
        var pName = $("#pName").val();
        if (sTime != "" && eTime != "") {
            if (sTime > eTime) {
                jx.alert("日期开始时间必须小于结束时间！");
                return;
            }
        }
        if (sTime == "") {
            sTime = "null";
        }
        if (eTime == "") {
            eTime = "null";
        }
        if (pName == "") {
            pName = "null";
        }
        var url = "/product/EvalList?statusType=" + statusTypeKey;
        url += "&sourceType=" + sourceTypeKey;
        url += "&sTime=" + sTime;
        url += "&eTime=" + eTime;
        url += "&pName=" + pName;
        window.location.href = url;
    });
}
eval.audit = function (obj, evalId) {
    parent.layer.msg("正在处理中...", { time: 1000 });
    var url = "/product/ModifyEvalStatus";
    var data = { evalid: evalId };
    defaultAjax(url, data, defaultCallback);
    if (_defaultData.status == true) {
        var strAuditStatus = $("#td_" + evalId).html();
        if (strAuditStatus == "未审核") {

            $("#td_" + evalId).html("已审核").next().html($(" <span class='span_eval'>已通过</span>"));;
        }
    }
    parent.layer.msg(_defaultData.msg, { time: 1500 });
}
eval.tdOver = function (evalId, obj) {
    var url = "/product/Eval_Get";
    var data = { evalid: evalId };
    var mtop = obj.id * 68 + 352;
    $(".Notice1").css("top", mtop);
    $(".Notice1").css("height", 185);
    $(".Notice1").show();

    $(".Notice1").find("#Contents").html("<p>正在加载评价内容...</p>");
    $(".cet").html("<span>正在加载相关关键字...</span>");
    defaultAjax(url, data, defaultCallback);
    var evalInfo = $.parseJSON(_defaultData.data);
    var selfContent = "<p>" + evalInfo.selfContent + "</p>";
    var parentContent;
    if (evalInfo.keyword != "") {
        $(".cet").show();
        $(".cet").html("<span>认为对治疗<i>" + evalInfo.keyword + "</i>效果不错。</span>");
    }
    else {
        $(".cet").hide();
    }

    if (evalInfo.parentId != "0") {
        parentContent = "<p>@@" + evalInfo.parentId + " " + evalInfo.parentContent + " </p>";
    } else {
        parentContent = "<p></p>";
    }
    $(".Notice1").find("#Contents").html(selfContent + parentContent);
}
eval.tdOut = function () {
    $(".Notice1").hide();
}
