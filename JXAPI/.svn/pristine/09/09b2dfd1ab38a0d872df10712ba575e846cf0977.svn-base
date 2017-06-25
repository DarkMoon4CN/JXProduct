using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using JXAPI.Component.BLL;
using System.Data;
using JXAPI.Component.Model;
using JXAPI.Component;

namespace JXAPI.ConsoleOrdersPlan
{
    class Program
    {
        static ILog myLog = LogManager.GetLogger("Default");
        private static int updateTimeSpan = 0, updateMaxCount = 0, sucessSum = 0, errorSum = 0;
        private static DateTime beginTime;

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            ConsoleMsg("\r\n\r\n");

            //  初始化页面
            Binding();

            //  药师回拨
            SQLProductOutBook_Insert();

            //  订单数据
            SQLOrders_Insert();

            //  订单状态
            SQLOrders_UpdateStatus();

            //  付款状态
            SQLOrders_UpdatePayStatus();

            //  CRM 同步至 Web
            MysqlOrders_UpdateStatus();

            Environment.Exit(0);
        }

        /// <summary>
        /// ProductOutBook 数据获取
        /// </summary>
        /// <param name="tbName"></param>
        static void SQLProductOutBook_Insert()
        {
            DateTime crrTime = DateTime.Now;
            sucessSum = 0;
            errorSum = 0;
            Console.WriteLine(string.Format("========================= BEGIN ProductOutBook"));
            try
            {
                //  查询待同步数据
                DataTable dTable = OrdersBLL.Instance.GetList(0, beginTime.ToString(), 0, 0, "ProductOutBook");
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    ConsoleMsg("ProductOutBook 有0条数据等待同步");
                }
                else
                {
                    ConsoleMsg(string.Format("ProductOutBook 有{0}条数据等待同步", dTable.Rows.Count));
                }
                //  同步数据
                ProductOutBookInfo info = null;
                foreach (DataRow row in dTable.Rows)
                {
                    //  转换数据
                    try
                    {
                        info = new ProductOutBookInfo() { Status = 0, HasBuy = 0 };
                        info.ProductID = int.Parse(row["ProductID"].ToString());
                        info.ProductName = row["ProductName"].ToString();
                        info.ProductCode = row["ProductCode"].ToString();
                        info.TradePrice = (string.IsNullOrEmpty(row["TradePrice"].ToString())) ? 0 : decimal.Parse(row["TradePrice"].ToString());
                        info.Name = row["UserName"].ToString();
                        info.UID = int.Parse(row["UID"].ToString());
                        info.Mobile = row["Mobile"].ToString();
                        info.Message = string.Format("{1} {0}", (row["Type"].ToString() == "1") ? "申请药师回拨" : "缺货登记", row["Message"].ToString());
                        info.Quantity = (string.IsNullOrEmpty(row["Quantity"].ToString())) ? 0 : int.Parse(row["Quantity"].ToString());
                        info.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                        info.UpdateTime = crrTime;
                    }
                    catch (Exception ex)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("ProductOutBook 数据：{0} Mobile：{1} 转换失败：{2}", row["ProductID"].ToString(), row["Mobile"].ToString(), ex.Message));
                        continue;
                    }

