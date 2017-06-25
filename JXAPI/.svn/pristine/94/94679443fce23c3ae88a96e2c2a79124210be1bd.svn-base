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
    public class OrdersService
    {
        private static DefaultJXClient client = new DefaultJXClient("order");
        private static string update = "/shopping/orders/cancel/admin";
        public static OrdersService Instance
        {
            get { return new OrdersService(); }
        }

        public bool Update(OrdersAdminRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CommonResponse response = client.Execute(request, update, postData);
            if (response != null && response.code == 0)
                return response.success;
            return false;
        }
    }
}
