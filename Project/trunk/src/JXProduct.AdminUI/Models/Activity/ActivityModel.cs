﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace JXProduct.AdminUI.Models.Activity
{
    public class ActivityModel
    {
        public ActivityModel()
        {
            this.StartTime = DateTime.Now.AddDays(1);
            this.EndTime = DateTime.Now.AddDays(7);
        }
        public int ActID { get; set; }

        [Required(ErrorMessage = "请填写名称")]
        [StringLength(64, ErrorMessage = "名称太长了")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请填写简写")]
        [StringLength(64, ErrorMessage = "请填写简写")]
        public string BriefName { get; set; }

        [Required(ErrorMessage = "请填写描述")]
        [StringLength(64, ErrorMessage = "只能有64个字符以下")]
        public string Description { get; set; }

        [StringLength(128, ErrorMessage = "请填写正确活动URL")]
        [RegularExpression("^[a-zA-z]+://[^\\s]*$", ErrorMessage = "必须以http://开始")]
        public string URL { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "请选择活动类型")]
        public short Type { get; set; }

        [Required]
        [Range(0, 2, ErrorMessage = "请选择限制条件")]
        public short Limit { get; set; }

        [Required(ErrorMessage = "开始时间必须选择")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "结束时间必须选择")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndTime { get; set; }
    }
}
