using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace JXPAI.Service.Controllers
{
    // 合作方需要的接口

    /// <summary>
    /// /Partner/Post
    /// </summary>
    public class PartnerController : ApiController
    {
        public HttpResponseMessage Post()
        {
            string str = string.Empty;
            var context = HttpContext.Current.Request;
            Orders.OrdersService service = new Orders.OrdersService();

            string method = context["method"].ToString();
            switch (method)
            {
                case "jxdyf.orders.keede.list":
                    str = service.List();
                    break;
                case "jxdyf.orders.keede.orderStatus":
                    str = service.Shipping_Status();
                    break;
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(str, Encoding.UTF8, "text/javascript")
            };
        }
    }
}
