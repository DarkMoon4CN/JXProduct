using System;
using System.Collections.Generic;
namespace JXProduct.Component.Model
{
    /// <summary>
    /// 科室
    /// </summary>
    [Serializable]
    public class SectionInfo
    {
        public SectionInfo()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }

        /// <summary>
        ///科室ID
        /// </summary>
        public int SectionID { get; set; }

        /// <summary>
        ///科室名称
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        ///拼音
        /// </summary>
        public string SpellName { get; set; }

        /// <summary>
        ///路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///父科室ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        ///状态  0：正常 1：删除
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// 关键字ID
        /// </summary>
        public int KeywordID { get; set; }
        public short IsKeyParent { get; set; }
        public int Level { get; set; }



        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updater { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
