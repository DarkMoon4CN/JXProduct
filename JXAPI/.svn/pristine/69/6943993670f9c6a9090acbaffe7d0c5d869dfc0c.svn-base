using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Specialized;
using Venus;

namespace JXAPI.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultJXService : IVenusService
    {
        private Venus.Logger.IVenusLogger VenusLogger;

        public DefaultJXService()
        {
            this.VenusLogger = new Venus.Logger.ErrorVenusLogeer();
        }

        public string Execute(VenusServiceResponse response)
        {
            return DoExecute(response);
        }

        private string DoExecute(VenusServiceResponse response)
        {
            try
            {
                //提前检查业务参数
                response.Validate();
                return response.ResultGet();
            }
            catch (VenusException e)
            {
                return response.ErrorResponse(e.ErrorCode, e.ErrorMsg);
            }
            catch (System.Exception ex)
            {
                NameValueCollection nv = response.NvParams;
                StringBuilder sb = new StringBuilder();
                foreach (String s in nv.AllKeys)
                {
                    sb.Append(string.Format("{0}={1}",s, nv[s]));
                }

                VenusLogger.Error(string.Format("参数:{0},结果:{1}", sb.ToString(), ex.Message));
                sb.Remove(0,sb.Length);
                nv.Clear();

                return response.ErrorResponse("1000", ex.Message);
            }
        }
    }
}
