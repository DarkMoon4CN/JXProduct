using JXProduct.Component.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Model
{
    public class ActivityInfo
    {
        public ActivityInfo()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }
        /// <summary>
        ///活动编号
        /// </summary>
        public int ActID { get; set; }

        /// <summary>
        ///名称简写
        /// </summary>
        public string BriefName { get; set; }

        /// <summary>
        ///活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///活动描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///活动地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 全站活动类型  包邮 = 1,满赠 = 2,满减 = 3,满返 = 4,换购 = 5,满折 = 6,直降 = 7,折扣 = 8
        /// </summary>
        public short Type { get; set; }
        public string TypeToString
        {
            get
            {
                return JXUtil.EnumHelper.GetText<ProductActivity>(this.Type);
            }
        }
        /// <summary>
        ///限制条件(1:金额 2:件数)
        /// </summary>
        public short Limit { get; set; }
        public string LimitToString
        {
            get
            {
                switch (this.Limit)
                {
                    case 1: return "金额";
                    case 2: return "件数";
                    default: return string.Empty;
                }
            }
        }
        /// <summary>
        ///活动商品参与活动的数量上限
        /// </summary>
        public int CountLimit { get; set; }

        /// <summary>
        ///用户参与活动的次数上限
        /// </summary>
        public int UserLimit { get; set; }

        /// <summary>
        ///开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        ///最后更新者
        /// </summary>
        public string Updater { get; set; }

        /// <summary>
        ///更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        ///状态： 正常：0  删除：1
        /// </summary>
        public short Status { get; set; }
        public string StatusToString
        {
            get
            {
                if (this.Status == 1)
                    return "下线";
                return "上线";
            }
        }

        /// <summary>
        ///参与用户0：所有会员，1：高级会员
        /// </summary>
        public short UserType { get; set; }
    }
}
