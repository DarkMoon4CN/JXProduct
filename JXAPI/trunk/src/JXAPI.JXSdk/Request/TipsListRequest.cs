﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using Venus.Utils;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 健康说列表
    /// </summary>
    public class TipsListRequest : Venus.IVenusRequest<TipsListResponse>
    {
        #region 参数
        /// <summary>
        /// 类别
        /// </summary>
        public int? categoryID{get;set;} 

        /// <summary>
        /// 开始时间
        /// </summary>
        public long? startTime{get;set;}
        
        /// <summary>
        /// 结束时间
        /// </summary>
        public long? endTime{get;set;}
        
        /// <summary>
        /// 排序：1:评论高到低2:评论低到高3:浏览高到低4:浏览低到高
        /// </summary>
        public int? soft{get;set;}

        /// <summary>
        /// 状态：0：全部 -1：未审核 1：已审核 2：屏蔽
        /// </summary>
        public int? status { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public PageFormInfo pageForm { get; set; }
        #endregion

        #region ITopRequest Members
        public string GetApiName()
        {
            return string.Empty;
        }

        public IDictionary<string, string> GetParameters()
        {
            return null;
        }

        public void Validate()
        {

        }
        #endregion
    }
}
