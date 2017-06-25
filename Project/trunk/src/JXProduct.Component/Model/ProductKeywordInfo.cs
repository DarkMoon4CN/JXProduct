using System;
using System.Collections.Generic;
namespace JXProduct.Component.Model
{
    public class ProductKeywordInfo
    {
        /// <summary>
        ///  商品ID
        ///</summary>
        public Int32 ProductID { get; set; }

        /// <summary>
        ///  标签ID
        ///</summary>
        public Int32 KeywordID { get; set; }

        /// <summary>
        /// 多对多的关系.需要CFID来支持关联
        /// </summary>
        public int CFID { get; set; }

        public string KeywordName { get; set; }
    }
}
