using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class PushMessageInfo
    {
        /// <summary>
        /// 推送Id
        /// </summary>
        public int PushID{get;set;}

        /// <summary>
        /// 推送目标类型：用于区分推送类型：ios(1)与android(2) 方式不一样 默认 全部推送(0)
        /// </summary>
        public int ChannelID { get; set; }

        private string _pushType;

        /// <summary>
        /// 推送类型  单推：single 群推：list  应用：app 默认：app
        /// </summary> 
        public string PushType
        {
            get
            {
                if (this._pushType == null || this._pushType == string.Empty)
                {
                    return "app";
                }
                return this._pushType;
            }
            set
            { this._pushType = value; }
        }

        /// <summary>
        /// 推送模板  1普通通知，2链接通知，3下载通知
        /// </summary>
        public int Template { get; set; }


        /// <summary>
        /// 一组别名中间逗号隔开
        /// </summary>
        public string TargetList { get; set; }

        /// <summary>
        /// 主体内容 
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        ///  Template=2 || template=3 时被使用，用户下载和链接的URL
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// 推送指向的一级目录
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        ///  推送指向的二级目录
        /// </summary>
        public int Section { get; set; }

        /// <summary>
        ///  附带参数，例：Section=物流中心后，提示订单状态，DataID转义成orderId
        /// </summary>
        public string DataID { get; set; }

        /// <summary>
        /// 调用的apiName
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 推送人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 推送状态（0=未推送 1=已推送，2=推送异常）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string Updater{get;set;}

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime{get;set;}

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime{get;set;}

        /// <summary>
        /// 记录已推送次数
        /// </summary>
        public int PushCount { get; set; }
    }
}
