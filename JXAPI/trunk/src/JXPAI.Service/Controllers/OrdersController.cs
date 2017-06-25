﻿using System;
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
    public class OrdersController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string str = string.Empty;
            var context = HttpContext.Current.Request;
            Orders.OrdersService service = new Orders.OrdersService();

            // 
            string method = context["method"].ToString();
            switch (method)
            {
                case "jxdyf.orders.detail.get":
                    str = service.ResultGet();
                    break;
                case "jxdyf.orders.pay.set":
                    str = service.Orders_UpdatePayStatus();
                    break;
                case "jxdyf.orders.paymethod.set":
                    str = service.Orders_UpdatePayMethod();
                    break;
                case "jxdyf.orders.union.insert":
                    str = service.UnionOrders_Insert();
                    break;
                case "jxdyf.orders.union.list":
                    str = service.UnionOrders_GetList();
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
                case "jxdyf.orders.detail.get":
                case "jxdyf.orders.pay.set":
                case "jxdyf.orders.paymethod.set":
                case "jxdyf.orders.union.insert":
                case "jxdyf.orders.union.list":
                    response = new Orders.OrdersService();
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
