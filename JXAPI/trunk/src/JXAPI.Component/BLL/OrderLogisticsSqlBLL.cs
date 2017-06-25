using JXAPI.Common;
using JXAPI.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrderLogisticsSqlBLL
    {
        private static readonly JXAPI.Component.SQLServerDAL.OrderLogisticsSqlDAL dal = new JXAPI.Component.SQLServerDAL.OrderLogisticsSqlDAL();
        public static OrderLogisticsSqlBLL Instance
        {
            get
            {
                return new OrderLogisticsSqlBLL();
            }
        }
        public OperationResult<int> OrderLogistics_Insert(OrderLogisticsInfo olInfo)
        {
            return dal.OrderLogistics_Insert(olInfo);
        }

        public OperationResult<int> Order_Update(OrdersInfo oInfo, string userName)
        {
            return dal.Order_Update(oInfo, userName);
        }

        public OperationResult<IList<OrdersInfo>> Orders_GetList(int pageIndex, int pageSize, string orderType, string strWhere, int IsHistory, out int recordCount)
        {
            return dal.Orders_GetList(pageIndex, pageSize, orderType, strWhere, 0, out recordCount);
        }

        public OperationResult<IList<OrderProductInfo>> OrderProduct_GetList(string strWhere)
        {
            return dal.OrderProduct_GetList(strWhere);
        }

        public OperationResult<int> OrderLogistics_Delete(int logisticsID)
        {
            return dal.OrderLogistics_Delete(logisticsID);
        }
    }
}
