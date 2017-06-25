using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXBigData.AdminUI.Models
{
    public class DataContrastInfo
    {
        /// <summary>
        /// 对比来源
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        /// 来源名称
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 总订单生成金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 总订单的同比
        /// </summary>
        public string AnOrderAmount { get; set; }

        /// <summary>
        /// 总订单的环比
        /// </summary>
        public string AdjOrderAmount { get; set; }

        /// <summary>
        /// 总订单数
        /// </summary>
        public int OrderQuan { get; set; }

        /// <summary>
        /// 总订单数同比
        /// </summary>
        public string AnOrderQuan { get; set; }

        /// <summary>
        /// 总订单数环比
        /// </summary>
        public string AdjOrderQuan { get; set; }

        /// <summary>
        /// 支付订单数
        /// </summary>
        public int PayQuan { get;set; }

        /// <summary>
        /// 支付订单量同比
        /// </summary>
        public string AnPayQuan { get; set; }

        /// <summary>
        /// 支付订单量环比
        /// </summary>
        public string AdjPayQuan { get; set; }

        /// <summary>
        /// 未支付订单
        /// </summary>
        public int NoPayQuan { get; set; }

        /// <summary>
        /// 未支付同比
        /// </summary>
        public string AnNoPayQuan { get; set; }

        /// <summary>
        /// 未支付订单环比
        /// </summary>
        public string AdjNoPayQuan { get;set; }
    }
}