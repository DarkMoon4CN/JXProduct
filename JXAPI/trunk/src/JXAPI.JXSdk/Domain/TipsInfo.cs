using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    public class TipsInfo
    {
        /// <summary>
        /// 健康说ID
        /// </summary>
        public int tipsID { get; set; }

        /// <summary>
        /// 类目
        /// </summary>
        public short categoryID { get; set; }

        /// <summary>
        /// 话题 0=健康说 1=话题
        /// </summary>
        public short type { get; set; }
        
        /// <summary>
        /// 内容
        /// </summary>
        public string contents { get; set; }
        
        /// <summary>
        /// 图片
        /// </summary>
        public string picture { get; set; }

        ///// <summary>
        ///// 喜欢数
        ///// </summary>
        //public int Favor { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int comment { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string channel { get; set; }

        /// <summary>
        /// 浏览数
        /// </summary>
        public int browse { get; set; }

        /// <summary>
        /// 发布用户ID
        /// </summary>
        public int userID { get; set; }
        
        /// <summary>
        /// 发布人
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string imageUrl { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public long cTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public short status { get; set; }

        public string updater { get; set; }
    }
}
