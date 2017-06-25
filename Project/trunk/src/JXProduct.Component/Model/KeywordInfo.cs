using System;

namespace JXProduct.Component.Model
{
    [Serializable]
    public class KeywordInfo
    {
        public KeywordInfo()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 关键词ID
        /// </summary>
        public int KeywordID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string PinyinName { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        public string FirstLetter { get; set; }

        /// <summary>
        /// 商品数
        /// </summary>
        public int ProductCount { get; set; }

        /// <summary>
        /// 关联数
        /// </summary>
        public int RelationCount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// 类型 0=关键词 1=疾病 2=分类
        /// </summary>
        public short TypeID { get; set; }

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
