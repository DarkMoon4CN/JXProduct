namespace JXProduct.Component.SQLServerDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data;

    using JXProduct.Component.Model;
    using JXProduct.Component.DataAccess;
    using System.Text;
    public class ProductDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region CURD

        /// <summary>
        /// Product_Insert Method		
        /// </summary>
        /// <returns></returns>
        internal int Product_Insert(ProductInfo product)
        {
            string sqlCommand = "Product_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "ProductCode", DbType.String, product.ProductCode);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, product.ChineseName);
            dbw.AddInParameter(dbCommand, "ProductType", DbType.Int16, product.ProductType);
            dbw.AddInParameter(dbCommand, "CADN", DbType.String, product.CADN);
            dbw.AddInParameter(dbCommand, "LongName", DbType.String, product.LongName);
            dbw.AddInParameter(dbCommand, "PinyinName", DbType.String, product.PinyinName);
            dbw.AddInParameter(dbCommand, "MarketPrice", DbType.Decimal, product.MarketPrice);
            dbw.AddInParameter(dbCommand, "TradePrice", DbType.Decimal, product.TradePrice);
            dbw.AddInParameter(dbCommand, "CostPrice", DbType.Decimal, product.CostPrice);
            dbw.AddInParameter(dbCommand, "MobilePrice", DbType.Decimal, product.MobilePrice);
            dbw.AddInParameter(dbCommand, "JXCFID", DbType.Int16, product.JXCFID);
            dbw.AddInParameter(dbCommand, "CFID", DbType.Int32, product.CFID);
            dbw.AddInParameter(dbCommand, "BrandID", DbType.Int32, product.BrandID);
            dbw.AddInParameter(dbCommand, "Selling", DbType.Int16, product.Selling);
            dbw.AddInParameter(dbCommand, "SellingTime", DbType.DateTime, product.SellingTime);
            dbw.AddInParameter(dbCommand, "Images", DbType.String, product.Images);
            dbw.AddInParameter(dbCommand, "Manufacter", DbType.String, product.Manufacter);
            dbw.AddInParameter(dbCommand, "Description", DbType.String, product.Description);
            dbw.AddInParameter(dbCommand, "Specifications", DbType.String, product.Specifications);
            dbw.AddInParameter(dbCommand, "ParameterType", DbType.Int16, product.ParameterType);
            dbw.AddInParameter(dbCommand, "ValueList", DbType.String, product.ValueList);
            dbw.AddInParameter(dbCommand, "LargePacking", DbType.Int32, product.LargePacking);
            dbw.AddInParameter(dbCommand, "Unit", DbType.String, product.Unit);
            dbw.AddInParameter(dbCommand, "Volume", DbType.String, product.Volume);
            dbw.AddInParameter(dbCommand, "Type", DbType.Int16, product.Type);
            dbw.AddInParameter(dbCommand, "Abbreviation", DbType.String, product.Abbreviation);
            dbw.AddInParameter(dbCommand, "ContainMHJ", DbType.Int16, product.ContainMHJ);
            dbw.AddInParameter(dbCommand, "IsFragile", DbType.Int16, product.IsFragile);
            dbw.AddInParameter(dbCommand, "Rank", DbType.Int16, product.Rank);
            dbw.AddInParameter(dbCommand, "IsBasic", DbType.Int16, product.IsBasic);
            dbw.AddInParameter(dbCommand, "IsRecommend", DbType.Int16, product.IsRecommend);
            dbw.AddInParameter(dbCommand, "IsTop", DbType.Int16, product.IsTop);
            dbw.AddInParameter(dbCommand, "ValidPeriod", DbType.DateTime, product.ValidPeriod);
            dbw.AddInParameter(dbCommand, "IsTaiKang", DbType.Int16, product.IsTaiKang);
            dbw.AddInParameter(dbCommand, "Comments", DbType.Int32, product.Comments);
            dbw.AddInParameter(dbCommand, "SellCount", DbType.Int32, product.SellCount);
            dbw.AddInParameter(dbCommand, "PV", DbType.Int32, product.PV);
            dbw.AddInParameter(dbCommand, "Sort", DbType.Int32, product.Sort);
            dbw.AddInParameter(dbCommand, "Actions", DbType.Int16, product.Actions);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, product.Status);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, product.Title);
            dbw.AddInParameter(dbCommand, "Keywords", DbType.String, product.Keywords);
            dbw.AddInParameter(dbCommand, "MetaKeywords", DbType.String, product.MetaKeywords);
            dbw.AddInParameter(dbCommand, "MetaDescription", DbType.String, product.MetaDescription);
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, product.Creator);
            dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, product.CreateTime);
            //dbw.AddInParameter(dbCommand, "Updater", DbType.String, product.Updater);
            //dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, product.UpdateTime);
            dbw.AddInParameter(dbCommand, "ImageDiff", DbType.Int16, product.ImageDiff);
            var result = int.Parse(dbw.ExecuteScalar(dbCommand).ToString());
            return result;
        }

        /// <summary>
        /// Product_Update Method
        /// </summary>
        /// <param name="ProductInfo">Product object</param>
        /// <returns>true:成功 false:失败</returns>
        internal bool Product_Update(ProductInfo product)
        {
            string sqlCommand = "Product_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "ProductID", DbType.Int32, product.ProductID);
            dbw.AddInParameter(dbCommand, "ProductCode", DbType.String, product.ProductCode);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, product.ChineseName);
            dbw.AddInParameter(dbCommand, "ProductType", DbType.Int16, product.ProductType);
            dbw.AddInParameter(dbCommand, "CADN", DbType.String, product.CADN);
            dbw.AddInParameter(dbCommand, "LongName", DbType.String, product.LongName);
            dbw.AddInParameter(dbCommand, "PinyinName", DbType.String, product.PinyinName);
            //dbw.AddInParameter(dbCommand, "MarketPrice", DbType.Decimal, product.MarketPrice);
            //dbw.AddInParameter(dbCommand, "TradePrice", DbType.Decimal, product.TradePrice);
            //dbw.AddInParameter(dbCommand, "CostPrice", DbType.Decimal, product.CostPrice);
            //dbw.AddInParameter(dbCommand, "MobilePrice", DbType.Decimal, product.MobilePrice);
            dbw.AddInParameter(dbCommand, "JXCFID", DbType.Int16, product.JXCFID);
            dbw.AddInParameter(dbCommand, "CFID", DbType.Int32, product.CFID);
            dbw.AddInParameter(dbCommand, "BrandID", DbType.Int32, product.BrandID);
            dbw.AddInParameter(dbCommand, "Selling", DbType.Int16, product.Selling);
            dbw.AddInParameter(dbCommand, "SellingTime", DbType.DateTime, product.SellingTime);
            dbw.AddInParameter(dbCommand, "Images", DbType.String, product.Images);
            dbw.AddInParameter(dbCommand, "Manufacter", DbType.String, product.Manufacter);
            dbw.AddInParameter(dbCommand, "Description", DbType.String, product.Description);
            dbw.AddInParameter(dbCommand, "Specifications", DbType.String, product.Specifications);
            dbw.AddInParameter(dbCommand, "ParameterType", DbType.Int16, product.ParameterType);
            dbw.AddInParameter(dbCommand, "ValueList", DbType.String, product.ValueList);
            dbw.AddInParameter(dbCommand, "LargePacking", DbType.Int32, product.LargePacking);
            dbw.AddInParameter(dbCommand, "Unit", DbType.String, product.Unit);
            dbw.AddInParameter(dbCommand, "Volume", DbType.String, product.Volume);
            dbw.AddInParameter(dbCommand, "Type", DbType.Int16, product.Type);
            dbw.AddInParameter(dbCommand, "Abbreviation", DbType.String, product.Abbreviation);
            dbw.AddInParameter(dbCommand, "ContainMHJ", DbType.Int16, product.ContainMHJ);
            dbw.AddInParameter(dbCommand, "IsFragile", DbType.Int16, product.IsFragile);
            dbw.AddInParameter(dbCommand, "Rank", DbType.Int16, product.Rank);
            dbw.AddInParameter(dbCommand, "IsBasic", DbType.Int16, product.IsBasic);
            dbw.AddInParameter(dbCommand, "IsRecommend", DbType.Int16, product.IsRecommend);
            dbw.AddInParameter(dbCommand, "IsTop", DbType.Int16, product.IsTop);
            dbw.AddInParameter(dbCommand, "ValidPeriod", DbType.DateTime, product.ValidPeriod);
            dbw.AddInParameter(dbCommand, "IsTaiKang", DbType.Int16, product.IsTaiKang);
            dbw.AddInParameter(dbCommand, "Comments", DbType.Int32, product.Comments);
            dbw.AddInParameter(dbCommand, "SellCount", DbType.Int32, product.SellCount);
            dbw.AddInParameter(dbCommand, "PV", DbType.Int32, product.PV);
            dbw.AddInParameter(dbCommand, "Sort", DbType.Int32, product.Sort);
            dbw.AddInParameter(dbCommand, "Actions", DbType.Int16, product.Actions);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, product.Status);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, product.Title);
            dbw.AddInParameter(dbCommand, "Keywords", DbType.String, product.Keywords);
            dbw.AddInParameter(dbCommand, "MetaKeywords", DbType.String, product.MetaKeywords);
            dbw.AddInParameter(dbCommand, "MetaDescription", DbType.String, product.MetaDescription);
            //dbw.AddInParameter(dbCommand, "Creator", DbType.String, product.Creator);
            //dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, product.CreateTime);
            dbw.AddInParameter(dbCommand, "Updater", DbType.String, product.Updater);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, product.UpdateTime);
            dbw.AddInParameter(dbCommand, "ImageDiff", DbType.Int16, product.ImageDiff);

            return dbw.ExecuteNonQuery(dbCommand) > 0;

        }

        /// <summary>
        /// Product_Get Method
        /// </summary>
        /// <returns>ProductInfo get from Product table.</returns>	
        internal ProductInfo Product_Get(Int32 productid)
        {
            ProductInfo product = null;

            string sqlCommand = "Product_Get";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "ProductID", DbType.Int32, productid);

            using (IDataReader read = dbr.ExecuteReader(dbCommand))
            {
                if (read.Read())
                {
                    product = RecoverModel(read);
                }
            }

            return product;
        }

        /// <summary>
        /// Product_Delete Method
        /// </summary>
        /// <returns>true:成功 false:失败</returns>	
        internal bool Product_Delete(int productid)
        {
            string sqlCommand = "Product_Delete";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);


            dbr.AddInParameter(dbCommand, "ProductID", DbType.Int32, productid);
            dbw.ExecuteNonQuery(dbCommand);

            return true;
        }

        /// <summary>
        /// Product_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of ProductInfo</returns>
        internal IList<ProductInfo> Product_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            IList<ProductInfo> productlist = new List<ProductInfo>();

            string sqlCommand = "Product_GetList";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "PageIndex", DbType.Int32, pageIndex);
            dbr.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);
            dbr.AddInParameter(dbCommand, "OrderType", DbType.String, orderType);
            dbr.AddInParameter(dbCommand, "StrWhere", DbType.String, strWhere);
            dbr.AddOutParameter(dbCommand, "RecordCount", DbType.Int32, 4);

            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    productlist.Add(RecoverModel(dataReader));
                }
            }

            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());

            return productlist;
        }

        /// <summary>
        /// 从 IDataReader 中恢复Product对象
        /// </summary> 
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private ProductInfo RecoverModel(IDataReader dataReader)
        {
            var product = new ProductInfo();
            product.ProductID = int.Parse(dataReader["ProductID"].ToString());
            product.ProductCode = dataReader["ProductCode"].ToString();
            product.ChineseName = dataReader["ChineseName"].ToString();
            product.ProductType = dataReader["productType"].ToShort();
            product.ParameterType = dataReader["ParameterType"].ToShort();
            product.CADN = dataReader["CADN"].ToString();
            product.LongName = dataReader["LongName"].ToString();
            //product.BriefName = dataReader["BriefName"].ToString();
            //product.Brief = dataReader["Brief"].ToString();
            product.MarketPrice = dataReader["MarketPrice"].ToDecimal();
            product.TradePrice = dataReader["TradePrice"].ToDecimal();
            product.CostPrice = dataReader["CostPrice"].ToDecimal();
            //product.SafeStock = dataReader["SafeStock"].ToInt();
            product.JXCFID = dataReader["JXCFID"].ToShort();
            product.CFID = dataReader["CFID"].ToInt();
            //product.CFName = dataReader["CFName"].ToString();
            product.Images = dataReader["Images"].ToString();
            product.BrandID = dataReader["BrandID"].ToInt();
            product.BrandName = dataReader["BrandName"].ToString();
            product.Selling = dataReader["Selling"].ToShort();
            product.SellingTime = dataReader["SellingTime"].ToDateTime();
            product.Manufacter = dataReader["Manufacter"].ToString();
            product.Description = dataReader["Description"].ToString();
            product.Specifications = dataReader["Specifications"].ToString();
            product.ValueList = dataReader["ValueList"].ToString();
            product.LargePacking = int.Parse(dataReader["LargePacking"].ToString());
            product.Unit = dataReader["Unit"].ToString();
            product.Volume = dataReader["Volume"].ToString();
            product.Type = byte.Parse(dataReader["Type"].ToString());
            product.Abbreviation = dataReader["Abbreviation"].ToString();
            product.IsFragile = dataReader["IsFragile"].ToShort();
            product.ContainMHJ = dataReader["ContainMHJ"].ToShort();
            product.Rank = dataReader["Rank"].ToShort();
            product.IsBasic = dataReader["IsBasic"].ToShort();
            product.IsRecommend = dataReader["IsRecommend"].ToShort();
            product.IsTop = byte.Parse(dataReader["IsTop"].ToString());
            product.ImageDiff = dataReader["ImageDiff"].ToShort();
            product.ValidPeriod = dataReader["ValidPeriod"].ToDateTime();
            product.IsTaiKang = byte.Parse(dataReader["IsTaiKang"].ToString());
            product.Comments = int.Parse(dataReader["Comments"].ToString());
            product.SellCount = int.Parse(dataReader["SellCount"].ToString());
            product.PV = dataReader["PV"].ToInt();
            product.Sort = dataReader["Sort"].ToInt();
            product.Actions = dataReader["Actions"].ToShort();
            product.Status = dataReader["Status"].ToShort();
            product.Title = dataReader["Title"].ToString();
            product.Keywords = dataReader["Keywords"].ToString();
            product.MetaKeywords = dataReader["MetaKeywords"].ToString();
            product.MetaDescription = dataReader["MetaDescription"].ToString();
            product.Creator = dataReader["Creator"].ToString();
            product.CreateTime = DateTime.Parse(dataReader["CreateTime"].ToString());
            product.Updater = dataReader["Updater"].ToString();
            product.UpdateTime = DateTime.Parse(dataReader["UpdateTime"].ToString());

            product.HasKeyword = dataReader["keywordCount"].ToString() == "1";
            return product;
        }

        #endregion

        #region 快速编辑

        /// <summary>
        /// 商品快速编辑
        /// </summary>
        /// <returns>1成功 0失败</returns>
        internal bool Product_QuickEdit(QuickEditModel model)
        {
            var sql = "Product_QuickEdit";
            var cmd = dbw.GetStoredProcCommand(sql);

            dbw.AddInParameter(cmd, "ProductID", DbType.Int32, model.ProductID);
            dbw.AddInParameter(cmd, "ProductGiftID", DbType.Int32, model.ProductGiftID);
            dbw.AddInParameter(cmd, "ProductGiftCount", DbType.Int32, model.ProductGiftCount);
            dbw.AddInParameter(cmd, "IsFreeShip", DbType.Int16, model.IsFreeShip);
            dbw.AddInParameter(cmd, "Selling", DbType.Int16, model.Selling);
            dbw.AddInParameter(cmd, "Actions", DbType.Int16, model.Actions);
            dbw.AddInParameter(cmd, "Sort", DbType.Int32, model.Sort);
            dbw.AddInParameter(cmd, "Creator", DbType.String, model.Creator);

            var result = int.Parse(dbw.ExecuteScalar(cmd).ToString());
            return result > 0;
        }

        #endregion

        #region  置顶 / 推荐

        //置顶
        internal bool Product_SetTop(int productid, bool istop)
        {
            var sql = "UPDATE p SET p.IsTop =" + (istop ? 1 : 0) + " FROM Product AS p WHERE p.ProductID = " + productid;
            return dbw.ExecuteNonQuery(CommandType.Text, sql) > 0;
        }

        //推荐
        internal bool Product_SetRecommend(int productid, bool isrecommend)
        {
            var sql = "UPDATE p SET p.isrecommend =" + (isrecommend ? 1 : 0) + " FROM Product AS p WHERE p.ProductID = " + productid;
            return dbw.ExecuteNonQuery(CommandType.Text, sql) > 0;
        }

        #endregion

        #region JXParmeter

        //JXCFID	ChineseName	PinyinName	ImagesLogo	Level	Path	ParentID	PathCount	Title	Keywords	Description	Sort	Status
        internal List<JXClassificationInfo> Product_GetJXClassification()
        {
            var sql = "SELECT j.* FROM JXClassification AS j ";
            var cmd = dbr.GetSqlStringCommand(sql);
            var list = new List<JXClassificationInfo>();
            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(new JXClassificationInfo()
                    {
                        JXCFID = read["JXCFID"].ToInt(),
                        ChineseName = read["ChineseName"].ToString(),
                        PinyinName = read["PinyinName"].ToString(),
                        Sort = read["Sort"].ToInt(),
                    });
                }
            }
            return list;
        }

        //JXParaID	JXCFID	ParaName	ParaType	ParaLength	ParaValueList	Sort	IsSearch	IsNull	ParaLabel	ParaValue
        internal List<JXClassificationParameterInfo> Product_GetJXClassificationParameter(int jxcfid, int productid)
        {
            var sql = @"SELECT jp.* ,jpv.ParaValue FROM JXClassificationParameter AS jp LEFT JOIN JXProductParameterValue AS jpv ON jp.JXParaID = jpv.JXParaID AND jpv.ProductID = @productid WHERE jp.JXCFID =@jxcfid order by  jp.ParaLabel asc";
            var cmd = dbr.GetSqlStringCommand(sql);
            var list = new List<JXClassificationParameterInfo>();
            dbr.AddInParameter(cmd, "JXCFID", DbType.Int32, jxcfid);
            dbr.AddInParameter(cmd, "ProductID", DbType.Int32, productid);

            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(new JXClassificationParameterInfo()
                    {
                        JXParaID = read["JXparaID"].ToInt(),
                        JXCFID = read["jxcfid"].ToInt(),
                        ParaName = read["ParaName"].ToString(),
                        ParaType = read["ParaType"].ToShort(),
                        ParaLength = read["ParaLength"].ToInt(),
                        ParaLabel = read["ParaLabel"].ToShort(),
                        IsNull = read["IsNull"].ToShort(),
                        IsSearch = read["IsSearch"].ToShort(),
                        ParaValue = read["ParaValue"].ToString(),
                        ParaValueList = read["ParaValueList"].ToString(),
                        Sort = read["Sort"].ToInt()
                    });
                }
            }
            return list;
        }


        internal bool Product_InsertJXCFParameterValues(int productid, Dictionary<int, string> dic)
        {
            var sql = new StringBuilder();
            sql.AppendFormat("DELETE FROM JXProductParameterValue WHERE ProductID ={0};", productid);
            foreach (var d in dic)
            {
                sql.AppendFormat("INSERT INTO JXProductParameterValue (ProductID,JXParaID,ParaValue)VALUES({0},{1},'{2}')", productid, d.Key, d.Value);
            }
            var cmd = dbw.GetSqlStringCommand(sql.ToString());
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }

        #endregion
    }
}