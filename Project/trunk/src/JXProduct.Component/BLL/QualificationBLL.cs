using JXProduct.Component.Enums;
using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class QualificationBLL
    {
        private QualificationBLL() { }
        private static QualificationBLL _instance;
        public static QualificationBLL Instance { get { return _instance ?? (_instance = new QualificationBLL()); } }
        private static readonly SQLServerDAL.QualificationDAL dal = new SQLServerDAL.QualificationDAL();


        public DateTime ExpireMonth { get { return DateTime.Now.AddMonths(3); } }
        #region 获取商品是否存在即将过期的审核

        public Dictionary<int, int> Qualification_GetExpire(List<int> productids)
        {
            if (productids == null || productids.Count > 0)
            {
                var ids = string.Join(",", productids);
                return dal.Qualification_GetExpire(ids);
            }
            return new Dictionary<int, int>();

        }

        /// <summary>
        /// 获取企业的过期时间 目前是3个月
        /// </summary>
        /// <param name="mid">企业ID</param>
        /// <returns></returns>
        public Dictionary<int, int> Qualification_GetManuExpire(int mid)
        {
            return dal.Qualification_GetManuExpire(mid);
        }
        #endregion

        /// <summary>
        /// 获取企业的信息
        /// </summary>
        /// <param name="mid">企业id</param>
        /// <param name="type">0 企业资质  1企业分类资质</param>
        /// <param name="categoryID">分类ID</param>
        /// <returns></returns>
        public List<ProductQualificationInfo> Qualification_GetManufacturerList(int mid, int type, int categoryID)
        {
            return dal.Qualification_GetManufacturerList(mid, type, categoryID);
        }

        public List<ProductQualificationInfo> Qualification_GetProductList(int productid)
        {
            return dal.Qualification_GetProductList(productid);
        }

        public int Qualification_Insert(ProductQualificationInfo info)
        {
            return dal.Qualification_Insert(info);
        }

        public List<QualificationCategoryInfo> Qualification_GetCategory()
        {
            return dal.Qualification_GetCategory();
        }
    }
}
