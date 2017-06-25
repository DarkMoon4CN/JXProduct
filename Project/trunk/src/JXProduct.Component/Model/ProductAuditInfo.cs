﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Model
{
    public class ProductAuditInfo
    {
        public ProductAuditInfo()
        {
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// AuditType
        /// </summary>
        public short Type { get; set; }
        public string Remarks { get; set; }
        /// <summary>
        ///审核状态  0未审核  1：审核通过 2：未通过
        /// </summary>
        public short Status { get; set; }


        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updater { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}