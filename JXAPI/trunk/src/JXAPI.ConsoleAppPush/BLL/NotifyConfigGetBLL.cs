
using JXAPI.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JXAPI.JXSdk.Base;
using JXAPI.JXSdk.Response;

namespace JXAPI.ConsoleAppPush
{
    public class NotifyConfigGetBLL
    {
        private static DefaultJXClient client = new DefaultJXClient("switch");
        private static string add = "notify/get/admin"; //获取ios 通知开关配置信息

        public NotifyConfigGetResponse NotityConfigGet(NotifyConfigGetRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            return client.Execute(request, add, postData);
        }
    }
}
