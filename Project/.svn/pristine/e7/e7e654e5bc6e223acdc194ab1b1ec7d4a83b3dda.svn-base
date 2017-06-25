using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Model
{
    public class AuditStuffEmailInfo
    {
        public AuditStuffEmailInfo()
        {
            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 自增长ID
        /// </summary>
        public int EmailID { get; set; }

        /// <summary>
        /// 类别：1: 质管部信息维护  2:产品部信息维护  3:质管部信息审核
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// 相关工作人员姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 相关工作人员邮件
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}