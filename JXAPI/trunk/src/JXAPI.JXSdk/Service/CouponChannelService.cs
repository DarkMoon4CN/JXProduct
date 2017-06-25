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
    public class CouponChannelService
    {
        private static DefaultJXClient client = new DefaultJXClient("coupon");
        private static string list = "couponChannel/getList";
        private static string get = "couponChannel/getByID";
        private static string add = "couponChannel/addCouponChannel";
        private static string update = "couponChannel/updateCouponChannel";
        private static string delete = "couponChannel/deleteCouponChannel";
        private static string channelList = "couponChannel/getChannelList";
        public static CouponChannelService Instance
        {
            get { return new CouponChannelService(); }
        }

        /// <summary>
        /// 优惠券渠道
        /// </summary>
        /// <param name="request">条件实体</param>
        /// <returns></returns>
        public CouponChannelListResponse List(CouponChannelListRequest request) 
        {
            string postData = JsonHelper.GetJson(request);
            CouponChannelListResponse response = client.Execute(request, list, postData);
            return response;
        }

        /// <summary>
        /// 优惠券渠道详细
        /// </summary>
        /// <param name="channelID">ID</param>
        /// <returns></returns>
        public CouponChannelInfo Get(int channelID) 
        {
            CouponChannelRequest request = new CouponChannelRequest()
            {
                channelID=channelID
            };
            string postData = JsonHelper.GetJson(request);
            CouponChannelResponse response = client.Execute(request, get, postData);
            if (response !=null && response.template != null)
            {
                return response.template;
            }
            return null;
        }

        /// <summary>
        /// 新增优惠券渠道
        /// </summary>
        /// <param name="info">优惠券渠道信息</param>
        /// <returns></returns>
        public bool Add(CouponChannelInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, add, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 编辑优惠券渠道
        /// </summary>
        /// <param name="info">优惠券渠道信息</param>
        /// <returns></returns>
        public bool Update(CouponChannelInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, update, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 删除优惠券渠道
        /// </summary>
        /// <param name="channelID">ID</param>
        /// <returns></returns>
        public bool Delete(int channelID)
        {
            CouponChannelRequest request = new CouponChannelRequest()
            {
                channelID = channelID
            };
            string postData = JsonHelper.GetJson(request);
            CouponChannelResponse response = client.Execute(request, delete, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 渠道集合（用于下拉框）
        /// </summary>
        /// <returns></returns>
        public CouponChannelListResponse ChannelList()
        {
            CouponChannelListRequest request = new CouponChannelListRequest();
            string postData = JsonHelper.GetJson(request);
            CouponChannelListResponse response = client.Execute(request, channelList, postData);
            if (response.list != null)
            {
                response.count = response.list.Count;
            }
            return response;
        }

    }
}
