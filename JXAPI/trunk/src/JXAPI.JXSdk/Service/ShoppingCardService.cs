﻿using JXAPI.JXSdk.Base;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Request;
using JXAPI.JXSdk.Response;
using JXAPI.JXSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Service
{
    public partial class ShoppingCardService
    {
        private static DefaultJXClient client = new DefaultJXClient("coupon");
        private static string list = "shoppingCard/getShoppingCardList";
        private static string get = "shoppingCard/cardDetailByCode";
        private static string add = "shoppingCard/addShoppingCard";
        private static string update = "shoppingCard/updateShoppingCard";
        private static string merge = "shoppingCard/mergeShoppingCard";
        private static string bind = "shoppingCard/bindAppShoppingCard";
        private static string notUse = "shoppingCard/notUseShopingCard";
        private static string use = "shoppingCard/useShopingCard";
        private static string webValidateCard = "shoppingCard/validateCardForWeb";
        private static string appValidateCard = "shoppingCard/validateCardForApp";
        private static string consumeList = "shoppingCardDetail/getList";
        private static string releaseBind = "orderUse/updateShoppingCardStatus";
        public static ShoppingCardService Instance
        {
            get { return new ShoppingCardService(); }
        }

        /// <summary>
        /// 购物卡
        /// </summary>
        /// <param name="request">条件实体</param>
        /// <returns></returns>
        public ShoppingCardListResponse List(ShoppingCardListRequest request) 
        {
            string postData = JsonHelper.GetJson(request);
            ShoppingCardListResponse response= client.Execute(request, list, postData);
            return response;
        }

        /// <summary>
        /// 新增购物卡
        /// </summary>
        /// <param name="info">购物卡信息</param>
        /// <returns></returns>
        public bool Add(ShoppingCardInfo info) 
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, add, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 购物卡详细
        /// </summary>
        /// <param name="code">购物卡号</param>
        /// <returns></returns>
        public ShoppingCardInfo Get(string code)
        {
            ShoppingCardByIdRequest request = new ShoppingCardByIdRequest()
            {
                code  = code
            };
            string postData = JsonHelper.GetJson(request);
            ShoppingCardDetailResponse response = client.Execute(request, get, postData);
            if (response != null && response.dto != null)
            {
                return response.dto;
            }
            return null;
        }

        /// <summary>
        /// 编辑购物卡
        /// </summary>
        /// <param name="info">购物卡信息</param>
        /// <returns></returns>
        public bool Update(ShoppingCardInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, update, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 合并购物卡
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns></returns>
        public CommonResponse Merge(MergeShoppingCardRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CommonResponse response = client.Execute(request, merge, postData);
            return response;
        }

        /// <summary>
        /// 绑定购物卡
        /// </summary>
        /// <param name="request">参数实体</param>
        /// <returns></returns>
        public CommonResponse Bind(ShoppingCardBindRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CommonResponse response = client.Execute(request, bind, postData);
            return response;
           
        }

        /// <summary>
        /// 用户可用购物卡列表
        /// </summary>
        /// <param name="request">参数实体 isUse：1可用的，0：不可用</param>
        /// <returns></returns>
        public UseShoppingCardListResponse UseList(UseShoppingCardListRequest request) 
        {
             string postData = JsonHelper.GetJson(request);
             UseShoppingCardListResponse response=null;
             if (request != null && request.isUse == 1)
             {
                 response = client.Execute(request, notUse, postData);
             }
             else 
             {
                 response = client.Execute(request, use, postData);
             }
            return response;
        }

        /// <summary>
        /// 验证购物卡
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns>error_response  包含验证失败信息</returns>
        public ShoppingCardValidateResponse WebValidateCard(ShoppingCardValidateRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            ShoppingCardValidateResponse response = null;
            response = client.Execute(request, webValidateCard, postData);
            return response;
        }

        /// <summary>
        /// 验证购物卡
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns>error_response  包含验证失败信息</returns>
        public ShoppingCardValidateResponse AppValidateCard(ShoppingCardValidateRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            ShoppingCardValidateResponse response = null;
            response = client.Execute(request, appValidateCard, postData);
            return response;
        }

        /// <summary>
        ///  购物卡消费列表
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ShoppingCardConsumeListResponse ConsumeList(string cardNo) 
        {
            ShoppingCardConsumeListRequest request = new ShoppingCardConsumeListRequest();
            request.cardNo = cardNo;
            string postData = JsonHelper.GetJson(request);
            ShoppingCardConsumeListResponse response = null;
            response = client.Execute(request, consumeList, postData);
            return response;
        }

        /// <summary>
        /// 解除绑定购物券 2015/9/10
        /// </summary>
        /// <param name="request">请求主体</param>
        /// <returns> 包含验证失败信息</returns>
        public CommonResponse ReleaseBind(ReleaseBindRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CommonResponse response = null;
            response = client.Execute(request, releaseBind, postData);
            return response;
        }


    }
}