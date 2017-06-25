<%@ webhandler Language="C#" class="Upload" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;

public class Upload : IHttpHandler
{
	private HttpContext context;

	public void ProcessRequest(HttpContext context)
	{
        context = HttpContext.Current;
        int fileSize = 5;   //  单位：MB
        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };
        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError("请选择文件。");
        }
        JXAPI.JXSdk.Utils.JsonResultObject result = new JXAPI.JXSdk.Utils.JsonResultObject();
        JXAPI.JXSdk.Service.UpLoadService us = JXAPI.JXSdk.Service.UpLoadService.Instance;
        JXAPI.JXSdk.Request.UpLoadImageRequest req = new JXAPI.JXSdk.Request.UpLoadImageRequest();
        var fs = imgFile.InputStream;
        string fileName = imgFile.FileName;
        if (Array.IndexOf(filetype, "." + req.ExtName) == -1)
        {
            showError("非图片类型不可上传。");
        }
        else if (imgFile.ContentLength > (fileSize * 1024 * 1024))
        {
            showError("图片大小超过限制：" + fileSize + "MB！");
        }
        else 
        {
            //  图片上传
            string imageByte = string.Empty;
            if (fs != null)
            {
                imageByte = JXAPI.JXSdk.Utils.ImageConvertUtil.ConvertImageToBase64(fs);
            }
            if (fileName != null && fileName.IndexOf(".") != -1)
            {
                string[] splitFileName = fileName.Split('.');
                req.ExtName = splitFileName[1];
            }
            req.ImageByte = imageByte;
            req.Method = "jxdyf.upload.uploadimage.post";
            req.Type = "other";
            req.SourceType = "web";
            result = us.UpLoadImage(req);
            if (!result.status)
            {
                showError(result.msg);
            }
            System.Collections.Generic.List<JXAPI.JXSdk.Request.ImagePathRequest> imagePathArray =
                               JXAPI.JXSdk.Utils.JsonHelper.ConvertToObj<System.Collections.Generic.List<JXAPI.JXSdk.Request.ImagePathRequest>>(result.data.ToString());
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");

            string outImageUrl = string.Empty;
            if (imagePathArray != null && imagePathArray.Count > 0)
            {
                outImageUrl = imagePathArray[0].Url + imagePathArray[0].ImagePath;
            }
            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = outImageUrl;
            string outJson = JXAPI.JXSdk.Utils.JsonHelper.ConvertToJson(hash);
            context.Response.Write(outJson);
            context.Response.End();
        }
	}

	private void showError(string message)
	{
        context = HttpContext.Current;
		Hashtable hash = new Hashtable();
		hash["error"] = 1;
		hash["message"] = message;
		context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JXUtil.JsonHelper.ConvertToJson(hash));
		context.Response.End();
	}

	public bool IsReusable
	{
		get
		{
			return true;
		}
	}
}
