using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui;
using com.igetui.api.openservice.igetui.template;
using com.igetui.api.openservice.payload;
using JXAPI.Component.BLL;
using JXAPI.Component.Enums;
using JXAPI.Component.Model;
using JXAPI.JXSdk.Response;
using JXAPI.JXSdk.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.ConsoleAppPush
{
    public class Push
    {
        #region 个推参数

        #region 线上
        //private static string APPID = "gorAbOpJHb7DknzkVhx8W6";                     //您应用的AppId
        //private static string APPKEY = "hkwvC1Ld0kAOKscmVkYaj8";                    //您应用的AppKey
        //private static string MASTERSECRET = "5OQ8fqJEOd6e6fAHfflPA1";              //您应用的MasterSecret 
        //private static string HOST = "http://sdk.open.api.igexin.com/apiex.htm";    //HOST：OpenService接口地址
        #endregion

        #region 测试
        private static string APPID = "Me9N498lSZ6c3tHQvaL4R3";                     //您应用的AppId
        private static string APPKEY = "lOZDgK13K6ABqD3bm3pOo4";                    //您应用的AppKey
        private static string MASTERSECRET = "t8bKWyygyG7KMWOlV0THI1";              //您应用的MasterSecret 
        private static string HOST = "http://sdk.open.api.igexin.com/apiex.htm";    //HOST：OpenService接口地址
        #endregion

        #endregion

        private static ILog myLog = log4net.LogManager.GetLogger(typeof(Push));

        public void PushStart() 
        {
            //获取mysql中需要推送的信息写入至推送表
            ImportPushData pushData = new ImportPushData();
            pushData.Init();

            //配置本地电脑的环境变量
            Environment.SetEnvironmentVariable("needDetails", "true");

            //log4net初始化
            log4net.Config.XmlConfigurator.Configure();

            PushLog(string.Format("推送执行开始！时间:{0}", System.DateTime.Now));

            //执行推送
            TimingPush();

            PushLog(string.Format("推送执行完毕！时间:{0}", System.DateTime.Now));
            PushLog(string.Format("==========================================="));
        }

        #region 推送实际逻辑
        public void TimingPush()
        {
            //获取数据库中所有等待推送的数据
            string Msg = string.Empty;
            List<PushMessageInfo> pushMessageInfoList = MobilePushBLL.Instance.MobilePush_GetList(ref Msg, 30);
            if (pushMessageInfoList == null || pushMessageInfoList.Count <= 0)
            {
                PushLog(string.Format("暂时没有需要推送信息：{0}", Msg));
                return;
            }
            PushLog(string.Format("推送信息总条数:{0}", pushMessageInfoList.Count));
            for (int i = 0; i < pushMessageInfoList.Count; i++)
            {
                PushMessageInfo pushMessageInfo = pushMessageInfoList[i];
                try
                {
                    if (pushMessageInfo.PushCount >= 5 && pushMessageInfo.Status==2)
                    {
                        pushMessageInfo.Status = 3;//设置为拒绝推送状态
                        PushLog(string.Format("推送数据已失败5次 推送ID：{0}", pushMessageInfo.PushID));
                    }
                    else
                    {
                        if (pushMessageInfo.PushType == "app" || string.IsNullOrEmpty(pushMessageInfo.PushType) == true)
                        {
                            //推送所有用户（app下）
                            pushMessageToApp(pushMessageInfo);
                        }
                        else
                        {
                            //推送1-n个用户
                            PushMessageToList(pushMessageInfo);
                        }
                        pushMessageInfo.Status = 1;
                        pushMessageInfo.PushCount += 1;
                    }
                }
                catch (Exception e)
                {
                    //推送异常,数据推pushCount计数
                    PushLog(string.Format("推送异常:{0}", e.Message));
                    PushLog(string.Format("异常数据:{0}", pushMessageInfo.PushID));
                    pushMessageInfo.Status = 2;
                    pushMessageInfo.PushCount += 1;
                }
                
                //1.修改推送后数据库数据 Status 为 1 表示已推送 0表示未推送
                //2.推送次数超过5此将不再推送 Status=3
                MobilePushBLL.Instance.UpdateMobilePush(pushMessageInfo);
            }
        }
        #endregion

        #region  推送所有用户（向app应用内发送）
        /// <summary>
        /// 推送所有用户（向app应用内发送）
        /// </summary>
        /// <param name="pushMessageInfo"></param>
        public void pushMessageToApp(PushMessageInfo pushMessageInfo)
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            string content = pushMessageInfo.Contents;
            string title = string.Empty;
            //为接收者组织数据
            var transmissionContent =
                        new
                        {
                            Template = pushMessageInfo.Template,
                            Content = pushMessageInfo.Contents,
                            Url = pushMessageInfo.Url,
                            Section = pushMessageInfo.TypeID + "/" + pushMessageInfo.Section,
                            NodeId = pushMessageInfo.DataID
                        };
            string jsonContent = JsonHelper.ConvertToJson(transmissionContent);
            PushLog("- - - - - - - - - - - - - ");
            PushLog(string.Format("推送类型:{0}", pushMessageInfo.PushType));
            PushLog(string.Format("推送内容:{0}", pushMessageInfo.Contents));
            PushLog(string.Format("推送目标:{0}", "应用内的所有用户"));
            if (pushMessageInfo.TypeID != 0)
            {
                title = Enum.GetName(typeof(SectionSecondType), pushMessageInfo.Section);
            }
            //透传模板
            TransmissionTemplate template = TransmissionTemplateDemo(content, title, jsonContent,0);
            template.TransmissionType = "2";
            template.TransmissionContent = jsonContent; //透传内容
            AppMessage message = new AppMessage();
            message.IsOffline = true;
            message.OfflineExpireTime = 1000 * 3600 * 12;
            message.Data = template;

            //账号APPID
            List<string> appIdList = new List<string>();
            appIdList.Add(APPID);

            //推送手机的类型 
            List<string> phoneTypeList = new List<string>();
            if (pushMessageInfo.ChannelID == 0)
            {
                phoneTypeList.Add("IOS");
                phoneTypeList.Add("ANDROID");
            }
            else if (pushMessageInfo.ChannelID == 1)
            {
                phoneTypeList.Add("IOS");
            }
            else 
            {
                phoneTypeList.Add("ANDROID");
            }
            message.AppIdList = appIdList;
            message.PhoneTypeList = phoneTypeList;
            string pushResult = push.pushMessageToApp(message);
            try
            {
                var result = JsonHelper.ConvertToObj<PushResult>(pushResult);
                PushLog("推送结果：" + result.result);
            }
            catch
            {
                PushLog("推送结果转换异常");
            }
        }
        #endregion

        #region 推送1-n个用户（用户别名）
        /// <summary>
        ///  推送1-n个用户（用户别名）
        /// </summary>
        /// <param name="pushMessageInfo">推送信息实体</param>
        private void PushMessageToList(PushMessageInfo pushMessageInfo)
        {
            int isMute = 0;//设定是否静音，只在于推送 客户等于1时可用
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            string content = pushMessageInfo.Contents;
            string title = string.Empty;
            var transmissionContent =
            new
            {
                Template = pushMessageInfo.Template,
                Content = pushMessageInfo.Contents,
                Url = pushMessageInfo.Url,
                Section = pushMessageInfo.TypeID + "/" + pushMessageInfo.Section,
                NodeId = pushMessageInfo.DataID
            };
            string jsonContent = JsonHelper.ConvertToJson(transmissionContent);
            PushLog("- - - - - - - - - - - - - ");
            PushLog(string.Format("推送类型:{0}", pushMessageInfo.PushType));
            PushLog(string.Format("推送内容:{0}",pushMessageInfo.Contents));
            PushLog(string.Format("推送目标:{0}",pushMessageInfo.TargetList));
            if (pushMessageInfo.TypeID != 0)
            {
                title = Enum.GetName(typeof(SectionSecondType), pushMessageInfo.Section);
            }
            //设置接收者
            List<com.igetui.api.openservice.igetui.Target> targetList = new List<com.igetui.api.openservice.igetui.Target>();
            
            if (pushMessageInfo.TargetList.IndexOf(",") == -1)
            {
                pushMessageInfo.TargetList += ",";
            }
            string[] targets = pushMessageInfo.TargetList.Split(',');

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] != null && targets[i] != string.Empty && targets[i] != "")
                {
                    com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
                    target.appId = APPID;
                    target.alias = targets[i];

                    #region 判定是否允许推送，并向java接口写入推送信息
                    MessagesAddBLL pushBLL = new MessagesAddBLL();
                    MessagesAddRequest maRequest = new MessagesAddRequest();
                    maRequest.creator = "推送程序";
                    maRequest.userID = Convert.ToInt32(target.alias);
                    maRequest.status = 0;
                    if (pushMessageInfo.Template == 1)
                    {
                        maRequest.contents = pushMessageInfo.Contents;
                    }
                    else
                    {
                        maRequest.contents = pushMessageInfo.Url;
                    }
                    if (pushMessageInfo.TypeID != 0)
                    {
                        maRequest.typeID = pushMessageInfo.TypeID;
                    }
                    if (pushMessageInfo.Section != 0) 
                    {
                        maRequest.dataTypeID = pushMessageInfo.Section;
                    }
                     maRequest.dataID = pushMessageInfo.DataID;

                     //写入至未读消息列表
                     var respon = pushBLL.MessagesAdd(maRequest);
                    //IOS 数据判定开关
                    if (pushMessageInfo.ChannelID == 1 || pushMessageInfo.ChannelID == 0)
                    {
                        NotifyConfigGetBLL notifyConfigGetBLL = new NotifyConfigGetBLL();
                        NotifyConfigGetRequest request = new NotifyConfigGetRequest();
                        request.userID = maRequest.userID;
                        request.userPlatform = 3;
                        NotifyConfigGetResponse ncgResponse = notifyConfigGetBLL.NotityConfigGet(request);
                        //转化为2进制字符串后换算出是否推送
                        //string allStatus = Convert.ToString(1, 2);
                        string allStatus = Convert.ToString(ncgResponse.notifyConfig.notifySwitch, 2);
                        int t = GetIntegerSomeBit(ncgResponse.notifyConfig.notifySwitch,maRequest.typeID-1);
                        if (targets.Length == 2)
                        {
                            //静音的二进制返回位数
                            isMute = GetIntegerSomeBit(ncgResponse.notifyConfig.notifySwitch, 4);
                        }
                        //包含用户已有权限+不存在的用户
                        if (t == 0)
                        {
                            if (respon.code == 0)
                            {
                                targetList.Add(target);
                            }
                            else
                            {
                                PushLog(string.Format("消息写入失败,无法推送：{0}", maRequest.userID));
                            }
                        }
                        else
                        {
                            PushLog(string.Format("跳过推送ID：{0}", maRequest.userID));
                        }
                    }
                    else //Android
                    {
                        if (respon.code == 0)
                        {
                            targetList.Add(target);
                        }
                        else 
                        {
                            PushLog(string.Format("消息写入失败,无法推送：{0}", maRequest.userID));
                        }
                    }

                    #endregion
                }
            }


            if (targetList == null || targetList.Count == 0)
            {
                return;
            }
            #region 因为IOS端需要静音,透传模板放置在方法底部
            ListMessage message = new ListMessage();

            //设置透传模板
            TransmissionTemplate template = TransmissionTemplateDemo(content, title, jsonContent, isMute);
            template.TransmissionType = "2";             //应用启动类型，1：强制应用启动 2：等待应用启动
            template.TransmissionContent = jsonContent;  //透传内容
            message.IsOffline = true;
            message.OfflineExpireTime = 1000 * 3600 * 12;
            message.Data = template;
            #endregion
            string contentId = push.getContentId(message);
            string pushResult = push.pushMessageToList(contentId, targetList);
            try
            {
                var result = JsonHelper.ConvertToObj<PushResult>(pushResult);
                PushLog("推送结果："+result.result);
            }
            catch 
            {
                PushLog("推送结果转换异常");
            }
            
        }
        #endregion

        #region Android 与IOS 透传模板 初始化设置
        /// <summary>
        /// Android 与IOS 透传模板 初始化设置
        /// </summary>
        /// <param name="content">内容主体</param>
        /// <param name="title">标题 </param>
        /// <param name="jsonContent">透传主体</param>
        /// <returns></returns>
        public static TransmissionTemplate TransmissionTemplateDemo(string content, string title, string jsonContent, int isMute)
        {
            TransmissionTemplate template = new TransmissionTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;

            //APN高级推送
            APNPayload apnpayload = new APNPayload();
            DictionaryAlertMsg alertMsg = new DictionaryAlertMsg();
            alertMsg.Body = "Body";
            alertMsg.ActionLocKey = "ActionLocKey";
            alertMsg.LocKey = content;
            alertMsg.addLocArg("LocArg");
            alertMsg.LaunchImage = "LaunchImage";
            //IOS8.2支持字段
            alertMsg.Title = "Title";
            alertMsg.TitleLocKey = title;  //IOS 标题
            alertMsg.addTitleLocArg("TitleLocArg");
            apnpayload.AlertMsg = alertMsg; //IOS 消息
            apnpayload.Badge = 0;
            if (isMute == 1)
            {
                apnpayload.Sound = "com.gexin.ios.silence";
            }
            else 
            {
                apnpayload.Sound = "default";
            }
            apnpayload.addCustomMsg("payload", jsonContent);
            template.setAPNInfo(apnpayload);
            return template;
        }
        #endregion

        #region Android 与IOS 普通模板 初始化设置
        /// <summary>
        ///  Android 与IOS 普通模板 初始化设置
        /// </summary>
        /// <returns></returns>
        public static NotificationTemplate NotificationTemplateDemo()
        {
            NotificationTemplate template = new NotificationTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            template.Title = "lm在测试上传";         //通知栏标题
            template.Text = "lm在测试上传";          //通知栏内容
            template.Logo = "";                      //通知栏显示本地图片
            template.LogoURL = "";                   //通知栏显示网络图标

            template.TransmissionType = "1";          //应用启动类型，1：强制应用启动  2：等待应用启动
            template.TransmissionContent = "请填写透传内容";   //透传内容

            template.IsRing = true;                //接收到消息是否响铃，true：响铃 false：不响铃
            template.IsVibrate = true;               //接收到消息是否震动，true：震动 false：不震动
            template.IsClearable = true;             //接收到消息是否可清除，true：可清除 false：不可清除
            return template;
        }
        #endregion

        #region 执行输出与日志记录
        /// <summary>
        /// 执行输出与日志记录
        /// </summary>
        /// <param name="message"></param>
        private void PushLog(string message) 
        {
            myLog.InfoFormat(message);
            Console.WriteLine(message);
        }
        #endregion


        /// <summary>
        /// 取得指定2进制位数值
        /// </summary>
        /// <param name="resource">要取某一位的整数</param>
        /// <param name="mask">要取的位置索引，自右至左为0-7</param>
        /// <returns>返回某一位的值（0或者1）</returns>
        public  int GetIntegerSomeBit(int resource, int mask)
        {
            return resource >> mask & 1;
        }

    }
    public class PushResult 
    {
        public string result { get; set; }
    }


     
}
