using System;
using System.Collections.Generic;
using System.Linq;
namespace JXProduct.Component.Model
{
    public class ClassificationParameterToPriceInfo
    {
        /// <summary>
        ///  报价属性编号
        ///</summary>
        public Int32 CFParaPriceID { get; set; }

        /// <summary>
        ///  分类ID
        ///</summary>
        public Int32 CFID { get; set; }

        /// <summary>
        ///  父分类ID（第一级分类的父分类为0）
        ///</summary>
        public Int32 FatherCFID { get; set; }

        /// <summary>
        ///  报价属性名称
        ///</summary>
        public String CFParaPriceName { get; set; }


        /// <summary>
        ///  报价属性对应字段
        ///</summary>
        public String CFParaPriceProp { get; set; }


        /// <summary>
        ///  报价属性值
        ///</summary>
        public String CFParaPriceValue { get; set; }

        private List<string> _proplist;
        public List<string> PropList
        {
            get
            {
                if (_proplist == null)
                {
                    if (!string.IsNullOrEmpty(this.CFParaPriceValue))
                    {
                        _proplist = this.CFParaPriceValue.Split('#').ToList();
                    }
                    else
                    {
                        _proplist = new List<string>();
                    }
                }
                return _proplist;
            }
        }

        /// <summary>
        ///  排序
        ///</summary>
        public short Sort { get; set; }

    }
}
