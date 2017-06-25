using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXAPI.ConsoleProduct
{
    public class ColNameManager
    {
        public static void GetColNameByTbName(string tableName,out string[] colName,out string[] sqlServerColName)
        {
            colName = null;
            sqlServerColName = null;
            switch (tableName)
            {
                case "Product":
                    colName = new string[]{"ProductID","ProductCode","CFID","BrandID","ChineseName","PinyinName","CADN","LongName","ProductType","MarketPrice","MobilePrice","TradePrice","CostPrice"
                                        ,"Stock","Selling","SellingTime","Manufacter","Promotion","Instructions","Images","Description","Specifications","ValueList","LargePacking","Unit","Volume","Type","Abbreviation"
                                        ,"Rank","IsBasic","IsRecommend","IsTop","ValidPeriod","IsTaiKang","Sort","Actions"
                                        ,"Status","Title","Keywords","MetaKeywords","MetaDescription","Creator","CreateTime","Updater","UpdateTime"};
                    sqlServerColName = new string[]{ "ProductID","ProductCode","CFID","BrandID","ChineseName","PinyinName","CADN","LongName","ProductType","MarketPrice","MobilePrice","TradePrice","CostPrice"
                                            ,"Stock","Selling","SellingTime","Manufacturer","Promotion","Instructions","Images","Description","Specifications","ValueList","LargePacking","Unit","Volume","Type","Abbreviation"
                                            ,"Rank","IsBasic","IsRecommend","IsTop","ValidPeriod","IsTaiKang","Sort","Actions"
                                            ,"Status","Title","Keywords","MetaKeywords","MetaDescription","Creator","CreateTime","Updater","UpdateTime"};
                    break;
                case "ProductActivity":
                    colName = new string[]{"ProductID","ActID","ActName","ActStock","ActQuantity","ActPrice","ActDesc","Type"
                                   ,"TypeLimit","CouponBatchNo","CouponName","Discount","ProductGiftID","IsFreeShip"
                                   ,"ExtType","Status","StartDate","EndDate","Creator","CreateTime","Updater","UpdateTime"
                                   ,"ProductCode","MarketPrice","TradePrice","MobilePrice","CostPrice"};
                     sqlServerColName = new string[]{"ProductID","ActID","ActName","ActStock","ActQuantity","ActPrice","ActDesc","Type"
                                   ,"TypeLimit","CouponBatchNo","CouponName","Discount","ProductGiftID","IsFreeShip"
                                   ,"ExtType","Status","StartDate","EndDate","Creator","CreateTime","Updater","UpdateTime"
                                   ,"ProductCode","MarketPrice","TradePrice","MobilePrice","CostPrice"};
                    break;
                case "ProductClassification":
                     colName = new string[]{ "ID", "ProductID", "CFID", "CFPath" };
                     sqlServerColName = new string[]{ "ID", "ProductID", "CFID", "CFPath" };
                    break;
                case "ProductCombo":
                     colName = new string[]{ "ID", "ProductID", "ComboProductID", "Name", "Price", "Quantity", "Creator", "CreateTime" };
                     sqlServerColName = new string[]{ "ID", "ProductID", "ComboProductID", "Name", "Price", "Quantity", "Creator", "CreateTime" };
                    break;
                case "ProductGift":
                     colName = new string[]{ "ID", "ProductID", "ProductGiftID", "Quantity", " Status", "Creator", "CreateTime", "Updater", "UpdateTime" };
                     sqlServerColName = new string[]{ "ID", "ProductID", "ProductGiftID", "Quantity", " Status", "Creator", "CreateTime", "Updater", "UpdateTime" };
                    break;
                case "ProductParameterPrice":
                     colName = new string[]{ "PriceParaID", "MainProductID", "ChildProductID", " Prop1", "Prop2", "Prop3" };
                     sqlServerColName = new string[]{ "ParaPriceID", "MainProductID", "ChildProductID", " Prop1", "Prop2", "Prop3" };
                    break;
                case "ProductRelated":
                     colName = new string[]{ "ID", "ProductID", "ChildProductID", "Name", "Type", "Creator", "CreateTime", "Updater", "UpdateTime" };
                     sqlServerColName = new string[]{ "RelatedID", "ProductID", "ChildProductID", "Name", "Type", "Creator", "CreateTime", "Updater", "UpdateTime" };
                    break;
                case "ProductStock":
                    colName = new string[]{ "StockID", "ProductCode", "HouseID", " TypeID", "Stock", "OutStock", "FreezedStock", "UsableStock", "ChangeTime" };
                    sqlServerColName = new string[]{ "StockID", "ProductCode", "HouseID", " TypeID", "Stock", "OutStock", "FreezedStock", "UsableStock", "ChangeTime" };
                    break;
                case "JXOrderProduct":
                    colName = new string[]{ "ProductID","ProductCode","ProductType","ChineseName","CFID","BrandID","MarketPrice","TradePrice","CostPrice","Images","IsFragile"
                                  ,"Dosage","Selling","BuyLimit","Status","Unit","Specifications","Volume","Abbreviation ","Manufacturer","LargePacking","BrandName" };
                    sqlServerColName = new string[]{ "ProductID","ProductCode","ProductType","ChineseName","CFID","BrandID","MarketPrice","TradePrice","CostPrice","Images","IsFragile"
                                  ,"Dosage","Selling","BuyLimit","Status","Unit","Specifications","Volume","Abbreviation ","Manufacturer","LargePacking","BrandName" };
                    break;
                case "JXOrderProductrelated":
                     colName = new string[]{ "ID", "ProductID", "ChildProductID", "Name", "Type", "Creator", "CreateTime", "Updater", "UpdateTime" };
                     sqlServerColName = new string[]{ "RelatedID", "ProductID", "ChildProductID", "Name", "Type", "Creator", "CreateTime", "Updater", "UpdateTime" };
                    break;
                case "Manufacter":
                    colName = new string[]{ "ManuID", "Manufacturer", "Address", "Postalcode", "Phone", "ConsultPhone", "ServicePhone", "Office", "Fax", "RegAddress", "Site", "Creator", "CreateTime", "LastUpdate", "LastUpdateTime" };
                    sqlServerColName = new string[]{ "ManuID", "Manufacturer", "Address", "Postalcode", "Phone", "ConsultPhone", "ServicePhone", "Office", "Fax", "RegAddress", "Site", "Creator", "CreateTime", "LastUpdate", "LastUpdateTime" };
                    break;
                case "ProducteValuation":
                    colName = new string[]{ "EvaluationID","ProductID","ProductCode","UID","ParentID","UserName","EvaTime","Title","Content","Score","Source","ScoreDes","ScoreService"
                                   ,"ScoreSend","ScoreLogistics","FlowerCount","EggCount","OrderID","Status","UpdateTime" };
                    sqlServerColName = new string[]{ "EvaluationID","ProductID","ProductCode","UID","ParentID","UserName","EvalTime","Title","Content","Score","Source","ScoreDes","ScoreService"
                                   ,"ScoreSend","ScoreLogistics","FlowerCount","EggCount","OrderID","Status","UpdateTime" };
                    break;
                case "Classification":
                     colName = new string[]{ "CFID","ChineseName","PinyinName","ImagesLogo","Level","Path","ParentID"
                                 ,"PathCount","Tittle","Keywords","Description","Sort","Status","RootCFID","IsTop","IsChannelHot","keywordID","ProductCount" };
                     sqlServerColName = new string[]{ "CFID","ChineseName","PinyinName","ImagesLogo","Level","Path","ParentID"
                                 ,"PathCount","Title","Keywords","Description","Sort","Status","RootCFID","IsTop","IsChannelHot","keywordID","ProductCount" };
                    break;
                case "Keyword":
                    colName = new string[]{ "KeywordID","ChineseName","PinyinName"," FirstLetter","ProductCount","RelationCount","Status","TypeID"
                                   ,"Creator","CreateTime","Updater","UpdateTime" };
                    sqlServerColName = new string[]{ "KeywordID","ChineseName","PinyinName"," FirstLetter","ProductCount","RelationCount","Status","TypeID"
                                   ,"Creator","CreateTime","Updater","UpdateTime" };
                    break;
                case "KeywordProduct":
                     colName = new string[]{ "RelationID", "KeywordID", "ProductID", "Sort" };
                     sqlServerColName = new string[]{ "RelationID", "KeywordID", "ProductID", "Sort" };
                    break;
                case "KeywordRelation":
                     colName = new string[]{ "ID", "KeywordID", "ChildKeywordID", "Sort" };
                     sqlServerColName = new string[]{ "ID", "KeywordID", "ChildKeywordID", "Sort" };
                    break;
                case "Activity":
                     colName = new string[]{ "ActID","BriefName","Name","Description","URL","Type","Restrictive","CountLimit","UserLimit"
                                   ,"StartTime","EndTime","CreateTime","Creator","Updater","UpdateTime","Status","UserType" };
                     sqlServerColName = new string[]{ "ActID","BriefName","Name","Description","URL","Type","Limit","CountLimit","UserLimit"
                                   ,"StartTime","EndTime","CreateTime","Creator","Updater","UpdateTime","Status","UserType" };
                    break;
                case "ActivityRule":
                     colName = new string[]{ "RuleID","ActID","Amount"," Quantity","DiscountAmount","Discount","ProductID","CouponBatchNo","CreateTime"
                                   ,"Creator","Updater","UpdateTime","Status" };
                     sqlServerColName = new string[]{ "RuleID","ActID","Amount"," Quantity","DiscountAmount","Discount","ProductID","CouponBatchNo","CreateTime"
                                   ,"Creator","Updater","UpdateTime","Status" };
                    break;
                case "Sale"://JXMarketingData
                     colName = new string[]{ "SaleID", "DataID", "Selling", "Sort", "CodeID", "KeyValue", "Creator", "CreateTime", "Updater", "UpdateTime" };
                     sqlServerColName = new string[]{ "SaleID", "DataID", "Selling", "Sort", "CodeID", "KeyValue", "Creator", "CreateTime", "Updater", "UpdateTime" };
                    break;
                case "ProductAll"://JXMarketingData
                     colName = new string[]{ "AllID", "Favor", "Promotion", "Salled", "Creator", "Sales", "CreateTime", "Updater", "UpdateTime" };
                     sqlServerColName = new string[]{ "AllID", "Like", "Promotion", "Salled", "Creator", "Sales", "CreateTime", "Updater", "UpdateTime" };
                    break;
                case "Promotion"://JXMarketingData
                     colName = new string[]{ "PromotionID","ChineseName","Picture"," Links","Sort","CodeID","KeyValue"
                                   ,"Creator","CreateTime","Updater","UpdateTime" };
                     sqlServerColName = new string[]{ "PromotionID","ChineseName","Picture"," Links","Sort","CodeID","KeyValue"
                                   ,"Creator","CreateTime","Updater","UpdateTime" };
                    break;
                case "ExpressNews"://JXMarketingData
                   colName = new string[]{ "NewsID","Author","Type"," Source","Link","Title","Keywords","Abstract","Content"
                                   ,"Creator","Updater","UpdateTime","IsStick" };
                   sqlServerColName = new string[]{ "NewsID","Author","Type"," Source","Link","Title","Keywords","Abstract","Content"
                                   ,"Creator","Updater","UpdateTime","IsStick" };
                    break;
                case "SearchKeyword":
                     colName = new string[]{ "KeywordID","Keywords","FirstLetter","Count","Rank","OrderCount","Related","Salled","Recommend"
                                   ,"Preferential","HotSalled","WeekSalled","Creator","CreateTime","Updater","UpdateTime" };
                     sqlServerColName = new string[]{ "KeywordID","Keywords","FirstLetter","Count","Rank","OrderCount","Related","Salled","Recommend"
                                   ,"Preferential","HotSalled","WeekSalled","Creator","CreateTime","Updater","UpdateTime" };
                    break;
                case "JXYXProduct":
                     colName = new string[]{ "ProductID","AlsoSee","AlsoPurchased","BuyAlsoSee","AlsoFavorite","RelateBrand","RelateClass","PriceEval","BrandEval","CategoryEval"
                                   ,"CreateTime","Updater","UpdateTime" };
                     sqlServerColName = new string[]{ "ProductID","AlsoSee","AlsoPurchased","Purchased","AlsoFavorite","RelateBrand","RelateClass","PriceEval","BrandEval"
                                            ,"CategoryEval","CreateTime","Updater","UpdateTime" };
                    break;
                default:
                    break;
            }
        }
    }
}
