using System.Collections.Generic;
using JXProduct.Component.Model;

namespace JXProduct.Component.BLL
{
	public class BrandBLL
	{
        private BrandBLL() { }
        private static BrandBLL _instance;
        public static BrandBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new BrandBLL());
            }
        }
		private static readonly SQLServerDAL.BrandDAL dal = new SQLServerDAL.BrandDAL() ;

	    /// <summary>
		/// Brand_Insert Method
		/// </summary>
		/// <param name="BrandInfo">Brand object</param>
		/// <returns>The new ID : int </returns>
	    public int Brand_Insert(BrandInfo brandInfo)
		{	
			return  dal.Brand_Insert(brandInfo);
		}		
		
		/// <summary>
		/// Brand_Update Method		
		/// </summary>
		/// <param name="BrandInfo">Brand object</param>
		/// <returns>true:成功 false:失败</returns>
		public bool Brand_Update(BrandInfo brandInfo) 
		{
			return  dal.Brand_Update(brandInfo);
		}	
		
		
		/// <summary>
		/// Brand_Get Method
		/// </summary>
		/// <param name="brandID">Brand Main ID</param>
		/// <returns>BrandInfo get from Brand table.</returns>	
		public BrandInfo Brand_Get(int brandID)
		{
			return  dal.Brand_Get(brandID);
		}

        /// <summary>
        /// Brand_Get Method
        /// </summary>
        /// <param name="brandIDs">Some Brand ID</param>
        /// <returns>BrandInfo get from Brand table.</returns>	
        public List<BrandInfo> Brand_Get(string brandIDs)
        {
            return dal.Brand_Get(brandIDs);
        }
		
		/// <summary>
		/// Brand_Delete Method
		/// </summary>
		/// <param name="brandID">Brand Main ID</param>
		/// <returns>true:成功 false:失败</returns>	
		public bool Brand_Delete(int brandID) 
		{
			return  dal.Brand_Delete(brandID);
		}
		
		/// <summary>
		/// Brand_GetList Method
		/// </summary>
		/// <param name="pageIndex">起始页码</param>
		/// <param name="pageSize">每页数据数</param>
		/// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
		/// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
		/// <param name="recordCount">总记录数</param>
		/// <returns>A Generic List of BrandInfo</returns>
		public IList<BrandInfo> Brand_GetList(int pageIndex,int pageSize,string orderType , string strWhere , out int recordCount)
		{
			return dal.Brand_GetList( pageIndex, pageSize, orderType , strWhere , out recordCount );
		}

        /// <summary>
        /// Brand_GetByCFID Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">SellCount 与 ProductCount  排序 </param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of BrandInfo</returns>
        public IList<BrandInfo> Brand_GetByCFID(int cfid,int pageIndex, int pageSize, string orderType, out int recordCount)
        {
            return dal.Brand_GetByCFID(cfid, pageIndex, pageSize, orderType, out recordCount);
        }
	}	
}
