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
    /// app版本
    /// </summary>
    public class AppVersionService
    {
        private static DefaultJXClient client = new DefaultJXClient("health");
        private static string list = "appVersion/getlist";
        private static string add = "appVersion/addAppVersion";

        public static AppVersionService Instance
        {
            get { return new AppVersionService(); }
        }

        /// <summary>
        /// app版本列表
        /// </summary>
        /// <param name="request">条件实体</param>
        /// <returns></returns>
        public AppVersionListResponse List(AppVersionListRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            return client.Execute(request, list, postData);
        }

        /// <summary>
        /// app版本实体
        /// </summary>
        /// <param name="info"> 实体信息</param>
        /// <returns></returns>
        public bool Add(AppVersionInfo info)
        {
            string postData = JsonHelper.GetJson(info);
            CommonRequest request = new CommonRequest();
            CommonResponse response = client.Execute(request, add, postData);
            if (response != null)
                return response.success;
            return false;
        }
    }
}
