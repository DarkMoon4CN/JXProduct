using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class MarketBrandBLL
    {
        private MarketBrandBLL() { }
        private static MarketBrandBLL _instance;
        private static readonly JXProduct.Component.SQLServerDAL.MarketBrandDAL dal = new JXProduct.Component.SQLServerDAL.MarketBrandDAL();
        public static MarketBrandBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new MarketBrandBLL());
            }
        }


        /// <summary>
        /// 根据CFID 查询
        /// </summary>
        /// <param name="cfid"></param>
        /// <returns></returns>
        public List<MarketBrandInfo> Brand_GetByCFID(int cfId,int brandId=0)
        {
            return dal.Brand_GetByCFID(cfId, brandId);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="brandInfo">brand实体</param>
        /// <returns></returns>
        public bool Brand_Insert(MarketBrandInfo brandInfo) 
        {
            return dal.Brand_Insert(brandInfo);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="brandInfo">brand实体</param>
        /// <returns></returns>
        public bool Brand_Delete(MarketBrandInfo brandInfo)
        {
            return dal.Brand_Delete(brandInfo);
        }
    }
}
