using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class OrdersHistoryMySqlDAL
    {
        private static Database dbw = JXOrderMySqlData.Writer;
        private static Database dbr = JXOrderMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(OrdersHistoryMySqlDAL));

        #region MySql订单信息表表相关操作

        /// <summary>
        /// 获取订单信息最大的ID
        /// </summary>
        /// <returns>订单信息ID最大值</returns>
        public int GetMaxOrdersHistoryId()
        {
            int maxId = -1;
            try
            {
                var sql = @"SELECT MAX(OrderID) FROM ordershistory";
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

        public bool UpdateOrdersHistory(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into ordershistory ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
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
                        errorCount = table.Rows.Count;
                        flag = false;
                        myLog.ErrorFormat("UpdateOrdersHistory 更新订单表失败,订单ID{0}-{1},订单ID{0}-{1},失败数:{2}", table.Rows[0]["OrderNO"], table.Rows[table.Rows.Count - 1]["OrderNO"], errorCount);
                    }
                    else
                    {
                        errorCount = (table.Rows.Count - result > 0) ? table.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            myLog.ErrorFormat("UpdateOrdersHistory 更新订单表失败,订单ID{0}-{1},订单ID{0}-{1},失败数:{2}", table.Rows[0]["OrderNO"], table.Rows[table.Rows.Count - 1]["OrderNO"], errorCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("UpdateOrdersHistory 更新订单表失败,订单ID{0}-{1},异常信息:{2}", table.Rows[0]["OrderNO"], table.Rows[table.Rows.Count - 1]["OrderNO"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool AddOrdersHistory(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into ordershistory ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
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
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\","\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = table.Rows.Count;
                        flag = false;
                        myLog.ErrorFormat("AddOrdersHistory 添加订单表失败,订单ID{0}-{1},订单ID{0}-{1},失败数:{2}", table.Rows[0]["OrderNO"], table.Rows[table.Rows.Count - 1]["OrderNO"], errorCount);
                    }
                    else
                    {
                        errorCount = (table.Rows.Count - result > 0) ? table.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            myLog.ErrorFormat("AddOrdersHistory 添加订单表失败,订单ID{0}-{1},订单ID{0}-{1},失败数:{2}", table.Rows[0]["OrderNO"], table.Rows[table.Rows.Count - 1]["OrderNO"], errorCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddOrdersHistory 添加订单表失败,订单ID{0}-{1},异常信息:{2}", table.Rows[0]["OrderNO"], table.Rows[table.Rows.Count - 1]["OrderNO"], ex.Message);
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
