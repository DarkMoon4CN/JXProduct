using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{

    public class LogisticsInfo
    {
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public int expressId { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string expressName { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string expressCode { get; set; }
        
        /// <summary>
        /// 进度
        /// </summary>
        public IList<LogisticsDetailInfo> list { get; set; }
    }

    public class LogisticsDetailInfo
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string cdate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string process { get; set; }
    }
}
