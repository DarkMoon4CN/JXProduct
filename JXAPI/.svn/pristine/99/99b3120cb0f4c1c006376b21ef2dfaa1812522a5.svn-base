using JXAPI.Component.DataAccess;
using JXAPI.Component.Model;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using JXAPI.Common.Utils;
using System.Text.RegularExpressions;

namespace JXAPI.Component.SQLServerDAL
{
    public partial class ProductMySqlDAL
    {
        private static Database dbw = JXProductMySqlData.Writer;
        private static Database dbr = JXProductMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(ProductMySqlDAL));

        #region CURD

        /// <summary>
        /// 查询商品库最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxProductID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from product order by ProductID DESC limit 0, 1";
                var cmd = dbr.GetSqlStringCommand(sqlCommand);
                if (dbr.ExecuteScalar(cmd).IsNotNULL())
                {
                    maxId = Convert.ToInt32(dbr.ExecuteScalar(cmd));
                }
                else
                {
                    maxId = 0;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("Product_MaxProductID 获取商品最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateProductEx(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into product ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}'
                                ,'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}'
                                ,'{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}')",
                                     dr["ProductID"].ToInt(), dr["ProductCode"].ToString().Replace("\'", "\""), dr["CFID"].ToInt(), dr["BrandID"].ToInt(), dr["ChineseName"].ToString().Replace("\'", "\""), dr["PinyinName"].ToString().Replace("\'", "\"")
                                     , dr["CADN"].ToString().Replace("\'", "\""), dr["LongName"].ToString().Replace("\'", "\""), dr["ProductType"].ToShort(), dr["MarketPrice"].ToDecimal(), dr["MobilePrice"].ToDecimal(), dr["TradePrice"].ToDecimal()
                                     , dr["CostPrice"].ToDecimal(), (dr["Stock"].ToInt() < 0) ? 0 : dr["Stock"].ToInt(), dr["Selling"].ToShort(), dr["SellingTime"].ToDateTime().ToString(), dr["Manufacturer"].ToString().Replace("\'", "\""), dr["Promotion"].ToString().Replace("\'", "\"")
                                     , dr["Instructions"].ToString().Replace("\'", "\""), dr["Images"].ToString().Replace("\'", "\""), dr["Description"].ToString().Replace("\'", "\""), dr["Specifications"].ToString().Replace("\'", "\""), dr["ValueList"].ToString().Replace("\'", "\""), dr["LargePacking"].ToShort(), dr["Unit"].ToString().Replace("\'", "\"")
                                     , dr["Volume"].ToString().Replace("\'", "\""), dr["Type"].ToShort(), dr["Abbreviation"].ToString().Replace("\'", "\""), dr["Rank"].ToShort(), dr["IsBasic"].ToShort(), dr["IsRecommend"].ToShort(), dr["IsTop"].ToShort(), string.IsNullOrEmpty(dr["ValidPeriod"].ToString()) ? "null" : dr["ValidPeriod"]
                                     , dr["IsTaiKang"].ToShort(), dr["Sort"].ToShort(), dr["Actions"].ToShort(), dr["Status"].ToShort(), dr["Title"].ToString().Replace("\'", "\"")
                                     , dr["Keywords"].ToString().Replace("\'", "\""), dr["MetaKeywords"].ToString().Replace("\'", "\""), dr["MetaDescription"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\"")
                                     , dr["UpdateTime"]);
                    if (i == 0)
                    {
                        strPlaceholder = Placeholder;
                    }
                    else
                    {
                        strPlaceholder += "," + Placeholder;
                    }
                }
                if (!string.IsNullOrEmpty(strPlaceholder))
                {
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = productTable.Rows.Count;
                        flag = false;
                    }
                    else
                    {
                        errorCount = (productTable.Rows.Count - result > 0) ? productTable.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("UpdateProduct 更新商品表失败,商品ID{0}-{1},异常信息:{2}", productTable.Rows[0]["ProductID"], productTable.Rows[productTable.Rows.Count - 1]["ProductID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateProduct(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                var dr = productTable.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update product set ");
                    var Placeholder = string.Format(@"ProductCode = '{0}',CFID = '{1}',BrandID = '{2}',ChineseName = '{3}',PinyinName = '{4}',CADN = '{5}',LongName = '{6}',ProductType = '{7}',MarketPrice = '{8}',MobilePrice = '{9}',TradePrice = '{10}'
                                ,CostPrice = '{11}',Stock = '{12}',Selling = '{13}',SellingTime = '{14}',Manufacter = '{15}',Promotion = '{16}',Instructions = '{17}',Images = '{18}',Description = '{19}'
                                ,Specifications = '{20}',ValueList = '{21}',LargePacking = '{22}',Unit = '{23}',Volume = '{24}',Type = '{25}',Abbreviation = '{26}',Rank = '{27}',IsBasic = '{28}',IsRecommend = '{29}',IsTop = '{30}',ValidPeriod = '{31}'
                                ,IsTaiKang = '{32}',Actions = '{33}',Status = '{34}',Title = '{35}'
                                ,Keywords = '{36}',MetaKeywords = '{37}',MetaDescription = '{38}',Updater = '{39}',UpdateTime = '{40}'",
                                     dr["ProductCode"].ToString().Replace("\'", "\""), dr["CFID"].ToInt(), dr["BrandID"].ToInt(), dr["ChineseName"].ToString().Replace("\'", "\""), dr["PinyinName"].ToString().Replace("\'", "\"")
                                     , dr["CADN"].ToString().Replace("\'", "\""), dr["LongName"].ToString().Replace("\'", "\""), dr["ProductType"].ToShort(), dr["MarketPrice"].ToDecimal(), dr["MobilePrice"].ToDecimal(), dr["TradePrice"].ToDecimal()
                                     , dr["CostPrice"].ToDecimal(), (dr["Stock"].ToInt() < 0) ? 0 : dr["Stock"].ToInt(), dr["Selling"].ToShort(), dr["SellingTime"].ToDateTime().ToString(), dr["Manufacturer"].ToString().Replace("\'", "\""), dr["Promotion"].ToString().Replace("\'", "\"")
                                     , dr["Instructions"].ToString().Replace("\'", "\""), dr["Images"].ToString().Replace("\'", "\""), dr["Description"].ToString().Replace("\'", "\""), dr["Specifications"].ToString().Replace("\'", "\""), dr["ValueList"].ToString().Replace("\'", "\""), dr["LargePacking"].ToShort(), dr["Unit"].ToString().Replace("\'", "\"")
                                     , dr["Volume"].ToString().Replace("\'", "\""), dr["Type"].ToShort(), dr["Abbreviation"].ToString().Replace("\'", "\""), dr["Rank"].ToShort(), dr["IsBasic"].ToShort(), dr["IsRecommend"].ToShort(), dr["IsTop"].ToShort(), string.IsNullOrEmpty(dr["ValidPeriod"].ToString()) ? "null" : dr["ValidPeriod"]
                                     , dr["IsTaiKang"].ToShort(), dr["Actions"].ToShort(), dr["Status"].ToShort(), dr["Title"].ToString().Replace("\'", "\"")
                                     , dr["Keywords"].ToString().Replace("\'", "\""), dr["MetaKeywords"].ToString().Replace("\'", "\""), dr["MetaDescription"].ToString().Replace("\'", "\""), dr["Updater"].ToString().Replace("\'", "\"")
                                     , dr["UpdateTime"]);
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where ProductID = {0}", dr["ProductID"].ToInt());
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\","\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateProduct 更新商品表失败,商品ID{0},受影响行为0", dr["ProductID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateProduct 更新商品表失败,商品ID{0},异常信息:{1}", dr["ProductID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddProduct(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into product ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}'
                                ,'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}'
                                ,'{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}')",
                                     dr["ProductID"].ToInt(), dr["ProductCode"].ToString().Replace("\'", "\""), dr["CFID"].ToInt(), dr["BrandID"].ToInt(), dr["ChineseName"].ToString().Replace("\'", "\""), dr["PinyinName"].ToString().Replace("\'", "\"")
                                     , dr["CADN"].ToString().Replace("\'", "\""), dr["LongName"].ToString().Replace("\'", "\""), dr["ProductType"].ToShort(), dr["MarketPrice"].ToDecimal(), dr["MobilePrice"].ToDecimal(), dr["TradePrice"].ToDecimal()
                                     , dr["CostPrice"].ToDecimal(), (dr["Stock"].ToInt() < 0) ? 0 : dr["Stock"].ToInt(), dr["Selling"].ToShort(), dr["SellingTime"].ToDateTime().ToString(), dr["Manufacturer"].ToString().Replace("\'", "\""), dr["Promotion"].ToString().Replace("\'", "\"")
                                     , dr["Instructions"].ToString().Replace("\'", "\""), dr["Images"].ToString().Replace("\'", "\""), dr["Description"].ToString().Replace("\'", "\""), dr["Specifications"].ToString().Replace("\'", "\""), dr["ValueList"].ToString().Replace("\'", "\""), dr["LargePacking"].ToShort(), dr["Unit"].ToString().Replace("\'", "\"")
                                     , dr["Volume"].ToString().Replace("\'", "\""), dr["Type"].ToShort(), dr["Abbreviation"].ToString().Replace("\'", "\""), dr["Rank"].ToShort(), dr["IsBasic"].ToShort(), dr["IsRecommend"].ToShort(), dr["IsTop"].ToShort(), string.IsNullOrEmpty(dr["ValidPeriod"].ToString()) ? "null" : dr["ValidPeriod"]
                                     , dr["IsTaiKang"].ToShort(), dr["Sort"].ToShort(), dr["Actions"].ToShort(), dr["Status"].ToShort(), dr["Title"].ToString().Replace("\'", "\"")
                                     , dr["Keywords"].ToString().Replace("\'", "\""), dr["MetaKeywords"].ToString().Replace("\'", "\""), dr["MetaDescription"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\"")
                                     , dr["UpdateTime"]);
                    if (i == 0)
                    {
                        strPlaceholder = Placeholder;
                    }
                    else
                    {
                        strPlaceholder += "," + Placeholder;
                    }
                }
                if (!string.IsNullOrEmpty(strPlaceholder))
                {
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = productTable.Rows.Count;
                        flag = false;
                    }
                    else
                    {
                        errorCount = (productTable.Rows.Count - result > 0) ? productTable.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddProduct 添加商品表失败,商品ID{0}-{1},异常信息:{2}", productTable.Rows[0]["ProductID"], productTable.Rows[productTable.Rows.Count - 1]["ProductID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        #region 帮助函数

        /// <summary>   
        /// 取得HTML中所有图片的 URL。   
        /// </summary>   
        /// <param name="sHtmlText">HTML代码</param>   
        /// <returns>图片的URL列表</returns>   
        private static string GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(sHtmlText);

            string sUrlList = string.Empty;
            // 取得匹配项列表  
            for (int i = 0; i < matches.Count; i++)
            {
                if (i == 0)
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

        #endregion

        private string parmsKey = string.Format(@"ProductID,ProductCode,CFID,BrandID,ChineseName,PinyinName,CADN,LongName,ProductType,MarketPrice,MobilePrice,TradePrice,CostPrice 
                            ,Stock,Selling,SellingTime,Manufacter,Promotion,Instructions,Images,Description,Specifications,ValueList,LargePacking,Unit,Volume,Type,Abbreviation 
                           ,Rank,IsBasic,IsRecommend,IsTop,ValidPeriod,IsTaiKang,Sort,Actions 
                           ,Status,Title,Keywords,MetaKeywords,MetaDescription,Creator,CreateTime,Updater,UpdateTime");
    }
}
