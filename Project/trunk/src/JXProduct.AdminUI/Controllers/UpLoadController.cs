using JXUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JXAPI.JXSdk.Service;
using JXAPI.JXSdk.Request;
namespace JXProduct.AdminUI.Controllers
{
    public class UpLoadController : Controller
    {
        public ActionResult UpLoadList(string TypeID="1/101")
        {
            //JXAPI.JXSdk.Service.UpLoadService us = UpLoadService.Instance;
            //JXAPI.JXSdk.Request.UpLoadImageRequest req = new UpLoadImageRequest();
            ////string imageByte = ImageConvertUtil.ConvertImageToByte64(Request.Files[0].InputStream);
            //string imageByte = "/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAAA8AAD/4QMraHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjMtYzAxMSA2Ni4xNDU2NjEsIDIwMTIvMDIvMDYtMTQ6NTY6MjcgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDUzYgKFdpbmRvd3MpIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjk2RTEwRjAwRkUwOTExRTRCNTQzRkRBNzQ3OEM3ODQ0IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjk2RTEwRjAxRkUwOTExRTRCNTQzRkRBNzQ3OEM3ODQ0Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6OTZFMTBFRkVGRTA5MTFFNEI1NDNGREE3NDc4Qzc4NDQiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6OTZFMTBFRkZGRTA5MTFFNEI1NDNGREE3NDc4Qzc4NDQiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz7/7gAOQWRvYmUAZMAAAAAB/9sAhAAGBAQEBQQGBQUGCQYFBgkLCAYG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";
            //string type = "health";
            //string filename = "test";
            //string method = "jxdyf.upload.uploadimage.post";
            ////string extName = "jpg";
            //string sourceType = "web";
            ////string filePath = @"pt\ps";
            //req.ImageByte = imageByte;
            //req.SourceType = sourceType;
            //req.Method = method;
            //req.FileName = filename;
            ////req.ExtName = extName;
            //req.Type = type;
            ////req.FilePath = filePath;
            //JXAPI.JXSdk.Utils.JsonResultObject result = us.UpLoadImage(req);

            //JXAPI.JXSdk.Service.UpLoadService us = UpLoadService.Instance;

            //JXAPI.JXSdk.Utils.JsonResultObject result = us.GetUrlByFileName("product");


            //群推
            //JXAPI.JXSdk.Service.PushService ps = PushService.Instance;
            //JXAPI.JXSdk.Request.PushMessageRequest req = new PushMessageRequest();
            //req.PushType = "app";                                            //单推：single 群推：list  应用：app 默认：app
            //req.ChannelID = 1;                                               //ChannelID=可用 区分推送类型：ios(1)与android(2) 方式不一样 默认全推(0)
            //req.Url = "http://www.jxdyf.com/app.apk";                         //Template=2 || template=3 时被使用，用户下载和链接的URL
            //req.Method = "jxdyf.push.pushmessage.post";                       //apiName

            ////上面是推送具体参数，下面是推送具体信息
            //req.Template = 1;                                                 //推送模板  1普通通知，2链接通知，3下载通知
            //req.Content = "测试推送信息";                                     //Template=1 时被使用，正常的主体内容 
            //req.TypeID = TypeID;                                            //推送至哪个目录
            //req.DataID = "111111";                                               //附带参数，例：Section=物流中心后，提示订单状态，DataId转义成orderId
            //req.Creator = "LCJ";                                              //推送信息
            //JXAPI.JXSdk.Utils.JsonResultObject result = ps.PushMessage(req);


            ////单推或者群推
            //JXAPI.JXSdk.Service.PushService ps = PushService.Instance;
            //JXAPI.JXSdk.Request.PushMessageRequest req = new PushMessageRequest();
            //req.PushType = "list";                                            //单推：single 群推：list  应用：app 默认：app
            //req.ChannelID = 0;                                               //ChannelID=a可用 区分推送类型：ios(1)与android(2) 方式不一样 默认全推(0)
            //req.Url = "http://www.jxdyf.com/app.apk";                         //Template=2 || template=3 时被使用，用户下载和链接的URL
            //req.Method = "jxdyf.push.pushmessage.post";                       //apiName
            //req.TargetList = "2000009";                                        //IsAlias=1 TargetList 一组别名中间逗号隔开 ;IsAlias=0 TargetList一组clientid
            //// android 2000009  //ios 2000011
            ////上面是推送具体参数，下面是推送具体信息
            //req.Template = 1;                                                 //推送模板  1普通通知，2链接通知，3下载通知
            //req.Content = "测试推送信息";                                     //Template=1 时被使用，正常的主体内容 
            //req.TypeID = TypeID;                                            //推送至哪个目录
            //req.DataID = "111111";                                               //附带参数，例：Section=物流中心后，提示订单状态，DataId转义成orderId
            //req.Creator = "LCJ";                                              //推送信息
            //JXAPI.JXSdk.Utils.JsonResultObject result = ps.PushMessage(req);

            return View();
        }

