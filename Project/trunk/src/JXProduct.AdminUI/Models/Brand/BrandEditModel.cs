﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.Models.Brand
{
    public class BrandEditModel
    {
        /// <summary>
        /// 品牌ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// 品牌中文名称
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 品牌英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 注册商标（P库）
        /// </summary>
        public string RegTrademark { get; set; }

        /// <summary>
        /// 非注册商标
        /// </summary>
        public string UnregTrademark { get; set; }

        /// <summary>
        /// 品牌分类(0：中华老字号 1：国际著名品牌 2：知名品牌 3：普通品牌)
        /// </summary>
        public short BrandType { get; set; }

        /// <summary>
        /// Logo图
        /// </summary>
        public string ImageLogo { get; set; }

        /// <summary>
        /// Banner图
        /// </summary>
        public string ImageBanner { get; set; }

        /// <summary>
        /// 品牌等级
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// 品牌路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 父品牌号
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 品牌级数
        /// </summary>
        public short PathCount { get; set; }

        /// <summary>
        /// 品牌标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 品牌描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MetaDescription { get; set; }


        public string Error { get; set; }
    }
}