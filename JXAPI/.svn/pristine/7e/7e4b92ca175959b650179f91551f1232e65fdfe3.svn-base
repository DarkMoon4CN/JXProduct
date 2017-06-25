using JXAPI.Common;
using JXAPI.Component.DataAccess;
using JXAPI.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class OrderLogisticsSqlDAL
    {
        private static Database dbr = JXThirdPartyData.Reader;
        private static Database dbw = JXThirdPartyData.Writer;


        /// <summary>
        ///   写入 OrderLogistics_Insert
        /// </summary>
        /// <param name="olInfo">实体</param>
        /// <returns></returns>
        public OperationResult<int> OrderLogistics_Insert(OrderLogisticsInfo olInfo) 
        {
            try
            {
                string sqlCommand = " INSERT INTO ThirdParty.dbo.OrderLogistics ";
                sqlCommand += " ( OrderID,ExpressID,LogisticsCompany,LogisticsNum,UID,Creator,CreateTime)  ";
                sqlCommand += "  VALUES ";
                sqlCommand += " ('{0}',{1},'{2}','{3}',{4},'{5}','{6}');SELECT @@IDENTITY;";
                sqlCommand = string.Format(sqlCommand,olInfo.OrderID,olInfo.ExpressID,olInfo.LogisticsCompany,olInfo.LogisticsNum,olInfo.UID,olInfo.Creator,olInfo.CreateTime );
                DbCommand cmd = dbw.GetSqlStringCommand(sqlCommand);
                int result = Convert.ToInt32(dbw.ExecuteScalar(cmd));
                if (result > 0)
                {
                    return new OperationResult<int>(OperationResultType.Success, null, result);
                }
                else 
                {
                    return new OperationResult<int>(OperationResultType.NoChanged, "操作没有引发任何变化", result);
                }
            }
            catch (Exception ex) 
            {
                return new OperationResult<int>(OperationResultType.Error, ex.Message);
            } 
        }


        public OperationResult<int> OrderLogistics_Delete(int logisticsID) 
        {
            try
            {
                string sqlCommand = " DELETE   ThirdParty.dbo.OrderLogistics  WHERE LogisticsID ={0} ";
                sqlCommand = string.Format(sqlCommand,logisticsID);
                DbCommand cmd = dbw.GetSqlStringCommand(sqlCommand);
                int result = Convert.ToInt32(dbw.ExecuteScalar(cmd));
                if (result > 0)
                {
                    return new OperationResult<int>(OperationResultType.Success, null, result);
                }
                else
                {
                    return new OperationResult<int>(OperationResultType.NoChanged, "操作没有引发任何变化", result);
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<int>(OperationResultType.Error, ex.Message);
            }
        }

       
        public OperationResult<int> Order_Update(OrdersInfo oInfo,string userName)
        {
            try
            {
                string sqlCommand = "ThirdParty.dbo.Orders_Dispose ";
                DbCommand cmd = dbw.GetStoredProcCommand(sqlCommand);
                dbw.AddInParameter(cmd, "OrderID", DbType.String, oInfo.OrderID);
                dbw.AddInParameter(cmd, "UID", DbType.Int32,100);
                dbw.AddInParameter(cmd, "UserType", DbType.Int32, 6);
                dbw.AddInParameter(cmd, "UserName", DbType.String, userName);
                dbw.AddInParameter(cmd, "DisposeID", DbType.Int32, 10);
                dbw.AddInParameter(cmd, "DisposeType", DbType.Int32, 2);
                dbw.AddInParameter(cmd, "Remarks", DbType.String, "快递已发货");
                dbw.AddInParameter(cmd, "Error", DbType.String,"");
                int result = dbw.ExecuteNonQuery(cmd);
                string msg = dbr.GetParameterValue(cmd, "Error").ToString();
                if (result > 0)
                {
                    return new OperationResult<int>(OperationResultType.Success, msg, result);
                }
                else
                {
                    return new OperationResult<int>(OperationResultType.NoChanged, "操作没有引发任何变化", result);
                }
            }
            catch (Exception ex) 
            {
                return new OperationResult<int>(OperationResultType.Error, ex.Message);
            }
        
        }


        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>订单列表</returns>
        public OperationResult<IList<OrdersInfo>> Orders_GetList(int pageIndex, int pageSize, string orderType, string strWhere, int IsHistory, out int recordCount)
        {
            try
            {
                IList<OrdersInfo> orderInfoList = new List<OrdersInfo>();

                string sqlCommand = "ThirdParty.dbo.Orders_GetList";
                DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);
                dbr.AddInParameter(dbCommand, "PageIndex", DbType.Int32, pageIndex);
                dbr.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);
                dbr.AddInParameter(dbCommand, "OrderType", DbType.String, orderType);
                dbr.AddInParameter(dbCommand, "StrWhere", DbType.String, strWhere);
                dbr.AddInParameter(dbCommand, "IsHistory", DbType.Int16, IsHistory);
                dbr.AddOutParameter(dbCommand, "RecordCount", DbType.Int32, 4);

                using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        orderInfoList.Add(RecoverModel(dataReader));
                    }
                }

                recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());
                return new OperationResult<IList<OrdersInfo>>(OperationResultType.Success,null,orderInfoList);
            }
            catch (Exception ex) 
            {
                recordCount = 0;
                return new OperationResult<IList<OrdersInfo>>(OperationResultType.Error, ex.Message);
            }
        }


        /// <summary>
        /// 从 IDataReader 中恢复Orders对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        public OrdersInfo RecoverModel(IDataReader dataReader)
        {
            OrdersInfo orderInfo = new OrdersInfo();
            orderInfo.OrderID = dataReader["OrderID"].ToString();
            orderInfo.UID = int.Parse(dataReader["UID"].ToString());
            orderInfo.PaySum = dataReader["PaySum"].ToDecimal();
            orderInfo.ProductFee = dataReader["ProductFee"].ToDecimal();
            orderInfo.NewShipFee = dataReader["NewShipFee"].ToDecimal();
            orderInfo.VoucherFee = dataReader["VoucherFee"].ToDecimal();
            orderInfo.ShipMethodID = dataReader["VoucherFee"].ToDecimal();
            orderInfo.Province = dataReader["Province"].ToString();
            orderInfo.City = dataReader["City"].ToString();
            orderInfo.County = dataReader["County"].ToString();
            orderInfo.Address = dataReader["Address"].ToString();
            orderInfo.Receiver = dataReader["Receiver"].ToString();
            orderInfo.Telephone = dataReader["Telephone"].ToString();
            orderInfo.Mobile = dataReader["Mobile"].ToString();
            orderInfo.Invoice = dataReader["Invoice"].ToString();
            orderInfo.Remarks = dataReader["Remarks"].ToString();
            orderInfo.CreateTime = dataReader["CreateTime"].ToDateTime();
            return orderInfo;
        }


        public OperationResult<IList<OrderProductInfo>> OrderProduct_GetList(string strWhere) 
        {
            IList<OrderProductInfo> orderProductInfoList = new List<OrderProductInfo>();
            try
            {
                string sqlCommand = " SELECT op.OrderID,op.ProductID,op.ProductCode,op.ProductName,op.Quantity,op.ProfeeDiscount FROM  OrderProduct AS op  ";
                sqlCommand += "  LEFT JOIN Orders AS o  ";
                sqlCommand += "  ON op.OrderID=o.OrderID ";
                sqlCommand += "  WHERE  1=1  AND {0} ";
                sqlCommand = string.Format(sqlCommand,strWhere);
                DbCommand dbCommand = dbr.GetSqlStringCommand(sqlCommand);
                using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        orderProductInfoList.Add(new OrderProductInfo()
                        {
                          OrderID=dataReader["OrderID"].ToString(),
                          ProductID = dataReader["ProductID"].ToInt(),
                          ProductName = dataReader["ProductName"].ToString(),
                          ProductCode = dataReader["ProductCode"].ToString(),
                          ProfeeDiscount = dataReader["ProfeeDiscount"].ToDecimal(),
                          Quantity = dataReader["Quantity"].ToInt()
                        });
                    }
                }
                return new OperationResult<IList<OrderProductInfo>>(OperationResultType.Success, null, orderProductInfoList);
            }
            catch (Exception ex) 
            {
                return new OperationResult<IList<OrderProductInfo>>(OperationResultType.Error,ex.Message);
            }
        }
    }
}
