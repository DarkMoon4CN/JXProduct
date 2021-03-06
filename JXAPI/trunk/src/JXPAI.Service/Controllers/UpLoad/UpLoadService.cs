﻿using JXAPI.Common.Config;
using JXAPI.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using JXAPI.Component.Model;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using log4net;

namespace JXPAI.Service.Controllers.UpLoad
{
    public class UpLoadService
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(UpLoadService));
        #region 上传图片  图片作byse64字符串流传递方式
        /// <summary>
        /// 上传图片  图片作byse64字符串流传递
        /// </summary>
        public JsonResultObject UpLoadImage()
        {
            myLog.InfoFormat("开始");
            var result = new JsonResultObject();
            UpLoadImageInfo upLoadImageInfo = new UpLoadImageInfo();
            upLoadImageInfo.ImageByte = HttpContext.Current.Request["imageByte"];
            upLoadImageInfo.SourceType = HttpContext.Current.Request["sourceType"];
            upLoadImageInfo.ExtName = HttpContext.Current.Request["extName"];
            upLoadImageInfo.Type = HttpContext.Current.Request["type"];
            upLoadImageInfo.FilePath = HttpContext.Current.Request["filePath"];
            upLoadImageInfo.FileName = HttpContext.Current.Request["fileName"];
            upLoadImageInfo.FilePath= upLoadImageInfo.FilePath==""? null :upLoadImageInfo.FilePath;
            upLoadImageInfo.FileName = upLoadImageInfo.FileName == "" ? null : upLoadImageInfo.FileName;
            string index =HttpContext.Current.Request["index"];

            if (index != null && index != "" && index != "0") 
            {
                upLoadImageInfo.Index = int.Parse(index);
            }
            if (upLoadImageInfo.Type == null || upLoadImageInfo.Type == "")
            {
                upLoadImageInfo.Type = "other";
            }
            if (string.IsNullOrEmpty(upLoadImageInfo.ImageByte) == false && upLoadImageInfo.ImageByte != "")
            {
                try
                {
                    byte[] ibyte = ImageConvertUtil.FromBase64ToByte(upLoadImageInfo.ImageByte);
                    result = ReturnUpLoadStatus(ibyte, upLoadImageInfo); //存放图片，并返回数据状态
                }
                catch(Exception ex)
                {
                    result.msg = "数据添加异常！";
                    myLog.InfoFormat("图片上传异常 "+ex.Message);
                }           
            }
            else
            {
                result.status = false;
                result.msg = "上传失败,没有解析图片！";
            }
            return result;
        }
        #endregion

        #region  将图片转成字符串
        /// <summary>
        /// 将图片转成字符串
        /// </summary>
        /// <returns></returns>
        public JsonResultObject ImageBase64()
        {

            var result = new JsonResultObject();
            HttpPostedFile imageFile = HttpContext.Current.Request.Files[0];
            var fs = imageFile.InputStream;

            if (fs != null)
            {
                result.data = ImageConvertUtil.ConvertImageToBase64(fs);
                result.status = true;
                result.msg = "已完成数据转换！";
            }
            else
            {
                result.status = false;
            }
            return result;
        }
        #endregion

        #region 存放图片，并返回数据状态
        private JsonResultObject ReturnUpLoadStatus(byte[] ibyte, UpLoadImageInfo upLoadImage)
        {
            var result = new JsonResultObject();
            //result.data的返回路径集合
            List<object> allOutPath = new List<object>();

            //允许上传图片集合后缀
            string rules = string.Empty;
            ImageConfigInfo imageConfigInfo = GetImageConfigInfo(ref rules, upLoadImage.Type);
            if (IsValidPic(ibyte) == false) 
            {
                result.msg = "您上传的不是图片类型";
                result.status = false;
                return result;
            }
            else if (rules.IndexOf(upLoadImage.ExtName) ==-1 || rules.Contains(upLoadImage.ExtName)==false)
            {
                result.msg = "您上传的不是图片类型" ;
                result.status = false;
                return result;
            }
            //二进制图片流会比原有的图片大130%-150%
            double maxSize = imageConfigInfo.Size * 1024 *1.5;
            if (upLoadImage.ImageByte.Length > maxSize)
            {
                result.msg = "图片已超过实际大小："+imageConfigInfo.Size +"KB";
                result.status = false;
                return result;
            }

            string filePath = string.Empty;//图片路径
            string savePath = string.Empty;//图片保存文件夹位置
            //创建默认原图
            imageConfigInfo.SaveConfig.Add(new SaveConfigInfo { Key = "Origin", Size=-1,Suffix="_O"});
            string defaultImagePath = string.Empty;
            for (int i = imageConfigInfo.SaveConfig.Count-1; i >= 0; i--)
            {
                //图片名称
                string imageFileName = FileNameStrategy(upLoadImage, imageConfigInfo.SaveConfig[i].Suffix, ibyte, ref filePath);
                //创建图片图片目录,判定是否有子文件夹
                if (upLoadImage.FilePath == null && upLoadImage.FileName == null)
                {
                    savePath = imageConfigInfo.SavePath + imageConfigInfo.FileName + "/" + filePath;
                }
                else
                {
                    savePath = imageConfigInfo.SavePath + imageConfigInfo.FileName + "/" + upLoadImage.FilePath;
                }

                if (imageConfigInfo.SaveConfig[i].Suffix=="_L")
                {
                    if (upLoadImage.FilePath == null && upLoadImage.FileName == null)
                    {
                        allOutPath.Add(new { ImagePath = filePath + "/" + imageFileName, Url = imageConfigInfo.URL });
                    }
                    else
                    {
                        allOutPath.Add(new { ImagePath = upLoadImage.FilePath + "/" + imageFileName, Url = imageConfigInfo.URL });
                    }

                }

                Directory.CreateDirectory(savePath);
                bool isOrigin;
                if (imageConfigInfo.SaveConfig[i].Size == -1 || imageConfigInfo.SaveConfig[i].Key == "Origin")
                {
                    isOrigin = true;
                    defaultImagePath = savePath +"/"+ imageFileName;
                    SaveImage(savePath
                            , imageFileName
                            , ibyte
                            , isOrigin
                            , imageConfigInfo.SaveConfig[i].Size
                            , imageConfigInfo.SaveConfig[i].Size
                            , upLoadImage.ExtName);
                }
                else
                {
                    isOrigin = false;
                    string otherImagePath = savePath + "/" + imageFileName;
                    SaveImage(otherImagePath, ibyte,upLoadImage.ExtName, imageConfigInfo.SaveConfig[i].Size, false);
                }
            }
            
            result.data = allOutPath;
            result.status = true;
            result.msg = "图片已上传完成！";
            return result;
        }
        #endregion 

        #region 保存图片
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="savePath">文件保存路径</param>
        /// <param name="imageFileName">图片名</param>
        /// <param name="ibyte">文件流</param>
        /// <param name="isOrigin">是否是原图</param>
        /// <param name="dstWith">图片宽</param>
        /// <param name="dstHeight">图片高</param>
        /// <param name="extName">后缀</param>
        private void SaveImage(string savePath, string imageFileName, byte[] ibyte, bool isOrigin
                              , int dstWith, int dstHeight,string extName)
        {
            try
            {
                string fullPath = string.Format(@"{0}\{1}", savePath, imageFileName);
                if (isOrigin)
                {
                    using (FileStream deflultStream = new System.IO.FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                    {
                        deflultStream.Write(ibyte, 0, ibyte.Length);
                        deflultStream.Close();
                    }
                }
            }
            catch (Exception ex) 
            {
                myLog.InfoFormat("原图添加时"+ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="desImgName"></param>
        /// <param name="imgSize">图片大小</param>
        /// <param name="isOverride">是否删除原图</param>
        private void SaveImage(string desImgName, byte[] ibyte,string extName,int imgSize, bool isOverride) 
        {
            try
            {
                JXAPI.Common.Utils.ImageHelper.CreateResizeImage(desImgName, ibyte, extName, imgSize, false);
            }
            catch (Exception ex)
            {
                myLog.InfoFormat("其他图片添加时" + ex.Message);
            }
        }
        #endregion

        #region 生成图片名
        /// <summary>
        ///  根据UpLoadImageInfo.Type返回文件名
        /// </summary>
        /// <param name="upLoadImage">UpLoadImageInfo</param>
        /// <param name="extName">后缀名</param>
        /// <param name="ibyte">没有名字的情况下</param>
        /// <param name="filePath">ref 文件路径</param>
        /// <returns>图片名</returns>
        private string FileNameStrategy(UpLoadImageInfo upLoadImageInfo, string suffix, byte[] ibyte, ref  string filePath) 
        {
            string imageFileName = string.Empty;
            string index = string.Empty;
            if (upLoadImageInfo.Index != 0)
            {
                index = upLoadImageInfo.Index.ToString();
            }
            if (string.IsNullOrEmpty(upLoadImageInfo.FileName) == false && upLoadImageInfo.FileName !="")
            {
                //已经有文件名
                if (upLoadImageInfo.FileName.IndexOf(".") != -1)
                {
                    //default.jpg完整名
                    string[] splitFileName = upLoadImageInfo.FileName.Split('.');
                    imageFileName = string.Format("{0}{1}{2}.{3}", splitFileName[0], suffix,index, splitFileName[1]);
                }
                else
                {
                    //例：default无完整名
                    imageFileName = string.Format("{0}{1}{2}.{3}", upLoadImageInfo.FileName, suffix, index,upLoadImageInfo.ExtName);
                }
            }
            else
            {
                //没有文件名时
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileKey = md5.ComputeHash(ibyte);
                string md5Ibyte = Convert.ToBase64String(fileKey);
                md5Ibyte = md5Ibyte.Replace('/', 'f');
                md5Ibyte = md5Ibyte.Replace('@', 'w');
                md5Ibyte = md5Ibyte.Replace('+', 'y');
                md5Ibyte = md5Ibyte.Replace(' ', 'j');
                md5Ibyte  =md5Ibyte.Replace('.', 'x');
                md5Ibyte =md5Ibyte.Replace('\\','d');
                md5Ibyte = md5Ibyte.Substring(0, 19);
                filePath = md5Ibyte.Substring(0,4)  + "/" +
                           md5Ibyte.Substring(4, 3) + "/" +
                           md5Ibyte.Substring(7, 4);
                imageFileName = md5Ibyte.Substring(11) +suffix+"." + upLoadImageInfo.ExtName;
            }
            return imageFileName;
        }
        #endregion

        #region 获取WebConfig.images下的某个子节点
        /// <summary>
        ///  获取WebConfig.images下的某个子节点
        /// </summary>
        /// <param name="rules">允许上传图片集合</param>
        /// <param name="ChildConfigName">节点名</param>
        /// <returns></returns>
        public ImageConfigInfo GetImageConfigInfo(ref string rules, string configName = "product")
        {
            try
            {
                var imagesSection = ConfigurationManager.GetSection("images") as XmlNode;
                ImageConfigInfo imageConfigInfo = new ImageConfigInfo();
                rules = imagesSection.Attributes["rules"].Value;

                List<SaveConfigInfo> saveConfigInfoList = new List<SaveConfigInfo>();
                XmlNode gradesNode = imagesSection.SelectSingleNode(configName + "image");
                for (int i = 0; i < gradesNode.Attributes.Count; i++)
                {
                    string name = gradesNode.Attributes[i].Name;
                    string value = gradesNode.Attributes[name].Value;
                    switch (name)
                    {
                        case "savepath":
                            imageConfigInfo.SavePath = value; break;
                        case "filename":
                            imageConfigInfo.FileName = value; break;
                        case "url":
                            imageConfigInfo.URL = value; break;
                        case "size":
                            imageConfigInfo.Size = int.Parse(value); break;
                        case "watermark":
                            imageConfigInfo.Watermark = value; break;
                    }
                }
                XmlNodeList childList = gradesNode.ChildNodes;
                for (int i = 0; i < childList.Count; i++)
                {
                    SaveConfigInfo saveConfigInfo = new SaveConfigInfo();
                    saveConfigInfo.Key = childList[i].Attributes["key"].Value;
                    saveConfigInfo.Size = int.Parse(childList[i].Attributes["size"].Value);
                    saveConfigInfo.Suffix = childList[i].Attributes["suffix"].Value;
                    saveConfigInfoList.Add(saveConfigInfo);
                }
                imageConfigInfo.SaveConfig = saveConfigInfoList;
                return imageConfigInfo;
            }
            catch 
            {
                return null;
            }

        }
        #endregion

        #region 验证图片流是否合法
        private bool IsValidPic(byte[] ibyte)
        {
            if (ibyte == null || ibyte.Length < 4)
            {
                return false;
            }
            string fileClass = "";
            int len = ibyte.Length;
            try
            {
                fileClass = ibyte[0].ToString();
                fileClass += ibyte[1].ToString();
                fileClass = fileClass.Trim();
                if (fileClass == "7173" || fileClass == "13780")//7173是gif;,13780是PNG;
                {
                    return true;
                }
                else      // JpgOrJpeg
                {
                    byte[] Jpg = new byte[4];

                    Jpg[0] = 0xff;

                    Jpg[1] = 0xd8;

                    Jpg[2] = 0xff;

                    Jpg[3] = 0xd9;

                    if (ibyte[0] == Jpg[0] && ibyte[1] == Jpg[1]
                        && ibyte[len - 2] == Jpg[2] && ibyte[len - 1] == Jpg[3])
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.InfoFormat("数据判定异常："+ex.Message);
                return false;
            }
        }
        #endregion
    }
}