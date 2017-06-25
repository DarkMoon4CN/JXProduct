
using JXAPI.Component.Enums;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace JXAPI.Component.Model
{
    /// <summary>
    ///商品信息
    /// </summary>
    [Serializable]
    public class ProductInfo
    {
        public Int32 ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ChineseName { get; set; }
        public string CADN { get; set; }
        public string SellingName { get; set; }
        public string LongName { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
        public string EnglishName { get; set; }
        public string PinyinName { get; set; }
        public int CFID { get; set; }
        public int BrandID { get; set; }
        public short ProductType { get; set; }
        public short DrugType { get; set; }
        public short ParameterType { get; set; }
        public short Type { get; set; }
        public short DrugClassification { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal TradePrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal MobilePrice { get; set; }
        public string Images { get; set; }
        public string Instructions { get; set; }
        public string Promotion { get; set; }

        private string _description;
        public string Description 
        {
            get
            {
                string urlSrc = string.Empty;
                if (!string.IsNullOrEmpty(_description))
                {
                   urlSrc = MatchHelper.GetHtmlImageUrlList(_description);
                }
                return urlSrc;
            }
            set
            {
                _description = value;
            }
        }

        public string Specifications { get; set; }
        public string ValueList { get; set; }
        public int ManufacturerID { get; set; }
        public string Manufacturer { get; set; }
        public string ApprovalNumber { get; set; }
        public string Origin { get; set; }
        public int LargePacking { get; set; }
        public string Unit { get; set; }
        public string Volume { get; set; }
        public string Storage { get; set; }
        public string Dosage { get; set; }
        public int Usage { get; set; }
        public string Abbreviation { get; set; }
        public short ContainMHJ { get; set; }
        public short IsFragile { get; set; }
        public short IsTaiKang { get; set; }
        public short IsDeluxe { get; set; }
        public short IsOdour { get; set; }
        public short IsProtection { get; set; }
        public short IsBasePowder { get; set; }
        public short IsYiBao { get; set; }
        public short IsBasic { get; set; }
        public short IsRecommend { get; set; }
        public short IsTop { get; set; }
        public short IsImageDiff { get; set; }
        public short Actions { get; set; }
        public short Rank { get; set; }
        public int SellCount { get; set; }
        private int _comments;
        public int Comments
        {
            get
            {
                if (_comments < 0)
                {
                    return 0;
                }
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }
        public int Favorite { get; set; }
        public int PV { get; set; }
        public int Sort { get; set; }
        public DateTime ValidPeriod { get; set; }
        public short Status { get; set; }
        public short Selling { get; set; }
        public DateTime SellingTime { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updater { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// 快速编辑数据实体
    /// </summary>
    public class QuickEditModel
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 赠品ID
        /// </summary>
        public int ProductGiftID { get; set; }
        /// <summary>
        /// 赠品数量
        /// </summary>
        public int ProductGiftCount { get; set; }

        /// <summary>
        /// 包邮
        /// </summary>
        public int IsFreeShip { get; set; }

        /// <summary>
        /// 上下架
        /// </summary>
        public int Selling { get; set; }

        /// <summary>
        /// 活动
        /// </summary>
        public int Actions { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Creator { get; set; }
    }

    public class MatchHelper
    {
        /// <summary>   
        /// 取得HTML中所有图片的 URL。   
        /// </summary>   
        /// <param name="sHtmlText">HTML代码</param>   
        /// <returns>图片的URL列表</returns>   
        public static string GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(sHtmlText);

            string sUrlList = string.Empty;
            // 取得匹配项列表  
            for (int i = 0; i < matches.Count; i++)
            {
                if(i == 0)
                {
                    sUrlList = matches[i].Groups["imgUrl"].Value;
                }
                else
                {
                    sUrlList += "|" + matches[i].Groups["imgUrl"].Value;
                }
            }
            return sUrlList;
        }  
    }


    public class ValueListInfo
    {
        private string split = "~#$";
        private ValueListInfo() { }
        public ValueListInfo(string valuelist)
        {
            this.ParameterTypeValue = string.Empty;
            this.JXParameterTypeValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(valuelist))
            {
                var index = valuelist.IndexOf(split);
                if (index > -1)
                {
                    this.ParameterTypeValue = valuelist.Substring(0, index);
                    this.JXParameterTypeValue = valuelist.Substring(index + split.Length);
                }
            }
        }

        /// <summary>
        /// 商品参数
        /// </summary>
        public string ParameterTypeValue { get; set; }

        /// <summary>
        /// 金象分类说明书
        /// </summary>
        public string JXParameterTypeValue { get; set; }

        public override string ToString()
        {
            return string.Concat(this.ParameterTypeValue, split, this.JXParameterTypeValue);
            //return this.ParameterTypeValue + split + this.JXParameterTypeValue;
            //return base.ToString();
        }
    }
}