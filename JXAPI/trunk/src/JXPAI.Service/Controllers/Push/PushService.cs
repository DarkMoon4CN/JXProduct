using JXAPI.Common.Utils;
using JXAPI.Component.BLL;
using JXAPI.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXPAI.Service.Controllers.Push
{
    public class PushService
    {
       

        public JsonResultObject PushMessage() 
        {
            var result = new JsonResultObject();
            Environment.SetEnvironmentVariable("needDetails", "true");
            PushMessageInfo pushMessageInfo = new PushMessageInfo();
            pushMessageInfo.PushType = HttpContext.Current.Request["pushType"];
            pushMessageInfo.TargetList = HttpContext.Current.Request["targetList"];
            pushMessageInfo.Contents = HttpContext.Current.Request["content"];
            pushMessageInfo.Creator = HttpContext.Current.Request["creator"];
            pushMessageInfo.DataID = HttpContext.Current.Request["dataID"];
            pushMessageInfo.Url = HttpContext.Current.Request["url"];

            string template = HttpContext.Current.Request["template"];
            string channelID = HttpContext.Current.Request["channelID"];
            string typeID = HttpContext.Current.Request["typeID"];
            #region 验证
            if (pushMessageInfo.PushType == null || pushMessageInfo.PushType == string.Empty)
            {
                result.msg = "推送失败，缺少必要的参数: pushType ！";
                return result;
            }
            if (pushMessageInfo.PushType!="app")
            {
                if (pushMessageInfo.TargetList == null || pushMessageInfo.TargetList == "") 
                {
                    result.msg = "推送失败，推送类型pushType:" + pushMessageInfo.PushType + "时，targetList 不能为空！";
                    return result;
                }
            }
            #endregion

            #region 处理
            if (template != null && template != string.Empty) 
            {
                pushMessageInfo.Template = int.Parse(template);
            }
            if (channelID != null && channelID != string.Empty)
            {
                pushMessageInfo.ChannelID = int.Parse(channelID);
            }
            else 
            {
                pushMessageInfo.ChannelID = 0;
            }
            //拆分 TypeID 1/102
            if (typeID.IndexOf('/') != -1)
            {
                string[] splitTypeID = typeID.Split('/');
                pushMessageInfo.TypeID = int.Parse(splitTypeID[0]);
                pushMessageInfo.Section = int.Parse(splitTypeID[1]);
            }
            #endregion

            //写入数据库
            DateTime dt = DateTime.Now;
            pushMessageInfo.CreateTime = dt;
            pushMessageInfo.UpdateTime = dt;
            result.status=MobilePushBLL.Instance.InsertMobilePush(pushMessageInfo);
            result.msg = "队列数据库无法打开！";
            if (result.status)
            {
                result.msg = "已加入发送队列！";
            }
            return result;
        }
    }
}