using System;
using System.Collections.Generic;
namespace JXProduct.Component.Model
{
    public class ProductGiftInfo
    {
        /// <summary>
        ///  商品ID
        ///</summary>
        public Int32 ProductID { get; set; }

        /// <summary>
        ///  赠品ID
        ///</summary>
        public Int32 ProductGiftID { get; set; }

        public string ProductGiftName { get; set; }

        /// <summary>
        ///  数量
        ///</summary>
        public Int32 Quantity { get; set; }

        /// <summary>
        ///  赠品状态 0 ：正常 1：过期（需要考虑库存）
        ///</summary>
        public short Status { get; set; }

        /// <summary>
        ///  创建者
        ///</summary>
        public String Creator { get; set; }

        /// <summary>
        ///  创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///  最后更新者
        ///</summary>
        public String Updater { get; set; }

        /// <summary>
        ///  更新时间
        ///</summary>
        public DateTime UpdateTime { get; set; }
    }
}