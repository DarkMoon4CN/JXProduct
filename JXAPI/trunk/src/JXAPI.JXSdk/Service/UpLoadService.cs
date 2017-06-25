using JXAPI.JXSdk.Config;
using JXAPI.JXSdk.Request;
using JXAPI.JXSdk.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
namespace JXAPI.JXSdk.Service
{
    /// <summary>
    /// 图片上传
    /// </summary>
    public class UpLoadService
    {
        public static readonly SecureConfigSection upLoadConfig = SecureConfigSection.Instance;
        public static UpLoadService Instance
        {
            get { return new UpLoadService(); }
        }

        public JsonResultObject UpLoadImage(UpLoadImageRequest req) 
        {
            var collection = upLoadConfig.Secures;
            string jsonStr = string.Empty;
            string upLoadImageUrl = collection["upload"].URL;
            var result = new JsonResultObject();

            //  请求
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(upLoadImageUrl);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            httpWebRequest.Method = "POST";
            //  imageByte 是base64, UTF8会修改特殊符号，在转码之前必要的匹配掉+号
            req.ImageByte = req.ImageByte.Replace("+", "%2B");
            string data = "type=" + req.Type + "&fileName=" + req.FileName + "&method=" + req.Method ;
            data += "&extName=" + req.ExtName + "&sourceType=" + req.SourceType + "&imageByte=" + req.ImageByte;
            data += "&filePath="+req.FilePath;
            data += "&index="+req.Index;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
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
                result = JsonHelper.ConvertToObj<JsonResultObject>(responseContent);

            }
            catch 
            {
                result.status = false;
                result.msg = "上传失败，网络连接失败！";
            }
            return result;
        }

        public JsonResultObject GetUrlByFileName(string fileName) 
        {
            var collection = upLoadConfig.Secures;
            string jsonStr = string.Empty;
            string upLoadImageUrl = collection["upload"].URL;
            var result = new JsonResultObject();
            //  请求
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(upLoadImageUrl);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            httpWebRequest.Method = "POST";
            string data = "method=jxdyf.upload.geturl.post";
                   data += "&fileName=" + fileName;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
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
                result = JsonHelper.ConvertToObj<JsonResultObject>(responseContent);

            }
            catch
            {
                result.status = false;
                result.msg = "获取失败，网络连接失败！";
            }
            return result;
        }
    }
}
