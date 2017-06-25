using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JXAPI.Common.Utils;
using log4net;

namespace JXPAI.Service.Controllers.YaoShiKa
{
    public class YaoShiKaService : Venus.VenusServiceResponse
    {
        static ILog log = LogManager.GetLogger("Default");
        public string ApiName
        {
            get
            {
                return "jxdyf.yaoshika.operate";
            }
        }

        public override string ResultGet()
        {
            #region 参数
            string msg = string.Empty;
            JsonResult result = new JsonResult();
            if (string.IsNullOrEmpty(HttpContext.Current.Request["order"]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request["createTime"]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request["no"]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request["pwd"]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request["paySum"]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request["sbPro"]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request["uid"]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request["sign"]))
            {
                msg = "钥匙卡支付信息未完善";
                log.Error(msg);
                result.Data = new JsonResultObject(false, msg, new { orderID = string.Empty, UID = string.Empty, rrn = string.Empty, paySum = string.Empty, rcDetail = msg });
                return JsonHelper.ConvertToJson(result.Data);
            }

            string order = HttpContext.Current.Request["order"];
            string createTime = HttpContext.Current.Request["createTime"];
            string no = HttpContext.Current.Request["no"];
            string pwd = HttpContext.Current.Request["pwd"];
            decimal paySum = decimal.Parse(HttpContext.Current.Request["paySum"]);
            string sbPro = HttpContext.Current.Request["sbPro"];
            string uid = HttpContext.Current.Request["uid"];
            string sign = HttpContext.Current.Request["sign"];
            string product = HttpContext.Current.Request["product"];

            //  验证签名
            string strSign = GetMd5Str(order + "*" + no + "#*" + pwd + "(" + paySum + ")=" + sbPro, 32);
            if (!strSign.Contains(sign))
            {
                msg = string.Format("Order：{0} PAN：{1} 钥匙卡签名验证失败", order, no);
                log.Error(msg);
                result.Data = new JsonResultObject(false, msg, new { orderID = order, UID = uid, rrn = string.Empty, paySum = paySum, rcDetail = msg });
                return JsonHelper.ConvertToJson(result.Data);
            }
            #endregion

            try
            {
                YaoShiKaAPI.SvcService svcs = new YaoShiKaAPI.SvcService();
                YaoShiKaAPI.SvcInfo svcInfo1 = new YaoShiKaAPI.SvcInfo();
                svcInfo1.txnId = "W505";
                svcInfo1.pan = no;                      //  卡号
                svcInfo1.pinData = pwd;                 //  密码
                svcInfo1.teller = "WsJinX";             //  WS操作员
                svcInfo1.password = "av42cv1yhd";       //
                YaoShiKaAPI.SvcInfo respInfo1 = svcs.transX(svcInfo1);
                if (respInfo1 == null)
                {
                    msg = string.Format("Order：{0} PAN：{1} 钥匙卡卡号或者卡密不正确", order, no);
                    log.Error(msg);
                    result.Data = new JsonResultObject(false, msg, new { orderID = order, UID = uid, rrn = string.Empty, paySum = paySum, rcDetail = msg });
                }

                YaoShiKaAPI.SvcInfo svcInfo = new YaoShiKaAPI.SvcInfo();
                svcInfo.txnId = "502";
                svcInfo.pan = no;                       //  卡号
                svcInfo.trAmt = paySum.ToString();      //  消费金额
                svcInfo.mid = "101001001000001";        //  本系统商户号
                svcInfo.pinData = respInfo1.pinData;    //  密码
                svcInfo.shopId = "";                    //  门店号

                svcInfo.hbInfo = EncodeUtil.Base64Encode(product);      //  核保药品明细
                svcInfo.voucher = string.Format("{0}", order);          //  格式为YYYYMMDD+最大12位顺序号，在线消费订单号，同一商户不可重复
                svcInfo.teller = "WsJinX";              //  WS操作员
                svcInfo.password = "av42cv1yhd";        //
                YaoShiKaAPI.SvcInfo respInfo = svcs.transX(svcInfo);
                if (respInfo.rc == "00" && decimal.Parse(respInfo.trAmt) == paySum)
                {
                    msg = string.Format("Order：{0} PAN：{1} RRN:{2} 订单在线支付完成", order, no, respInfo.rrn);
                    log.Info(msg);
                    result.Data = new JsonResultObject(true, msg, new { orderID = order, UID = uid, rrn = respInfo.rrn, paySum = paySum, rcDetail = respInfo.rcDetail });
                }
                else if (decimal.Parse(respInfo.trAmt) != paySum)
                {
                    msg = string.Format("Order：{0} PAN：{1} 支付金额有误，请联系客服", order, no);
                    log.Error(msg);
                    result.Data = new JsonResultObject(false, msg, new { orderID = order, UID = uid, rrn = string.Empty, paySum = paySum, rcDetail = respInfo.rcDetail });
                }
                else if (respInfo.rc == "51")
                {
                    msg = string.Format("Order：{0} PAN：{1} 钥匙卡余额不足", order, no);
                    log.Error(msg);
                    result.Data = new JsonResultObject(false, msg, new { orderID = order, UID = uid, rrn = string.Empty, paySum = paySum, rcDetail = respInfo.rcDetail });
                }
                else
                {
                    msg = string.Format("Order：{0} PAN：{1} 钥匙卡卡号或者卡密有误", order, no);
                    log.Error(msg);
                    result.Data = new JsonResultObject(false, msg, new { orderID = order, UID = uid, rrn = string.Empty, paySum = paySum, rcDetail = respInfo.rcDetail });
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Order：{0} PAN：{1} {2}", ex.Message, order, no);
                log.Error(msg);
                result.Data = new JsonResultObject(false, msg, new { orderID = order, UID = uid, rrn = string.Empty, paySum = paySum, rcDetail = "服务器异常" });
            }
            return JsonHelper.ConvertToJson(result.Data);
        }

        /// <summary>
        /// MD5 16位加密 加密后密码为小写
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string GetMd5Str(string ConvertString, int code)
        {
            if (code == 16)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ConvertString, "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ConvertString, "MD5").ToLower();
            }
            return "00000000000000000000000000000000";
        }

        public override void Validate()
        {

        }
    }
}
