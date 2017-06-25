using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JXAPI.Common.Utils;
using JXAPI.Component.Model;
using JXSearch.Engine;
using Venus;

namespace JXPAI.Service.Controllers.Search
{
    public class ListService : Venus.VenusServiceResponse
    {
        public string ApiName
        {
            get
            {
                return "jxdyf.search.list.get";
            }
        }

        public override string ResultGet()
        {
            JXSearchProductResult result = new JXSearchProductResult() { resultCode = "Fail" };
            try
            {
                string szJson = HttpContext.Current.Request["para"];
                if (string.IsNullOrEmpty(szJson))
                {
                    result.resultMsg = "未指定查询参数";
                    return ResultResponse(result, "listProductGetByCidResponse");
                }

                //  构造查询FORM
                RequestForm form = JsonHelper.ParseFromJson<RequestForm>(szJson);
                if (form == null || form.pageForm == null || form.queryForm == null)
                {
                    result.resultMsg = "解析查询参数出错";
                    return ResultResponse(result, "listProductGetByCidsssResponse");
                }

                //  搜索数据
                int recCount = 0;
                JXSearchProvider provider = JXSearchProviderCreator.CreateProvider(JXSearchType.Product);
                result = provider.Search(form, out recCount);
                if (result == null)
                    result = new JXSearchProductResult() { resultCode = "Fail", resultMsg = "无结果" };
                else
                    result.total = recCount;
            }
            catch (Exception ex)
            {
                result.resultMsg = ex.Message;
            }
            return ResultResponse(result, "listProductGetByCidResponse");
        }

        public override void Validate()
        {

        }
    }
}
