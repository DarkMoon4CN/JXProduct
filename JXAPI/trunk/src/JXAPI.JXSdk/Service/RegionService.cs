using JXAPI.JXSdk.Base;
using JXAPI.JXSdk.Request;
using JXAPI.JXSdk.Response;
using JXAPI.JXSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Service
{
    public class RegionService
    {
        private static DefaultJXClient client = new DefaultJXClient("user");
        private static string list = "region/get";
        public static RegionService Instance
        {
            get { return new RegionService(); }
        }

        /// <summary>
        ///  查询省市县某节点
        /// </summary>
        /// <param name="parentID">节点级别</param>
        /// <returns></returns>
        public RegionListResponse List(int parentID=0) 
        {
            RegionListRequest request = new RegionListRequest() { parentID = parentID };
            string postData = JsonHelper.GetJson(request);
            return client.Execute(request, list, postData);
        }

    }
}
