using System;
using System.Linq;
using System.Collections.Generic;
namespace JXProduct.Component.Model
{
    /// <summary>
    /// ClassficationParameter 
    /// 2015-03-31
    /// lw   9
    /// </summary>
    [Serializable]
    public class ClassificationParameterInfo
    {
        /// <summary>
        ///  属性编号
        ///</summary>
        public Int32 ParaID { get; set; }

        /// <summary>
        ///  分类编号
        ///</summary>
        public Int32 CFID { get; set; }

        /// <summary>
        ///  属性名称
        ///</summary>
        public String ParaName { get; set; }

        /// <summary>
        ///  属性类型（0：输入框 1：单选框 2：复选框）
        ///</summary>
        public short ParaType { get; set; }

        /// <summary>
        ///  属性长度
        ///</summary>
        public Int32 ParaLength { get; set; }

        /// <summary>
        ///  属性默认值#分割
        ///</summary>
        public String ParaValueList { get; set; }

        public List<string> ParaValueListToCollection
        {
            get
            {
                var list = new List<string>();
                if (!string.IsNullOrWhiteSpace(ParaValueList))
                {
                    return this.ParaValueList.Split('#').ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
        }

        /// <summary>
        ///  排序
        ///</summary>
        public Int32 Sort { get; set; }

        /// <summary>
        ///  是否允许搜索(0：不允许 1：允许）
        ///</summary>
        public short IsSearch { get; set; }

        /// <summary>
        ///  是否允许为空（0：允许，1：不允许）
        ///</summary>
        public short IsNull { get; set; }

    }
}
