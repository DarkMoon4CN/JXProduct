using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    [Serializable]
    public class SaleInfo
    {
        /// <summary>
        /// 促销ID
        /// </summary>
        public int SaleID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int DataID { get; set; }

        /// <summary>
        /// 卖点
        /// </summary>
        public string Selling { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public short Sort { get; set; }

        //类型
        public string CodeID { get;set;}

        /// <summary>
        /// 键值/促销时间、楼层位置（热销、运营板块）
        /// </summary>
        public string KeyValue { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string Updater { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
