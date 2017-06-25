﻿using JXProduct.Component.BLL;
using JXProduct.Component.Model;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using JXAPI.JXSdk.Service;
using JXAPI.JXSdk.Request;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using Newtonsoft.Json;
using JXProduct.Component.Enums;
using JXProduct.AdminUI.Models.OM;
namespace JXProduct.AdminUI.Controllers
{
    public class OMController : JXAdminAuthBase.BaseController
    {
        //OM:Operation Management
        #region  lm For网站公告
        public ActionResult NoticeList(int pageIndex = 1)
        {
            IList<ExpressNewsInfo> expressNewsInfoList = null;//网站公告结果集
            int count = 0;// 网站结果总条数
            int pageSize = 15;
            expressNewsInfoList = ExpressNewsBLL.Instance.ExpressNews_GetList(pageIndex, pageSize, " IsStick DESC ", "", out count);
            ViewBag.PageList = new PagedList<ExpressNewsInfo>(expressNewsInfoList, pageIndex, pageSize, count);
            ViewBag.ExpressNewsInfoList = expressNewsInfoList;
            ViewBag.PageIndex = pageIndex;
            ViewBag.Count = count;
            return View();
        }

        public ActionResult NoticeEidt(int newsID)
        {
            ExpressNewsInfo expressNewsInfo = ExpressNewsBLL.Instance.ExpressNews_Get(newsID);
            ViewBag.ExpressNewsInfo = expressNewsInfo;
            return View();
        }

        [HttpPost]
        public JsonResult ModifyNotice
            (int newsID, int type = 0, string title = "", string content = "")
        {
            var result = new JsonResultObject();
            ExpressNewsInfo expressNewsInfo = ExpressNewsBLL.Instance.ExpressNews_Get(newsID);
            if (newsID == 0)
            {
                result.msg = "此公告ID异常！";
                result.status = false;
                return Json(result);
            }
            if (title != "")
            {
                expressNewsInfo.Title = title;
            }
            if (content != "" && type == 2)
            {
                expressNewsInfo.Link = content;
                expressNewsInfo.Content = null;

            }
            else
            {
                expressNewsInfo.Link = null;
                expressNewsInfo.Content = content;
            }
            expressNewsInfo.Type = type;
            result.status = ExpressNewsBLL.Instance.ExpressNews_Update(expressNewsInfo);
            if (result.status == true)
            {
                result.msg = "已编辑完成！";
            }
            else
            {
                result.msg = "编辑失败，未能连接至网络！";
            }
            return Json(result);
        }

        public JsonResult UpdateStick(int newsID, short stick = 0)
        {
            var result = new JsonResultObject();
            ExpressNewsInfo expressNewsInfo = ExpressNewsBLL.Instance.ExpressNews_Get(newsID);
            expressNewsInfo.IsStick = stick;
            result.status = ExpressNewsBLL.Instance.ExpressNews_Update(expressNewsInfo);
            if (result.status == true && stick == 0)
            {
                result.msg = "取消置顶成功！";
            }
            else if (result.status == true && stick == 1)
            {
                result.msg = "设置置顶成功！";
            }
            else
            {
                result.msg = "设置失败，未能连接至网络！";
            }
            return Json(result);
        }

        public JsonResult UpdateRed(int newsID, short red = 0)
        {
            var result = new JsonResultObject();
            ExpressNewsInfo expressNewsInfo = ExpressNewsBLL.Instance.ExpressNews_Get(newsID);
            expressNewsInfo.IsRed = red;
            result.status = ExpressNewsBLL.Instance.ExpressNews_Update(expressNewsInfo);
            if (result.status == true && red == 0)
            {
                result.msg = "取消套红成功！";
            }
            else if (result.status == true && red == 1)
            {
                result.msg = "设置套红成功！";
            }
            else
            {
                result.msg = "设置失败，未能连接至网络！";
            }
            return Json(result);
        }

