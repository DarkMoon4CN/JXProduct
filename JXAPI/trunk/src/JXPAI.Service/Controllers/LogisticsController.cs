using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using JXAPI.Common;
using Venus;

namespace JXPAI.Service.Controllers
{
    public class LogisticsController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string str = string.Empty;
            var context = HttpContext.Current.Request;

            // 
            string method = context["method"].ToString();
            switch (method)
            {
                case "jxdyf.logistics.list.get":
                    Logistics.ListService list = new Logistics.ListService();
                    str = list.ResultGet();
                    break;
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(str, Encoding.UTF8, "text/javascript")
            };
        }

        public HttpResponseMessage Post()
        {
            VenusServiceResponse response = null;
            var context = HttpContext.Current.Request;
            string method = context.Form["method"].ToString();
            switch (method)
            {
                case "jxdyf.logistics.list.get":
                    response = new Logistics.ListService();
                    break;
            }

            IVenusService service = new DefaultJXService();
            if (response != null)
            {
                response.ClientIP = context.UserHostAddress;
                response.NvParams = context.Form;
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(service.Execute(response), Encoding.UTF8, "text/javascript")
                };
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
