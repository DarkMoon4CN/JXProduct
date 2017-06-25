using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace JXAPI.Common.Utils
{
    public class CommUtil
    {
        /// <summary>
        /// 请求获取页面返回信息
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encodetype">编码</param>
        /// <returns></returns>
        public static string SendGetWebRequest(string url, string encodetype)
        {
            string result = string.Empty;

            //  请求配送数据源
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            req.Timeout = 30000;    //  30秒
            using (WebResponse wr = req.GetResponse())
            {
                using (StreamReader responseReader = new StreamReader(wr.GetResponseStream(), Encoding.GetEncoding(encodetype)))
                {
                    result = responseReader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
