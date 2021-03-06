﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 图片上传
    /// </summary>
    public class UpLoadImageRequest
    {
        /// <summary>
        /// 图片的二进制字符串
        /// </summary>
        public string ImageByte { get; set; }

        private string _sourceType;
        /// <summary>
        /// 图片来源（web m ios andriod）
        /// </summary>
        public string SourceType
        {
            get
            {
                if (this._sourceType == null || this._sourceType == string.Empty)
                {
                    return "web";
                }
                return this._sourceType;
            }
            set
            { this._sourceType = value; }
        }

        private string _extName;
        /// <summary>
        /// 后缀名 默认为jpg
        /// </summary>
        public string ExtName
        {
            get
            {
                if (this._extName == null || this._extName == string.Empty)
                {
                    return "jpg";
                }
                return this._extName;
            }
            set
            { this._extName = value; }
        }

        /// <summary>
        /// 类型 此字段作为存储的依据
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 文件名 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///  调用的apiName
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        ///  子级文件夹路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 用于多张图队列下标
        /// </summary>
        public int Index { get; set; }
    }

    public class ImagePathRequest 
    {
        public string ImagePath{get;set;}
        public string Url { get; set; }
    }
}