        public JsonResult NoticeRemove(int newsID = 0)
        {
            var result = new JsonResultObject();
            result.status = ExpressNewsBLL.Instance.ExpressNews_Delete(newsID);
            if (result.status == true)
            {
                result.msg = "删除公告成功！";
            }
            else
            {
                result.msg = "删除失败，未能连接至网络！";
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult NoticeAdd()
        {
            return View();
        }

        [HttpPost]
        public JsonResult NoticeAddInfo(int type = 0, string title = "", string content = "")
        {
            int newsID;//添加完毕后返回的ID
            ExpressNewsInfo expressNewsInfo = new ExpressNewsInfo();
            var result = new JsonResultObject();
            expressNewsInfo.Type = type;
            expressNewsInfo.Title = title;
            if (expressNewsInfo.Type == 2)
            {
                expressNewsInfo.Link = content;
            }
            else
            {
                expressNewsInfo.Content = content;
            }
            expressNewsInfo.CreateTime = DateTime.Now;
            expressNewsInfo.UpdateTime = expressNewsInfo.CreateTime;
            newsID = ExpressNewsBLL.Instance.ExpressNews_Insert(expressNewsInfo);
            if (newsID > 0)
            {
                result.status = true;
                result.msg = "已发布完成！";
            }
            else
            {
                result.status = false;
                result.msg = "发布失败，未能连接至网络！";
            }
            return Json(result);
        }
        #endregion

        #region lm For意见反馈
        public ActionResult FeedBackList(int statusType = -1, string sourceType = "-1", string sTime = "null", string eTime = "null", int pageIndex = 1)
        {
            FeedBackListRequest far = new FeedBackListRequest();
            int count = 0;
            int pageSize = 10;
            if (sourceType != "-1")
            {
                far.channels = sourceType;
            }
            if (sTime != "null")
            {

                far.startTime = JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse(sTime));
            }
            if (eTime != "null")
            {
                DateTime endTime = DateTime.Parse(eTime);
                endTime = endTime.AddDays(1).AddMinutes(-1);
                far.endTime = JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertDataTimeLong(endTime);
            }

            JXAPI.JXSdk.Domain.PageFormInfo pageInfo = new JXAPI.JXSdk.Domain.PageFormInfo() { page = pageIndex, size = pageSize };
            far.status = 0;
            far.pageForm = pageInfo;
            FeedBackListResponse feedBackData = FeedBackService.Instance.List(far);
            IList<JXAPI.JXSdk.Domain.FeedBackInfo> feedBackInfoList = feedBackData.list;

            if (feedBackInfoList != null)
            {
                if (statusType != -1)
                {
                    feedBackInfoList = feedBackInfoList.Where(p => p.status == statusType).ToList();
                }
                count = feedBackInfoList.Count;
                feedBackInfoList = feedBackInfoList.Take(pageIndex * pageSize).Skip(pageSize * (pageIndex - 1)).ToList();
                ViewBag.FeedBackInfoList = feedBackInfoList;
                ViewBag.PageList = new PagedList<JXAPI.JXSdk.Domain.FeedBackInfo>(feedBackInfoList, pageIndex, pageSize, count);
            }
            ViewBag.StatusType = statusType;//处理状态
            ViewBag.SourceType = sourceType;//来源
            ViewBag.STime = sTime;//开始时间
            ViewBag.ETime = eTime;//结束时间
            return View();
        }

        public JsonResult FeedBackRemove(int feedBackId)
        {
            var result = new JsonResultObject();
            result.status = FeedBackService.Instance.Delete(feedBackId, base.UNICKNAME);
            if (result.status == true)
            {
                result.msg = "删除公告成功！";
            }
            else
            {
                result.msg = "删除失败，未能连接至网络！";
            }
            return Json(result);
        }

        public JsonResult FeedBackReplyDetail(int feedBackId, string Content)
        {
            var result = new JsonResultObject();
            JXAPI.JXSdk.Domain.FeedBackInfo feedBackInfo = FeedBackService.Instance.Get(feedBackId);
            if (feedBackInfo != null)
            {
                result.status = true;
                var outFBInfo = new JXProduct.AdminUI.Models.OM.FeedBackInfo();
                outFBInfo.feedBackId = feedBackInfo.feedBackId;
                outFBInfo.contact = feedBackInfo.contact;
                outFBInfo.contents = feedBackInfo.contents;
                outFBInfo.createTime =
                             JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertLongDateTime(feedBackInfo.createTime).ToString();
                outFBInfo.replyContents = feedBackInfo.replyContents;
                outFBInfo.replyTime =
                             JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertLongDateTime(feedBackInfo.replyTime).ToString();
                outFBInfo.replyUserName = feedBackInfo.replyUserName;
                outFBInfo.status = feedBackInfo.status;
                outFBInfo.typeID = feedBackInfo.typeID;
                outFBInfo.typeName = FeedBackType(outFBInfo.typeID);
                outFBInfo.userID = feedBackInfo.userID;
                outFBInfo.userName = feedBackInfo.userName;
                outFBInfo.image = feedBackInfo.image;
                outFBInfo.replyImage = feedBackInfo.replyImage;
                result.data = Newtonsoft.Json.JsonConvert.SerializeObject(outFBInfo);
                result.msg = "已完成数据获取！";
            }
            else
            {
                result.msg = "获取失败，未能连接至网络！";
                result.status = false;
                return Json(result);
            }
            return Json(result);
        }

        public JsonResult FeedBackReply(int feedBackId, string replyContent)
        {
            var result = new JsonResultObject();
            JXAPI.JXSdk.Domain.FeedBackInfo feedBackInfo = FeedBackService.Instance.Get(feedBackId);

            if (feedBackInfo == null)
            {
                result.msg = "修改失败，反馈已被删除！";
                result.status = false;
                return Json(result);
            }
            feedBackInfo.replyContents = replyContent;
            feedBackInfo.replyUserName = base.UNICKNAME;
            feedBackInfo.replyTime = JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Now);
            FeedBackReplyRequest rReply = new FeedBackReplyRequest()
            {
                feedBackId = feedBackInfo.feedBackId
               ,
                replyContents = replyContent + " 感谢您对金象网一如既往的支持!"
               ,
                uid = base.UID
               ,
                userName = base.UNICKNAME
            };
            result.status = FeedBackService.Instance.Reply(rReply);
            if (result.status == true)
            {
                var outFBInfo = new JXProduct.AdminUI.Models.OM.FeedBackInfo();
                outFBInfo.feedBackId = feedBackInfo.feedBackId;
                outFBInfo.contact = feedBackInfo.contact;
                outFBInfo.contents = feedBackInfo.contents;
                outFBInfo.createTime = JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertLongDateTime(feedBackInfo.createTime).ToString();
                outFBInfo.replyContents = feedBackInfo.replyContents;
                outFBInfo.replyTime = JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertLongDateTime(feedBackInfo.replyTime).ToString();
                outFBInfo.replyUserName = feedBackInfo.replyUserName;
                outFBInfo.status = feedBackInfo.status;
                outFBInfo.typeID = feedBackInfo.typeID;
                outFBInfo.typeName = FeedBackType(outFBInfo.typeID);
                outFBInfo.userID = feedBackInfo.userID;
                outFBInfo.userName = feedBackInfo.userName;
                result.data = Newtonsoft.Json.JsonConvert.SerializeObject(outFBInfo);
                result.msg = "回复成功！";
            }
            else
            {
                result.msg = "回复失败，未能连接至网络！";
            }
            return Json(result);
        }
        private string FeedBackType(int typeID)
        {
            string typeString = string.Empty;
            switch (typeID)
            {
                case 0: typeString = "其他"; break;
                case 1: typeString = "物流问题"; break;
                case 2: typeString = "功能意见"; break;
                case 3: typeString = "流量问题"; break;
                case 4: typeString = "操作体验问题"; break;
                case 5: typeString = "新需求反馈"; break;
                default: break;
            }
            return typeString;
        }
        #endregion

