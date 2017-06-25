using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Base;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Request;
using JXAPI.JXSdk.Response;
using JXAPI.JXSdk.Utils;

namespace JXAPI.JXSdk.Service
{
    /// <summary>
    /// 精彩活动
    /// </summary>
    public class ActivityService
    {
        private static DefaultJXClient client = new DefaultJXClient("health");
        private static string add = "activity/addActivity";
        private static string get = "activity/getAcDetailList";
        private static string list = "activity/getActivityList";
        private static string update = "activity/updateActivity";
        private static string delete = "activity/deleteActivity";

        private static string productList = "activity/getActivityProduct";
        private static string addProduct = "activity/addActivityProduct";
        private static string updateProduct = "activity/updateActivityProduct";
        private static string deleteProduct = "activity/deleteActivityProduct";

        public static ActivityService Instance
        {
            get { return new ActivityService(); }
        }

        /// <summary>
        /// 健康头条列表
        /// </summary>
        /// <param name="request">条件实体</param>
        /// <returns></returns>
        public ActivityListResponse List(ActivityListRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            return client.Execute(request, list, postData);
        }

        /// <summary>
        /// 新增精彩活动
        /// </summary>
        /// <param name="info">实体信息</param>
        /// <returns></returns>
        public int Add(ActivityInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, add, postData);
            if (response != null)
                return response.count;
            return 0;
        }

        /// <summary>
        /// 查询精彩活动
        /// </summary>
        /// <param name="actID">ID</param>
        /// <returns></returns>
        public ActivityListResponse Get(int actID)
        {
            ActivityListRequest request = new ActivityListRequest()
            {
                actID = actID
            };
            string postData = JsonHelper.GetJson(request);
            return client.Execute(request, get, postData);
        }

        /// <summary>
        /// 编辑精彩活动
        /// </summary>
        /// <param name="info">实体信息</param>
        /// <returns></returns>
        public bool Update(ActivityInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, update, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 删除精彩活动
        /// </summary>
        /// <param name="actID">活动ID</param>
        /// <returns></returns>
        public bool Delete(int actID)
        {
            ActivityInfo info = new ActivityInfo() { actID = actID };
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, delete, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 精彩活动商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActivityProductResponse GetProductList(ActivityProductRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            return client.Execute(request, productList, postData);
        }

        /// <summary>
        /// 新增精彩活动商品
        /// </summary>
        /// <param name="actID">活动ID</param>
        /// <param name="productID">商品ID</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public bool AddActivityProduct(int actID, int productID, short sort)
        {
            ActivityProductInfo info = new ActivityProductInfo()
            {
                actID = actID,
                productID = productID,
                sort = sort
            };
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, addProduct, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 编辑精彩活动商品
        /// </summary>
        /// <param name="actID">活动ID</param>
        /// <param name="productID">商品ID</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public bool UpdateActivityProduct(int actID, int productID, short sort)
        {
            ActivityProductInfo info = new ActivityProductInfo()
            {
                actID = actID,
                productID = productID,
                sort = sort
            };
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, updateProduct, postData);
            if (response != null)
                return response.success;
            return false;
        }

        /// <summary>
        /// 删除精彩活动商品
        /// </summary>
        /// <param name="actID">活动ID</param>
        /// <param name="productID">商品ID</param>
        /// <returns></returns>
        public bool DeleteActivityProduct(int actID, int productID)
        {
            ActivityProductInfo info = new ActivityProductInfo()
            {
                actID = actID,
                productID = productID
            };
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, deleteProduct, postData);
            if (response != null)
                return response.success;
            return false;
        }
    }
}
