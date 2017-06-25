using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using JXAPI.Component.Model;
using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;

namespace JXAPI.Component.BLL
{
    public class OrdersSqlBLL
    {
        private static readonly OrdersSqlDAL dal = new OrdersSqlDAL();

        public static OrdersSqlBLL Instance
        {
            get
            {
                return new OrdersSqlBLL();
            }
        }

        /// <summary>
        /// 查询待处理订单
        /// </summary>
        /// <returns></returns>
        public DataTable GetList( string createTime, string tableName)
        {
            return dal.GetList(string.Empty, createTime, 0, 0, tableName, 0);
        }

        /// <summary>
        /// 查询待处理订单
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string id, string createTime, int start, int pageSize, string tableName, int status = 0)
        {
            return dal.GetList(id, createTime, start, pageSize, tableName, status);
        }

        /// <summary>
        /// 查询三方运单号
        /// </summary>
        /// <param name="ThirdOrderId">三方单号</param>
        /// <returns></returns>
        public OrdersInfo GetOrderIDByThirdOrderID(string thirdOrderID)
        {
            return dal.GetOrderIDByThirdOrderID(thirdOrderID);
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="OrderXML">订单数据XML</param>
        /// <param name="Result">添加状态</param>
        public void Orders_InsertByXML(string OrderXML, out int Result)
        {
            dal.Orders_InsertByXML(OrderXML, out Result);
        }

        /// <summary>
        /// 更新订单 指定列的值
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <param name="colName">列名</param>
        /// <param name="colValue">列值</param>
        /// <returns>true、false</returns>
        public bool Orders_Update(string orderID, string[] colName, object[] colValue)
        {
            return dal.Orders_Update(orderID, colName, colValue);
        }

        /// <summary>
        /// 订单状态修改
        /// </summary>
        /// <param name="log">状态日志内容</param>
        /// <param name="disposeType">修改类型，1=支付状态，2=订单状态</param>
        /// <returns>返回：0=成功，1=失败，其他自定义错误</returns>
        public string Orders_Dispose(OrderOperateLogInfo log, int disposeType)
        {
            return dal.Orders_Dispose(log, disposeType);
        }

        /// <summary>
        /// 订单日志
        /// </summary>
        /// <param name="ool"></param>
        /// <returns></returns>
        public string OrderOperateLog_Insert(OrderOperateLogInfo ool)
        {
            return dal.OrderOperateLog_Insert(ool);
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        public OrdersInfo GetByOrderId(string orderId)
        {
            return dal.GetByOrderId(orderId);
        }
        
        /// <summary>
        /// 根据收货人电话、手机号查询订单信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public OrdersInfo GetByOrderId(string orderId, string mobile)
        {
            return dal.GetByOrderId(orderId, mobile);
        }

        /// <summary>
        /// 查询订单商品
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        public IList<OrderProductInfo> GetOrderProduct(string orderId)
        {
            return dal.GetOrderProduct(orderId);
        }

        /// 新增联盟订单
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="unionPara">网盟唯一码</param>
        /// <param name="source">来源</param>
        /// <param name="advertId">广告ID</param>
        /// <param name="siteId">站点ID</param>
        /// <param name="clientIP">IP</param>
        /// <param name="clientIdentity">联盟商识别码</param>
        /// <param name="customerInfo">联盟商自定义参数</param>
        /// <returns></returns>
        public string UnionOrders_Insert(int orderId, string unionPara, string source, int advertId, int siteId, string clientIP, string clientIdentity, string customerInfo)
        {
            return dal.UnionOrders_Insert(orderId, unionPara, source, advertId, siteId, clientIP, clientIdentity, customerInfo);
        }

        /// <summary>
        /// 查询联盟订单列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">数量</param>
        /// <param name="orderType">排序</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public IList<OrdersInfo> UnionOrders_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            return dal.UnionOrders_GetList(pageIndex, pageSize, orderType, strWhere, out recordCount);
        }
    }
}