        #region  lm For我的消息
        public ActionResult RoleMessageList(int status = 0, int pageIndex = 1)
        {
            int count = 0;// 网站结果总条数
            int pageSize = 50;
            //当一个用户有多个角色时,优先判定
            Type enumType = typeof(RoleType);
            int roleID = 0;
            string roleName = string.Empty;
            string[] roleNames = Enum.GetNames(enumType);
            for (int i = 0; i < roleNames.Length; i++)
            {
                if (ROLENAME.IndexOf(roleNames[i]) != -1)
                {
                    roleID = (int)Enum.Parse(typeof(RoleType), roleNames[i]);
                    roleName = roleNames[i];
                    break;
                }
            }
            // roleID = 36;


            var model = new RoleMessagesModel();
            model.RoleID = roleID;
            model.Messages = RoleMessagesBLL.Instance.RoleMessages_GetList(roleID, status, pageIndex, pageSize, " MessageID DESC ", out count);
            model.PagedList = new PagedList<RoleMessagesInfo>(model.Messages, pageIndex, pageSize, count);
            model.pageindex = pageIndex;
            model.recordcount = count;
            if (status != 0)
            {
                model.recordcount = RoleMessagesBLL.Instance.RoleMessages_Count(roleID);
            }

            model.Greeting = Get_Greetings() + "好！" + UNICKNAME;
            model.Status = status;
            return View(model);
        }

