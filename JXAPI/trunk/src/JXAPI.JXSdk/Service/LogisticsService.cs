using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Config;
using JXAPI.JXSdk.Utils;
using System.Net;
using System.IO;
using JXAPI.JXSdk.Base;
using JXAPI.JXSdk.Response;
using JXAPI.JXSdk.Request;

namespace JXAPI.JXSdk.Service
{
    public class LogisticsService
    {
        public static readonly SecureConfigSection logisticsConfig = SecureConfigSection.Instance;
        public static LogisticsService Instance
        {
            get { return new LogisticsService(); }
        }

        public LogisticsResponse Orders_GetExpressList(LogisticsRequest request)
        {
            var collection = logisticsConfig.Secures;
            string jsonStr = string.Empty;
            string upLoadImageUrl = collection["logistics"].URL;
            //var result = new JsonResultObject();
            var result = new LogisticsResponse();
            //  请求
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(upLoadImageUrl);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            httpWebRequest.Method = "POST";
            string data = "method=jxdyf.logistics.list.get";
            data += "&expressId=" + request.expressId + "&code=" + request.code;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);

            //req.ImageByte = req.ImageByte.Replace("+", "%2B");
            httpWebRequest.ContentLength = bytes.Length;
            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Flush();
            requestStream.Close();

            //  响应
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                string responseContent = streamReader.ReadToEnd();
                httpWebResponse.Close();
                streamReader.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();
                result = JsonHelper.ConvertToObj<LogisticsResponse>(responseContent);

            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}
