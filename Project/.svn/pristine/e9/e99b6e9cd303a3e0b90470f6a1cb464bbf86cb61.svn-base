using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.Models.Brand
{
    public class BrandSearchModel : JXUtil.RequestPagedBase
    {
        public string BrandName { get; set; }


        public string OrderBy
        {
            get
            {
                //0 1 不能用
                return " BrandID Desc";
            }
        }
        public string ToWhere
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(BrandName))
                {
                    return string.Format(" ChineseName like '%{0}%'", this.BrandName);
                }
                return string.Empty;
            }
        }
    }
}