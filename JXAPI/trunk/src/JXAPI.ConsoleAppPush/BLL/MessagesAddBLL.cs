﻿
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
    public class MessagesAddBLL
    {
        private static DefaultJXClient client = new DefaultJXClient("messages");
        private static string add = "messages/addMessages"; //添加通知信息

        public MessagesAddResposne MessagesAdd(MessagesAddRequest request) 
        {
            string postData = JsonHelper.GetJson(request);
            return client.Execute(request, add, postData);
        }
    }
}
