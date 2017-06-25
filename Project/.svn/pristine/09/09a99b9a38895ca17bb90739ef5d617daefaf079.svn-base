using JXProduct.Component.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Model
{
    public class RoleMessagesInfo
    {
        public RoleMessagesInfo()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            this.Status = 0;
            this.RoleID = 0;
            this.RoleName = string.Empty;
        }
        /// <summary>
        ///  消息ID
        /// </summary>
        public int MessageID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息主体
        /// </summary>
        public string Contents { get; set; }


        /// <summary>
        /// 商品ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int TypeID { get; set; }


        /// <summary>
        /// 0 未处理 1已处理
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public int CreatorID { get; set; }

        /// <summary>
        /// 创建者
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

        /// <summary>
        /// 放置url
        /// </summary>
        public string Url
        {
            get
            {
                string url = string.Empty;
                switch ((RoleType)this.RoleID)
                {
                    case RoleType.产品编辑:
                        url = "/product/edit?productid=" + this.PID; break;
                    case RoleType.质管编辑:
                        url = "/audit/edit?productid=" + this.PID; break;
                    case RoleType.运营报价员:
                        url = "/product/edit?productid=" + this.PID + "#price"; break;
                    case RoleType.质管审核:
                    case RoleType.商品信息:
                        url = "/product/edit?productid=" + this.PID; break;
                    case RoleType.商品资质:
                        url = "/audit/edit?productid=" + this.PID; break;
                }
                return url;
            }
        }
    }
}
