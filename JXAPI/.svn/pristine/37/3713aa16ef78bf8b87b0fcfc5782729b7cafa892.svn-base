using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using PanGu;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;

using JXSearch.Engine.Cache;
using JXAPI.Common.Utils;
using JXAPI.Common.Config;

namespace JXSearch.Engine.Product
{
    internal class ProductSearchProvider : JXSearchProvider
    {
        private static string key = "ProductEngine";
        private EnginesElement config = SearchEngineConfig.Instance.Engines[key];

        public override string INDEX_DIR
        {
            get { return config.IndexDir; }
        }

        public override string CURR_INDEX_DIR
        {
            get
            {
                SearchDictDirCacheProvider.SearchDictDirCache info = SearchDictDirCacheProvider.Get(key);
                if (info != null)
                    return info.dictDir;
                else
                    return string.Format("{0}/{1}", config.IndexDir, config.DictDir);
            }
        }

        public override string DICT_DIR
        {
            get { return config.DictDir; }
        }

        string[] fieldsAbbr = { "ProductID", "ProductCode", "PinyinName", "ChineseName" };
        string[] fields = { "ChineseName", "CADN", "PinyinName", "ChineseNameWord", "Keywords", "LongName", "Manufacturer", "BrandName", "CFName1", "CFName2", "CFName3", "ProductTag", "Efficacy" };

