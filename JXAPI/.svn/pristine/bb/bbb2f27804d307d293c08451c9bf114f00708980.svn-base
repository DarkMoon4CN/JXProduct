using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

using JXAPI.Common.Config;
using JXAPI.Common.Utils;
using JXAPI.Component.Model;
using JXAPI.Component.BLL;
using Venus;
using log4net;
using JXAPI.Component;
using JXPAI.Service.Base;
using JXAPI.Service.Models;
using JXAPI.Component.Enums;
using JXAPI.Common;


namespace JXPAI.Service.Controllers.Orders
{
    public class OrdersService : Venus.VenusServiceResponse
    {
        static ILog log = LogManager.GetLogger("Default");

        public string ApiName
        {
            get
            {
                return "jxdyf.orders.detail.get";
            }
        }

        /// <summary>
        /// 收货人 收货地址 订单金额
        /// </summary>
        /// <returns></returns>
        public override string ResultGet()
        {
            JsonResultObject result = null;
            try
            {
                string orderId = string.Empty, mobile = string.Empty;
                if (HttpContext.Current.Request["orderId"] != null)
                {
                    orderId = HttpContext.Current.Request["orderId"];
                } if (HttpContext.Current.Request["mobile"] != null)
                {
                    mobile = HttpContext.Current.Request["mobile"];
                }
                if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(mobile))
                {
                    result = new JsonResultObject(false, "订单号/手机号不允许为空", null);
                    return ResultResponse(result, "info");
                }

                //  查询订单
                OrdersInfo info = JXAPI.Component.BLL.OrdersSqlBLL.Instance.GetByOrderId(orderId, mobile);
                if (info == null)
                {
                    result = new JsonResultObject(false, "未找到相关订单", null);
                }
                else
                {
                    //  订单明细
                    System.Text.StringBuilder sbPro = new System.Text.StringBuilder();
                    try
                    {
                        IList<OrderProductInfo> list = JXAPI.Component.BLL.OrdersSqlBLL.Instance.GetOrderProduct(info.OrderID);
                        if (list != null && list.Count > 0)
                        {
                            foreach (OrderProductInfo item in list)
                            {
                                sbPro.Append("{");
                                sbPro.Append(item.ProductName + ",");
                                sbPro.Append(item.ProfeeDiscount + ",");
                                sbPro.Append(item.Quantity);
                                sbPro.Append("}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(string.Format("订单：{0} 查询明细失败：{1}", info.OrderID, ex.Message));
                    }

                    //  返回参数
                    result = new JsonResultObject(true, "succeed", new
                    {
                        OrderID = info.OrderID,
                        PaySum = info.PaySum,
                        PaymentMethodID = info.PaymentMethodID,
                        Province = info.Province,
                        City = info.City,
                        County = info.County,
                        Receiver = info.Receiver,
                        Telephone = info.Telephone,
                        Mobile = info.Mobile,
                        Address = info.Address,
                        Product = sbPro.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                result = new JsonResultObject(false, ex.Message, null);
            }
            return ResultResponse(result, "info");
        }

        public override void Validate()
        {

        }

        /// <summary>
        /// 更新后台订单订单支付方式
        /// </summary>
        /// <returns></returns>
        public string Orders_UpdatePayMethod()
        {
            string orderId = string.Empty;
            JsonResultObject result = null;
            try
            {
                //  订单
                if (HttpContext.Current.Request["orderId"] != null)
                {
                    orderId = HttpContext.Current.Request["orderId"];
                }
                if (string.IsNullOrEmpty(orderId))
                {
                    result = new JsonResultObject(false, "未指定订单", null);
                    return ResultResponse(result, "info");
                }

                //  参数
                if (HttpContext.Current.Request["paymethod"] == null)
                {
                    result = new JsonResultObject(false, "未指定支付方式", null);
                    return ResultResponse(result, "info");
                }
                int paymethod = 0;
                int.TryParse(HttpContext.Current.Request["paymethod"], out paymethod);
                
                //  查询订单状态
                OrdersInfo oInfo = OrdersSqlBLL.Instance.GetByOrderId(orderId);
                if (oInfo == null)
                {
                    result = new JsonResultObject(false, string.Format("未找到订单：{0}", orderId), null);
                    return ResultResponse(result, "info");
                }
                else if (oInfo.PaymentMethodID == paymethod)
                {
                    result = new JsonResultObject(true, "支付方式修改成功！", null);
                    return ResultResponse(result, "info");
                }

                //  更改支付方式
                string[] colName = { "PaymentMethodID" };
                bool code = OrdersSqlBLL.Instance.Orders_Update(oInfo.OrderID, colName, new object[] { paymethod });
                if (code)
                {
                    OrderOperateLogInfo info = new OrderOperateLogInfo()
                    {
                        OrderID = oInfo.OrderID,
                        Tittle = string.Format("修改支付方式：{0}", oInfo.PaymentMethodID),
                        OldStatus = oInfo.OrderStatus,
                        NewStatus = oInfo.OrderStatus
                    };
                    OrdersSqlBLL.Instance.OrderOperateLog_Insert(info);
                    result = new JsonResultObject(true, "支付方式修改成功！", null);
                }
                else
                {
                    result = new JsonResultObject(false, string.Format("订单号：{0} 修改支付方式失败！", oInfo.OrderID), null);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("OrderID：{0} 修改支付方式失败：{1}", orderId, ex.Message));
                result = new JsonResultObject(false, ex.Message, null);
            }
            return ResultResponse(result, "info");
        }

        /// <summary>
        /// 修改后台订单支付状态
        /// </summary>
        /// <returns></returns>
        public string Orders_UpdatePayStatus()
        {
            string orderId = string.Empty;
            JsonResultObject result = null;
            try
            {
                //  订单
                if (HttpContext.Current.Request["orderId"] != null)
                {
                    orderId = HttpContext.Current.Request["orderId"];
                }
                if (string.IsNullOrEmpty(orderId))
                {
                    result = new JsonResultObject(false, "未指定订单", null);
                    return ResultResponse(result, "info");
                }

                //  参数
                int paymethodID = 0;
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["PaymentMethodID"]))
                {
                    int.TryParse(HttpContext.Current.Request["PaymentMethodID"], out paymethodID);
                }

                //  参数
                string serialNumber = string.Empty;
                if (HttpContext.Current.Request["SerialNumber"] != null)
                {
                    serialNumber = HttpContext.Current.Request["SerialNumber"];
                }

                /*
                //  验证签名
                string sign = string.Empty;
                if (HttpContext.Current.Request["sign"] != null)
                {
                    sign = HttpContext.Current.Request["sign"];
                }
                */

                //  查询订单状态
                OrdersInfo oInfo = OrdersSqlBLL.Instance.GetByOrderId(orderId);
                if (oInfo == null)
                {
                    result = new JsonResultObject(false, string.Format("未找到订单：{0}", orderId), null);
                    return ResultResponse(result, "info");
                }
                else if (oInfo.PayStatus > 0)
                {
                    result = new JsonResultObject(true, "支付成功！", null);
                    return ResultResponse(result, "info");
                }

                //  更改支付状态
                OrderOperateLogInfo info = new OrderOperateLogInfo()
                {
                    OrderID = oInfo.OrderID,
                    Tittle = "已付款",
                    OldStatus = 0,
                    NewStatus = 1
                };
                string code = OrdersSqlBLL.Instance.Orders_Dispose(info, 1);
                if (code == "0")
                {
                    log.Info(string.Format("OrderID：{0} 支付成功", oInfo.OrderID));

                    //  更改支付方式
                    string[] colName = { "PaymentMethodID", "SerialNumber" };
                    int paymethod = (oInfo.PaymentMethodID > 1) ? oInfo.PaymentMethodID : 22;
                    OrdersSqlBLL.Instance.Orders_Update(info.OrderID, colName, new object[] { paymethod, serialNumber.ToString() });
                    result = new JsonResultObject(true, "支付成功！", null);
                }
                else
                {
                    log.Info(string.Format("OrderID：{0} 支付失败：{1}", oInfo.OrderID, code));
                    result = new JsonResultObject(false, string.Format("订单号：{0} 支付失败：{1}！", info.OrderID, code), null);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("OrderID：{0} 更新支付状态失败：{1}", orderId, ex.Message));
                result = new JsonResultObject(false, ex.Message, null);
            }
            return ResultResponse(result, "info");
        }

        /// <summary>
        /// 新增联盟订单
        /// </summary>
        public string UnionOrders_Insert()
        {
            int orderId = 0;
            JsonResultObject result = null;
            try
            {
                //  订单
                if (HttpContext.Current.Request["orderId"] != null)
                {
                    int.TryParse(HttpContext.Current.Request["orderId"], out orderId);
                }
                string unionPara = string.Empty;
                if (HttpContext.Current.Request["unionPara"] != null)
                {
                    unionPara = HttpContext.Current.Request["unionPara"];
                }
                if (orderId <= 0 || string.IsNullOrEmpty(unionPara))
                {
                    result = new JsonResultObject(false, "未指定联盟订单/用户", null);
                    return ResultResponse(result, "info");
                }
                //  参数
                int advertId = 0, siteId = 0;
                if (HttpContext.Current.Request["advertId"] != null)
                {
                    int.TryParse(HttpContext.Current.Request["advertId"], out advertId);
                }
                if (HttpContext.Current.Request["siteId"] != null)
                {
                    int.TryParse(HttpContext.Current.Request["siteId"], out siteId);
                }
                string source = string.Empty, clientIP = string.Empty, clientIdentity = string.Empty, customerInfo = string.Empty;
                if (HttpContext.Current.Request["source"] != null)
                {
                    source = HttpContext.Current.Request["source"];
                }
                if (HttpContext.Current.Request["clientIP"] != null)
                {
                    clientIP = HttpContext.Current.Request["clientIP"];
                }
                if (HttpContext.Current.Request["clientIdentity"] != null)
                {
                    clientIdentity = HttpContext.Current.Request["clientIdentity"];
                }
                if (HttpContext.Current.Request["customerInfo"] != null)
                {
                    customerInfo = HttpContext.Current.Request["customerInfo"];
                }

                //  查询订单
                string code = OrdersSqlBLL.Instance.UnionOrders_Insert(orderId, unionPara, source, advertId, siteId, clientIP, clientIdentity, customerInfo);
                if (code == "0")
                {
                    result = new JsonResultObject(true, "保存联盟订单成功！", null);
                }
                else
                {
                    log.Error(string.Format("Union OrderID：{0} 失败：{1}", orderId, code));
                    result = new JsonResultObject(false, string.Format("保存联盟订单失败：{0}", orderId), null);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Union OrderID：{0} 失败：{1}", orderId, ex.Message));
                result = new JsonResultObject(false, ex.Message, null);
            }
            return ResultResponse(result, "info");
        }

        /// <summary>
        /// 联盟订单列表
        /// </summary>
        public string UnionOrders_GetList()
        {
            JsonResultObject result = null;
            try
            {
                //  参数
                string unionPara = string.Empty;
                if (HttpContext.Current.Request["unionPara"] != null)
                {
                    unionPara = HttpContext.Current.Request["unionPara"];
                }
                if (string.IsNullOrEmpty(unionPara))
                {
                    result = new JsonResultObject(false, "未指定查询用户", null);
                    return ResultResponse(result, "info");
                }

                string start = string.Empty, end = string.Empty;
                if (HttpContext.Current.Request["start"] != null)
                {
                    start = HttpContext.Current.Request["start"];
                }
                if (HttpContext.Current.Request["end"] != null)
                {
                    end = HttpContext.Current.Request["end"];
                }

                //  分页
                int pageIndex = 1, pageSize = 10;
                if (HttpContext.Current.Request["pageIndex"] != null)
                {
                    pageIndex = int.Parse(HttpContext.Current.Request["pageIndex"].ToString());
                }
                if (HttpContext.Current.Request["pageSize"] != null)
                {
                    pageIndex = int.Parse(HttpContext.Current.Request["pageSize"].ToString());
                }

                //  查询
                System.Text.StringBuilder strWhere = new System.Text.StringBuilder("o.OrderID!=o.ThirdOrderID");
                if (!string.IsNullOrEmpty(unionPara))
                {
                    strWhere.Append(string.Format(" AND o.unionpara='{0}'", unionPara));
                }
                if (!string.IsNullOrEmpty(start))
                {
                    strWhere.Append(string.Format(" AND o.createtime>='{0}'", start));
                }
                if (!string.IsNullOrEmpty(end))
                {
                    strWhere.Append(string.Format(" AND o.createtime<='{0}'", end));
                }

                int recCount = 0;
                IList<OrdersInfo> list = OrdersSqlBLL.Instance.UnionOrders_GetList(pageIndex, pageSize, "o.CreateTime DESC", strWhere.ToString(), out recCount);
                if (list == null || list.Count <= 0)
                {
                    result = new JsonResultObject(false, "无相关结果", null);
                }
                else
                {
                    return ResultResponse(list, "list");
                }
            }
            catch (Exception ex)
            {
                result = new JsonResultObject(false, ex.Message, null);
            }
            return ResultResponse(result, "info");
        }


        /// <summary>
        /// 金象隐形眼镜 自动设置订单已发货
        /// </summary>
        /// <returns></returns>
        public string  Shipping_Status()
        {
            var result = new JsonResult();
            string appid = HttpContext.Current.Request["appid"];
            string sign = HttpContext.Current.Request["sign"];
            string jsonData = HttpContext.Current.Request["jsonData"];

            //验证秘钥
            bool isPass = Security.SecurityCheck(appid, sign, jsonData);
            if (!isPass)
            {
                result.code = (int)OrderError.接口秘钥无效;
                result.message = OrderError.接口秘钥无效.ToString();
                result.data = null;
                return JsonHelper.GetJson(result);
            }

            //数据更新异常结果集
            IList<string> errorOrderIDList = new List<string>();

            //序列化json
            IList<ShippingInfo> spInfoList = JsonHelper.ConvertToObj<IList<ShippingInfo>>(jsonData);

            for (int i = 0; i < spInfoList.Count; i++)
            {
                //更新数据
                DateTime createTime = ConvertDataTimeHelper.ConvertLongDateTime(spInfoList[i].Shipped_Time);
                OrderLogisticsInfo olInfo = new OrderLogisticsInfo()
                {
                    OrderID = spInfoList[i].OrderID,
                    ExpressID = Convert.ToInt32(spInfoList[i].Express_Id),
                    LogisticsCompany = spInfoList[i].Express_Name,
                    LogisticsNum = spInfoList[i].Express_Sn,
                    UID = 0,
                    Creator = spInfoList[i].Sender,
                    CreateTime = createTime
                };
                OperationResult<int> olResult = OrderLogisticsSqlBLL.Instance.OrderLogistics_Insert(olInfo);
                if (olResult.AppendData > 0)
                {
                    int logisticsID = olResult.AppendData;
                    OrdersInfo oInfo = new OrdersInfo()
                    {
                        OrderID = spInfoList[i].OrderID,
                    };
                    olResult = OrderLogisticsSqlBLL.Instance.Order_Update(oInfo, "可得");
                    if (olResult.ResultType!= OperationResultType.Success) 
                    {
                        //没有更新成功 取消插入
                        OrderLogisticsSqlBLL.Instance.OrderLogistics_Delete(logisticsID);
                        errorOrderIDList.Add(spInfoList[i].OrderID);
                        continue;
                    }
                }
                else
                {
                    errorOrderIDList.Add(spInfoList[i].OrderID);
                    continue;
                }
            }
            if (errorOrderIDList.Count() > 0)
            {
                result.data = JsonHelper.GetJson(errorOrderIDList);
                result.code = (int)OrderError.未知的订单号;
                result.message = OrderError.未知的订单号.ToString();
            }
            else
            {
                result.code = (int)OrderError.数据返回成功;
                result.message = OrderError.数据返回成功.ToString();
                result.data = null;
            }

            return JsonHelper.GetJson(result);
        }

        /// <summary>
        /// 拉取订单待发货订单
        /// </summary>
        /// <returns></returns>
        public string List()
        {
            var result = new JsonResult();
            string appid= HttpContext.Current.Request["appid"];
            string sign = HttpContext.Current.Request["sign"];
            string jsonData = HttpContext.Current.Request["jsonData"];

            //验证秘钥
            bool isPass=Security.SecurityCheck(appid,sign,jsonData);
            if (!isPass)
            {
                result.code = (int)OrderError.接口秘钥无效;
                result.message = OrderError.接口秘钥无效.ToString();
                result.data = null;
                return JsonHelper.GetJson(result);
            }

            //序列化json
            PageForm pageFrom = JsonHelper.ConvertToObj<PageForm>(jsonData);

            string strWhere = string.Empty;

            strWhere += " CreateTime > '" + ConvertDataTimeHelper.ConvertLongDateTime(pageFrom.StartTime).ToString() + "'";
            strWhere += " AND CreateTime < '" + ConvertDataTimeHelper.ConvertLongDateTime(pageFrom.EndTime).ToString() + "'";
            int count = 0;//总条数
            OperationResult<IList<OrdersInfo>> orderInfoList =
                     OrderLogisticsSqlBLL.Instance.Orders_GetList(pageFrom.Page, pageFrom.Size, " CreateTime DESC ", strWhere, 0, out count);

            OperationResult<IList<OrderProductInfo>> orderProductInfoList =
                             OrderLogisticsSqlBLL.Instance.OrderProduct_GetList(strWhere);
            //数据组合
            IList<OrdersResponse> response = new List<OrdersResponse>();
            foreach (var o in orderInfoList.AppendData)
            {
                OrdersResponse or = new OrdersResponse();
                or.OrderID = o.OrderID;
                or.PaySum = o.PaySum;
                or.ProductFee = o.ProductFee;
                or.NewShipFee = o.NewShipFee;
                or.VoucherFee = o.VoucherFee;
                or.ShipMethodID = o.ShipMethodID;
                or.Province = o.Province;
                or.City = o.City;
                or.County = o.County;
                or.Address = o.Address;
                or.Receiver = o.Receiver;
                or.Telephone = o.Telephone;
                or.Mobile = o.Mobile;
                or.Invoice = o.Invoice;
                or.Remarks = o.Remarks;
                or.CreateTime = ConvertDataTimeHelper.ConvertDataTimeLong(o.CreateTime);
                or.OrderDetails = new List<OrderProductInfo>();
                foreach (var op in orderProductInfoList.AppendData)
                {
                    if (o.OrderID == op.OrderID)
                    {
                        OrderProductInfo opInfo = new OrderProductInfo();
                        opInfo.OrderID = op.OrderID;
                        opInfo.ProductCode = op.ProductCode;
                        opInfo.ProductID = op.ProductID;
                        opInfo.ProductName = op.ProductName;
                        opInfo.ProfeeDiscount = op.ProfeeDiscount;
                        opInfo.Quantity = opInfo.Quantity;
                        or.OrderDetails.Add(opInfo);
                    }
                }
                response.Add(or);
            }
            //返回结果
            if (orderInfoList.ResultType != OperationResultType.Success)
            {
                result.message = orderInfoList.Message;
                result.code = (int)OrderError.数据连接失败;
                result.data = null;
            }
            else
            {
                result.message = OrderError.数据返回成功.ToString();
                result.code = (int)OrderError.数据返回成功;
                result.data = JsonHelper.GetJson(response);
            }
            return JsonHelper.GetJson(result);
        }
    }
}
