using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Model
{
    public class ProductStockInfo
    {
        public ProductStockInfo()
        {
            this.ChangeTime = DateTime.Now;
        }
        /// <summary>
        /// 库存ID
        /// </summary>
        public int StockID { get; set; }

        public int ProductID { get; set; }
        public string ChineseName { get; set; }

        /// <summary>
        /// 金象码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 库房ID
        /// </summary>
        public int HouseID { get; set; }

        /// <summary>
        /// 类别 1:实库 2:虚库
        /// </summary>
        public short TypeID { get; set; }
        public string TypeIDString
        {
            get
            {
                switch (this.TypeID)
                {
                    case 1: return "实库";
                    case 2: return "虚库";
                    default: return string.Empty;
                }
            }
        }


        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 其他库房的库存
        /// </summary>
        public int OutStock { get; set; }

        /// <summary>
        ///  冻结库存
        /// </summary>
        public int FreezedStock { get; set; }

        /// <summary>
        /// 可用库存
        /// </summary>
        public int UsableStock { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        public int LogType { get; set; }
    }
}
