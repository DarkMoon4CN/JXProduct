using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class ExpressNewsBLL
    {
        private ExpressNewsBLL() { }
        private static ExpressNewsBLL _instance;
        public static ExpressNewsBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ExpressNewsBLL());
            }
        }
        private static readonly JXProduct.Component.SQLServerDAL.ExpressNewsDAL dal = new JXProduct.Component.SQLServerDAL.ExpressNewsDAL();

        /// <summary>
        /// ExpressNews_Insert Method
        /// </summary>
        /// <param name="expressNewsInfo">ExpressNews object</param>
        /// <returns>The new ID : int </returns>
        public int ExpressNews_Insert(ExpressNewsInfo expressNewsInfo)
        {
            return dal.ExpressNews_Insert(expressNewsInfo);
        }

        /// <summary>
        /// ExpressNews_Update Method		
        /// </summary>
        /// <param name="expressNewsInfo">ExpressNews object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool ExpressNews_Update(ExpressNewsInfo expressNewsInfo)
        {
            return dal.ExpressNews_Update(expressNewsInfo);
        }

        /// <summary>
        /// ExpressNews_Delete Method		
        /// </summary>
        /// <param name="newsID">ExpressNews Main ID</param>
        /// <returns>true:成功 false:失败</returns>
        public bool ExpressNews_Delete(int  newsID)
        {
            return dal.ExpressNews_Delete(newsID);
        }

        /// <summary>
        /// ExpressNews_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of ExpressNewsInfo</returns>
        public IList<ExpressNewsInfo> ExpressNews_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            return dal.ExpressNews_GetList(pageIndex, pageSize, orderType, strWhere, out recordCount);
        }

        /// <summary>
        /// ExpressNews_Get Method
        /// </summary>
        /// <param name="newsID">ExpressNewsInfo Main ID</param>
        /// <returns>ExpressNewsInfo get from newsID table.</returns>	
        public ExpressNewsInfo ExpressNews_Get(int newsID)
        {
            return dal.ExpressNews_Get(newsID)[0];
        }

    }
}
