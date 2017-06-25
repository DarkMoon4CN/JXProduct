using JXAPI.Component.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public partial class OrdersMySqlDAL
    {
        /// <summary>
        /// 查询出所有将要超时订单 
        /// </summary>
        /// <param name="numberHour">订单超时时间</param>
        /// <returns></returns>
        public IList<OrdersInfo> OrdersTimeOutMySql_GetAll(int numberHour)
        {
            string startTime = DateTime.Now.AddHours(-numberHour).ToString("yyyy-MM-dd");
            string endTime = DateTime.Parse(startTime).AddDays(1).AddSeconds(-1).ToString();
            IList<OrdersInfo> couponInfoList = new List<OrdersInfo>();
            string sql = string.Empty;
            sql += " SELECT UID,OrderID,CreateTime FROM  Orders ";
            sql += " WHERE  PayStatus=0 AND PaymentMethodID>0 AND  orderstatus=0 ";
            sql += " AND  CreateTime > '{0}'";
            sql += " AND  CreateTime < '{1}'";
            sql += " GROUP BY UID ";
            sql = string.Format(sql, startTime,endTime);
            DbCommand cmd = dbr.GetSqlStringCommand(sql);
            using (IDataReader dataReader = dbr.ExecuteReader(cmd))
            {
                while (dataReader.Read())
                {
                    couponInfoList.Add(OrdersRecoverModel(dataReader));
                }
            }
            return couponInfoList;
        }

        /// <summary>
        /// 从 IDataReader 中恢复OrdersInfo对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        public OrdersInfo OrdersRecoverModel(IDataReader dataReader)
        {
            OrdersInfo ordersInfo = new OrdersInfo();
            ordersInfo.UID = dataReader["UID"].ToInt();
            ordersInfo.OrderID = dataReader["OrderID"].ToString();
            ordersInfo.CreateTime = dataReader["CreateTime"].ToDateTime();
            return ordersInfo;
        }
    }
}
