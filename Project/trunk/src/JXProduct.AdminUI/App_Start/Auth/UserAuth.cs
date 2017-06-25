using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.App_Start.Auth
{
    public class UserAuth
    {
        public static bool hasAddInfo(string roles)
        {
            return roles.Contains("采购编辑");
        }

        /// <summary>
        /// 管理商品信息权限
        /// </summary>
        public static bool hasEditInfo(string roles)
        {
            return roles.Contains("产品编辑") || hasAudit(roles);
        }

        /// <summary>
        /// 管理商品详情图权限
        /// </summary>
        public static bool hasEditDescription(string roles)
        {
            return roles.Contains("商品详情") || hasAudit(roles) || hasEditInfo(roles);
        }

        /// <summary>
        /// 管理商品价格权限
        /// </summary>
        public static bool hasEditPrice(string roles)
        {
            return roles.Contains("运营报价员");
        }

        /// <summary>
        /// 管理商品资质权限
        /// </summary>
        public static bool hasEditQualification(string roles)
        {
            return roles.Contains("质管编辑") || hasAudit(roles);
        }

        /// <summary>
        /// 审核权限
        /// </summary>
        public static bool hasAudit(string roles)
        {
            return roles.Contains("质管审核");
        }
    }
}