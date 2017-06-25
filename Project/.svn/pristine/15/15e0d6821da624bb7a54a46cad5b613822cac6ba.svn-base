using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JXUtil;

namespace JXProduct.AdminUI.Models.Activity
{
    public class SearchProductModel : RequestPagedBase
    {
        public int ActID { get; set; }
        public int CF1 { get; set; }
        public int CF2 { get; set; }
        public int CF3 { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int ProductID { get; set; }
        public string ProductIDs { get; set; }
        public string ProductCode { get; set; }

        public string GetCFPath
        {
            get
            {
                var p = string.Empty;
                if (CF1 > 0)
                {
                    p = CF1.ToString() + "/";
                    if (CF2 > 0)
                    {
                        p += CF2.ToString() + "/";
                        if (CF3 > 0)
                            p += CF3.ToString() + "/";
                    }
                }
                return p;

            }
        }

        public bool ValidateProductIDs
        {
            get
            {
                return ActID > 0 && !string.IsNullOrWhiteSpace(this.ProductIDs);
            }
        }
        public bool ValidateBatUpdate
        {
            get
            {
                return ActID > 0 && (
                    !string.IsNullOrWhiteSpace(this.GetCFPath) || BrandID > 0 || ProductID > 0 || !string.IsNullOrWhiteSpace(ProductCode)
                    );
            }
        }
    }
}