using JXProduct.Component.BLL;
using JXProduct.Component.Enums;
using JXProduct.Component.Model;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JXProduct.AdminUI.App_Start.Mail
{
    public class SendAuditMail
    {
        private static List<AuditStuffEmailInfo> _emails;
        private static Dictionary<string, string[]> _titles;
        public static List<AuditStuffEmailInfo> Emails
        {
            get { if (_emails == null) _emails = AuditStuffEmailBLL.Instance.GetList(); return _emails; }
            set { _emails = value; }
        }
        public static Dictionary<string, string[]> Titles
        {
            get
            {
                if (_titles == null)
                {
                    _titles = new Dictionary<string, string[]>();

                    var url = "http://admin1.jxdyf.com";

                    _titles.Add("11", new string[] { "以下商品需要添加资质（包括企业资质和商品资质）", url + "/audit/edit" });
                    _titles.Add("12", new string[] { "以下商品资质审核不通过，请尽快完善，以免耽误销售", url + "/audit/edit" });
                    _titles.Add("13", new string[] { "以下商品资质已通过审核", url + "/audit/edit" });

                    _titles.Add("21", new string[] { "以下商品需要添加商品基本信息（包括基础信息、五张图、商品详情图片以及商品说明书）", url + "/product/edit" });
                    _titles.Add("22", new string[] { "以下商品基本信息审核不通过，请尽快完善，以免耽误销售", url + "/product/edit" });
                    _titles.Add("23", new string[] { "以下商品基本信息已通过审核", url + "/product/edit" });

                    _titles.Add("31", new string[] { "以下商品已经完善基本信息和商品资质，请尽快审核，以免耽误销售", url + "/audit/edit" });
                    _titles.Add("32", new string[] { "以下商品更改了基本信息/商品资质/商品详情图片，请尽快审核，以免耽误销售", url + "/product/preview" });
                    _titles.Add("33", new string[] { "以下商品已经完善商品详情图片，请尽快审核，以免耽误销售", url + "/product/preview" });
                }
                return _titles;
            }
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="EmailType">类别：1: 质管部商品信息维护  2:产品部商品信息维护  3:质管部信息审核</param>
        /// <param name="EmailStatus">邮件状态:[类别1,2] 对应 1,2,3 是新品,不通过,已通过  [类别3] 对应的1,2,3 商品资质,商品信息,商品详情图</param>
        /// <param name="audit">审核信息</param>
        /// <param name="products">商品信息</param>
        private static void Send(int EmailType, int EmailStatus, ProductAuditInfo audit, List<ProductInfo> products)
        {
            try
            {
                if (null != products && products.Count == 0)
                    return;

                var emails = SendAuditMail.Emails.Where(t => t.Type == EmailType);
                if (emails.Count() == 0)
                    return;



                //根据type分类
                //标题
                var info = SendAuditMail.Titles[EmailType.ToString() + EmailStatus.ToString()];
                string title = info[0];
                string url = info[1];


                //内容
                var index = 1;
                var sb = new StringBuilder();
                sb.AppendFormat("<div style='width:100%;height:60px;'>{0}</div>", title);

                foreach (var p in products)
                {
                    sb.AppendFormat("<div>{0}、商品ID:{1}，名称：<a href='" + url + "?productid={1}' target='_blank'>{2}</a></div>", index, p.ProductID, p.ChineseName);
                    index++;
                }
                if (audit != null)
                {
                    if (audit.Status == (int)Product.AuditStatus.未通过审核)
                    {
                        sb.AppendFormat("<div>未通过审核原因：{0}</div>", audit.Remarks);
                        sb.AppendFormat("<div>审核人：{0}  审核时间：{1}</div>", audit.Creator, audit.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }

                var config = JXProduct.ConfigComponent.AuditEmailConfig.Instance;
                var mail = new MailHelper();
                mail.MailDomain = config.DoMain;
                mail.MailDomainPort = config.Port;
                mail.MailServerUserName = config.UserName;
                mail.MailServerPassWord = config.Password;
                mail.From = config.From;
                mail.FromName = "金象后台管理系统";
                mail.AddRecipient(emails.Select(t => t.UserEmail).ToArray());
                mail.Subject = title;
                mail.Html = true;
                mail.Body = sb.ToString();
                mail.Send();
            }
            catch (Exception e)
            {
                //OperateLogBLL.Instance.OperateLog_Insert(0, "发送邮件","", e.Message);
                //Log

                using (var sw = new System.IO.StreamWriter(HttpRuntime.AppDomainAppPath + "/emaillog.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "   " + e.Message);
                }
            }
        }
    }
}