        /// <summary>
        ///  以消息ID 更新消息状态  默认更新至消息已处理
        /// </summary>
        /// <param name="messageID">消息ID</param>
        /// <param name="status">消息状态</param>
        /// <returns></returns>
        public JsonResult RoleMessageUpdate(int messageID = 0, int status = 1)
        {
            var result = new JsonResultObject();
            if (messageID == 0)
            {
                result.msg = "消息ID为空！";
                return Json(result);
            }
            RoleMessagesInfo rmInfo = RoleMessagesBLL.Instance.RoleMessages_Get(messageID);
            if (rmInfo == null)
            {
                result.msg = "没有查询出匹配数据！";
            }
            else
            {
                //将消息设置为已读
                rmInfo.Status = (short)status;
                result.status = RoleMessagesBLL.Instance.RoleMessages_Update(rmInfo);
                if (result.status)
                    result.msg = "该消息已处理！";
                else
                    result.msg = "修改失败，未能连接至网络！";
            }
            return Json(result);
        }

        public JsonResult RoleMessageGetStatus(int messageID = 0) 
        {
            var result = new JsonResultObject();
            if (messageID == 0)
            {
                result.msg = "消息ID为空！";
                return Json(result);
            }
            RoleMessagesInfo rmInfo = RoleMessagesBLL.Instance.RoleMessages_Get(messageID);
            if (rmInfo.Status == 1)
            {
                result.msg = "该消息已完成处理，无需设置！";
                result.status = false;
            }
            else 
            {
                result.status = true; 
            }
            return Json(result); 
        }

        /// <summary>
        /// 问候语
        /// </summary>
        /// <returns></returns>
        public string Get_Greetings()
        {
            string str = string.Empty;
            var now = DateTime.Now;
            int times = now.Hour;
            if (times >= 0 && times < 6) { str = "凌晨"; }
            if (times >= 6 && times < 9) { str = "早上"; }
            if (times >= 9 && times < 11) { str = "上午"; }
            if (times >= 11 && times < 13) { str = "中午"; }
            if (times >= 13 && times < 17) { str = "下午"; }
            if (times >= 17 && times < 19) { str = "傍晚"; }
            if (times >= 19 && times < 24) { str = "晚上"; }
            return str;
        }

        #endregion

        #region lm For递推
        public ActionResult PromotionList(int status = 0, int pageIndex = 1)
        {
            int count = 0;  //总条数
            int pageSize = 14;
            int startCount=(pageIndex-1)*pageSize;
            string strWhere=string.Empty;
            IList<BelowLinePromotionInfo> blpInfoList= BelowLinePromotionBLL.Instance.BelowLinePromotion_GetList(startCount, pageSize, strWhere);
            count = BelowLinePromotionBLL.Instance.BelowLinePromotion_GetCount(strWhere);
            ViewBag.Promotion = blpInfoList;
            ViewBag.PageList = new PagedList<BelowLinePromotionInfo>(blpInfoList, pageIndex, pageSize, count);
            return View();
        }

        public JsonResult PromotionInsert(string trueName) 
        {
            var result = new JsonResultObject();
            string invitationCode=string.Empty;
            invitationCode="DT";
            BelowLinePromotionInfo blpInfo = new BelowLinePromotionInfo();
            blpInfo.CreateTime = DateTime.Now;
            blpInfo.Status = 0;
            blpInfo.TrueName = trueName;
            while (true)
            {
                Random random = new Random();
                int randKey = random.Next(100000, 999999);
                bool isExist=BelowLinePromotionBLL.Instance.BelowLinePromotion_IsExist(invitationCode + randKey);
                if (!isExist) 
                {
                    blpInfo.InvitationCode = "DT" + randKey;
                    result.status=BelowLinePromotionBLL.Instance.BelowLinePromotion_Insert(blpInfo);
                    break;
                }
            }
            if (result.status)
            {
                result.msg = "已生成成功！推广ID:" + blpInfo.InvitationCode;
            }
            else 
            {
                result.msg = "生成失败，未能连接至网络！";
            }
            return Json(result);
        }

        #endregion
    }
}