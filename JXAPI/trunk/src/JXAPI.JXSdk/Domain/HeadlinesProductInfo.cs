using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    public class HeadlinesProductInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int headProductID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int headID { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public int productID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string picture { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string intro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short sort { get; set; }
    }
}
