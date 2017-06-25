using JXAPI.JXSdk.Base;
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
    public class CouponService
    {
        private static DefaultJXClient client = new DefaultJXClient("coupon");
        private static string list = "coupon/getList";
        private static string add = "coupon/addCoupon";
        private static string bind = "coupon/bindAppCoupon";
        private static string notUse = "coupon/notUseCoupon";
        private static string use = "coupon/useCoupon";
        private static string webValidateCard = "coupon/validateCardForWeb";
        private static string appValidateCard = "coupon/validateCardForApp";
        private static string releaseBind = "orderUse/updateCouponStatus";

        public static CouponService Instance
        {
            get { return new CouponService(); }
        }

        /// <summary>
        /// 优惠券
        /// </summary>
        /// <param name="request">条件实体</param>
        /// <returns></returns>
        public CouponListResponse List(CouponListRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CouponListResponse response = client.Execute(request, list, postData);
            return response;
        }

        /// <summary>
        /// 添加优惠券
        /// </summary>
        /// <param name="info">优惠券信息</param>
        /// <returns></returns>
        public bool Add(CouponInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, add, postData);
            if (response != null)
                return response.success;
            return false;
        }


        /// <summary>
        /// 用户绑定优惠券
        /// </summary>
        /// <param name="request">参数实体</param>
        /// <returns></returns>
        public CommonResponse Bind(CouponBindRequest request) 
        {
            string postData = JsonHelper.GetJson(request);
            CommonResponse response = client.Execute(request, bind, postData);
            return response;
        }

        /// <summary>
        /// 用户不可用优惠券列表
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns></returns>
        public UseCouponListResponse UseList(UseCouponListRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            UseCouponListResponse response = client.Execute(request, use, postData);
            return response;
        }

        /// <summary>
        /// 用户可用优惠券列表
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns></returns>
        public NoUseCouponListResponse NotUseList(NoUseCouponListRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            NoUseCouponListResponse response = client.Execute(request, notUse, postData);
            return response;
        }

        /// <summary>
        /// 验证优惠券
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns>error_response 包含验证失败信息/returns>
        public CouponValidateResponse WebValidateCard(CouponValidateRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CouponValidateResponse response = null;
            response = client.Execute(request, webValidateCard, postData);
            return response;
        }

        /// <summary>
        /// 验证优惠券
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns>error_response 包含验证失败信息/returns>
        public CouponValidateResponse AppValidateCard(CouponValidateRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CouponValidateResponse response = null;
            response = client.Execute(request, appValidateCard, postData);
            return response;
        }

        /// <summary>
        /// 解除绑定优惠券 2015/9/10
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
