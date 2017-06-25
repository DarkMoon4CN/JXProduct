using JXBigData.Component.Model;
using JXBigData.Component.SqlServerDAL;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXBigData.Component.BLL
{
    public class OrderProductDailyBLL
    {
        private OrderProductDailyBLL() { }
        private static OrderProductDailyBLL _instance;
        public static OrderProductDailyBLL Instance { get { return _instance ?? (_instance = new OrderProductDailyBLL()); } }
        private static readonly OrderProductDailyDAL dal = new OrderProductDailyDAL();

        public OperationResult<IList<OrderProductDailyInfo>> OrderProductDaily_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            return dal.OrderProductDaily_GetList(pageIndex, pageSize, orderType, strWhere, out recordCount);
        }
    }
}
