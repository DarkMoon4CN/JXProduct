﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Venus;

using JXAPI.Common;
using JXAPI.JXSdk.Service;
using JXAPI.JXSdk.Request;
using JXAPI.JXSdk.Utils;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;

namespace JXAPI.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //  lm  For Test UpLoad
            string upLoadImageUrl = "http://localhost:14249/UpLoad/Post";
            string imageByte = "/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAAA8AAD/4QMraHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjMtYzAxMSA2Ni4xNDU2NjEsIDIwMTIvMDIvMDYtMTQ6NTY6MjcgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDUzYgKFdpbmRvd3MpIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjk2RTEwRjAwRkUwOTExRTRCNTQzRkRBNzQ3OEM3ODQ0IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjk2RTEwRjAxRkUwOTExRTRCNTQzRkRBNzQ3OEM3ODQ0Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6OTZFMTBFRkVGRTA5MTFFNEI1NDNGREE3NDc4Qzc4NDQiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6OTZFMTBFRkZGRTA5MTFFNEI1NDNGREE3NDc4Qzc4NDQiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz7/7gAOQWRvYmUAZMAAAAAB/9sAhAAGBAQEBQQGBQUGCQYFBgkLCAYG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";

            //imageByte 是base64, UTF8会修改特殊符号，在转码之前必要的匹配掉+号
            imageByte = imageByte.Replace("+", "%2B");
            string type = "product";
            string filename = "test";
            string method = "jxdyf.upload.uploadimage.post";
            string extName = "jpg";
            string sourceType = "web";

            //////请求
            //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(upLoadImageUrl);
            //httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            //httpWebRequest.Method = "POST";
            ////imageByte: imageStr, type: 'product', method: 'jxdyf.upload.uploadimage.post',fileName:'test',ExtName:'jpg',sourceType:'web'
            //string data = "type=" + type + "&fileName=" + filename + "&method=" + method + "&extName=" + extName + "&sourceType=" + sourceType + "&imageByte=" + imageByte;
            //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
            //httpWebRequest.ContentLength = bytes.Length;
            //Stream requestStream = httpWebRequest.GetRequestStream();
            //requestStream.Write(bytes, 0, bytes.Length);
            //requestStream.Flush();
            //requestStream.Close();

            ////响应
            //HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            //string responseContent = streamReader.ReadToEnd();
            //httpWebResponse.Close();
            //streamReader.Close();
            //httpWebRequest.Abort();
            //httpWebResponse.Close();

            //UpLoadService us = UpLoadService.Instance;
            //UpLoadImageRequest req = new UpLoadImageRequest();
            //req.ImageByte = imageByte;
            //req.SourceType = sourceType;
            //req.Method = method;
            //req.FileName = filename;
            //req.ExtName = extName;
            //us.UpLoadImage(req);
            */
            long testStartTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Now.AddDays(-40));
            long testEndTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Now);

            #region 测试使用的时间
            long startTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-08-05 0:00:00"));
            long endTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-08-07 23:59:59"));
            #endregion


            JXAPI.JXSdk.Response.RegionListResponse response = JXAPI.JXSdk.Service.RegionService.Instance.List();
            if (response.code == 0)
            {
                IList<JXAPI.JXSdk.Domain.RegionInfo> regionInfoList = response.regionTemplates;
            }


            #region 购物卡
            ShoppingCardService sc = ShoppingCardService.Instance;
            ////list
            //ShoppingCardListRequest request = new ShoppingCardListRequest();
            ////request.cardNo = "744122134"; //选填
            ////request.orderID = "666666";
            //request.cardType = "1"; //选填
            //request.startTime = startTime;
            //request.endTime = endTime;
            //request.pageForm = new JXSdk.Domain.PageFormInfo { page = 1, size = 10 };
            //ShoppingCardListResponse result = sc.List(request);

            //add
            //ShoppingCardInfo scInfo = new ShoppingCardInfo();
            //scInfo.productCode = "666666";
            //scInfo.productID = 666666;
            //scInfo.orderID = "666666";
            //scInfo.cardType = "1";
            //scInfo.startTime = startTime;
            //scInfo.endTime = endTime;
            //scInfo.remainingSum = 666666;
            //scInfo.totalAmount = 666666;
            //scInfo.type = 0;
            //scInfo.remarks = "test 测试";
            //bool result = sc.Add(scInfo);

            //get 
            // sc.Get("744122134");

            //update
            //ShoppingCardInfo scInfo2 = new ShoppingCardInfo();
            //scInfo2.productCode = "777777";
            //scInfo2.productID = 777777;
            //scInfo2.orderID = "777777";
            //scInfo2.cardType = "1";
            //scInfo2.startTime = startTime;
            //scInfo2.endTime = endTime;
            //scInfo2.remainingSum = 777777;
            //scInfo2.totalAmount = 777777;
            //scInfo2.type = 0;
            //scInfo2.remarks = "test 测试";
            //scInfo2.cardNo = "744122134";
            //sc.Update(scInfo2);


            //merge 合并购物卡(未测试)
            //MergeShoppingCardRequest request = new MergeShoppingCardRequest();
            //request.mainCode = "";
            //request.mainPass = "";
            //request.viceCode = "";
            //request.vicePass = "";
            //request.creator = "LM";
            //request.remarks = "测试合并";
            //sc.Merge(request);
            
            //JXAPI.JXSdk.Request.UseShoppingCardListRequest request=new UseShoppingCardListRequest();
            //request.uid = 600404;
            //request.isUse = 1;
            //sc.UseList(request);

            #endregion 

            #region 优惠渠道
            CouponChannelService ccs = CouponChannelService.Instance;
            ////list   
            //CouponChannelListRequest request = new CouponChannelListRequest();
            //request.pageForm = new JXSdk.Domain.PageFormInfo { page = 1, size = 10 };
            //CouponChannelListResponse response = ccs.List(request);

            //add
            //CouponChannelInfo ccInfo = new CouponChannelInfo();
            //ccInfo.channelName = "测试";
            //ccInfo.spellName = "ceshi";
            //ccInfo.description = "我是测试";
            //ccInfo.creator = "LM";
            //ccInfo.isOuter = 0;
            //ccs.Add(ccInfo);


            //update
            //CouponChannelInfo ccInfo2 = new CouponChannelInfo();
            //ccInfo2.channelName = "test";
            //ccInfo2.spellName = "test";
            //ccInfo2.description = "test";
            //ccInfo2.creator = "LM";
            //ccInfo2.isOuter = 0;
            //ccInfo2.channelID = 101;
            //ccs.Update(ccInfo2);

            //get
            //ccs.Get(101);

            //delete
            //bool result=ccs.Delete(101);


            //ChannelList
            //CouponChannelListResponse response=ccs.ChannelList();
            ////int responseCount=response.count;//此参数在ChannelList返回里是无效参数,接口不提供count
            #endregion

            #region  优惠券类型
            CouponTypeService cts =  CouponTypeService.Instance;

            ////list
            //CouponTypeListRequest request = new CouponTypeListRequest();
            //request.typeCategory = 0;

            //request.pageForm = new JXSdk.Domain.PageFormInfo { page = 1, size = 10 };
            //CouponTypeListResponse response = cts.List(request);


            //get
            //cts.Get(66);

            //add
            //CouponTypeInfo ctInfo = new CouponTypeInfo();
            //ctInfo.typeCategory = 1;
            //ctInfo.typeName = "现金";
            //ctInfo.paymentMethod = "0";
            //ctInfo.productInfo = "6666,7777";//测试
            //ctInfo.productPriceMin = 0;
            //ctInfo.orderMin = 0;
            //ctInfo.toShipFee = 0;
            //ctInfo.couponChannel = 0;
            //ctInfo.toSpecial = 0;
            //ctInfo.nominalValue = 10;
            //ctInfo.preferential = 0;
            //ctInfo.useType = 0;
            //ctInfo.creator = "LM";
            //ctInfo.status = 2;
            //ctInfo.remarks = "测试添加";
            //cts.Add(ctInfo);

            //update
            //CouponTypeInfo ctInfo2 = new CouponTypeInfo();
            //ctInfo2.typeCategory = 1;
            //ctInfo2.typeName = "现金";
            //ctInfo2.paymentMethod = "0";
            //ctInfo2.productInfo = "555,555";//测试
            //ctInfo2.productPriceMin = 0;
            //ctInfo2.orderMin = 0;
            //ctInfo2.toShipFee = 0;
            //ctInfo2.couponChannel = 0;
            //ctInfo2.toSpecial = 0;
            //ctInfo2.nominalValue = 10;
            //ctInfo2.preferential = 0;
            //ctInfo2.useType = 0;
            //ctInfo2.creator = "LM";
            //ctInfo2.status = 2;
            //ctInfo2.remarks = "测试修改";
            //ctInfo2.typeID = 172;
            //cts.Update(ctInfo2);

            //delete
            //cts.Delete(172);

            //typeList
            //CouponTypeListResponse response=cts.TypeList();//返回里的count值无效
            #endregion

            #region 优惠券批次
            //CouponBatchService cbs =  CouponBatchService.Instance;
            ////list
            //CouponBatchListRequest request = new CouponBatchListRequest();
            //request.StartTime = startTime;
            //request.EndTime = endTime;
            //request.pageForm = new PageFormInfo { page = 1, size = 10 };
            //CouponBatchListResponse response = cbs.List(request);

            
            //add
            //CouponBatchInfo cbInfo = new CouponBatchInfo();
            //cbInfo.channelID = 52;
            //cbInfo.typeID = 107;
            //cbInfo.numCount = 1;
            //cbInfo.startTime = startTime;
            //cbInfo.endTime = endTime;
            //cbInfo.creator = "LM";
            //cbInfo.description = "测试添加";
            //cbs.Add(cbInfo);

            //update
            //CouponBatchInfo cbInfo2 = new CouponBatchInfo();
            //cbInfo2.channelID = 52;
            //cbInfo2.description = "测试更新";
            //cbInfo2.batchID = "3D3C6434";
            //cbInfo2.startTime = startTime;
            //cbInfo2.endTime = endTime;
            //cbInfo2.numCount = 10;
            //cbInfo2.typeID = 107;
            //cbs.Update(cbInfo2);

            //delete
            //cbs.Delete("3E88417C");

            //batchList
            //CouponBatchListResponse response = cbs.BatchList();//返回里的count值无效
            #endregion

            #region 优惠券
            //CouponService cs = CouponService.Instance;
            ////list
            //CouponListRequest request = new CouponListRequest();
            ////request.batchID = "BH45B259AB";
            ////request.couponCode = "";
            ////request.channelID = 52;
            ////request.typeID = 170;
            ////request.isUsed = 0;
            ////request.hasSend = 0;
            ////request.userName="";
            //request.channelID = 66;
            //request.pageForm = new PageFormInfo { page = 1, size = 10 };
            //CouponListResponse response = cs.List(request);

            //add
            //CouponInfo cInfo = new CouponInfo();
            //cInfo.batchID = "BH44C56EBC";
            //cInfo.channelID = 52;
            //cInfo.typeID = 170;
            //cInfo.startTime = startTime;
            //cInfo.endTime = endTime;
            //cInfo.creator = "LM";
            //cInfo.remark = "测试添加";
            //cs.Add(cInfo);
            #endregion

            #region 优惠券-购物卡订单交互
            OrderUseService ous = OrderUseService.Instance;
            //UseCouponRequest ucrequest=new UseCouponRequest();
            //ucrequest.couponNo = "00011CD0";
            //ous.UseCoupon(ucrequest);

            //UseCouponRequest ucrequest = new UseCouponRequest();
            //ucrequest.couponNo = "00011CD0";
            //ous.NotUseCoupon(ucrequest);

            #endregion


            #region  用户列表
            AccountService accountServer = AccountService.Instance;
            //AccountListRequest accountListRequest = new AccountListRequest();
            //accountListRequest.pageForm = new PageFormInfo() {page=1,size=10 };
            //AccountListResponse response = accountServer.List(accountListRequest);


            // accountServer.ResetPwd("941761");


            //AccountBalanceListRequest request = new AccountBalanceListRequest();
            //request.pageForm = new PageFormInfo() { page = 1, size = 10 };
            //request.passportID = 1;
            //AccountBalanceListResponse response = accountServer.BalanceList(request);

            //AccountRecordListRequest request = new AccountRecordListRequest();
            //request.pageForm = new PageFormInfo() { page = 1, size = 10 };
            //accountServer.RecordList(request);

            //AccountUpdateBalanceRequest accountUpdateBalanceRequest = new AccountUpdateBalanceRequest();
            //accountUpdateBalanceRequest.passportID = 1;
            //accountUpdateBalanceRequest.balance = 11;
            //accountUpdateBalanceRequest.remarks = "lm测试更新";
            //accountServer.UpdateBalance(accountUpdateBalanceRequest);
            //accountServer.EnableStutus(new AccountEnableStutusRequest { passportID=1,status=1});
            #endregion

            #region 临时调测
            //ProductEvaluationListRequest request = new ProductEvaluationListRequest();
            //request.ceilTime = endTime;
            //request.floorTime = startTime;
            //request.pageForm = new JXAPI.JXSdk.Domain.PageFormInfo() { page = 1, size = 10 };
            //ProductEvaluationListResponse response= ProductEvaluationService.Instance.List(request);

            //FeedBackListRequest far = new FeedBackListRequest();
            //JXAPI.JXSdk.Domain.PageFormInfo pageInfo = new JXAPI.JXSdk.Domain.PageFormInfo() { page = 1, size = 10 };
            //far.pageForm = pageInfo;
            //FeedBackListResponse feedBackData = FeedBackService.Instance.List(far);

            //FeedBackService.Instance.Get(2);

            //CouponService coupoonService = CouponService.Instance;
            //JXAPI.JXSdk.Request.CouponValidateRequest request=new CouponValidateRequest();
            //coupoonService.WebValidateCard(request);


           
            #endregion      



             
        }

        static string SignVenusRequest(string postData, string secret)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}{1}", postData, secret)));
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}