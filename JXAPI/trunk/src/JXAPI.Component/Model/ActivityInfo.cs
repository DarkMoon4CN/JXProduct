using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    [Serializable]
    public class ActivityInfo
    {
        /// <summary>
        /// 活动编号
        /// </summary>
        public int ActID { get; set; }

        /// <summary>
        /// 名称简写
        /// </summary>
        public string BriefName { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 活动地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 活动类型(1:满减 2:满折 3:满赠 4:满返券 5:免运费)
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// 限制条件(1:金额 2:件数)
        /// </summary>
        public short Limit { get; set; }

        /// <summary>
        /// 活动商品参与活动的数量上限
        /// </summary>
        public int CountLimit { get; set; }

        /// <summary>
        /// 用户参与活动的次数上限
        /// </summary>
        public int UserLimit { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 最后更新者
        /// </summary>
        public string Updater { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 状态： 正常：0  删除：1
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// 参与用户0：所有会员，1：高级会员
        /// </summary>
        public short UserType { get; set; }
    }
}
