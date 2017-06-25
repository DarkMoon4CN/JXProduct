using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JXAPI.Component.Model;
using JXAPI.Component.IBLL;
using System.Data;
namespace JXAPI.Component.BLL
{
    public class ProductBLL: IProductPlanBLL
    {
        private static readonly JXAPI.Component.SQLServerDAL.ProductDAL dal = new JXAPI.Component.SQLServerDAL.ProductDAL();
        public static ProductBLL Instance
        {
            get
            {
                return new ProductBLL();
            }
        }

        #region CURD

        public DataTable GetAddList(int StockID, string updateTime, int pageSize,string tableName)
        {
            return dal.GetAddList(StockID, updateTime, pageSize, tableName);
        }

        public DataTable GetUpdateList(int StockInitID, int StockNowID, string updateTime, int pageSize, string tableName)
        {
            return dal.GetUpdateList(StockInitID, StockNowID, updateTime, pageSize, tableName);
        }

        #endregion

        public DataTable Product_ListForSearch(string strWhere)
        {
            return dal.Product_ListForSearch(strWhere);
        }
        
    }
}