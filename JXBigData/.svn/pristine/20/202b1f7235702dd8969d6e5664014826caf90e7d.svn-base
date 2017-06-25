using JXBigData.Component.Model;
using JXBigData.Component.SqlServerDAL;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXBigData.Component.BLL
{
    public class OrderDailyBLL
    {
        private OrderDailyBLL() { }
        private static OrderDailyBLL _instance;
        public static OrderDailyBLL Instance { get { return _instance ?? (_instance = new OrderDailyBLL()); } }
        private static readonly OrderDailyDAL dal = new OrderDailyDAL();

        public OperationResult<IList<OrderDailyInfo>> OrderDaily_GetSourcePrice(DateTime startTime, DateTime endTime)
        {
            return dal.OrderDaily_GetSourcePrice(startTime, endTime);
        }
        public OperationResult<IList<OrderDailyInfo>> OrderDaily_GetStationePrice(DateTime startTime, DateTime endTime) 
        {
            return dal.OrderDaily_GetStationePrice(startTime,endTime);
        }

        public OperationResult<IList<OrderDailyInfo>> OrderDaily_GetSUMAll(DateTime startTime, DateTime endTime) 
        {
            return dal.OrderDaily_GetSUMAll(startTime, endTime);
        }
    }
}
