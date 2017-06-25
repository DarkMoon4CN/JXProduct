using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JXAPI.Component.Model;

namespace JXAPI.Component.SQLServerDAL
{
    public partial class OrdersMySqlDAL
    {
        private static Database dbw = JXOrderMySqlData.Writer;
        private static Database dbr = JXOrderMySqlData.Reader;
        private static Database dbr_Order = JXOrderData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(OrdersMySqlDAL));

        #region MySql订单信息表相关操作

        /// <summary>
        /// 获取订单信息最大的ID
        /// </summary>
        /// <returns>订单信息ID最大值</returns>
        public int GetMaxOrdersId()
        {
            int maxId = -1;
            try
            {
                var sql = @"SELECT MAX(OrderID) FROM orders";
                var cmd = dbr.GetSqlStringCommand(sql);
                if (dbr.ExecuteScalar(cmd).IsNotNULL())
                {
                    maxId = Convert.ToInt32(dbr.ExecuteScalar(cmd));
                }
                else
                {
                    maxId = 0;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("GetMaxOrdersId 获取订单信息ID最大值异常：{0}", ex.Message);
            }
            return maxId;
        }
        
        public bool UpdateOrders(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into orders ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}'
                                ,'{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}'
                                ,'{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}',{57})",
                                                 dr["OrderNO"].ToInt(), dr["UID"].ToInt(), dr["UserName"].ToString().Replace("\'", "\""), dr["PaySum"].ToDecimal(), dr["ProductFee"].ToDecimal(), dr["ShipFee"].ToDecimal(), dr["NewShipFee"].ToDecimal(), dr["VoucherFee"].ToDecimal(), dr["VoucherSeriall"].ToString().Replace("\'", "\""), dr["PaymentMethodID"].ToShort()
                                                 , dr["ShipMethodID"].ToShort(), dr["Province"].ToString(), dr["City"].ToString(), dr["County"].ToString().Replace("\'", "\""), dr["Receiver"].ToString().Replace("\'", "\""), dr["Telephone"].ToString(), dr["Mobile"].ToString(), dr["Address"].ToString().Replace("\'", "\""), dr["PostalCode"].ToString(), dr["Invoice"].ToString().Replace("\'", "\"")
                                                 , dr["SendTimeInfo"].ToShort(), dr["SplitType"].ToShort(), dr["Remarks"].ToString().Replace("\'", "\""), string.IsNullOrEmpty(dr["CreateTime"].ToString()) ? "null" : dr["CreateTime"], dr["Creator"].ToString().Replace("\'", "\""), dr["PayStatus"].ToShort(), string.IsNullOrEmpty(dr["PayTime"].ToString()) ? "null" : dr["PayTime"], string.IsNullOrEmpty(dr["AuditTime"].ToString()) ? "null" : dr["AuditTime"], dr["Auditor"].ToString().Replace("\'", "\""), dr["Sender"].ToString().Replace("\'", "\"")
                                                 , string.IsNullOrEmpty(dr["SendTime"].ToString()) ? "null" : dr["SendTime"], dr["OrderStatus"].ToShort(), dr["FeeHasUpdate"].ToShort(), string.IsNullOrEmpty(dr["FeeUpdateTime"].ToString()) ? "null" : dr["FeeUpdateTime"], string.IsNullOrEmpty(dr["StatusChangTime"].ToString()) ? "null" : dr["StatusChangTime"], string.IsNullOrEmpty(dr["NextOperateTime"].ToString()) ? "null" : dr["NextOperateTime"], dr["SerialNumber"].ToString().Replace("\'", "\""), dr["BalanceFee"].ToDecimal(), dr["RefundFee"].ToDecimal(), dr["Refunder"].ToString().Replace("\'", "\"")
                                                 , string.IsNullOrEmpty(dr["RefundTime"].ToString()) ? "null" : dr["RefundTime"], string.IsNullOrEmpty(dr["CacelTime"].ToString()) ? "null" : dr["CacelTime"], dr["Cancer"].ToString(), string.IsNullOrEmpty(dr["RevokeTime"].ToString()) ? "null" : dr["RevokeTime"], dr["Revoker"].ToString().Replace("\'", "\""), dr["MediumSource"].ToString().Replace("\'", "\""), dr["OrderSource"].ToShort(), dr["ReduceType"].ToShort(), dr["ReduceFee"].ToDecimal(), dr["ActiveReduceFee"].ToDecimal()
                                                 , dr["ActiveReduceType"].ToShort(), dr["UseAccountFee"].ToDecimal(), Convert.ToBoolean(dr["IsFreeShip"]) ? 1 : 0, string.IsNullOrEmpty(dr["VisitTime"].ToString()) ? "null" : dr["VisitTime"], dr["Visit"].ToString().Replace("\'", "\""), dr["VisitStatus"].ToShort()
                                                 , dr["OrderID"].ToString().Replace("\'", "\""), dr["status"].ToShort());
                    if (i == 0)
                    {
                        strPlaceholder = Placeholder;
                    }
                    else
                    {
                        strPlaceholder += "," + Placeholder;
                    }
                }
                if (!string.IsNullOrEmpty(strPlaceholder))
                {
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = productTable.Rows.Count;
                        myLog.ErrorFormat("UpdateOrders 更新订单表失败,订单ID{0}-{1},失败数:{2}", productTable.Rows[0]["OrderNO"], productTable.Rows[productTable.Rows.Count - 1]["OrderNO"], errorCount);
                        flag = false;
                    }
                    else
                    {
                        errorCount = (productTable.Rows.Count - result > 0) ? productTable.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            myLog.ErrorFormat("UpdateOrders 更新订单表失败,订单ID{0}-{1},失败数:{2}", productTable.Rows[0]["OrderNO"], productTable.Rows[productTable.Rows.Count - 1]["OrderNO"], errorCount);
                        }  
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("UpdateOrders 更新订单表失败,订单ID{0}-{1},异常信息:{2}", productTable.Rows[0]["OrderNO"], productTable.Rows[productTable.Rows.Count - 1]["OrderNO"], ex.Message);
                flag = false;
            }
            return flag;
        }
        
        public bool AddOrders(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into orders ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}'
                                ,'{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}'
                                ,'{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}',{57})",
                                                 dr["OrderNO"].ToInt(), dr["UID"].ToInt(), dr["UserName"].ToString().Replace("\'", "\""), dr["PaySum"].ToDecimal(), dr["ProductFee"].ToDecimal(), dr["ShipFee"].ToDecimal(), dr["NewShipFee"].ToDecimal(), dr["VoucherFee"].ToDecimal(), dr["VoucherSeriall"].ToString().Replace("\'", "\""), dr["PaymentMethodID"].ToShort()
                                                 , dr["ShipMethodID"].ToShort(), dr["Province"].ToString(), dr["City"].ToString(), dr["County"].ToString().Replace("\'", "\""), dr["Receiver"].ToString().Replace("\'", "\""), dr["Telephone"].ToString(), dr["Mobile"].ToString(), dr["Address"].ToString().Replace("\'", "\""), dr["PostalCode"].ToString(), dr["Invoice"].ToString().Replace("\'", "\"")
                                                 , dr["SendTimeInfo"].ToShort(), dr["SplitType"].ToShort(), dr["Remarks"].ToString().Replace("\'", "\""), string.IsNullOrEmpty(dr["CreateTime"].ToString()) ? "null" : dr["CreateTime"], dr["Creator"].ToString().Replace("\'", "\""), dr["PayStatus"].ToShort(), string.IsNullOrEmpty(dr["PayTime"].ToString()) ? "null" : dr["PayTime"], string.IsNullOrEmpty(dr["AuditTime"].ToString()) ? "null" : dr["AuditTime"], dr["Auditor"].ToString().Replace("\'", "\""), dr["Sender"].ToString().Replace("\'", "\"")
                                                 , string.IsNullOrEmpty(dr["SendTime"].ToString()) ? "null" : dr["SendTime"], dr["OrderStatus"].ToShort(), dr["FeeHasUpdate"].ToShort(), string.IsNullOrEmpty(dr["FeeUpdateTime"].ToString()) ? "null" : dr["FeeUpdateTime"], string.IsNullOrEmpty(dr["StatusChangTime"].ToString()) ? "null" : dr["StatusChangTime"], string.IsNullOrEmpty(dr["NextOperateTime"].ToString()) ? "null" : dr["NextOperateTime"], dr["SerialNumber"].ToString().Replace("\'", "\""), dr["BalanceFee"].ToDecimal(), dr["RefundFee"].ToDecimal(), dr["Refunder"].ToString().Replace("\'", "\"")
                                                 , string.IsNullOrEmpty(dr["RefundTime"].ToString()) ? "null" : dr["RefundTime"], string.IsNullOrEmpty(dr["CacelTime"].ToString()) ? "null" : dr["CacelTime"], dr["Cancer"].ToString(), string.IsNullOrEmpty(dr["RevokeTime"].ToString()) ? "null" : dr["RevokeTime"], dr["Revoker"].ToString().Replace("\'", "\""), dr["MediumSource"].ToString().Replace("\'", "\""), dr["OrderSource"].ToShort(), dr["ReduceType"].ToShort(), dr["ReduceFee"].ToDecimal(), dr["ActiveReduceFee"].ToDecimal()
                                                 , dr["ActiveReduceType"].ToShort(), dr["UseAccountFee"].ToDecimal(), Convert.ToBoolean(dr["IsFreeShip"]) ? 1 : 0, string.IsNullOrEmpty(dr["VisitTime"].ToString()) ? "null" : dr["VisitTime"], dr["Visit"].ToString().Replace("\'", "\""), dr["VisitStatus"].ToShort()
                                                 , dr["OrderID"].ToString().Replace("\'", "\""), dr["status"].ToShort());

                    if (i == 0)
                    {
                        strPlaceholder = Placeholder;
                    }
                    else
                    {
                        strPlaceholder += "," + Placeholder;
                    }
                }
                if (!string.IsNullOrEmpty(strPlaceholder))
                {
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = productTable.Rows.Count;
                        flag = false;
                        myLog.ErrorFormat("AddOrders 添加订单表失败,订单ID{0}-{1},失败数:{2}", productTable.Rows[0]["OrderNO"], productTable.Rows[productTable.Rows.Count - 1]["OrderNO"], errorCount);
                    }
                    else
                    {
                        errorCount = (productTable.Rows.Count - result > 0) ? productTable.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            myLog.ErrorFormat("AddOrders 添加订单表失败,订单ID{0}-{1},失败数:{2}", productTable.Rows[0]["OrderNO"], productTable.Rows[productTable.Rows.Count - 1]["OrderNO"], errorCount);
                        }  
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddOrders 添加订单表失败,订单ID{0}-{1},异常信息:{2}", productTable.Rows[0]["OrderNO"], productTable.Rows[productTable.Rows.Count - 1]["OrderNO"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool AddOrdersToMySql(DataTable table, out int errorCount)
        {
            bool flag = true;
            errorCount = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = table.Rows[i];
                switch (dr["OrderStatus"].ToString())
                {
                    case "11":
                    case "15":
                    case "16":
                        if(!UpdateOrderStatus(dr))
                        {
                            flag = false;
                            errorCount++;
                        }
                        break;
                    case "6":
                    case "9":
                    case "10":
                    case "17":
                        if(!GetAndAddOrderLogistics(dr["OrderNO"].ToInt()))
                        {
                            flag = false;
                            errorCount++;
                        }
                        break;
                    default:
                        if (!UpdateOrdersInfoAndAddOrderOperateLog(dr))
                        {
                            flag = false;
                            errorCount++;
                        }
                        break;
                }
            }
            return flag;
        }

        private bool UpdateOrderStatus(DataRow dr)
        {
            bool flag = true;
            try
            {
                //OrdersService service = OrdersService.Instance;
                //OrdersAdminRequest ordersInfo = new OrdersAdminRequest();
                //ordersInfo.orderID = dr["OrderNO"].ToInt();
                //ordersInfo.cause = dr["Remarks"].ToString();
                //ordersInfo.orderStatus = dr["OrderStatus"].ToInt();
                //if(!service.Update(ordersInfo))
                //{
                //    myLog.ErrorFormat("UpdateOrderStatus 更新订单状态失败失败，订单OrderNO：{0}", dr["OrderNO"].ToString());
                //    flag = false;
                //}
            }
            catch(Exception ex)
            {
                myLog.ErrorFormat("UpdateOrderStatus 更新订单状态失败失败，订单OrderNO：{0}, 异常信息：{1}", dr["OrderNO"].ToString(), ex.Message);
                flag = false;
            }
            return flag;
        }

        //添加订单的状态和状态改变时间并且向操作日志表写入相应数据
        private bool UpdateOrdersInfoAndAddOrderOperateLog(DataRow dr)
        {
            bool flag = true;
            try
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.AppendFormat(@"update orders set OrderStatus = {0},StatusChangTime = '{1}' where orderid = {2}", dr["OrderStatus"].ToInt(), dr["StatusChangTime"].ToDateTime(), dr["OrderNO"].ToInt());
                var dbCommand = dbw.GetSqlStringCommand(sqlCommand.ToString());
                dbw.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("UpdateOrdersInfoAndAddOrderOperateLog 添加订单的状态和状态改变时间失败，订单OrderNO：{0},异常信息：{1}", dr["OrderNO"].ToInt(),ex.Message);
                flag = false;
            }
            return flag;
        }

        private bool GetAndAddOrderLogistics(int OrderNO)
        {
            bool flag = true;
            try
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.AppendFormat(@"select * from OrderLogistics where OrderNO = {0}", OrderNO);
                var dbCommand = dbr_Order.GetSqlStringCommand(sqlCommand.ToString());
                DataSet dSet = dbr_Order.ExecuteDataSet(dbCommand);
                if (dSet != null && dSet.Tables.Count > 0 && dSet.Tables[0].Rows.Count > 0)
                {
                    int errorCount = 0;
                    OrderLogisticsMySqlDAL dal = new OrderLogisticsMySqlDAL();
                    if (!dal.AddOrderLogistics(dSet.Tables[0], out errorCount))
                    {
                        myLog.ErrorFormat("GetAndAddOrderLogistics 添加订单物流信息失败，订单OrderNO：{0}", OrderNO);
                        flag = false;
                    }
                }
                else
                {
                    myLog.ErrorFormat("GetAndAddOrderLogistics 获取订单物流信息失败，订单物流信息为空，订单OrderNO：{0}", OrderNO);
                    flag = false;
                }
            }
            catch(Exception ex)
            {
                myLog.ErrorFormat("GetAndAddOrderLogistics 获取订单物流信息失败，订单OrderNO：{0}, 异常信息：{1}", OrderNO,ex.Message);
                flag = false;
            }
            return flag;
        }

        private string parmsKey = string.Format(@"OrderID,UID,UserName,PaySum,ProductFee,ShipFee,NewShipFee,VoucherFee,VoucherSeriall,PaymentMethodID
                                                 ,ShipMethodID,Province,City,County,Receiver,Telephone,Mobile,Address,PostalCode,Invoice
                                                 ,SendTimeInfo,SplitType,Remarks,CreateTime,Creator,PayStatus,PayTime,AuditTime,Auditor,Sender
                                                 ,SendTime,OrderStatus,FeeHasUpdate,FeeUpdateTime,StatusChangTime,NextOperateTime,SerialNumber,BalanceFee,RefundFee,Refunder
                                                 ,RefundTime,CancelTime,Cancer,RevokeTime,Revoker,MediumSource,OrderSource,ReduceType,ReduceFee,ActiveReduceFee
                                                 ,ActiveReduceType,UseAccountFee,IsFreeShip,VisitTime,Visit,VisitStatus,OldOrderID,status");

        #endregion
    }
}
