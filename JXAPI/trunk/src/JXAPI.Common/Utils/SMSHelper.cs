using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Configuration;
using System.Collections;

namespace JXAPI.Common.Utils
{
    public class SMSHelper
    {
        public static Hashtable config = (Hashtable)ConfigurationManager.GetSection("sms");

        /// <summary>
        /// 发送短信，给系统管理员发送短信
        /// </summary>
        /// <param name="content">短信内容</param>
        public static string Send(string content)
        {
            string mobile = config["mobile"].ToString();
            if (!string.IsNullOrEmpty(mobile))
                return Send(content, mobile);

            return string.Empty;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="content">短信内容</param>
        /// <param name="mobile">手机号码，多号码使用‘，’</param>
        public static string Send(string content, string mobile)
        {
            string result = string.Empty;
            try
            {
                string encodetype = "UTF-8";
                Encoding myEncoding = Encoding.GetEncoding(encodetype);
                string address = string.Format("{0}?username={1}&password={2}&content={3}&mobile={4}&dstime=&productid={5}", config["submiturl"].ToString(), config["username"].ToString(), config["password"].ToString(), HttpUtility.UrlEncode(content, myEncoding), mobile, config["productid"].ToString());
                result = CommUtil.SendGetWebRequest(address, encodetype);
                if (result.Length > 1 && result.Substring(0, 2) == "1,") result = "成功";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}