        #region 上传图片  图片作byte64字符串流传递方式
        /// <summary>
        /// 上传图片  图片作byte64字符串流传递
        /// </summary>
        /// <param name="imageByte">图片的二进制文件</param>
        /// <param name="sourceType">图片来源（web m ios andriod）</param>
        /// <param name="extName">后缀名 默认为jpg</param>
        /// <param name="imagePath">图片存放路径 默认为 1级： image  2级：image/image </param>
        /// <param name="isNarrow">是否需要缩略图 默认为:0（不需要）</param>
        /// <param name="with">IsNarrow =1 时被使用,缩略图宽度</param>
        /// <param name="height">IsNarrow =1 时被使用,缩略图高度</param>
        /// <returns></returns>
        public JsonResult UpLoadImageByAjax(string imageByte, string sourceType="web", string extName = "jpg"
                                             , string imagePath = "image", int isNarrow = 0, int with = 150, int height = 150)
        {
            var result = new JsonResultObject();

            //保存后的文件名
            string imageFileName = string.Format("{0}_{1}.{2}",
                                   DateTime.Now.ToString("yyyyMMddHHmmssffff"), sourceType, extName);
            if (string.IsNullOrEmpty(imageByte) == false && imageByte != "")
            {
                byte[] ibyte = JXAPI.JXSdk.Utils.ImageConvertUtil.FromBase64ToByte(imageByte);
                //默认存放位置
                //result = ReturnUpLoadStatus(imageFileName, ibyte, extName, "", isNarrow, with, height);
            }
            else 
            {
                result.status = false;
                result.msg = "上传失败,没有解析图片！";
            }
            return Json(result);
        }
        #endregion

        //#region 上传图片  表单提交方式
        //public JsonResult UpLoadImageByForm(string sourceType = "web", string imagePath = "image", int isNarrow = 0, int with = 150, int height = 150)
        //{
        //    //图片作为post From表单请求的一部分
        //    var file = Request.Files[0];
        //    var result = new JsonResultObject();
        //    if (file != null)
        //    {
        //        string imageFileName = file.FileName;
        //        string extName = imageFileName.Split('.')[1];
        //        //保存后的文件名
        //        imageFileName = string.Format("{0}_{1}.{2}",
        //                          DateTime.Now.ToString("yyyyMMddHHmmssffff"), sourceType, extName);
        //        Stream fs = file.InputStream;
        //        string byte64= ImageConvertUtil.ConvertImageToByte64(fs);
        //        byte[] ibyte= ImageConvertUtil.FromBase64ToByte(byte64);
        //        result = ReturnUpLoadStatus(imageFileName, ibyte, extName, imagePath, isNarrow, with, height); 
        //    }
        //    return Json(result);
        //}
        //#endregion

        //#region 最终存放逻辑，并返回数据状态
        //private JsonResultObject ReturnUpLoadStatus(string imageFileName, byte[] ibyte,string extName
        //                                            , string imagePath,int isNarrow, int with, int height) 
        //{
        //    var result = new JsonResultObject();
        //    //默认存放位置
        //    var defaultPath = string.Format(@"{0}\" + imagePath, AppDomain.CurrentDomain.BaseDirectory);
        //    Directory.CreateDirectory(defaultPath);
        //    string defaultFullPath = string.Format(@"{0}\{1}", defaultPath, imageFileName);

        //    FileStream deflultStream = new System.IO.FileStream(defaultFullPath, FileMode.Create, FileAccess.ReadWrite);
        //    deflultStream.Write(ibyte, 0, ibyte.Length);
        //    deflultStream.Close();

        //    //缩略图存放位置
        //    if (isNarrow == 1)
        //    {
        //        ImageConvertibleInfo icInfo = new ImageConvertibleInfo();
        //        byte[] dstImageByte;
        //        icInfo.srcByte = ibyte;
        //        icInfo.dstWidth = with;   // 目标图片像素宽
        //        icInfo.dstHeight = height;  // 目标图片像素高
        //        icInfo.dstFmt = extName; // 目标图片格式
        //        var narrowImagepath = string.Format(@"{0}\" + imagePath + @"\narrow\", AppDomain.CurrentDomain.BaseDirectory);
        //        Directory.CreateDirectory(narrowImagepath);

        //        if ((ImageConvertUtil.ConvertByte64ToImage(icInfo, out dstImageByte)) == 1)
        //        {
        //            ibyte = dstImageByte;
        //        }
        //        string narrowFullImagepath = string.Format(@"{0}\{1}", narrowImagepath, imageFileName);
        //        FileStream narrowStream = new System.IO.FileStream(narrowFullImagepath, FileMode.Create, FileAccess.Write);
        //        narrowStream.Write(ibyte, 0, ibyte.Length);
        //        narrowStream.Close();
        //    }
        //    result.data = "{imageName:" + imageFileName + "}";
        //    result.status = true;
        //    result.msg = "图片已上传完成！";
        //    return result;
        //}
        //#endregion 

        //#region  将图片转成字符串
        //public JsonResult imageByte64() 
        //{
        //    var file = Request.Files[0];
        //    var result = new JsonResultObject();
        //    if (file != null)
        //    {
        //        Stream fs = file.InputStream;
        //        result.data = ImageConvertUtil.ConvertImageToByte64(fs);
        //        result.status = true;
        //        result.msg = "已完成数据转换！";
        //    }
        //    else 
        //    {
        //        result.status = false;
        //    }
        //    return Json(result);
        //}
        //#endregion

      

    }
}
