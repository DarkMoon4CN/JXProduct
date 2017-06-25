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
    public class CouponBatchService
    {
        private static DefaultJXClient client = new DefaultJXClient("coupon");
        private static string list = "couponBatch/getList";
        private static string get = "couponBatch/getByID";
        private static string add = "couponBatch/addCouponBatch";
        private static string update = "couponBatch/updateCouponBatch";
        private static string delete = "couponBatch/deleteCouponBatch";
        private static string batchList = "couponBatch/getBatchList";
        public static CouponBatchService Instance
        {
            get { return new CouponBatchService(); }
        }

        /// <summary>
        /// 优惠券批次
        /// </summary>
        /// <param name="request">条件实体</param>
        /// <returns></returns>
        public CouponBatchListResponse List(CouponBatchListRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CouponBatchListResponse response = client.Execute(request, list, postData);
            return response;
        }

        /// <summary>
        ///  优惠券批次详细
        /// </summary>
        /// <param name="batchID">ID</param>
        /// <returns></returns>
        public CouponBatchInfo Get(string batchID)
        {
            CouponBatchIDRequest request = new CouponBatchIDRequest()
            {
                batchID = batchID
            };

            string postData = JsonHelper.GetJson(request);
            CouponBatchResponse response = client.Execute(request, get, postData);
            if (response != null && response.template != null)
            {
                return response.template;
            }
            return null;
        }

        /// <summary>
        /// 新增优惠券批次
        /// </summary>
        /// <param name="info">优惠券批次信息</param>
        /// <returns></returns>
        public bool Add(CouponBatchInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, add, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 编辑优惠券批次
        /// </summary>
        /// <param name="info">优惠券批次信息</param>
        /// <returns></returns>
        public bool Update(CouponBatchInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, update, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 删除优惠券批次
        /// </summary>
        /// <param name="batchID">ID</param>
        /// <returns></returns>
        public bool Delete(string batchID)
        {
            CouponBatchIDRequest request = new CouponBatchIDRequest()
            {
                batchID = batchID
            };
            string postData = JsonHelper.GetJson(request);
            CouponBatchResponse response = client.Execute(request, delete, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 批次集合（用于下拉框）
        /// </summary>
        /// <returns></returns>
        public CouponBatchListResponse BatchList()
        {
            CouponBatchListRequest request = new CouponBatchListRequest();
            string postData = JsonHelper.GetJson(request);
            CouponBatchListResponse response = client.Execute(request, batchList, postData);
            return response;
        }  
    }
}
