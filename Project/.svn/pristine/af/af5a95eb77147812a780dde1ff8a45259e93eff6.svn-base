using JXProduct.AdminUI.App_Start.Mail;
using JXProduct.Component.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXProduct.AdminUI.App_Start.Filters
{
    public class ProductPermission
    {
        //检查是否有编辑权限，如果没有跳转到显示页面
        /// <summary>
        /// 检查是否有编辑权限，如果没有跳转到显示页面
        /// </summary>
        public static bool CheckEdit(string employeename)
        {
            //return;
            return SendAuditMail.Emails.Any(t => t.UserName == employeename && t.Type == 2);
        }

        //验证权限,如果没有权限,跳转显示页面
        /// <summary>
        /// 验证权限,如果没有权限,跳转显示页面
        /// </summary>
        public static bool CheckQualification(string employeename)
        {
            return SendAuditMail.Emails.Any(t => t.UserName == employeename && t.Type == 1);
        }

        //检查有没有审核权限
        /// <summary>
        /// 检查有没有审核权限
        /// </summary>
        public static bool CheckAudit(string employeename)
        {
            return SendAuditMail.Emails.Any(t => t.UserName == employeename && t.Type == 3);
        }
    }
}