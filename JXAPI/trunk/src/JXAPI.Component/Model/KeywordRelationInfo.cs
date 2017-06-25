using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    [Serializable]
    public class KeywordRelationInfo
    {
        /// <summary>
        /// 关键词ID
        /// </summary>
        public int KeywordID { get; set; }

        /// <summary>
        /// 关联关键词
        /// </summary>
        public int ChildKeywordID { get; set; }


        //排序
        public short Sort { get; set; }
    }
}
