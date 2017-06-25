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
    public class YaoShiKaController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string str = string.Empty;
            var context = HttpContext.Current.Request;

            //  ?method=jxdyf.yaoshika.operate&order=2000001&createTime=20150820101223&no=4444004400101117&pwd=888888&paySum=0.01&sbPro=&sign=&uid=600404&product=e7jQw7AsMTMuMCwzfXvNt8zbLDExLjUwLDEwfQ==
            string method = context["method"].ToString();
            switch (method)
            {
                case "jxdyf.yaoshika.operate":
                    YaoShiKa.YaoShiKaService service = new YaoShiKa.YaoShiKaService();
                    str = service.ResultGet();
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
                case "jxdyf.yaoshika.operate":
                    response = new YaoShiKa.YaoShiKaService();
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
