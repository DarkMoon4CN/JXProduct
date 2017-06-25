using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXAPI.Component.Model
{
    public class UpLoadImageInfo
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
        ///  子级文件夹路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 用于多张图队列下标
        /// </summary>
        public int Index { get; set; }

    }

    public class ImageConfigInfo
    {
        private string _savepath;
        /// <summary>
        /// 保存路径 默认："E:\upload\"
        /// </summary>
        public string SavePath
        {
            get
            {
                if (this._savepath == null || this._savepath == string.Empty)
                {
                    return @"E:\upload\";
                }
                return this._savepath;
            }
            set
            { this._savepath = value; }
        }
        private string _filename;
        /// <summary>
        /// 文件名或图片名 默认：product
        /// </summary>
        public string FileName 
        {
            get
            {
                if (this._filename == null || this._filename == string.Empty)
                {
                    return "product";
                }
                return this._filename;
            }
            set
            { this._filename = value; }
        }
        private string _url;
        /// <summary>
        /// 文件链接路径 默认：http://img.jxdyf.com/product/
        /// </summary>
        public string URL 
        {
            get
            {
                if (this._url == null || this._url == string.Empty)
                {
                    return "http://img.jxdyf.com/product/";
                }
                return this._url;
            }
            set
            { this._url = value; }
        }
        private int _size;
        /// <summary>
        /// 文件大小 默认:1024
        /// </summary>
        public int Size 
        {
            get
            {
                if (this._size == 0)
                {
                    return 1024;
                }
                return this._size;
            }
            set
            { this._size = value; }
        }
        private string _watermark;
        /// <summary>
        /// 水印图片
        /// </summary>
        public string Watermark 
        {
            get
            {
                if (this._watermark == null || this._watermark == string.Empty)
                {
                    return "E:\new_-LOGO740.png";
                }
                return this._watermark;
            }
            set
            { this._watermark = value; }
        }
        /// <summary>
        /// 保存图片时的配置
        /// </summary>
        public List<SaveConfigInfo> SaveConfig { get; set; }

         
    }

    public class SaveConfigInfo 
    {
        public string Key { get; set; }
        public int Size { get; set; }
        public string Suffix { get; set; }
    }
     
}