        /// <summary>
        /// 关键词多条件搜索
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="brand">品牌</param>
        /// <param name="cfid">分类</param>
        /// <param name="pricelimit">价格区间</param>
        /// <param name="store">是否有货</param>
        /// <param name="order">排序</param>
        /// <param name="recCount">总记录数</param>
        /// <param name="pageLen">条数</param>
        /// <param name="pageNo">页码</param>
        /// <returns>搜索结果</returns>
        public override JXSearchProductResult Search(RequestForm form, out int recCount)
        {
            recCount = 0;
            JXSearchProductResult result = new JXSearchProductResult();
            IndexSearcher search = new IndexSearcher(CURR_INDEX_DIR);
            try
            {
                #region 搜索条件
                Query query1 = null;
                BooleanQuery query = new BooleanQuery();
                if (StringUtil.IsNatural_Number(form.queryForm.keyword))
                {
                    //  商品ID、金象码、拼音
                    BooleanClause.Occur[] flags = new BooleanClause.Occur[fieldsAbbr.Length];
                    for (int n = 0; n < fieldsAbbr.Length; n++)
                    {
                        flags[n] = BooleanClause.Occur.SHOULD;
                    }
                    query1 = MultiFieldQueryParser.Parse(Lucene.Net.Util.Version.LUCENE_29, form.queryForm.keyword, fieldsAbbr, flags, new PanGuAnalyzer(true));
                    query.Add(query1, BooleanClause.Occur.MUST);
                }
                else
                {
                    //  关键词检索字段\t\r\n\\t
                    BooleanClause.Occur[] flags = new BooleanClause.Occur[fields.Length];
                    for (int n = 0; n < fields.Length; n++)
                    {
                        flags[n] = BooleanClause.Occur.SHOULD;
                    }
                    //  转换关键词拼音
                    string pinyinName = PinyinUtil.ConvertToPinyin(GetKeyWordsSplitFilter(form.queryForm.keyword, new PanGuTokenizer()));

                    //  关键词检索
                    //  BooleanClause.Occur.MUST 表示 and
                    //  BooleanClause.Occur.MUST_NOT 表示 not
                    //  BooleanClause.Occur.SHOULD 表示 or
                    string q = GetKeyWordsSplitBySpace(string.Format("{0} {1}", form.queryForm.keyword, pinyinName), new PanGuTokenizer());
                    query1 = MultiFieldQueryParser.Parse(Lucene.Net.Util.Version.LUCENE_29, q, fields, flags, new PanGuAnalyzer(true));
                    query.Add(query1, BooleanClause.Occur.MUST);
                }

                //  品牌
                if (form.queryForm.brandIds != null && form.queryForm.brandIds.Count() > 0)
                {
                    BooleanQuery tmpBQ = new BooleanQuery();
                    foreach (int key in form.queryForm.brandIds)
                    {
                        tmpBQ.Add(new TermQuery(new Term("BrandID", key.ToString())), BooleanClause.Occur.SHOULD);
                    }
                    query.Add(tmpBQ, BooleanClause.Occur.MUST);
                }

                //  分类
                if (form.queryForm.cfid > 0)
                {
                    BooleanQuery tmpBQ = new BooleanQuery();
                    tmpBQ.Add(new TermQuery(new Term("CFID1", form.queryForm.cfid.ToString())), BooleanClause.Occur.SHOULD);
                    tmpBQ.Add(new TermQuery(new Term("CFID2", form.queryForm.cfid.ToString())), BooleanClause.Occur.SHOULD);
                    tmpBQ.Add(new TermQuery(new Term("CFID3", form.queryForm.cfid.ToString())), BooleanClause.Occur.SHOULD);
                    query.Add(tmpBQ, BooleanClause.Occur.MUST);
                }

                //  库存
                if (form.queryForm.stock > 0)
                {
                    query.Add(new TermQuery(new Term("Stock", "1")), BooleanClause.Occur.MUST);
                }

                //  价格区间
                if (!string.IsNullOrEmpty(form.queryForm.pricelimit))
                {
                    string[] price = form.queryForm.pricelimit.Split(",".ToCharArray());
                    if (price.Length > 0 && double.Parse(price[1]) > 0)
                    {
                        query.Add(NumericRangeQuery.NewDoubleRange("TradePrice", double.Parse(price[0]), double.Parse(price[1]), true, false), BooleanClause.Occur.MUST);
                    }
                }

                //  是否有货    Selling 是否在卖 1：在卖 0：下架 2：暂无库存
                if (form.queryForm.store == 1) query.Add(new TermQuery(new Term("Selling", form.queryForm.store.ToString())), BooleanClause.Occur.MUST);

                Hits hits = search.Search(query);
                #endregion

                //  构造商品结果
                result.listProductList = ProductBinding(hits, form.queryForm.keyword, form.queryForm.order, form.pageForm.page, form.pageForm.size, out recCount);

                #region 构造结果参数 品牌/分类
                //  排除搜索条件重新搜索
                if ((form.queryForm.brandIds != null && form.queryForm.brandIds.Count() > 0) || form.queryForm.cfid > 0 || form.queryForm.store == 1 || form.queryForm.order > 0 || !string.IsNullOrEmpty(form.queryForm.pricelimit))
                {
                    BooleanQuery booleanQuery = new BooleanQuery();
                    booleanQuery.Add(query1, BooleanClause.Occur.MUST);
                    hits = search.Search(query);
                }

                //  构造分类、品牌结果
                IList<JXSearchEntity> list = ProductParaList(hits);
                if (list != null)
                {
                    //  品牌
                    result.listBrand = list.Where(g => g.typeID == 5).ToList();

                    //  一级分类
                    IList<JXSearchEntity> categoryList = list.Where(g => g.typeID == 2 && g.parentID == 0).ToList();
                    if (categoryList != null)
                    {
                        foreach (JXSearchEntity item in categoryList)
                        {
                            //  二级分类
                            item.listSubClassification = list.Where(g => g.typeID == 2 && g.parentID == item.id).ToList();
                            foreach (JXSearchEntity subItem in item.listSubClassification)
                            {
                                //  三级分类
                                subItem.listSubClassification = list.Where(g => g.typeID == 2 && g.parentID == subItem.id).ToList();
                            }
                        }
                    }
                    result.listClassification = categoryList;
                }
                #endregion

                //  页数
                result.totalPage = Convert.ToInt32(Math.Ceiling((double)recCount / form.pageForm.size));
                result.resultCode = "SUCCEED";
                result.resultMsg = "SUCCEED";
            }
            catch { }
            finally
            {
                search.Close();
                search.Dispose();
            }
            return result;
        }

