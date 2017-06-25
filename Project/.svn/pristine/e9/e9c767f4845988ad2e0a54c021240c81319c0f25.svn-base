using JXProduct.Component.Enums;
using JXProduct.Component.Model;
using JXProduct.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class ProductProcessBLL
    {
        private static ProductProcessBLL _instance;
        private static ProductProcessDAL dal;
        private ProductProcessBLL() { dal = new ProductProcessDAL(); }
        public static ProductProcessBLL Instance
        {
            get { return _instance ?? (_instance = new ProductProcessBLL()); }
        }

        public ProductProcessInfo Get(int productid)
        {
            return dal.Get(productid);
        }

        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="productid">商品ID</param>
        /// <param name="pp">进度</param>
        /// <param name="Updater">更新人</param>
        /// <param name="isdefault">驳回时,状态还原</param>
        /// <returns></returns>
        public bool Update(int productid, Product.Process pp, string Updater, bool isdefault = false)
        {
            var process = this.Get(productid) ?? new ProductProcessInfo(productid, 0, 0, 0, 0, Updater);
            switch (pp)
            {
                case Product.Process.产品信息维护:
                    process.Process1 = (byte)(isdefault ? 0 : 1);
                    process.Process3 = 0;
                    break;
                case Product.Process.质管部信息维护:
                    process.Process2 = (byte)(isdefault ? 0 : 1);
                    process.Process3 = 0;
                    break;
                case Product.Process.质管部信息审核:
                    process.Process3 = 1;
                    break;
                case Product.Process.运营部价格维护:
                    process.Process4 = 1;
                    RoleMessagesBLL.Instance.RoleMessages_SetProcessed(RoleType.运营报价员, productid);
                    break;
                default: break;
            }

            return this.Save(process);
        }

        private bool Save(ProductProcessInfo info)
        {
            return dal.Save(info);
        }

        public bool Product_RoleMessage_Save(int productid, RoleType rt, string updater, bool isRebut = false, ProductAuditInfo audit = null)
        {
            var message = new RoleMessagesInfo();
            var product = ProductBLL.Instance.Product_Get(productid);

            if (product == null)
                return false;

            message.Title = string.Concat(product.TradeName, product.CADN, "   规格:", product.Specifications);
            message.RoleID = (int)rt;
            message.RoleName = rt.ToString();
            message.PID = productid;
            message.Creator = updater;
            message.Updater = updater;

            if (isRebut)
            {
                //Product 需要驳回至编辑状态
                if (audit == null)
                    return false;
                switch (rt)
                {
                    case RoleType.产品编辑:
                        message.Contents = string.Concat(JXUtil.EnumHelper.GetText<Product.AuditType>(audit.Type), " 不通过审核原因：", audit.Remarks, "<br/>", "审核人:", audit.Updater);
                        break;
                    case RoleType.质管编辑:
                        message.Contents = string.Concat("不通过审核原因：", audit.Remarks, "<br/>", "审核人:", audit.Updater);
                        break;
                }
            }
            else if (audit != null && audit.Status == 1)
            {
                //通过审核时
                switch (rt)
                {
                    case RoleType.产品编辑:
                        message.Contents = "已通过审核!";
                        break;
                    case RoleType.质管编辑:
                        message.Contents = "已通过审核!";
                        break;
                }
            }
            else
            {
                //正常流程
                switch (rt)
                {
                    case RoleType.产品编辑:
                        message.Contents = "请及时添加商品基础信息,以免耽误销售!";
                        break;
                    case RoleType.质管编辑:
                        message.Contents = "请及时添加商品资质和企业资质信息,以免耽误销售!";
                        break;
                    case RoleType.商品信息:
                        message.Contents = "请及时审核基础信息、商品资质以及企业资质,以免耽误销售!";
                        RoleMessagesBLL.Instance.RoleMessages_SetProcessed(RoleType.产品编辑, productid);
                        break;
                    case RoleType.商品资质:
                        message.Contents = "请及时审核基础信息、商品资质以及企业资质,以免耽误销售!";
                        RoleMessagesBLL.Instance.RoleMessages_SetProcessed(RoleType.质管编辑, productid);
                        break;
                    case RoleType.运营报价员:
                        if (product.TradePrice > 0)
                            return true;
                        message.Contents = "请尽快设置价格，以免耽误销售!";
                        break;
                    default:
                        break;
                }
            }

            //处理审核状态 价格在上面 #52
            switch (rt)
            {
                case RoleType.商品信息:
                    RoleMessagesBLL.Instance.RoleMessages_SetProcessed(RoleType.产品编辑, productid);
                    break;
                case RoleType.商品资质:
                    RoleMessagesBLL.Instance.RoleMessages_SetProcessed(RoleType.质管编辑, productid);
                    break;
                case RoleType.产品编辑:
                    RoleMessagesBLL.Instance.RoleMessages_SetProcessed(RoleType.商品信息, productid);
                    break;
                case RoleType.质管编辑:
                    RoleMessagesBLL.Instance.RoleMessages_SetProcessed(RoleType.商品资质, productid);
                    break;
            }
            return RoleMessagesBLL.Instance.RoleMessages_Insert(message) > 0;
        }
    }
}