using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXPAI.Service.Base
{
    public class JsonResult
    {
        public JsonResult() { }

        public JsonResult(int status, string msg, object data)
        {
            this.code = status;
            this.message = msg;
            this.data = data;
        }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}