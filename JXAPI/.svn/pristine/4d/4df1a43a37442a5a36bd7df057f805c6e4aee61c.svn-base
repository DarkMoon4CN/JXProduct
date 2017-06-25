using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace JXAPI.ConsoleProductPlan
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            MySqlProductPlanManager.CompleteEvent += new Action<string>(MySqlUpdateComplete);
            #region 读配置文件

            ProductUpdateTimeSpan = GetAppSettingsValue("ProductUpdateTimeSpan");
            double timeSpan = 0;
            if (!Double.TryParse(ProductUpdateTimeSpan, out timeSpan))
            {
                Console.WriteLine("配置文件中商品更新时间间隔配置错误，请重新配置");
                Console.WriteLine("请按任意键结束");
                Console.ReadKey(true);
                return;
            }
            if (timeSpan < 0)
            {
                ProductUpdateTime = string.Empty;
            }
            else
            {
                ProductUpdateTime = System.DateTime.Now.AddMinutes(-timeSpan).ToString();
            }

            ProductUpdateMaxCountStr = GetAppSettingsValue("ProductUpdateMaxCount");
            if (!Int32.TryParse(ProductUpdateMaxCountStr, out ProductUpdateMaxCount)
                  || ProductUpdateMaxCount < 100 || ProductUpdateMaxCount > 500)
            {
                Console.WriteLine("批量更新商品的最大数量配置错误，该值在100到 500之间,请重新配置");
                Console.WriteLine("请按任意键结束");
                Console.ReadKey(true);
                return;
            }

            #endregion
            InitTableList();

            if (productPlanTableList != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("MySql数据库更新开始...");
                myLog.InfoFormat("MySql数据库开始更新时间:{0}", System.DateTime.Now);
                for (int i = 0; i < productPlanTableList.Count; i++)
                {
                    Thread updateProductThread = new Thread(new ParameterizedThreadStart(OnStartUpdateMySqlProductPlanTables)) { IsBackground = true };
                    updateProductThread.Name = "MySql-" + productPlanTableList[i];
                    updateProductThread.Start(productPlanTableList[i]);
                }
            }

           Console.ReadKey(true);
        }

        private static void OnStartUpdateMySqlProductPlanTables(object obj)
        {
            Thread.Sleep(1000*5);
            var tableName = obj as string;
            switch (tableName)
            {
                case "Product":
                    MySqlProductPlanManager.ProductUpdate(tableName);
                    break;
                case "ProductActivity":
                    MySqlProductPlanManager.ProductActivityUpdate(tableName);
                    break;
                case "ProductClassification":
                    MySqlProductPlanManager.ProductClassificationUpdate(tableName);
                    break;
                case "ProductCombo":
                    MySqlProductPlanManager.ProductComboUpdate(tableName);
                    break;
                case "ProductGift":
                    MySqlProductPlanManager.ProductGiftUpdate(tableName);
                    break;
                case "ProductParameterPrice":
                    MySqlProductPlanManager.ProductParameterPriceUpdate(tableName);
                    break;
                case "ProductRelated":
                    MySqlProductPlanManager.ProductRelatedUpdate(tableName);
                    break;
                case "ProductStock":
                    MySqlProductPlanManager.ProductStockUpdate(tableName);
                    break;
                case "JXOrderProduct":
                    MySqlProductPlanManager.JXOrderProductUpdate(tableName);
                    break;
                case "JXOrderProductrelated":
                    MySqlProductPlanManager.JXOrderProductrelatedUpdate(tableName);
                    break;
                case "Manufacter":
                    MySqlProductPlanManager.ManufacterUpdate(tableName);
                    break;
                case "ProducteValuation":
                    MySqlProductPlanManager.ProducteValuationUpdate(tableName);
                    break;
                case "ProductQuestion":
                    MySqlProductPlanManager.ProductQuestionUpdate(tableName);
                    break;
                case "ClassificationParameterToPrice":
                    MySqlProductPlanManager.ClassificationParameterToPriceUpdate(tableName);
                    break;
                case "Brand":
                    MySqlProductPlanManager.BrandUpdate(tableName);
                    break;
                case "Classification":
                    MySqlProductPlanManager.ClassificationUpdate(tableName);
                    break;
                case "Keyword":
                    MySqlProductPlanManager.KeywordUpdate(tableName);
                    break;
                case "KeywordProduct":
                    MySqlProductPlanManager.KeywordProductUpdate(tableName);
                    break;
                case "KeywordRelation":
                    MySqlProductPlanManager.KeywordRelationUpdate(tableName);
                    break;
                case "Activity":
                    MySqlProductPlanManager.ActivityUpdate(tableName);
                    break;
                case "ActivityRule":
                    MySqlProductPlanManager.ActivityRuleUpdate(tableName);
                    break;
                case "Sale"://JXMarketingData
                    MySqlProductPlanManager.SaleUpdate(tableName);
                    break;
                case "ProductAll"://JXMarketingData
                    MySqlProductPlanManager.ProductAllUpdate(tableName);
                    break;
                case "Promotion"://JXMarketingData
                    MySqlProductPlanManager.PromotionUpdate(tableName);
                    break;
                case "ExpressNews"://JXMarketingData
                    MySqlProductPlanManager.ExpressNewsUpdate(tableName);
                    break;
                case "SearchKeyword":
                    MySqlProductPlanManager.SearchKeywordUpdate(tableName);
                    break;
                case "JXYXProduct":
                    MySqlProductPlanManager.JXYXProductUpdate(tableName);
                    break;
                default:
                    break;
            }
        }

        private static void MySqlUpdateComplete(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                myLog.InfoFormat("MySql数据库完成更新时间:{0}", System.DateTime.Now);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
                Environment.Exit(0);
            }
        }

        public static string GetAppSettingsValue(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        /// <summary>
        /// 初始加载MySql中产品关联需要更新数据的表
        /// </summary>
        private static void InitTableList()
        {
            if (productPlanTableList != null)
            {
                productPlanTableList.Add("Product");                //  商品表
                productPlanTableList.Add("ProductActivity");        //  单品活动信息表
                productPlanTableList.Add("ProductClassification");  //  产品分类表
                productPlanTableList.Add("ProductCombo");           //  套餐商品表
                productPlanTableList.Add("ProductGift");            //  商品赠品表
                productPlanTableList.Add("ProductParameterPrice");  //  商品属性报价表
                productPlanTableList.Add("ProductRelated");         //  商品关联信息表
                productPlanTableList.Add("ProductStock");           //  商品库存表
                productPlanTableList.Add("Brand");

                productPlanTableList.Add("JXOrderProduct");
                productPlanTableList.Add("JXOrderProductrelated");
                productPlanTableList.Add("Manufacter");
                productPlanTableList.Add("ProducteValuation");

                productPlanTableList.Add("Classification");    //  分类表             
                productPlanTableList.Add("Keyword");           //  关键词表            
                productPlanTableList.Add("KeywordProduct");    //  关键词关联商品表    
                productPlanTableList.Add("KeywordRelation");   //  关键词关联表
                productPlanTableList.Add("Activity");          //  活动表
                productPlanTableList.Add("ActivityRule");      //  活动规则表 
                productPlanTableList.Add("Sale");              //  促销商品表
                productPlanTableList.Add("ProductAll");        //  全站推荐表
                productPlanTableList.Add("Promotion");         //  展位资源表
                productPlanTableList.Add("ExpressNews");       //  新闻快讯、公告

                productPlanTableList.Add("SearchKeyword");
                //productPlanTableList.Add("JXYXProduct");

                #region 暂时不需要更新
                //productPlanTableList.Add("ProductQuestion");
                //productPlanTableList.Add("ClassificationParameterToPrice");
                #endregion

            }
        }

        public static readonly IList<string> productPlanTableList = new List<string>();
        public static string ProductUpdateTimeSpan = string.Empty;
        public static string ProductUpdateTime = string.Empty;
        private static string ProductUpdateMaxCountStr = string.Empty;
        public static int ProductUpdateMaxCount = 0;
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(Program));
    }
}
