using JXAPI.Component.BLL;
using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace JXAPI.ConsoleProductPlan
{
    public class MySqlProductPlanManager
    {
        /// <summary>
        /// 商品表数据更新
        /// </summary>
        public static void ProductUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 单品活动信息表数据更新
        /// </summary>
        public static void ProductActivityUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductActivityMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 产品分类表数据更新
        /// </summary>
        public static void ProductClassificationUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductClassificationMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 套餐商品表数据更新
        /// </summary>
        public static void ProductComboUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductComboMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 商品赠品表数据更新
        /// </summary>
        public static void ProductGiftUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductGiftMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 商品属性报价表数据更新
        /// </summary>
        public static void ProductParameterPriceUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductParameterPriceMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 商品关联信息表数据更新
        /// </summary>
        public static void ProductRelatedUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductRelatedMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 商品库存表数据更新
        /// </summary>
        public static void ProductStockUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductStockMySqlBLL.Instance, tbName);
        }


        public static void JXOrderProductUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, JxorderProductMySqlBLL.Instance, tbName);
        }

        public static void JXOrderProductrelatedUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, JxorderProductRelatedMySqlBLL.Instance, tbName);
        }

        public static void ManufacterUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ManufacturerMySqlBLL.Instance, tbName);
        }

        public static void ProducteValuationUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, productevaluationMySqlBLL.Instance, tbName);
        }

        public static void ProductQuestionUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductQuestionMySqlBLL.Instance, tbName);
        }

        public static void ClassificationParameterToPriceUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ClassificationParameterToPriceMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 品牌表数据更新
        /// </summary>
        public static void BrandUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, BrandMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 分类表数据更新
        /// </summary>
        public static void ClassificationUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ClassificationMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 关键词表数据更新
        /// </summary>
        public static void KeywordUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, KeywordMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 关键词关联商品表数据更新
        /// </summary>
        public static void KeywordProductUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, KeywordProductMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 关键词关联表表数据更新
        /// </summary>
        public static void KeywordRelationUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, KeywordRelationMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 活动表数据更新
        /// </summary>
        public static void ActivityUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ActivityMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 活动规则表数据更新
        /// </summary>
        public static void ActivityRuleUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ActivityRuleMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 促销商品表数据更新
        /// </summary>
        public static void SaleUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, SaleMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 全站推荐表数据更新
        /// </summary>
        public static void ProductAllUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ProductAllMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 展位资源表数据更新
        /// </summary>
        public static void PromotionUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, PromotionMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 新闻快讯、公告表数据更新
        /// </summary>
        public static void ExpressNewsUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, ExpressNewsMySqlBLL.Instance, tbName);
        }

        public static void SearchKeywordUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, SearchKeywordMySqlBLL.Instance, tbName);
        }

        public static void JXYXProductUpdate(string tbName)
        {
            MySqlProductPlanUpdate(ProductBLL.Instance, JXYXProductMySqlBLL.Instance, tbName);
        }

        /// <summary>
        /// 更新mysql 商品相关表的公共方法
        /// </summary>
        /// <typeparam name="T">表的实体</typeparam>
        /// <param name="iBLL">sql server数据库相关接口</param>
        /// <param name="iMysqlBLL">my server数据库相关接口</param>
        /// <param name="msg">不同表提示信息</param>
        private static void MySqlProductPlanUpdate(IProductPlanBLL iBLL, IProductPlanMySqlBLL iMysqlBLL, string tbName)
        {
            try
            {
                // Mysql 商品相关表的最大ID
                int maxID = iMysqlBLL.GetMaxID();
                UpdateParam param = new UpdateParam();
                param.maxID = maxID;
                param.iBLL = iBLL;
                param.iMysqlBLL = iMysqlBLL;
                param.tbName = tbName;

                Thread childUpdateThred = new Thread(new ParameterizedThreadStart(UpdateProduct)) { IsBackground = true };
                childUpdateThred.Name = "MySqlUpdateProductPlan";
                childUpdateThred.Start(param);
                Thread childAddThred = new Thread(new ParameterizedThreadStart(AddProduct)) { IsBackground = true };
                childAddThred.Name = "MySqlAddProductPlan";
                childAddThred.Start(param);
                //1.批量更新 
                //UpdateProduct(maxID,iBLL,iMysqlBLL,tbName);
                //2.批量添加
                //AddProduct(param);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("{0} 更新失败：{1}", tbName, ex.Message));
            }

            //  通知主线程MySql数据库更新结束
            //OnNotifyMainThreadCompleted(++completeCount);
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="maxProductID"></param>
        private static void UpdateProduct(object obj)
        {
            Thread.Sleep(1000 * 5);
            var param = obj as UpdateParam;
            if(param == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("数据更新失败"));
                return;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("========== {0} UPDATE BEGIN ==========", param.tbName));
            myLog.InfoFormat("========== {0} UPDATE BEGIN ==========", param.tbName);
            int errorCount = 0;
            DataTable dTable = null;
            int errorSum = 0;
            int sucessSum = 0;
            int productInitID = 0;
            while (true)
            {
                //  查询新数据
                dTable = param.iBLL.GetUpdateList(param.maxID, productInitID, Program.ProductUpdateTime, Program.ProductUpdateMaxCount, param.tbName);
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    break;
                }
                //  保存
                if (!param.iMysqlBLL.Update(dTable, out errorCount))
                {
                    errorSum += errorCount;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("{0} 表总共需更新 {1} 条数据,失败：{2}", param.tbName, dTable.Rows.Count, errorCount));
                }
                else
                {
                    sucessSum += dTable.Rows.Count;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Format("{0} 表已更新 {1} 条数据", param.tbName, sucessSum));
                }
                productInitID = Convert.ToInt32(dTable.Rows[dTable.Rows.Count - 1][GetTbIDStrByTbName(param.tbName)]);
            }
            myLog.InfoFormat("========== {0} UPDATE END ==========", param.tbName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("========== {0} UPDATE END ==========", param.tbName));
            //  通知主线程MySql数据库更新结束
            OnNotifyMainThreadCompleted(++completeCount);
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="maxProductID"></param>
        private static void AddProduct(object obj)
        {
            Thread.Sleep(1000 * 5);
            var param = obj as UpdateParam;
            if(param == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("数据更新失败"));
                return;
            }
            myLog.InfoFormat("========== {0} ADD BEGIN ==========", param.tbName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("========== {0} ADD BEGIN ==========", param.tbName));
            int maxID = param.maxID;
            int errorCount = 0;
            DataTable dTable = null;
            int errorSum = 0;
            int sucessSum = 0;
            while (true)
            {
                //  查询新数据
                dTable = param.iBLL.GetAddList(maxID, Program.ProductUpdateTime, Program.ProductUpdateMaxCount, param.tbName);
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    break;
                }
                //  保存
                if (!param.iMysqlBLL.Add(dTable, out errorCount))
                {
                    errorSum += errorCount;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("{0} 表总共需添加 {1} 条数据,失败：{2}", param.tbName, dTable.Rows.Count, errorCount));
                }
                else
                {
                    sucessSum += dTable.Rows.Count;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Format("{0} 表已添加 {1} 条数据", param.tbName, sucessSum));
                }
                maxID = Convert.ToInt32(dTable.Rows[dTable.Rows.Count - 1][GetTbIDStrByTbName(param.tbName)]);
            }
            myLog.InfoFormat("========== {0} ADD END ==========", param.tbName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("========== {0} ADD END ==========", param.tbName));
            //  通知主线程MySql数据库更新结束
            OnNotifyMainThreadCompleted(++completeCount);
        }

        private static void OnNotifyMainThreadCompleted(int completeCount)
        {
            if (completeCount == Program.productPlanTableList.Count * 2)
            {
                //更新商品表中的评论数，好评率字段
                UpdateEvaluationField();
                //更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段
                UpdateHealthActivityField();
                completeCount = 0;
                var hander = CompleteEvent;
                if (hander != null)
                {
                    //
                    hander(mySqlEndStr);
                }
            }
        }

        //更新商品表中的评论数，好评率字段
        private static void UpdateEvaluationField()
        {
            try
            {
                productevaluationMySqlBLL maSqlBll = productevaluationMySqlBLL.Instance;
                if (!maSqlBll.UpdateEvaluationCount())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("商品评论数更新失败"));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Format("商品评论数更新成功"));
                }
                if (!maSqlBll.UpdateEvaluationPraiseRate())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("商品好评率更新失败"));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Format("商品好评率更新成功"));
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(string.Format("商品评论数，好评率更新失败：{0}", ex.Message));
            }
        }

        //更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段
        private static void UpdateHealthActivityField()
        {
            try
            {
                string createTime = System.DateTime.Now.AddMonths(-1).ToString();
                ActivityMySqlBLL mySqlBll = ActivityMySqlBLL.Instance;
                if (mySqlBll.UpdateHealthActivity(createTime))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Format("更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段成功"));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段失败"));
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段失败：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 根据表获取表相应的主键ID名
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        private static string GetTbIDStrByTbName(string tbName)
        {
            string tbIDStr = string.Empty;
            switch (tbName)
            {
                case "Product":
                case "JXYXProduct":
                    tbIDStr = "ProductID";
                    break;
                case "ProductActivity":
                    tbIDStr = "ProductID";
                    break;
                case "ProductCombo":
                    tbIDStr = "ID";
                    break;
                case "ProductGift":
                    tbIDStr = "ID";
                    break;
                case "ProductParameterPrice":
                    tbIDStr = "ParaPriceID";
                    break;
                case "ProductRelated":
                    tbIDStr = "RelatedID";
                    break;
                case "ProductStock":
                    tbIDStr = "StockID";
                    break;
                case "ProductClassification":
                    tbIDStr = "ID";
                    break;
                case "JXOrderProduct":
                    tbIDStr = "ProductID";
                    break;
                case "ClassificationParameterToPrice":
                    tbIDStr = "CFParaPriceID";
                    break;
                case "JXOrderProductrelated":
                    tbIDStr = "RelatedID";
                    break;
                case "Manufacter":
                    tbIDStr = "ManuID";
                    break;
                case "ProducteValuation":
                    tbIDStr = "EvaluationID";
                    break;
                case "ProductQuestion":
                    tbIDStr = "QuestionID";
                    break;
                case "Brand":
                    tbIDStr = "BrandID";
                    break;
                case "Classification":
                    tbIDStr = "CFID";
                    break;
                case "Keyword":
                    tbIDStr = "KeywordID";
                    break;
                case "KeywordProduct":
                    tbIDStr = "RelationID";
                    break;
                case "KeywordRelation":
                    tbIDStr = "ID";
                    break;
                case "Activity":
                    tbIDStr = "ActID";
                    break;
                case "ActivityRule":
                    tbIDStr = "RuleID";
                    break;
                case "Sale":
                    tbIDStr = "SaleID";
                    break;
                case "ProductAll":
                    tbIDStr = "AllID";
                    break;
                case "Promotion":
                    tbIDStr = "PromotionID";
                    break;
                case "ExpressNews":
                    tbIDStr = "NewsID";
                    break;
                case "SearchKeyword":
                    tbIDStr = "KeywordID";
                    break;
                default:
                    break;
            }
            return tbIDStr;
        }

        private static ILog myLog = log4net.LogManager.GetLogger(typeof(MySqlProductPlanManager));
        public static Action<string> CompleteEvent;
        private static int completeCount = 0;
        private const string mySqlEndStr = "MySql数据库更新已完成，请按任意键结束";
    }


    internal class UpdateParam
    {
        public int maxID;
        public IProductPlanBLL iBLL;
        public IProductPlanMySqlBLL iMysqlBLL;
        public string tbName;
    }
}
