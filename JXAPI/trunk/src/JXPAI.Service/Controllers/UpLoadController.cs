﻿using JXAPI.Common.Utils;
using JXPAI.Service.Controllers.UpLoad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using JXAPI.Common.Config;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Text;
namespace JXPAI.Service.Controllers
{
    public class UpLoadController : ApiController
    {
        public JsonResultObject Get()
        {
            var result = new JsonResultObject();
            string jsonStr = string.Empty;
            string method = HttpContext.Current.Request["method"];
            UpLoad.UpLoadService uploadService = new UpLoadService();
            if (string.IsNullOrEmpty(method))
            {
                result.msg = "上传失败,缺少必填项：method！";
            }
            else
            {
                switch (method)
                {
                    case "jxdyf.upload.uploadimage.post":
                        result = uploadService.UpLoadImage();
                        break;
                    case "jxdyf.upload.byse64.post":
                        result = uploadService.ImageBase64();
                        break;
                }
            }
            HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return result;
        }

        public JsonResultObject Post() 
        {
            //log4net初始化
            log4net.Config.XmlConfigurator.Configure();

            var result = new JsonResultObject();
            string jsonStr = string.Empty;
            string method = HttpContext.Current.Request["method"];
            UpLoad.UpLoadService uploadService = new UpLoadService();
            if (string.IsNullOrEmpty(method))
            {
                result.msg = "上传失败,缺少必填项：method！";
            }
            else
            {
                switch (method)
                {
                    case "jxdyf.upload.uploadimage.post":
                        result = uploadService.UpLoadImage();
                        break;
                    case "jxdyf.upload.byse64.post":
                        result = uploadService.ImageBase64();
                        break;
                }
            }
            HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return result;
        }

    }
}