                    //  保存
                    try
                    {
                        int outId = ProductOutBookSqlBLL.Instance.Add(info);
                        if (outId > 0)
                        {
                            sucessSum++;
                        }
                        else
                        {
                            errorSum++;
                            ConsoleMsg(string.Format("ProductOutBook 数据：{0} Mobile：{1} 保存失败", info.ProductID, info.Mobile));
                        }
                    }
                    catch (Exception ex)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("ProductOutBook 数据：{0} Mobile：{1} 保存失败：{2}", info.ProductID, info.Mobile, ex.Message));
                    }
                }
                ConsoleMsg(string.Format("ProductOutBook 同步完成：{0} 失败：{1}", sucessSum, errorSum));
            }
            catch (Exception ex)
            {
                ConsoleMsg(string.Format("ProductOutBook ERROR：{0}", ex.Message));
            }
            Console.WriteLine("========================= END ProductOutBook\r\n");
            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// Orders 数据获取
        /// </summary>
        static void SQLOrders_Insert()
        {
            DateTime crrTime = DateTime.Now;
            sucessSum = 0;
            errorSum = 0;
            Console.WriteLine(string.Format("========================= BEGIN Orders Insert"));
            try
            {
                //  查询待同步数据
                DataTable dTable = OrdersBLL.Instance.GetList(0, beginTime.ToString(), 0, 0, "Orders");
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    Console.WriteLine("Orders 有0条数据等待同步");
                    return;
                }
                else
                {
                    Console.WriteLine(string.Format("Orders 有{0}条数据等待同步", dTable.Rows.Count));
                }

                //  保存订单
                int thirdOrderID = 0;
                string orderID = string.Empty;
                OrdersInfo oInfo = null;
                StringBuilder orderXML = new StringBuilder();
                foreach (DataRow row in dTable.Rows)
                {
                    //  单号
                    int.TryParse(row["OrderID"].ToString(), out thirdOrderID);
                    if (thirdOrderID <= 0)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("Orders Insert 单号错误 ThirdOrderID：{0}", thirdOrderID));
                        continue;
                    }
                    #region 同步订单
                    try
                    {
                        //  是否已经添加
                        oInfo = OrdersSqlBLL.Instance.GetOrderIDByThirdOrderID(thirdOrderID.ToString());
                        if (oInfo != null)
                        {
                            sucessSum++;
                            continue;
                        }

                        #region 订单数据转换
                        orderID = string.Format("{0}{1}", crrTime.ToString("yyMMddHH"), thirdOrderID);
                        orderXML.Length = 0;
                        orderXML.Append("<JXOrder>");
                        orderXML.Append("<BasicInfo>");
                        orderXML.Append(string.Format("<OrderID>{0}</OrderID>", orderID));
                        orderXML.Append(string.Format("<UID>{0}</UID>", row["UID"].ToInt()));
                        orderXML.Append(string.Format("<UserName>{0}</UserName>", row["UserName"].ToString()));
                        orderXML.Append(string.Format("<ProductFee>{0}</ProductFee>", row["ProductFee"].ToDecimal()));
                        orderXML.Append(string.Format("<ShipFee>{0}</ShipFee>", row["ShipFee"].ToDecimal()));
                        orderXML.Append(string.Format("<NewShipFee>{0}</NewShipFee>", row["NewShipFee"].ToDecimal()));
                        orderXML.Append(string.Format("<IsFreeShip>{0}</IsFreeShip>", row["IsFreeShip"].ToShort()));
                        orderXML.Append(string.Format("<FreeShipCardNo>{0}</FreeShipCardNo>", 0));                  //  包邮卡卡号
                        orderXML.Append(string.Format("<PaymentMethodID>{0}</PaymentMethodID>", row["PaymentMethodID"].ToShort()));
                        orderXML.Append(string.Format("<ShipMethodID>{0}</ShipMethodID>", row["ShipMethodID"].ToShort()));
                        orderXML.Append(string.Format("<Province>{0}</Province>", row["Province"].ToString()));
                        orderXML.Append(string.Format("<City>{0}</City>", row["City"].ToString()));
                        orderXML.Append(string.Format("<Country>{0}</Country>", row["County"].ToString()));
                        orderXML.Append(string.Format("<Receiver>{0}</Receiver>", row["Receiver"].ToString()));
                        orderXML.Append(string.Format("<Telephone>{0}</Telephone>", row["Telephone"].ToString()));
                        orderXML.Append(string.Format("<Mobile>{0}</Mobile>", row["Mobile"].ToString()));
                        orderXML.Append(string.Format("<Email>{0}</Email>", string.Empty));
                        orderXML.Append(string.Format("<Address>{0}</Address>", row["Address"].ToString()));
                        orderXML.Append(string.Format("<PostalCode>{0}</PostalCode>", row["PostalCode"].ToString()));
                        orderXML.Append(string.Format("<Invoice>{0}</Invoice>", row["Invoice"].ToString()));
                        orderXML.Append(string.Format("<SendTimeInfo>{0}</SendTimeInfo>", row["SendTimeInfo"].ToShort()));
                        orderXML.Append(string.Format("<Remarks>{0}</Remarks>", row["Remarks"].ToString()));
                        orderXML.Append(string.Format("<CreateTime>{0}</CreateTime>", string.IsNullOrEmpty(row["CreateTime"].ToString()) ? DateTime.Now : row["CreateTime"]));
                        orderXML.Append(string.Format("<OrderStatus>0</OrderStatus>", row["OrderStatus"].ToShort()));
                        orderXML.Append(string.Format("<PayStatus>{0}</PayStatus>", row["PayStatus"].ToShort()));
                        orderXML.Append(string.Format("<RebateFee>{0}</RebateFee>", 0));                                        //  返利比例
                        orderXML.Append(string.Format("<MediumSource>{0}</MediumSource>", row["MediumSource"].ToString()));
                        orderXML.Append(string.Format("<OrderSource>{0}</OrderSource>", row["OrderSource"].ToShort()));
                        orderXML.Append(string.Format("<ReduceType>{0}</ReduceType>", row["ReduceType"].ToShort()));            //  优惠减免类型
                        orderXML.Append(string.Format("<ReduceFee>{0}</ReduceFee>", row["ReduceFee"].ToDecimal()));             //  优惠减免金额
                        orderXML.AppendFormat("<ActiveReduceType>{0}</ActiveReduceType>", row["ActiveReduceType"].ToShort());   //  满减类型
                        orderXML.AppendFormat("<ActiveReduceFee>{0}</ActiveReduceFee>", row["ActiveReduceFee"].ToDecimal());    //  满减金额
                        orderXML.Append(string.Format("<CardNo>{0}</CardNo>", string.Empty));         //  包邮卡

                        decimal voucherFee = 0;
                        if (!string.IsNullOrEmpty(row["UseScoreFee"].ToString()))
                        {
                            voucherFee += decimal.Parse(row["UseScoreFee"].ToString());
                        }
                        if (!string.IsNullOrEmpty(row["VoucherFee"].ToString()))
                        {
                            voucherFee += decimal.Parse(row["VoucherFee"].ToString());
                        }
                        orderXML.Append(string.Format("<VoucherFee>{0}</VoucherFee>", voucherFee.ToString("0.00")));                //  优惠劵金额
                        orderXML.Append(string.Format("<VoucherSerial>{0}</VoucherSerial>", row["VoucherSeriall"].ToString()));     //  优惠劵号码
                        orderXML.Append(string.Format("<ShoppingCardNo>{0}</ShoppingCardNo>", string.Empty));           //  购物卡卡号
                        orderXML.Append(string.Format("<ShoppingCardPwd>{0}</ShoppingCardPwd>", string.Empty));         //  购物卡密码
                        orderXML.Append(string.Format("<ShoppingCardFee>0</ShoppingCardFee>"));                         //  购物卡使用金额
                        decimal useAccountFee = 0;
                        if (!string.IsNullOrEmpty(row["UseAccountFee"].ToString()))
                        {
                            useAccountFee += decimal.Parse(row["UseAccountFee"].ToString());
                        }
                        orderXML.Append(string.Format("<UseAccountFee>{0}</UseAccountFee>", useAccountFee.ToString("0.00")));       //  账户余额
                        orderXML.Append(string.Format("<UnionPara>{0}</UnionPara>", string.Empty));
                        orderXML.Append(string.Format("<UnionType>{0}</UnionType>", "0"));
                        orderXML.Append(string.Format("<Creator>{0}</Creator>", row["Creator"].ToString()));
                        orderXML.Append(string.Format("<SplitType>{0}</SplitType>", row["SplitType"].ToString()));
                        orderXML.Append(string.Format("<ThirdOrderID>{0}</ThirdOrderID>", row["OrderID"].ToString()));
                        orderXML.Append(string.Format("<ThirdStatus>0</ThirdStatus>", row["OrderStatus"].ToShort()));
                        orderXML.Append("</BasicInfo>");

                        //  订单商品
                        string orderProductXML = GetOrderProductXML(thirdOrderID, orderID);
                        if (orderProductXML.Length <= 10 || orderProductXML.IndexOf("OrderProducts") <= 0)
                        {
                            ConsoleMsg(string.Format("Orders ThirdOrderID：{0} 无商品信息", thirdOrderID));
                            continue;
                        }
                        orderXML.Append(orderProductXML);
                        orderXML.Append("</JXOrder>");
                        #endregion

                        //  保存订单
                        int result = 0;
                        OrdersSqlBLL.Instance.Orders_InsertByXML(orderXML.ToString().Replace("&", "＆"), out result);
                        if (result == 0)
                        {
                            sucessSum++;

                            //  操作日志
                            AddOrderoperatelog(thirdOrderID, orderID, new OrderOperateLogInfo()
                            {
                                OrderID = orderID,
                                Tittle = "在线下单",
                                OldStatus = 0,
                                NewStatus = short.Parse(row["OrderStatus"].ToString()),
                                UID = int.Parse(row["UID"].ToString()),
                                UserName = row["UserName"].ToString()
                            });
                        }
                        else
                        {
                            errorSum++;
                            ConsoleMsg(string.Format("Orders Insert 订单：{0} 新增失败", thirdOrderID));
                        }
                    }
                    catch (Exception ex)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("Orders Insert 订单：{0} 出错：{1}", thirdOrderID, ex.Message));
                    }
                    #endregion  
                }
                ConsoleMsg(string.Format("Orders Insert 同步完成：{0} 失败：{1}", sucessSum, errorSum));
            }
            catch (Exception ex)
            {
                ConsoleMsg(string.Format("Orders Insert ERROR：{0}", ex.Message));
            }
            Console.WriteLine("========================= END Orders Insert\r\n");
            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// 订单商品
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <returns></returns>
        static string GetOrderProductXML(int thirdOrderID, string orderID)
        {
            try
            {
                DataTable dTable = OrdersBLL.Instance.GetList(thirdOrderID, string.Empty, 0, 0, "OrderProduct");
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    return string.Empty;
                }
                StringBuilder orderProductXML = new StringBuilder();
                orderProductXML.Append("<OrderProducts>");
                foreach (DataRow row in dTable.Rows)
                {
                    orderProductXML.Append("<OrderProduct>");
                    orderProductXML.Append(string.Format("<OrderProductID>{0}</OrderProductID>", row["OrderProductID"].ToInt()));
                    orderProductXML.Append(string.Format("<OrderID>{0}</OrderID>", orderID));
                    orderProductXML.Append(string.Format("<ProductID>{0}</ProductID>", row["ProductID"].ToInt()));
                    orderProductXML.Append(string.Format("<OrderSplitID>{0}</OrderSplitID>", string.Empty));
                    orderProductXML.Append(string.Format("<ProductCode>{0}</ProductCode>", row["ProductCode"].ToString()));

                    orderProductXML.Append(string.Format("<ProductName>{0}</ProductName>", row["ProductName"].ToString()));
                    orderProductXML.Append(string.Format("<ProductImage>{0}</ProductImage>", row["ProductImage"].ToString()));
                    orderProductXML.Append(string.Format("<ProductType>{0}</ProductType>", (string.IsNullOrEmpty(row["ProductType"].ToString())) ? 1 : int.Parse(row["ProductType"].ToString())));
                    orderProductXML.Append(string.Format("<MarketPrice>{0}</MarketPrice>", row["MarketPrice"].ToDecimal()));
                    orderProductXML.Append(string.Format("<TradePrice>{0}</TradePrice>", row["TradePrice"].ToDecimal()));

                    orderProductXML.Append(string.Format("<CostPrice>{0}</CostPrice>", row["CostPrice"].ToDecimal()));
                    orderProductXML.Append(string.Format("<Quantity>{0}</Quantity>", row["Quantity"].ToInt()));
                    orderProductXML.Append(string.Format("<OutQuantity>{0}</OutQuantity>", row["SendQuantity"].ToInt()));
                    orderProductXML.Append(string.Format("<ReturnQuantity>{0}</ReturnQuantity>", row["ReturnQuantity"].ToInt()));
                    orderProductXML.Append(string.Format("<ProfeeDiscount>{0}</ProfeeDiscount>", row["ProFeeDiscount"].ToDecimal()));

                    orderProductXML.Append(string.Format("<ParaValue>{0}</ParaValue>", string.Empty));
                    orderProductXML.Append(string.Format("<PriceParaID>0</PriceParaID>"));
                    orderProductXML.Append(string.Format("<PriceParaValue>{0}</PriceParaValue>", string.Empty));
                    orderProductXML.Append(string.Format("<ProBatchNumber>{0}</ProBatchNumber>", row["ProBatchNumber"].ToString()));
                    orderProductXML.Append(string.Format("<Memo>{0}</Memo>", row["Memo"].ToString()));

                    orderProductXML.Append(string.Format("<CommissionRatio>{0}</CommissionRatio>", string.Empty));
                    orderProductXML.Append(string.Format("<CommissionType>0</CommissionType>"));
                    orderProductXML.Append(string.Format("<VoucherFee>{0}</VoucherFee>", row["VoucherFee"].ToDecimal()));
                    orderProductXML.Append(string.Format("<ActiveReduceFee>0</ActiveReduceFee>", row["ActiveReduceFee"].ToDecimal()));
                    orderProductXML.Append(string.Format("<ActiveReduceType>{0}</ActiveReduceType>", row["ActiveReduceType"].ToShort()));

                    orderProductXML.Append(string.Format("<BuyScore>0</BuyScore>"));                            //  返利比例
                    orderProductXML.Append(string.Format("<HouseID>0</HouseID>"));
                    orderProductXML.Append("</OrderProduct>");
                }
                orderProductXML.Append("</OrderProducts>");
                return orderProductXML.ToString();
            }
            catch (Exception ex)
            {
                ConsoleMsg(string.Format("OrderProduct ThirdOrderID:{0} 构造订单商品出错：{1}", thirdOrderID, ex.Message));
            }
            return string.Empty;
        }

        /// <summary>
        /// 添加订单日志
        /// </summary>
        /// <param name="thirdOrderID"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        static void AddOrderoperatelog(int thirdOrderID, string orderID, OrderOperateLogInfo info)
        {
            try
            {
                DataTable dTable = OrdersBLL.Instance.GetList(thirdOrderID, string.Empty, 0, 0, "orderoperatelog");
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    OrdersSqlBLL.Instance.OrderOperateLog_Insert(info);
                    return;
                }

                //  遍历操作日志
                foreach (DataRow row in dTable.Rows)
                {
                    info = new OrderOperateLogInfo()
                    {
                        OrderID = orderID,
                        Tittle = row["Tittle"].ToString(),
                        OldStatus = short.Parse(row["OldStatus"].ToString()),
                        NewStatus = short.Parse(row["NewStatus"].ToString()),
                        UID = int.Parse(row["UID"].ToString()),
                        UserName = row["UserName"].ToString(),
                        Content = row["Content"].ToString()
                    };
                    OrdersSqlBLL.Instance.OrderOperateLog_Insert(info);
                }
            }
            catch (Exception ex)
            {
                ConsoleMsg(string.Format("OrderOperatelog ThirdOrderID:{0} OrderID：{1} 订单日志：{2}", thirdOrderID, orderID, ex.Message));
            }
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        static void SQLOrders_UpdateStatus()
        {
            sucessSum = 0;
            errorSum = 0;
            Console.WriteLine(string.Format("========================= BEGIN OrderStatus：{0}", DateTime.Now.ToString()));
            try
            {
                //  查询待处理数据
                DataTable dTable = OrdersBLL.Instance.GetList(0, beginTime.ToString(), 0, 0, "ordersstatus");
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    Console.WriteLine("OrderStatus 有0条订单状态等待处理");
                    return;
                }
                else
                {
                    Console.WriteLine(string.Format("OrderStatus 有{0}条订单状态等待处理", dTable.Rows.Count));
                }
                //  处理数据
                int thirdOrderID = 0;
                short orderStatus = 0;
                string[] colName = { "Cancer", "CacelTime" };
                OrdersInfo oInfo = null;
                string orderID = string.Empty, result = string.Empty;
                foreach (DataRow row in dTable.Rows)
                {
                    //  单号
                    int.TryParse(row["OrderID"].ToString(), out thirdOrderID);
                    if (thirdOrderID <= 0)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("OrderStatus 单号错误 ThirdOrderID：{0}", thirdOrderID));
                        continue;
                    }
                    else
                    {
                        orderStatus = short.Parse(row["OrderStatus"].ToString());
                    }

                    #region 更新订单状态
                    try
                    {
                        //  是否已经添加
                        oInfo = OrdersSqlBLL.Instance.GetOrderIDByThirdOrderID(thirdOrderID.ToString());
                        if (oInfo == null)
                        {
                            errorSum++;
                            ConsoleMsg(string.Format("OrderStatus ThirdOrderID：{0} 未找到：{1}", thirdOrderID, orderID));
                            continue;
                        }
                        else if (oInfo.OrderStatus == orderStatus)
                        {
                            sucessSum++;
                            continue;
                        }
                        else
                            orderID = oInfo.OrderID;
                        //  更改状态
                        result = OrdersSqlBLL.Instance.Orders_Dispose(new OrderOperateLogInfo()
                        {
                            OrderID = orderID,
                            Tittle = "取消订单",
                            OldStatus = 0,
                            NewStatus = orderStatus,
                            UID = int.Parse(row["UID"].ToString()),
                            UserName = row["Cancer"].ToString()
                        }, 2);
                        if (result == "0")
                        {
                            OrdersSqlBLL.Instance.Orders_Update(orderID, colName, new object[] { row["Cancer"].ToString(), row["CancelTime"].ToString() });
                            sucessSum++;
                        }
                        else
                        {
                            errorSum++;
                            Console.WriteLine(string.Format("OrderStatus 订单：{0} ThirdOrderID：{1} 保存状态失败：{2}", orderID, thirdOrderID, result));
                        }
                    }
                    catch (Exception ex)
                    {
                        errorSum++;
                        Console.WriteLine(string.Format("OrderStatus 订单：{0} ThirdOrderID：{1} 出错：{2}", orderID, thirdOrderID, ex.Message));
                    }
                    #endregion
                }
                ConsoleMsg(string.Format("OrderStatus 同步完成：{0} 失败：{1}", sucessSum, errorSum));
            }
            catch (Exception ex)
            {
                ConsoleMsg(string.Format("OrdersStatus ERROR：{0}", ex.Message));
            }
            Console.WriteLine("========================= END OrdersStatus\r\n");
            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// 付款状态
        /// </summary>
        static void SQLOrders_UpdatePayStatus()
        {
            sucessSum = 0;
            errorSum = 0;
            Console.WriteLine(string.Format("========================= BEGIN PayStatus"));
            try
            {
                //  查询待处理数据
                DataTable dTable = OrdersBLL.Instance.GetList(0, beginTime.ToString(), 0, 0, "orderspay");
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    Console.WriteLine("PayStatus 有0条支付状态等待处理");
                    return;
                }
                else
                {
                    Console.WriteLine(string.Format("PayStatus 有{0}条支付状态等待处理", dTable.Rows.Count));
                }
                //  处理数据
                int thirdOrderID = 0;
                short payStatus=0;
                string[] colName = { "PaymentMethodID", "SerialNumber" };
                OrdersInfo oInfo = null;
                string orderID = string.Empty, result = string.Empty;
                foreach (DataRow row in dTable.Rows)
                {
                    //  单号
                    int.TryParse(row["OrderID"].ToString(), out thirdOrderID);
                    if (thirdOrderID <= 0)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("PayStatus 单号错误 ThirdOrderID：{0}", thirdOrderID));
                        continue;
                    }
                    else
                    {
                        payStatus = short.Parse(row["PayStatus"].ToString());
                    }
                    #region 更新订单支付状态
                    try
                    {
                        //  是否已经添加
                        oInfo = OrdersSqlBLL.Instance.GetOrderIDByThirdOrderID(thirdOrderID.ToString());
                        if (oInfo == null)
                        {
                            errorSum++;
                            ConsoleMsg(string.Format("PayStatus ThirdOrderID：{0} 未找到：{1}", thirdOrderID, orderID));
                            continue;
                        }
                        else if (oInfo.PayStatus == payStatus)
                        {
                            sucessSum++;
                            continue;
                        }
                        else
                            orderID = oInfo.OrderID;
                        OrderOperateLogInfo info = new OrderOperateLogInfo()
                        {
                            OrderID = orderID,
                            Tittle = "已付款",
                            OldStatus = 0,
                            NewStatus = payStatus,
                            UID = int.Parse(row["UID"].ToString()),
                            UserName = row["Creator"].ToString()
                        };
                        //  查询支付日志
                        DataTable dTableLog = OrdersBLL.Instance.GetList(thirdOrderID, string.Empty, 0, 0, "orderoperatelog", 1);
                        if (dTableLog != null && dTableLog.Rows.Count > 0)
                        {
                            info.Tittle = dTableLog.Rows[0]["Tittle"].ToString();
                            info.OldStatus = short.Parse(dTableLog.Rows[0]["OldStatus"].ToString());
                            info.NewStatus = short.Parse(dTableLog.Rows[0]["NewStatus"].ToString());
                            info.UID = int.Parse(dTableLog.Rows[0]["UID"].ToString());
                            info.UserName = dTableLog.Rows[0]["UserName"].ToString();
                            info.Content = dTableLog.Rows[0]["Content"].ToString();
                        }
                        //  更改支付状态
                        result = OrdersSqlBLL.Instance.Orders_Dispose(info, 1);
                        if (result == "0")
                        {
                            int paymentMethodID = int.Parse(row["PaymentMethodID"].ToString());
                            OrdersSqlBLL.Instance.Orders_Update(orderID, colName, new object[] { paymentMethodID.ToString(), row["SerialNumber"].ToString() });
                            sucessSum++;
                        }
                        else
                        {
                            errorSum++;
                            ConsoleMsg(string.Format("PayStatus 订单：{0} ThirdOrderID：{1} 保存支付状态失败：{2}", orderID, thirdOrderID, result));
                        }
                    }
                    catch (Exception ex)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("PayStatus 订单：{0} ThirdOrderID：{1} 出错：{2}", orderID, thirdOrderID, ex.Message));
                    }
                    #endregion
                }
                ConsoleMsg(string.Format("PayStatus 同步完成：{0} 失败：{1}", sucessSum, errorSum));
            }
            catch (Exception ex)
            {
                ConsoleMsg(string.Format("PayStatus ERROR：{0}", ex.Message));
            }
            Console.WriteLine("========================= END PayStatus\r\n");
            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// 更新MYSQL订单状态
        /// </summary>
        static void MysqlOrders_UpdateStatus()
        {
            sucessSum = 0;
            errorSum = 0;
            Console.WriteLine(string.Format("========================= BEGIN Mysql OrderStatus"));
            try
            {
                //  查询待处理数据
                DataTable dTable = OrdersSqlBLL.Instance.GetList(string.Empty, beginTime.ToString(), 0, 0, "orders");
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    Console.WriteLine("OrderStatus 有0条订单状态等待同步Web");
                    return;
                }
                else
                {
                    Console.WriteLine(string.Format("OrderStatus 有{0}条订单状态等待同步Web", dTable.Rows.Count));
                }
                //  遍历订单
                int thirdOrderID = 0, uid = 1;
                string[] colName = { "OrderStatus", "StatusChangTime", "Sender", "SendTime" };
                string orderID = string.Empty, result = string.Empty, content = string.Empty, userName = string.Empty;
                foreach (DataRow row in dTable.Rows)
                {
                    #region 三方订单
                    short orderStatus = 0;
                    short.TryParse(row["OrderStatus"].ToString(), out orderStatus);
                    orderID = row["OrderID"].ToString();
                    if (!string.IsNullOrEmpty(row["ThirdOrderID"].ToString()))
                    {
                        int.TryParse(row["ThirdOrderID"].ToString(), out thirdOrderID);
                    }
                    if (thirdOrderID <= 0)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("OrderStatus 单号错误 ThirdOrderID：{0} OrderID：{1}", thirdOrderID, orderID));
                        continue;
                    }
                    DataTable dTableMysql = OrdersBLL.Instance.GetList(thirdOrderID, string.Empty, 0, 0, "orders");
                    if (dTableMysql == null || dTableMysql.Rows.Count <= 0)
                    {
                        errorSum++;
                        ConsoleMsg(string.Format("OrderStatus 未找到三方订单 ThirdOrderID：{0} OrderID：{1}", thirdOrderID, orderID));
                        continue;
                    }
                    if (short.Parse(dTableMysql.Rows[0]["OrderStatus"].ToString()) == orderStatus)
                    {
                        sucessSum++;
                        continue;
                    }
                    #endregion

                    #region 更新状态
                    switch (orderStatus)
                    {
                        case 11:
                        case 15:
                        case 16:
                            #region 撤单/退货
                            try
                            {
                                //  日志
                                DataTable dTableLog = OrdersSqlBLL.Instance.GetList(orderID, string.Empty, 0, 0, "orderoperatelog", orderStatus);
                                if (dTableLog != null && dTableLog.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(dTableLog.Rows[0]["UID"].ToString()))
                                    {
                                        uid = int.Parse(dTableLog.Rows[0]["UID"].ToString());
                                    }
                                    userName = dTableLog.Rows[0]["UserName"].ToString();
                                    content = dTableLog.Rows[0]["Content"].ToString();
                                }
                                else
                                {
                                    uid = 100;
                                    userName = "金象网";
                                }

                                //  取消/撤单
                                bool bol = JXSdk.Service.OrdersService.Instance.Update(new JXSdk.Request.OrdersAdminRequest()
                                {
                                    uid = uid,
                                    orderID = thirdOrderID,
                                    loginUsername = userName,
                                    cause = content,
                                    orderStatus = orderStatus
                                });
                                if (bol)
                                {
                                    sucessSum++;
                                }
                                else
                                {
                                    errorSum++;
                                    ConsoleMsg(string.Format("Mysql Orders OrderStatus：{0} API更新失败", orderID));
                                }
                            }
                            catch (Exception ex)
                            {
                                ConsoleMsg(string.Format("Mysql OrderStatus：{0} 撤单/退货失败：{1}", orderID, ex.Message));
                            }
                            #endregion
                            break;
                        case 6:
                        case 9:
                        case 10:
                            #region 发货
                            try
                            {
                                //  更新订单状态
                                bool bol = OrdersBLL.Instance.Orders_Update(thirdOrderID, colName, new object[] { orderStatus, row["StatusChangTime"].ToString(), row["Sender"].ToString(), row["SendTime"].ToString() });
                                if (bol)
                                {
                                    sucessSum++;
                                    //  记录快递单号
                                    AddOrderLogistics(thirdOrderID, orderID, row["UID"].ToString());

                                    try
                                    {
                                        OrdersBLL.Instance.OrderOperateLog_Insert(new OrderOperateLogInfo()
                                        {
                                            ThirdOrderID = thirdOrderID,
                                            Tittle = "已发货",
                                            UID = 0,
                                            UserName = row["Sender"].ToString(),
                                            OldStatus = 1,
                                            NewStatus = orderStatus
                                        });
                                    }
                                    catch (Exception ex)
                                    {
                                        ConsoleMsg(string.Format("Mysql OrderOperateLog：{0} 记录日志出错：{1}", orderID, ex.Message));
                                    }
                                }
                                else
                                {
                                    errorSum++;
                                    ConsoleMsg(string.Format("Mysql Orders OrderStatus：{0} 更新失败", orderID));
                                }
                            }
                            catch (Exception ex)
                            {
                                sucessSum++;
                                ConsoleMsg(string.Format("Mysql Orders OrderStatus：{0} 更新出错：{1}", orderID, ex.Message));
                            }
                            #endregion
                            break;
                    }
                    #endregion
                }
                ConsoleMsg(string.Format("OrderStatus 同步完成：{0} 失败：{1}", sucessSum, errorSum));
            }
            catch (Exception ex)
            {
                ConsoleMsg(string.Format("Mysql OrderStatus ERROR：{0}", ex.Message));
            }
            Console.WriteLine("========================= END Mysql OrderStatus\r\n");
            Console.WriteLine("\r\n");
        }

        /// <summary>
        /// 更新快递单号
        /// </summary>
        static void AddOrderLogistics(int thirdOrderID, string orderID, string uid)
        {
            try
            {
                DataTable dTable = OrdersSqlBLL.Instance.GetList(orderID, string.Empty, 0, 0, "OrderLogistics");
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    return;
                }

                //  遍历存储快递单号
                foreach (DataRow row in dTable.Rows)
                {
                    #region 存储快递单号
                    int expressID = int.Parse(row["ExpressID"].ToString());
                    try
                    {
                        OrdersBLL.Instance.OrderLogistics_Insert(new OrderLogisticsInfo()
                        {
                            ThirdOrderID = thirdOrderID,
                            ExpressID = expressID,
                            LogisticsCompany = row["LogisticsCompany"].ToString(),
                            LogisticsNum = row["LogisticsNum"].ToString(),
                            Creator = row["Creator"].ToString(),
                            CreateTime = DateTime.Parse(row["CreateTime"].ToString()),
                        });
                    }
                    catch (Exception ex)
                    {
                        ConsoleMsg(string.Format("OrderLogisticsInfo ThirdOrderID:{0} OrderID：{1} 快递单同步：{2}", thirdOrderID, orderID, ex.Message));
                    }
                    #endregion

                    #region 推送
                    try
                    {
                        JXSdk.Service.PushService.Instance.PushMessage(new JXSdk.Request.PushMessageRequest()
                        {
                            ChannelID = 0,
                            PushType = "single",
                            Template = 1,
                            TargetList = uid,
                            Content = string.Format("您的订单已经拣货完毕，待出库交付{0}，运单号：{1}", GetExpressName(expressID), row["LogisticsNum"].ToString()),
                            TypeID = "3/301",
                            DataID = string.Format("{0}_{1}", expressID, row["LogisticsNum"].ToString()),
                            Creator = "物流",
                            Method = "jxdyf.push.pushmessage.post",
                        });
                    }
                    catch (Exception ex)
                    {
                        ConsoleMsg(string.Format("PushMessage ThirdOrderID:{0} OrderID：{1} 推送失败：{2}", thirdOrderID, orderID, ex.Message));
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ConsoleMsg(string.Format("OrderLogistics ThirdOrderID:{0} OrderID：{1} 快递单出错：{2}", thirdOrderID, orderID, ex.Message));
            }
        }

        /// <summary>
        /// 快递名称
        /// </summary>
        /// <param name="expressID"></param>
        /// <returns></returns>
        static string GetExpressName(int expressID)
        {
            string result = string.Empty;
            switch (expressID)
            {
                case 0:
                case 1:
                    result = "中通快递";
                    break;
                case 5:
                    result = "京东快递";
                    break;
                case 9:
                    result = "顺丰快递";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 消息输出
        /// </summary>
        /// <param name="msg"></param>
        static void ConsoleMsg(string msg)
        {
            myLog.Info(msg);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        static void Binding()
        {
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["UpdateTimeSpan"].ToString(), out updateTimeSpan);
            if (updateTimeSpan < 0)
            {
                updateTimeSpan = 60;
            }
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["UpdateMaxCount"].ToString(), out updateMaxCount);
            if (updateMaxCount < 0)
            {
                updateMaxCount = 100;
            }
            beginTime = DateTime.Now.AddMinutes(-updateTimeSpan);
        }
    }
}
