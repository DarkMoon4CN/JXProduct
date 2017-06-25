using System;
using System.Collections.Generic;
namespace JXProduct.Component.Model
{
    /// <summary>
    /// Manufacturer 
    /// 2015-03-31
    /// lw   15
    /// </summary>
    [Serializable]
    public class ManufacturerInfo
    {
        public ManufacturerInfo()
        {
            this.CreateTime = DateTime.Now;
            this.LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        ///  厂家ID
        ///</summary>
        public Int32 ManuID { get; set; }

        /// <summary>
        ///  企业名称（Q库）
        ///</summary>
        public String Manufacturer { get; set; }

        /// <summary>
        ///  生产地址
        ///</summary>
        public String Address { get; set; }

        /// <summary>
        ///  邮政编码
        ///</summary>
        public String Postalcode { get; set; }

        /// <summary>
        ///  电话
        ///</summary>
        public String Phone { get; set; }

        /// <summary>
        ///  药物安全咨询电话
        ///</summary>
        public String ConsultPhone { get; set; }

        /// <summary>
        ///  售后服务电话
        ///</summary>
        public String ServicePhone { get; set; }

        /// <summary>
        ///  驻中国办事处（仅进口药）
        ///</summary>
        public String Office { get; set; }

        /// <summary>
        ///  传真号码
        ///</summary>
        public String Fax { get; set; }

        /// <summary>
        ///  注册地址
        ///</summary>
        public String RegAddress { get; set; }

        /// <summary>
        ///  网址
        ///</summary>
        public String Site { get; set; }

        /// <summary>
        ///  创建者
        ///</summary>
        public String Creator { get; set; }

        /// <summary>
        ///  创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///  最后更新者
        ///</summary>
        public String LastUpdate { get; set; }

        /// <summary>
        ///  最后更新时间
        ///</summary>
        public DateTime LastUpdateTime { get; set; }

    }
}
