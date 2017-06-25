﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    public class CouponBatchInfo
    {

        /// <summary>
        /// 优惠券批次号
        /// </summary>
        public string batchID{get;set;}
        /// <summary>
        /// 渠道编号
        /// </summary>
        public int channelID{get;set;}

        /// <summary>
        /// 渠道名
        /// </summary>
        public string channelName { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        public int typeID{get;set;}

        /// <summary>
        /// 类型名称
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 生成优惠券数量
        /// </summary>
        public int numCount{get;set;} 
        /// <summary>
        /// 起始日期
        /// </summary>
        public long? startTime{get;set;}   
        /// <summary>
        /// 结束日期
        /// </summary>
        public long? endTime{get;set;}   
        /// <summary>
        /// 创建人
        /// </summary>
        public string creator{get;set;}
        /// <summary>
        /// 此批优惠券的描述
        /// </summary>
        public string description{get;set;}
    }
}