        public override JXSearchEntityResult Search(string q, int pageLen, int pageNo, out int recCount)
        {
            recCount = 0;
            return null;
        }

        #region 搜索结果处理
        /// <summary>
        /// 构造返回结果
        /// </summary>
        /// <returns></returns>
        private List<ProductSimpleInfo> ProductBinding(Hits hits, string key, int order, int pageNo, int pageLen, out int recCount)
        {
            recCount = hits.Length();
            //  合并
            int n = 0;
            ProductSimpleInfo info = null;
            IList<ProductSimpleInfo> list = new List<ProductSimpleInfo>();
            while (n < recCount)
            {
                //  去除匹配度太低结果
                if (hits.Score(n) < 0.01)
                {
                    n++;
                    continue;
                }
                try
                {
                    info = new ProductSimpleInfo()
                    {
                        productID = int.Parse(hits.Doc(n).Get("ProductID")),
                        productCode = hits.Doc(n).Get("ProductCode"),
                        chineseName = hits.Doc(n).Get("ChineseName"),
                        cadn = hits.Doc(n).Get("CADN"),
                        longName = hits.Doc(n).Get("LongName"),
                        pinyinName = hits.Doc(n).Get("PinyinName"),
                        marketPrice = decimal.Parse(hits.Doc(n).Get("MarketPrice")),
                        tradePrice = decimal.Parse(hits.Doc(n).Get("TradePrice")),
                        sellCount = int.Parse(hits.Doc(n).Get("SellCount")),
                        favorCount = int.Parse(hits.Doc(n).Get("Favorite")),
                        productType = short.Parse(hits.Doc(n).Get("ProductType")),
                        specifications = hits.Doc(n).Get("Specifications"),
                        images = hits.Doc(n).Get("Images"),
                        actions = short.Parse(hits.Doc(n).Get("Actions")),
                        comments = int.Parse(hits.Doc(n).Get("Comments")),
                        selling = int.Parse(hits.Doc(n).Get("Selling")),
                        manufacturer = hits.Doc(n).Get("Manufacturer"),
                        sellingTime = DateTime.Parse(hits.Doc(n).Get("SellingTime")),
                        recommend = float.Parse(hits.Doc(n).Get("Recommend")),
                        preferential = float.Parse(hits.Doc(n).Get("Preferential")),
                        brandName = hits.Doc(n).Get("BrandName")
                    };
                    if (order <= 0)
                    {
                        info.level = GetProductLevel(hits.Score(n), 1);
                        info.score = GetProductLevel(info.sellCount, 0) + n;
                    }
                }
                catch { }
                finally
                {
                    if (info != null) list.Add(info);
                    n++;
                }
            }
            recCount = list.Count();

            //  返回数据
            switch (order)
            {
                case 1:
                    //  人气 降序
                    return list.OrderByDescending(i => i.favorCount).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                case 2:
                    //  人气 升序
                    return list.OrderByDescending(i => i.favorCount).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                case 3:
                    //  新品 降序
                    return list.OrderByDescending(i => i.sellingTime).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                case 4:
                    //  新品 升序
                    return list.OrderBy(i => i.sellingTime).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                case 5:
                    //  销量 降序
                    return list.OrderByDescending(i => i.sellCount).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                case 6:
                    //  销量 升序
                    return list.OrderBy(i => i.sellCount).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                case 7:
                    //  价格 降序
                    return list.OrderByDescending(i => i.tradePrice).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                case 8:
                    //  价格 升序
                    return list.OrderBy(i => i.tradePrice).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                case 9:
                    //  综合 升序
                    return list.OrderBy(i => i.level).ThenBy(i => i.score).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
                default:
                    return list.OrderByDescending(i => i.level).ThenBy(i => i.score).Skip(pageLen * (pageNo - 1)).Take(pageLen).ToList();
            }
        }
        
