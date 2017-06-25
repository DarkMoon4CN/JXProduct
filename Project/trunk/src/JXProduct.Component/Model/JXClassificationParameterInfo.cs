using System;
using System.Collections.Generic;
namespace JXProduct.Component.Model
{
    [Serializable]
    public class JXClassificationParameterInfo
    {
        /// <summary>
        ///  分类属性编号
        ///</summary>
        public Int32 JXParaID { get; set; }

        /// <summary>
        ///  分类编号
        ///</summary>
        public Int32 JXCFID { get; set; }

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

        /// <summary>
        ///  属性标签 0：商品说明书 1：商品补充说明)
        ///</summary>
        public short ParaLabel { get; set; }

        /// <summary>
        /// 商品对应的标签数据
        /// </summary>
        public string ParaValue { get; set; }
    }
}
