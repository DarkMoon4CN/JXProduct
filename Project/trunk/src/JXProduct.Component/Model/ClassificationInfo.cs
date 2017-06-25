using System;
using System.Collections.Generic;
namespace JXProduct.Component.Model
{
    /// <summary>
    /// Classification 
    /// 2015-03-31
    /// lw   16
    /// </summary>
    [Serializable]
    public class ClassificationInfo
    {
        /// <summary>
        ///  分类编号
        ///</summary>
        public Int32 CFID { get; set; }

        /// <summary>
        ///  分类中文名
        ///</summary>
        public String ChineseName { get; set; }

        /// <summary>
        ///  分类拼音
        ///</summary>
        public String PinyinName { get; set; }

        /// <summary>
        ///  分类Logo
        ///</summary>
        public String ImagesLogo { get; set; }

        /// <summary>
        ///  分类等级
        ///</summary>
        public short Level { get; set; }

        /// <summary>
        ///  分类路径
        ///</summary>
        public String Path { get; set; }

        /// <summary>
        ///  父分类号
        ///</summary>
        public Int32 ParentID { get; set; }

        /// <summary>
        ///  分类级数
        ///</summary>
        public short PathCount { get; set; }

        /// <summary>
        ///  显示标题
        ///</summary>
        public string Title { get; set; }

        /// <summary>
        ///  关键词
        ///</summary>
        public String Keywords { get; set; }

        /// <summary>
        ///  分类描述
        ///</summary>
        public String Description { get; set; }

        /// <summary>
        ///  排序
        ///</summary>
        public Int32 Sort { get; set; }

        /// <summary>
        ///  状态 0：可用 1：不可用
        ///</summary>
        public short Status { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public String RootCFID { get; set; }

        /// <summary>
        ///  置顶
        ///</summary>
        public short IsTop { get; set; }

        /// <summary>
        ///  频道首页
        ///</summary>
        public short IsChannelHot { get; set; }

        public short IsKeyParent { get; set; }
        public int KeywordID { get; set; }

        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updater { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
