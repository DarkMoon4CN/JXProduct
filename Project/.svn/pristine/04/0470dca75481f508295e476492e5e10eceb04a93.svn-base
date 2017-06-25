using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class ProductParameterBLL
    {
        private ProductParameterBLL() { }
        private static ProductParameterBLL _instance;
        public static ProductParameterBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ProductParameterBLL());
            }
        }


        /// <summary>
        /// 属性类别
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> ProductParameter_GetType()
        {
            return new Dictionary<int, string>()
            {
                {0,"药品类"},
                {1,"保健食品（蓝帽子/食健字产品）"},
                {2,"膳食营养补充剂（非食健字产品）"},
                {3,"医疗器械/成人用品/隐形眼镜（食药监械字号产品）"},
                {4,"护理/保健/防护/成人/百货类用品（非食药监械字号产品）"},
                {5,"化妆品"},
                {6,"消毒用品"}
            };
        }

        public Dictionary<int, List<string>> Init()
        {
            var dic = new Dictionary<int, List<string>>();
            dic.Add(0, new List<string> { "药品通用名称", "商品名称", "药品批准文号", "剂型", "规格", "用法用量", "有效期" });
            dic.Add(1, new List<string> { "保健食品名称", "保健食品批准文号", "规格", "保健功能", "功效成分/标志性成分含量", "适宜人群", "不适宜人群", "食用方法及食用量", "注意事项", "保质期及贮藏方法", "生产企业", "生产许可证编号" });
            dic.Add(2, new List<string> { "食品名称", "规格", "成分/配料清单", "食用方法", "注意事项", "不适宜人群", "贮藏方法", "保质期", "执行标准", "产地", "生产企业", "生产许可证编号" });
            dic.Add(3, new List<string> { "医疗器械名称", "医疗器械注册号", "规格/型号", "适用范围", "性能结构及组成", "使用及保养说明", "注意事项", "执行标准", "有效期", "生产企业", "生产许可证编号" });
            dic.Add(4, new List<string> { "产品名称", "规格/型号", "适用范围", "性能结构及组成", "使用及保养说明", "注意事项", "执行标准", "有效期", "生产企业" });
            dic.Add(5, new List<string> { "化妆品名称", "特殊用途/进口化妆品批准文号", "成分", "净含量/规格", "适用范围/注意事项", "使用方法", "保质期", "执行标准", "产地", "生产企业", "生产许可证编号", "卫生许可证编号" });
            dic.Add(6, new List<string> { "产品名称", "主要成分", "产品规格", "应用范围", "使用方法", "注意事项", "执行标准", "卫生标准", "有效期", "生产企业", "卫生许可证编号" });
            return dic;
        }
    }
}