        /// <summary>
        /// 计算匹配度
        /// </summary>
        /// <param name="score">匹配度/销售量</param>
        /// <param name="type">类型 0=销售量，1=匹配度</param>
        /// <returns></returns>
        private float GetProductLevel(float score, int type)
        {
            int level = 1, n = 1;
            float begin = 0, section = 0;
            switch (type)
            {
                case 1:
                    //  计算匹配度区间
                    begin = 0.03f;
                    section = begin;
                    while (section < score)
                    {
                        section = begin * level;
                        level = level * 2;
                    }
                    break;
                default:
                    //  计算销量区间
                    level = 2048;
                    begin = 40f;
                    section = begin;
                    while (section < score)
                    {
                        section = begin * n;
                        n = n * 2;
                        level = level / 2;
                    }
                    break;
            }
            return level;
        }

        /// <summary>
        /// 商品结果参数 品牌/分类
        /// </summary>
        /// <param name="hits"></param>
        private IList<JXSearchEntity> ProductParaList(Hits hits)
        {
            int id = 0;
            IList<JXSearchEntity> list = new List<JXSearchEntity>();
            try
            {
                int recCount = hits.Length();       //  总记录数 
                for (int j = 0; j < recCount; j++)
                {
                    if (hits.Score(j) < 0.01)
                    {
                        continue;
                    }
                    #region 分类1
                    if (hits.Doc(j).Get("CFID1").Length > 0)
                    {
                        id = int.Parse(hits.Doc(j).Get("CFID1"));
                        if (list.Where(g => g.id == id && g.typeID == 2).Count() <= 0)
                        {
                            list.Add(new JXSearchEntity()
                            {
                                id = id,
                                chineseName = hits.Doc(j).Get("CFName1"),
                                typeID = 2,
                                parentID = int.Parse(hits.Doc(j).Get("ParentID1"))
                            });
                        }
                    }
                    #endregion

                    #region 分类2
                    if (hits.Doc(j).Get("CFID2").Length > 0)
                    {
                        id = int.Parse(hits.Doc(j).Get("CFID2"));
                        if (list.Where(g => g.id == id && g.typeID == 2).Count() <= 0)
                        {
                            list.Add(new JXSearchEntity()
                            {
                                id = id,
                                chineseName = hits.Doc(j).Get("CFName2"),
                                typeID = 2,
                                parentID = int.Parse(hits.Doc(j).Get("ParentID2"))
                            });
                        }
                    }
                    #endregion

                    #region 分类3
                    if (hits.Doc(j).Get("CFID3").Length > 0)
                    {
                        id = int.Parse(hits.Doc(j).Get("CFID3"));
                        if (list.Where(g => g.id == id && g.typeID == 2).Count() <= 0)
                        {
                            list.Add(new JXSearchEntity()
                            {
                                id = id,
                                chineseName = hits.Doc(j).Get("CFName3"),
                                typeID = 2,
                                parentID = int.Parse(hits.Doc(j).Get("ParentID3"))
                            });
                        }
                    }
                    #endregion

                    #region 品牌
                    if (hits.Doc(j).Get("BrandID").Length > 0)
                    {
                        id = int.Parse(hits.Doc(j).Get("BrandID"));
                        if (list.Where(g => g.brandID == id && g.typeID == 5).Count() <= 0)
                        {
                            list.Add(new JXSearchEntity()
                            {
                                brandID = id,
                                brandName = hits.Doc(j).Get("BrandName"),
                                letter = hits.Doc(j).Get("BrandLetter"),
                                typeID = 5
                            });
                        }
                    }
                    #endregion
                }
            }
            catch { }
            return list;
        }
        #endregion
    }

}
